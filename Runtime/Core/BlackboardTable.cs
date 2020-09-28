// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using UnityEngine.Profiling;
using Zor.EventBasedBlackboard.Debugging;
using Zor.EventBasedBlackboard.Helpers;

namespace Zor.EventBasedBlackboard.Core
{
	/// <summary>
	/// Entry of <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/>. A <see cref="BlackboardTable{T}"/>
	/// contains values of <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	internal sealed class BlackboardTable<T> : IBlackboardTable
	{
		private static readonly EqualityComparer<T> s_equalityComparer = EqualityComparer<T>.Default;

		/// <summary>
		/// Flushed values.
		/// </summary>
		private readonly Dictionary<BlackboardPropertyName, T> m_table =
			new Dictionary<BlackboardPropertyName, T>();
		/// <summary>
		/// Not flushed values.
		/// </summary>
		private readonly Dictionary<BlackboardPropertyName, T> m_bufferedTable =
			new Dictionary<BlackboardPropertyName, T>();

		private readonly Dictionary<BlackboardPropertyName, Action> m_onChanged =
			new Dictionary<BlackboardPropertyName, Action>();
		private readonly Dictionary<BlackboardPropertyName, Action<BlackboardChangeInfo<T>>> m_onTypedChanged =
			new Dictionary<BlackboardPropertyName, Action<BlackboardChangeInfo<T>>>();

		/// <summary>
		/// Changed properties that are to be applied in <see cref="Flush"/>.
		/// </summary>
		private readonly List<BlackboardPropertyName> m_changedProperties = new List<BlackboardPropertyName>();
		/// <summary>
		/// List of messages that are created in <see cref="Flush"/> and sent in <see cref="SendCallbacks"/>.
		/// </summary>
		private readonly List<ChangeMessage> m_changeMessages = new List<ChangeMessage>();

		/// <summary>
		/// Type of values that are contained in the <see cref="BlackboardTable{T}"/>.
		/// </summary>
		public Type valueType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(T);
		}

		/// <summary>
		/// How many flushed values are contained in the <see cref="BlackboardTable{T}"/>.
		/// </summary>
		public int count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_table.Count;
		}

		/// <summary>
		/// <para>True if there's at least one not flushed value.</para>
		/// <para>False if there's no not flushed value.</para>
		/// </summary>
		/// <seealso cref="Flush"/>
		public bool dirty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_changedProperties.Count > 0;
		}

		/// <summary>Tries to get and return a value of <paramref name="propertyName"/>.</summary>
		/// <param name="propertyName">Name of the value property to get.</param>
		/// <param name="value">If the property is found, this contains its value; otherwise
		/// this contains default value.</param>
		/// <returns>True if the property is found; false otherwise.</returns>
		/// <remarks>
		/// <para>Call this method with the same <paramref name="propertyName"/> that it was set with to get it.</para>
		/// </remarks>
		/// <seealso cref="SetValue(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,T)"/>
		/// <seealso cref="TryGetValue(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,out object)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public bool TryGetValue(BlackboardPropertyName propertyName, out T value)
		{
			Profiler.BeginSample("BlackboardTable.TryGetValue<T>");

			bool answer = m_table.TryGetValue(propertyName, out value);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>Tries to get and return a value of <paramref name="propertyName"/>.</summary>
		/// <param name="propertyName">Name of the value property to get.</param>
		/// <param name="value">If the property is found, this contains its value; otherwise
		/// this contains default value.</param>
		/// <returns>True if the property is found; false otherwise.</returns>
		/// <remarks>
		/// <para>Call this method with the same <paramref name="propertyName"/> that it was set with to get it.</para>
		/// </remarks>
		/// <seealso cref="SetValue(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,object)"/>
		/// <seealso cref="TryGetValue(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,out T)"/>
		[Pure]
		public bool TryGetValue(BlackboardPropertyName propertyName, out object value)
		{
			Profiler.BeginSample("BlackboardTable.TryGetValue");

			if (m_table.TryGetValue(propertyName, out T typedValue))
			{
				value = typedValue;

				Profiler.EndSample();

				return true;
			}

			value = default;

			Profiler.EndSample();

			return false;
		}

		/// <summary>
		/// Sets a value of <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to set.</param>
		/// <param name="value">Value to set.</param>
		/// <remarks>
		/// <para>If the value of <paramref name="propertyName"/> doesn't exist
		/// in the <see cref="BlackboardTable{T}"/>, this method may allocate because of resizing.</para>
		/// <para>To apply the change of the value and send callbacks subscribed to it, call <see cref="Flush"/>.</para>
		/// </remarks>
		/// <seealso cref="TryGetValue(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,out T)"/>
		/// <seealso cref="SetValue(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,object)"/>
		public void SetValue(BlackboardPropertyName propertyName, [CanBeNull] T value)
		{
			Profiler.BeginSample("BlackboardTable.SetValue<T>");

			if (m_bufferedTable.TryGetValue(propertyName, out T bufferedValue)
				&& s_equalityComparer.Equals(bufferedValue, value))
			{
				Profiler.EndSample();

				return;
			}

			BlackboardDebug.LogDetails($"[BlackboardTable] Set value '{value}' of type '{valueType.FullName}' into property '{propertyName}'");

			m_bufferedTable[propertyName] = value;

			if (m_table.TryGetValue(propertyName, out T currentValue)
				&& s_equalityComparer.Equals(currentValue, value))
			{
				m_changedProperties.Remove(propertyName);

				Profiler.EndSample();

				return;
			}

			if (!m_changedProperties.Contains(propertyName))
			{
				m_changedProperties.Add(propertyName);
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Sets a value of <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to set.</param>
		/// <param name="value">Value to set.</param>
		/// <remarks>
		/// <para>If the value of <paramref name="propertyName"/> doesn't exist
		/// in the <see cref="BlackboardTable{T}"/>, this method may allocate because of resizing.</para>
		/// <para>To apply the change of the value and send callbacks subscribed to it, call <see cref="Flush"/>.</para>
		/// </remarks>
		/// <seealso cref="TryGetValue(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,out object)"/>
		/// <seealso cref="SetValue(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,T)"/>
		public void SetValue(BlackboardPropertyName propertyName, object value)
		{
			Profiler.BeginSample("BlackboardTable.SetValue");

			if (!(value is T typedValue)
				|| m_bufferedTable.TryGetValue(propertyName, out T bufferedValue)
				&& s_equalityComparer.Equals(bufferedValue, typedValue))
			{
				Profiler.EndSample();

				return;
			}

			BlackboardDebug.LogDetails($"[BlackboardTable] Set value '{value}' of type '{valueType.FullName}' into property '{propertyName}'");

			m_bufferedTable[propertyName] = typedValue;

			if (m_table.TryGetValue(propertyName, out T currentValue)
				&& s_equalityComparer.Equals(currentValue, typedValue))
			{
				m_changedProperties.Remove(propertyName);

				Profiler.EndSample();

				return;
			}

			if (!m_changedProperties.Contains(propertyName))
			{
				m_changedProperties.Add(propertyName);
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Gets all properties and adds them to <paramref name="properties"/>.
		/// </summary>
		/// <param name="properties">Properties are added to this.</param>
		/// <seealso cref="GetProperties(System.Collections.Generic.List{System.Collections.Generic.KeyValuePair{Zor.EventBasedBlackboard.Core.BlackboardPropertyName,object}})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetProperties([NotNull] List<KeyValuePair<BlackboardPropertyName, T>> properties)
		{
			Profiler.BeginSample("BlackboardTable.GetProperties<T>");

			properties.AddRange(m_table);

			Profiler.EndSample();
		}

		/// <summary>
		/// Gets all properties and adds them to <paramref name="properties"/>.
		/// </summary>
		/// <param name="properties">Properties are added to this.</param>
		/// <seealso cref="GetProperties(System.Collections.Generic.List{System.Collections.Generic.KeyValuePair{Zor.EventBasedBlackboard.Core.BlackboardPropertyName,T}})"/>
		public void GetProperties(List<KeyValuePair<BlackboardPropertyName, object>> properties)
		{
			Profiler.BeginSample("BlackboardTable.GetProperties");

			ListHelper.EnsureCapacity(properties, m_table.Count);

			Dictionary<BlackboardPropertyName, T>.Enumerator enumerator = m_table.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<BlackboardPropertyName, T> property = enumerator.Current;
				properties.Add(new KeyValuePair<BlackboardPropertyName, object>(property.Key, property.Value));
			}
			enumerator.Dispose();

			Profiler.EndSample();
		}

		/// <summary>
		/// Checks if the <see cref="BlackboardTable{T}"/> contains a property of <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to find.</param>
		/// <returns>
		/// True if the property of <paramref name="propertyName"/> is contained in the
		/// <see cref="BlackboardTable{T}"/>; false otherwise.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public bool Contains(BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("BlackboardTable.Contains");

			bool answer = m_table.ContainsKey(propertyName);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Removes a property of <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to remove.</param>
		/// <returns>True if the property is removed; false if it doesn't exist.</returns>
		/// <remarks>
		/// To apply the change of the value and send callbacks subscribed to it, call <see cref="Flush"/>.
		/// </remarks>
		public bool Remove(BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("BlackboardTable.Remove");

			BlackboardDebug.LogDetails($"[BlackboardTable] Remove property '{propertyName}' of type '{valueType.FullName}'");

			bool answer = m_bufferedTable.Remove(propertyName);

			if (answer)
			{
				if (!m_table.ContainsKey(propertyName))
				{
					m_changedProperties.Remove(propertyName);
				}
				else if (!m_changedProperties.Contains(propertyName))
				{
					m_changedProperties.Add(propertyName);
				}
			}

			Profiler.EndSample();

			return answer;
		}


		/// <summary>
		/// Clears of all properties.
		/// </summary>
		/// <remarks>
		/// To apply the change of the values and send callbacks subscribed to it, call <see cref="Flush"/>.
		/// </remarks>
		public void Clear()
		{
			Profiler.BeginSample("BlackboardTable.Clear");

			BlackboardDebug.LogDetails($"[BlackboardTable] Clear blackboard table of type '{valueType.FullName}'");

			Dictionary<BlackboardPropertyName, T>.KeyCollection.Enumerator enumerator =
				m_bufferedTable.Keys.GetEnumerator();
			while (enumerator.MoveNext())
			{
				BlackboardPropertyName propertyName = enumerator.Current;

				if (m_table.ContainsKey(propertyName) && !m_changedProperties.Contains(propertyName))
				{
					m_changedProperties.Add(propertyName);
				}
			}
			enumerator.Dispose();

			m_bufferedTable.Clear();

			Profiler.EndSample();
		}

		/// <summary>
		/// Subscribes <paramref name="onChanged"/> to a value of <paramref name="propertyName"/> change.
		/// It's called when the value is changed in <see cref="Flush"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property which change to subscribe to.</param>
		/// <param name="onChanged">Callback that is called when the value is changed.</param>
		/// <remarks>
		/// This method may allocate because of resizing of the list of callbacks.
		/// </remarks>
		/// <seealso cref="Unsubscribe(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action)"/>
		/// <seealso cref="Subscribe(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action{BlackboardChangeInfo{T}})"/>
		public void Subscribe(BlackboardPropertyName propertyName, Action onChanged)
		{
			Profiler.BeginSample("BlackboardTable.Subscribe not typed");

			BlackboardDebug.LogDetails($"[BlackboardTable] Subscribe callback to property '{propertyName}' of type '{valueType.FullName}'");

			if (m_onChanged.TryGetValue(propertyName, out Action existingOnChanged))
			{
				existingOnChanged += onChanged;
				m_onChanged[propertyName] = existingOnChanged;
			}
			else
			{
				m_onChanged[propertyName] = onChanged;
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Unsubscribes <paramref name="onChanged"/> from a value of <paramref name="propertyName"/> change.
		/// </summary>
		/// <param name="propertyName">Name of the property which change to unsubscribe from.</param>
		/// <param name="onChanged">Callback to unsubscribe.</param>
		/// <remarks>
		/// To unsubscribe successfully, use the same <paramref name="onChanged"/> that was used in
		/// <see cref="Subscribe(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action)"/>.
		/// </remarks>
		/// <seealso cref="Subscribe(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action)"/>
		public void Unsubscribe(BlackboardPropertyName propertyName, Action onChanged)
		{
			Profiler.BeginSample("BlackboardTable.Unsubscribe not typed");

			if (!m_onChanged.TryGetValue(propertyName, out Action existingOnChanged))
			{
				Profiler.EndSample();

				return;
			}

			BlackboardDebug.LogDetails($"[BlackboardTable] Unsubscribe callback from property '{propertyName}' of type '{valueType.FullName}'");

			existingOnChanged -= onChanged;

			if (existingOnChanged == null)
			{
				m_onChanged.Remove(propertyName);
			}
			else
			{
				m_onChanged[propertyName] = existingOnChanged;
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Subscribes <paramref name="onChanged"/> to a value of <paramref name="propertyName"/> change.
		/// It's called when the value is changed in <see cref="Flush"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property which change to subscribe to.</param>
		/// <param name="onChanged">Callback that is called when the value is changed.</param>
		/// <remarks>
		/// This method may allocate because of resizing of the list of callbacks.
		/// </remarks>
		/// <seealso cref="Unsubscribe(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action{BlackboardChangeInfo{T}})"/>
		/// <seealso cref="Subscribe(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action)"/>
		public void Subscribe(BlackboardPropertyName propertyName, [NotNull] Action<BlackboardChangeInfo<T>> onChanged)
		{
			Profiler.BeginSample("BlackboardTable.Subscribe typed");

			BlackboardDebug.LogDetails($"[BlackboardTable] Subscribe typed callback to property '{propertyName}' of type '{valueType.FullName}'");

			if (m_onTypedChanged.TryGetValue(propertyName, out Action<BlackboardChangeInfo<T>> existingOnChanged))
			{
				existingOnChanged += onChanged;
				m_onTypedChanged[propertyName] = existingOnChanged;
			}
			else
			{
				m_onTypedChanged[propertyName] = onChanged;
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Unsubscribes <paramref name="onChanged"/> from a value of <paramref name="propertyName"/> change.
		/// </summary>
		/// <param name="propertyName">Name of the property which change to unsubscribe from.</param>
		/// <param name="onChanged">Callback to unsubscribe.</param>
		/// <remarks>
		/// To unsubscribe successfully, use the same <paramref name="onChanged"/> and that was used in
		/// <see cref="Subscribe(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action{BlackboardChangeInfo{T}})"/>.
		/// </remarks>
		/// <seealso cref="Subscribe(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action{BlackboardChangeInfo{T}})"/>
		public void Unsubscribe(BlackboardPropertyName propertyName,
			[NotNull] Action<BlackboardChangeInfo<T>> onChanged)
		{
			Profiler.BeginSample("BlackboardTable.Unsubscribe typed");

			if (!m_onTypedChanged.TryGetValue(propertyName, out Action<BlackboardChangeInfo<T>> existingOnChanged))
			{
				Profiler.EndSample();

				return;
			}

			BlackboardDebug.LogDetails($"[BlackboardTable] Unsubscribe typed callback from property '{propertyName}' of type '{valueType.FullName}'");

			existingOnChanged -= onChanged;

			if (existingOnChanged == null)
			{
				m_onTypedChanged.Remove(propertyName);
			}
			else
			{
				m_onTypedChanged[propertyName] = existingOnChanged;
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Applies all changes in values and creates a list of current changes.
		/// </summary>
		/// <returns>True if at least one value is flushed; false otherwise.</returns>
		/// <remarks>
		/// <para>To apply changes made in methods like
		/// <see cref="SetValue(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,T)"/>, <see cref="Remove"/> and
		/// <see cref="Clear"/>, call this method.</para>
		/// <para>This method may allocate because of resizing of pools. When pools reach a required size, this
		/// method stops allocating.</para>
		/// <para>To send callbacks of changed values, call <see cref="SendCallbacks"/>.</para>
		/// </remarks>
		public bool Flush()
		{
			Profiler.BeginSample("BlackboardTable.Flush");

			if (!dirty)
			{
				Profiler.EndSample();

				return false;
			}

			for (int i = 0, propertiesCount = m_changedProperties.Count; i < propertiesCount; ++i)
			{
				BlackboardPropertyName propertyName = m_changedProperties[i];
				BlackboardChangeInfo<T> changeInfo;

				if (m_bufferedTable.TryGetValue(propertyName, out T value))
				{
					m_table[propertyName] = value;
					changeInfo = BlackboardChangeInfo<T>.CreateChanged(value);
				}
				else
				{
					m_table.Remove(propertyName);
					changeInfo = BlackboardChangeInfo<T>.CreateRemoved();
				}

				var changeMessage = new ChangeMessage(propertyName, changeInfo);
				m_changeMessages.Add(changeMessage);
			}

			m_changedProperties.Clear();

			Profiler.EndSample();

			return true;
		}

		/// <summary>
		/// Send current callbacks of changed values.
		/// </summary>
		/// <remarks>
		/// The list of current callbacks is created in <see cref="Flush"/>.
		/// </remarks>
		public void SendCallbacks()
		{
			Profiler.BeginSample("BlackboardTable.SendCallbacks");

			for (int i = 0, messagesCount = m_changeMessages.Count; i < messagesCount; ++i)
			{
				ChangeMessage changeMessage = m_changeMessages[i];
				BlackboardPropertyName propertyName = changeMessage.propertyName;

				if (m_onChanged.TryGetValue(propertyName, out Action onChanged))
				{
					Profiler.BeginSample("BlackboardTable.SendCallbacks.OnChanged");

					try
					{
						onChanged();
					}
					catch (Exception exception)
					{
						BlackboardDebug.LogException(exception);
					}

					Profiler.EndSample();
				}

				if (m_onTypedChanged.TryGetValue(propertyName, out Action<BlackboardChangeInfo<T>> onTypedChanged))
				{
					Profiler.BeginSample("BlackboardTable.SendCallbacks.OnTypedChanged");

					try
					{
						onTypedChanged(changeMessage.changeInfo);
					}
					catch (Exception exception)
					{
						BlackboardDebug.LogException(exception);
					}

					Profiler.EndSample();
				}
			}

			m_changeMessages.Clear();

			Profiler.EndSample();
		}

		[Pure]
		public override string ToString()
		{
			var builder = new StringBuilder();

			Dictionary<BlackboardPropertyName, T>.Enumerator enumerator = m_table.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<BlackboardPropertyName, T> current = enumerator.Current;
				builder.Append($"[{current.Key.ToString()}, {current.Value.ToString()}], ");
			}
			enumerator.Dispose();

			int builderLength = builder.Length;
			int length = builderLength >= 2 ? builderLength - 2 : 0;

			return builder.ToString(0, length);
		}

		/// <summary>
		/// Info about a changed property. It's generated in <see cref="BlackboardTable{T}.Flush"/>.
		/// </summary>
		private readonly struct ChangeMessage : IEquatable<ChangeMessage>
		{
			public readonly BlackboardPropertyName propertyName;
			public readonly BlackboardChangeInfo<T> changeInfo;

			public ChangeMessage(BlackboardPropertyName propertyName, BlackboardChangeInfo<T> changeInfo)
			{
				this.propertyName = propertyName;
				this.changeInfo = changeInfo;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			public override int GetHashCode()
			{
				return propertyName.GetHashCode() ^ changeInfo.GetHashCode();
			}

			[Pure]
			public override bool Equals(object obj)
			{
				return obj is ChangeMessage other && Equals(other);
			}

			[Pure]
			public bool Equals(ChangeMessage other)
			{
				return propertyName.Equals(other.propertyName) && changeInfo.Equals(other.changeInfo);
			}

			[Pure]
			public override string ToString()
			{
				return $"{propertyName.ToString()} {changeInfo.ToString()}";
			}
		}
	}
}
