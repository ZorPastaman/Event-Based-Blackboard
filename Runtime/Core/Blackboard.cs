// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using UnityEngine.Profiling;
using Zor.EventBasedBlackboard.Debugging;

namespace Zor.EventBasedBlackboard.Core
{
	/// <summary>
	/// The main class of the blackboard system.
	/// </summary>
	/// <remarks>
	/// <para>Initialize this class with its default constructor to use it in a regular c# code.</para>
	/// <para>To use it in the Unity components system,
	/// use <see cref="Zor.EventBasedBlackboard.Components.Main.BlackboardContainerComponent"/>.</para>
	/// </remarks>
	public sealed class Blackboard
	{
		/// <summary>
		/// Dictionary of value type to <see cref="IBlackboardTable"/> of that type.
		/// </summary>
		private readonly Dictionary<Type, IBlackboardTable> m_tables = new Dictionary<Type, IBlackboardTable>();
		/// <summary>
		/// Temporary list of <see cref="IBlackboardTable"/> used inside <see cref="Flush"/>.
		/// </summary>
		private readonly List<IBlackboardTable> m_flushTables = new List<IBlackboardTable>();

		/// <summary>
		/// If at least one value changed in <see cref="Flush"/>, this event is called.
		/// </summary>
		public event Action OnChanged;

		/// <summary>
		/// How many value types are contained in the <see cref="Blackboard"/>.
		/// </summary>
		public int valueTypesCount
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_tables.Count;
		}

		/// <summary>
		/// How many properties are contained in the <see cref="Blackboard"/>.
		/// </summary>
		public int propertiesCount
		{
			[Pure]
			get
			{
				Profiler.BeginSample("EventBasedBlackboard.propertiesCount");

				int count = 0;

				Dictionary<Type, IBlackboardTable>.ValueCollection.Enumerator enumerator =
					m_tables.Values.GetEnumerator();
				while (enumerator.MoveNext())
				{
					count += enumerator.Current.count;
				}
				enumerator.Dispose();

				Profiler.EndSample();

				return count;
			}
		}

		/// <summary>
		/// <para>True if there's at least one not flushed value.</para>
		/// <para>False if there's no not flushed value.</para>
		/// </summary>
		/// <seealso cref="Flush"/>
		public bool dirty
		{
			[Pure]
			get
			{
				Profiler.BeginSample("EventBasedBlackboard.dirty");

				bool isDirty = false;

				Dictionary<Type, IBlackboardTable>.ValueCollection.Enumerator enumerator =
					m_tables.Values.GetEnumerator();
				while (enumerator.MoveNext() & !isDirty)
				{
					isDirty = enumerator.Current.dirty;
				}
				enumerator.Dispose();

				Profiler.EndSample();

				return isDirty;
			}
		}

		/// <summary>Tries to get and return a value of <typeparamref name="T"/>
		/// and <paramref name="propertyName"/>.</summary>
		/// <param name="propertyName">Name of the value property to get.</param>
		/// <param name="value">If the property is found, this contains its value; otherwise
		/// this contains default value.</param>
		/// <typeparam name="T">Type of the value to get.</typeparam>
		/// <returns>True if the property is found; false otherwise.</returns>
		/// <remarks>
		/// <para>Call this method with the same <paramref name="propertyName"/> and
		/// <typeparamref name="T"/> that it was set with to get it.</para>
		/// <para><see cref="Blackboard"/> doesn't support derivation.</para>
		/// </remarks>
		/// <seealso cref="SetValue{T}"/>
		/// <seealso cref="TryGetValue"/>
		[Pure]
		public bool TryGetValue<T>(BlackboardPropertyName propertyName, out T value)
		{
			Profiler.BeginSample("EventBasedBlackboard.TryGetValue<T>");

			if (!m_tables.TryGetValue(typeof(T), out IBlackboardTable table))
			{
				value = default;

				Profiler.EndSample();

				return false;
			}

			var typedTable = (BlackboardTable<T>)table;
			bool answer = typedTable.TryGetValue(propertyName, out value);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>Tries to get and return a value of <paramref name="valueType"/>
		/// and <paramref name="propertyName"/>.</summary>
		/// <param name="valueType">Type of the value to get.</param>
		/// <param name="propertyName">Name of the value property to get.</param>
		/// <param name="value">If the property is found, this contains its value;
		/// otherwise this contains default value.</param>
		/// <returns>True if the property is found; false otherwise.</returns>
		/// <remarks>
		/// <para>Call this method with the same <paramref name="propertyName"/> and
		/// <paramref name="valueType"/> that it was set with to get it.</para>
		/// <para><see cref="Blackboard"/> doesn't support derivation.</para>
		/// <para>If it's possible, use <see cref="TryGetValue{T}"/> because this method may be slower
		/// and box a value of a struct.</para>
		/// </remarks>
		/// <seealso cref="SetValue"/>
		/// <seealso cref="TryGetValue{T}"/>
		[Pure]
		public bool TryGetValue([NotNull] Type valueType, BlackboardPropertyName propertyName, out object value)
		{
			Profiler.BeginSample("EventBasedBlackboard.TryGetValue");

			if (!m_tables.TryGetValue(valueType, out IBlackboardTable table))
			{
				value = default;

				Profiler.EndSample();

				return false;
			}

			bool answer = table.TryGetValue(propertyName, out value);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Sets a value of <typeparamref name="T"/> and <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to set.</param>
		/// <param name="value">Value to set.</param>
		/// <typeparam name="T">Type of the value to set.</typeparam>
		/// <remarks>
		/// <para>If the value of <typeparamref name="T"/> is set first, this method allocates.</para>
		/// <para>If the value of <paramref name="propertyName"/> and <typeparamref name="T"/> doesn't exist
		/// in the <see cref="Blackboard"/>, this method may allocate because of resizing.</para>
		/// <para>To apply the change of the value and send callbacks subscribed to it, call <see cref="Flush"/>.</para>
		/// </remarks>
		/// <seealso cref="TryGetValue{T}"/>
		/// <seealso cref="SetValue"/>
		public void SetValue<T>(BlackboardPropertyName propertyName, [CanBeNull] T value)
		{
			Profiler.BeginSample("EventBasedBlackboard.SetValue<T>");

			BlackboardTable<T> typedTable = m_tables.TryGetValue(typeof(T), out IBlackboardTable table)
				? (BlackboardTable<T>)table
				: CreateTable<T>();
			typedTable.SetValue(propertyName, value);

			Profiler.EndSample();
		}

		/// <summary>
		/// Sets the value of <paramref name="valueType"/> and <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="valueType">Type of the value to set.</param>
		/// <param name="propertyName">Name of the value property to set.</param>
		/// <param name="value">Value to set.</param>
		/// <remarks>
		/// <para>If the value of <paramref name="valueType"/> is set first, this method allocates.</para>
		/// <para>If the value of <paramref name="propertyName"/> doesn't exist in <see cref="Blackboard"/>,
		/// this method may allocate because of resizing.</para>
		/// <para>To apply the change of the value and send callbacks subscribed to it, call <see cref="Flush"/>.</para>
		/// <para>If it's possible, use <see cref="SetValue{T}"/> because this method may be slower
		/// and box a value of a struct.</para>
		/// </remarks>
		/// <seealso cref="TryGetValue"/>
		/// <seealso cref="SetValue{T}"/>
		public void SetValue([NotNull] Type valueType, BlackboardPropertyName propertyName, [CanBeNull] object value)
		{
			Profiler.BeginSample("EventBasedBlackboard.SetValue");

			if (!m_tables.TryGetValue(valueType, out IBlackboardTable table))
			{
				table = CreateTable(valueType);
			}

			table.SetValue(propertyName, value);

			Profiler.EndSample();
		}

		/// <summary>
		/// Gets all properties of <typeparamref name="T"/> and adds them to <paramref name="properties"/>.
		/// </summary>
		/// <param name="properties">Found properties are added to this.</param>
		/// <typeparam name="T">Value type of the properties to add.</typeparam>
		/// <seealso cref="GetProperties"/>
		public void GetProperties<T>([NotNull] List<KeyValuePair<BlackboardPropertyName, T>> properties)
		{
			Profiler.BeginSample("EventBasedBlackboard.GetProperties<T>");

			if (m_tables.TryGetValue(typeof(T), out IBlackboardTable table))
			{
				var typedTable = (BlackboardTable<T>)table;
				typedTable.GetProperties(properties);
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Gets all properties of <paramref name="valueType"/> and adds them to <paramref name="properties"/>.
		/// </summary>
		/// <param name="valueType">Value type of the properties to add.</param>
		/// <param name="properties">Found properties are added to this.</param>
		/// <remarks>If it's possible, use <see cref="GetProperties{T}"/> because this method may be slower
		/// and box a value of a struct property.</remarks>
		/// <seealso cref="GetProperties{T}"/>
		public void GetProperties([NotNull] Type valueType,
			[NotNull] List<KeyValuePair<BlackboardPropertyName, object>> properties)
		{
			Profiler.BeginSample("EventBasedBlackboard.GetProperties");

			if (m_tables.TryGetValue(valueType, out IBlackboardTable table))
			{
				table.GetProperties(properties);
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Gets all value types contained in the <see cref="Blackboard"/>
		/// and adds them to <paramref name="valueTypes"/>.
		/// </summary>
		/// <param name="valueTypes">Found value types are added to this.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetValueTypes([NotNull] List<Type> valueTypes)
		{
			Profiler.BeginSample("EventBasedBlackboard.GetValueTypes");

			valueTypes.AddRange(m_tables.Keys);

			Profiler.EndSample();
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains a property of <paramref name="propertyName"/>
		/// and <typeparamref name="T"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to find.</param>
		/// <typeparam name="T">Type of the property value to find.</typeparam>
		/// <returns>
		/// True if the property of <paramref name="propertyName"/> and <typeparamref name="T"/>
		/// is contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="Contains(System.Type,Zor.EventBasedBlackboard.Core.BlackboardPropertyName)"/>
		[Pure]
		public bool Contains<T>(BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("EventBasedBlackboard.ContainsProperty<T>");

			bool answer = m_tables.TryGetValue(typeof(T), out IBlackboardTable table)
				&& ((BlackboardTable<T>)table).Contains(propertyName);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains a property of <paramref name="propertyName"/>
		/// and <paramref name="valueType"/>.
		/// </summary>
		/// <param name="valueType">Type of the property value to find.</param>
		/// <param name="propertyName">Name of the property to find.</param>
		/// <returns>
		/// True if the property of <paramref name="propertyName"/> and <paramref name="valueType"/>
		/// is contained in the <see cref="Blackboard"/>; false otherwise.
		/// </returns>
		/// <seealso cref="Contains{T}(Zor.EventBasedBlackboard.Core.BlackboardPropertyName)"/>
		[Pure]
		public bool Contains([NotNull] Type valueType, BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("EventBasedBlackboard.ContainsProperty");

			bool answer = m_tables.TryGetValue(valueType, out IBlackboardTable table)
				&& table.Contains(propertyName);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains a property of <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">Type of the value to find.</typeparam>
		/// <returns>True if the <see cref="Blackboard"/> contains at least one value of <typeparamref name="T"/>;
		/// false otherwise.</returns>
		/// <seealso cref="Contains(System.Type)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public bool Contains<T>()
		{
			Profiler.BeginSample("EventBasedBlackboard.ContainsTable<T>");

			bool answer = m_tables.ContainsKey(typeof(T));

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Checks if the <see cref="Blackboard"/> contains a property of <paramref name="valueType"/>.
		/// </summary>
		/// <param name="valueType">Type of the value to find.</param>
		/// <returns>True if the <see cref="Blackboard"/> contains at least one value of <paramref name="valueType"/>;
		/// false otherwise.</returns>
		/// <seealso cref="Contains{T}()"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public bool Contains([NotNull] Type valueType)
		{
			Profiler.BeginSample("EventBasedBlackboard.ContainsTable");

			bool answer = m_tables.ContainsKey(valueType);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Gets how many properties of <typeparamref name="T"/> are contained in the <see cref="Blackboard"/>.
		/// </summary>
		/// <typeparam name="T">Type of the value to find.</typeparam>
		/// <returns>
		/// <para>How many values of <typeparamref name="T"/> are contained in the <see cref="Blackboard"/>.</para>
		/// <para>-1 if the <see cref="Blackboard"/> doesn't contain a value of <typeparamref name="T"/>.</para>
		/// </returns>
		/// <seealso cref="GetCount"/>
		[Pure]
		public int GetCount<T>()
		{
			Profiler.BeginSample("EventBasedBlackboard.GetCount<T>");

			int answer = m_tables.TryGetValue(typeof(T), out IBlackboardTable table)
				? ((BlackboardTable<T>)table).count
				: -1;

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Gets how many properties of <paramref name="valueType"/> are contained in the <see cref="Blackboard"/>.
		/// </summary>
		/// <param name="valueType">Type of the value to find.</param>
		/// <returns>
		/// <para>How many values of <paramref name="valueType"/> are contained in the <see cref="Blackboard"/>.</para>
		/// <para>-1 if the <see cref="Blackboard"/> doesn't contain a value of <paramref name="valueType"/>.</para>
		/// </returns>
		/// <seealso cref="GetCount{T}"/>
		[Pure]
		public int GetCount([NotNull] Type valueType)
		{
			Profiler.BeginSample("EventBasedBlackboard.GetCount");

			int answer = m_tables.TryGetValue(valueType, out IBlackboardTable table)
				? table.count
				: -1;

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Removes a property of <paramref name="propertyName"/> and <typeparamref name="T"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to remove.</param>
		/// <typeparam name="T">Type of the value to remove.</typeparam>
		/// <returns>True if the property is removed; false if it doesn't exist.</returns>
		/// <remarks>
		/// To apply the change of the value and send callbacks subscribed to it, call <see cref="Flush"/>.
		/// </remarks>
		/// <seealso cref="Remove"/>
		public bool Remove<T>(BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("EventBasedBlackboard.Remove<T>");

			bool answer = m_tables.TryGetValue(typeof(T), out IBlackboardTable table)
				&& ((BlackboardTable<T>)table).Remove(propertyName);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Removes a property of <paramref name="propertyName"/> and <paramref name="valueType"/>.
		/// </summary>
		/// <param name="valueType">Type of the value to remove.</param>
		/// <param name="propertyName">Name of the property to remove.</param>
		/// <returns>True if the property is removed; false if it doesn't exist.</returns>
		/// <remarks>
		/// To apply the change of the value and send callbacks subscribed to it, call <see cref="Flush"/>.
		/// </remarks>
		/// <seealso cref="Remove{T}"/>
		public bool Remove([NotNull] Type valueType, BlackboardPropertyName propertyName)
		{
			Profiler.BeginSample("EventBasedBlackboard.Remove");

			bool answer = m_tables.TryGetValue(valueType, out IBlackboardTable table)
				&& table.Remove(propertyName);

			Profiler.EndSample();

			return answer;
		}

		/// <summary>
		/// Clears of all properties of <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">Value type to clear of.</typeparam>
		/// <remarks>
		/// To apply the change of the values and send callbacks subscribed to it, call <see cref="Flush"/>.
		/// </remarks>
		/// <seealso cref="Clear(System.Type)"/>
		public void Clear<T>()
		{
			Profiler.BeginSample("EventBasedBlackboard.Clear<T>");

			if (m_tables.TryGetValue(typeof(T), out IBlackboardTable table))
			{
				((BlackboardTable<T>)table).Clear();
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Clears of all properties of <paramref name="valueType"/>.
		/// </summary>
		/// <param name="valueType">Value type to clear of.</param>
		/// <remarks>
		/// To apply the change of the values and send callbacks subscribed to it, call <see cref="Flush"/>.
		/// </remarks>
		/// <seealso cref="Clear{T}"/>
		public void Clear([NotNull] Type valueType)
		{
			Profiler.BeginSample("EventBasedBlackboard.Clear");

			if (m_tables.TryGetValue(valueType, out IBlackboardTable table))
			{
				table.Clear();
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Clears of all properties.
		/// </summary>
		/// <remarks>
		/// To apply the change of the values and send callbacks subscribed to it, call <see cref="Flush"/>.
		/// </remarks>
		public void Clear()
		{
			Profiler.BeginSample("EventBasedBlackboard.ClearEverything");

			BlackboardDebug.LogDetails("[Blackboard] Clear blackboard");

			Dictionary<Type, IBlackboardTable>.ValueCollection.Enumerator enumerator = m_tables.Values.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.Clear();
			}
			enumerator.Dispose();

			Profiler.EndSample();
		}

		/// <summary>
		/// Subscribes <paramref name="onChanged"/> to a value of <paramref name="propertyName"/> and
		/// <typeparamref name="T"/> change. It's called when the value is changed in <see cref="Flush"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property which change to subscribe to.</param>
		/// <param name="onChanged">Callback that is called when the value is changed.</param>
		/// <typeparam name="T">Type of the value which change to subscribe to.</typeparam>
		/// <remarks>
		/// This method may allocate if the <see cref="Blackboard"/> doesn't contain a value of <typeparamref name="T"/>
		/// or because of resizing of the list of callbacks.
		/// </remarks>
		/// <seealso cref="Unsubscribe{T}(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action)"/>
		/// <seealso cref="Subscribe"/>
		/// <seealso cref="Subscribe{T}(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action{BlackboardChangeInfo{T}})"/>
		public void Subscribe<T>(BlackboardPropertyName propertyName, [NotNull] Action onChanged)
		{
			Profiler.BeginSample("EventBasedBlackboard.Subscribe<T> not typed");

			BlackboardTable<T> typedTable = m_tables.TryGetValue(typeof(T), out IBlackboardTable table)
				? (BlackboardTable<T>)table
				: CreateTable<T>();
			typedTable.Subscribe(propertyName, onChanged);

			Profiler.EndSample();
		}

		/// <summary>
		/// Unsubscribes <paramref name="onChanged"/> from a value of <paramref name="propertyName"/> and
		/// <typeparamref name="T"/> change.
		/// </summary>
		/// <param name="propertyName">Name of the property which change to unsubscribe from.</param>
		/// <param name="onChanged">Callback to unsubscribe.</param>
		/// <typeparam name="T">Type of the value which change to unsubscribe from.</typeparam>
		/// <remarks>
		/// To unsubscribe successfully, use the same <paramref name="onChanged"/> and <typeparamref name="T"/>
		/// that were used in
		/// <see cref="Subscribe{T}(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action)"/>.
		/// </remarks>
		/// <seealso cref="Subscribe{T}(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action)"/>
		public void Unsubscribe<T>(BlackboardPropertyName propertyName, [NotNull] Action onChanged)
		{
			Profiler.BeginSample("EventBasedBlackboard.Unsubscribe<T> not typed");

			if (m_tables.TryGetValue(typeof(T), out IBlackboardTable table))
			{
				var typedTable = (BlackboardTable<T>)table;
				typedTable.Unsubscribe(propertyName, onChanged);
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Subscribes <paramref name="onChanged"/> to a value of <paramref name="propertyName"/> and
		/// <paramref name="valueType"/> change. It's called when the value is changed in <see cref="Flush"/>.
		/// </summary>
		/// <param name="valueType">Type of the value which change to subscribe to.</param>
		/// <param name="propertyName">Name of the property which change to subscribe to.</param>
		/// <param name="onChanged">Callback that is called when the value is changed.</param>
		/// <remarks>
		/// This method may allocate if the <see cref="Blackboard"/> doesn't contain a value of
		/// <paramref name="valueType"/> or because of resizing of the list of callbacks.
		/// </remarks>
		/// <seealso cref="Unsubscribe"/>
		/// <seealso cref="Subscribe{T}(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action)"/>
		/// <seealso cref="Subscribe{T}(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action{BlackboardChangeInfo{T}})"/>
		public void Subscribe([NotNull] Type valueType, BlackboardPropertyName propertyName, [NotNull] Action onChanged)
		{
			Profiler.BeginSample("EventBasedBlackboard.Subscribe not typed");

			if (!m_tables.TryGetValue(valueType, out IBlackboardTable table))
			{
				table = CreateTable(valueType);
			}

			table.Subscribe(propertyName, onChanged);

			Profiler.EndSample();
		}

		/// <summary>
		/// Unsubscribes <paramref name="onChanged"/> from a value of <paramref name="propertyName"/> and
		/// <paramref name="valueType"/> change.
		/// </summary>
		/// <param name="valueType">Type of the value which change to unsubscribe from.</param>
		/// <param name="propertyName">Name of the property which change to unsubscribe from.</param>
		/// <param name="onChanged">Callback to unsubscribe.</param>
		/// <remarks>
		/// To unsubscribe successfully, use the same <paramref name="onChanged"/> and <paramref name="valueType"/>
		/// that were used in <see cref="Subscribe"/>.
		/// </remarks>
		/// <seealso cref="Subscribe"/>
		public void Unsubscribe([NotNull] Type valueType, BlackboardPropertyName propertyName,
			[NotNull] Action onChanged)
		{
			Profiler.BeginSample("EventBasedBlackboard.Unsubscribe not typed");

			if (m_tables.TryGetValue(valueType, out IBlackboardTable table))
			{
				table.Unsubscribe(propertyName, onChanged);
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Subscribes <paramref name="onChanged"/> to a value of <paramref name="propertyName"/> and
		/// <typeparamref name="T"/> change. It's called when the value is changed in <see cref="Flush"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property which change to subscribe to.</param>
		/// <param name="onChanged">Callback that is called when the value is changed.</param>
		/// <typeparam name="T">Type of the value which change to subscribe to.</typeparam>
		/// <remarks>
		/// This method may allocate if the <see cref="Blackboard"/> doesn't contain a value of <typeparamref name="T"/>
		/// or because of resizing of the list of callbacks.
		/// </remarks>
		/// <seealso cref="Unsubscribe{T}(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action{BlackboardChangeInfo{T}})"/>
		/// <seealso cref="Subscribe"/>
		/// <seealso cref="Subscribe{T}(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action)"/>
		public void Subscribe<T>(BlackboardPropertyName propertyName,
			[NotNull] Action<BlackboardChangeInfo<T>> onChanged)
		{
			Profiler.BeginSample("EventBasedBlackboard.Subscribe<T> typed");

			BlackboardTable<T> typedTable = m_tables.TryGetValue(typeof(T), out IBlackboardTable table)
				? (BlackboardTable<T>)table
				: CreateTable<T>();
			typedTable.Subscribe(propertyName, onChanged);

			Profiler.EndSample();
		}

		/// <summary>
		/// Unsubscribes <paramref name="onChanged"/> from a value of <paramref name="propertyName"/> and
		/// <typeparamref name="T"/> change.
		/// </summary>
		/// <param name="propertyName">Name of the property which change to unsubscribe from.</param>
		/// <param name="onChanged">Callback to unsubscribe.</param>
		/// <typeparam name="T">Type of the value which change to unsubscribe from.</typeparam>
		/// <remarks>
		/// To unsubscribe successfully, use the same <paramref name="onChanged"/> and <typeparamref name="T"/>
		/// that were used in
		/// <see cref="Subscribe{T}(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action{BlackboardChangeInfo{T}})"/>.
		/// </remarks>
		/// <seealso cref="Subscribe{T}(Zor.EventBasedBlackboard.Core.BlackboardPropertyName,System.Action{BlackboardChangeInfo{T}})"/>
		public void Unsubscribe<T>(BlackboardPropertyName propertyName,
			[NotNull] Action<BlackboardChangeInfo<T>> onChanged)
		{
			Profiler.BeginSample("EventBasedBlackboard.Unsubscribe<T> typed");

			if (m_tables.TryGetValue(typeof(T), out IBlackboardTable table))
			{
				var typedTable = (BlackboardTable<T>)table;
				typedTable.Unsubscribe(propertyName, onChanged);
			}

			Profiler.EndSample();
		}

		/// <summary>
		/// Applies all changes in values and sends callbacks.
		/// </summary>
		/// <param name="forceSinglePass">If true, the flush of all values is processed only once.
		/// If false, the flush of values is processed until the <see cref="Blackboard"/> is not dirty.</param>
		/// <returns>True if at least one value is flushed; false otherwise.</returns>
		/// <remarks>
		/// <para>To apply changes made in methods like <see cref="SetValue{T}"/>, <see cref="Remove{T}"/> and
		/// <see cref="Clear{T}"/>, call this method.</para>
		/// <para>Because values can be changed in callbacks, this method continues to flush values in cycle until the
		/// <see cref="Blackboard"/> is not dirty. To force to flush only once, set <paramref name="forceSinglePass"/>
		/// to true.</para>
		/// <para>This method may allocate because of resizing of pools. When pools reach a required size, this
		/// method stops allocating.</para>
		/// </remarks>
		public bool Flush(bool forceSinglePass = false)
		{
			Profiler.BeginSample("EventBasedBlackboard.Flush");

			if (!dirty)
			{
				Profiler.EndSample();

				return false;
			}

			bool changed = false;

			do
			{
				m_flushTables.AddRange(m_tables.Values);
				int count = m_flushTables.Count;

				for (int i = 0; i < count; ++i)
				{
					changed |= m_flushTables[i].Flush();
				}

				for (int i = 0; i < count; ++i)
				{
					m_flushTables[i].SendCallbacks();
				}

				m_flushTables.Clear();
			} while (!forceSinglePass && dirty);

			if (changed && OnChanged != null)
			{
				try
				{
					OnChanged();
				}
				catch (Exception exception)
				{
					BlackboardDebug.LogException(exception);
				}
			}

			Profiler.EndSample();

			return changed;
		}

		[Pure]
		public override string ToString()
		{
			var builder = new StringBuilder();

			Dictionary<Type, IBlackboardTable>.Enumerator enumerator = m_tables.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<Type, IBlackboardTable> current = enumerator.Current;
				builder.Append($"{{{current.Key.FullName}, {current.Value}}},\n");
			}
			enumerator.Dispose();

			int builderLength = builder.Length;
			int length = builderLength >= 2 ? builderLength - 2 : 0;

			return builder.ToString(0, length);
		}

		/// <summary>
		/// Creates a new table of <typeparamref name="T"/> and adds it to <see cref="m_tables"/>.
		/// </summary>
		/// <typeparam name="T">Type of the new table.</typeparam>
		/// <returns>Created table.</returns>
		/// <seealso cref="CreateTable"/>
		[NotNull]
		private BlackboardTable<T> CreateTable<T>()
		{
			Profiler.BeginSample("EventBasedBlackboard.CreateTable<T>");

			Type valueType = typeof(T);

			BlackboardDebug.Log($"[Blackboard] Create blackboard table of type '{valueType.FullName}'");

			var table = new BlackboardTable<T>();
			m_tables.Add(valueType, table);

			Profiler.EndSample();

			return table;
		}

		/// <summary>
		/// Creates a new table of <paramref name="valueType"/> and adds it to <see cref="m_tables"/>.
		/// </summary>
		/// <param name="valueType">Type of the new table.</param>
		/// <returns>Created table.</returns>
		/// <remarks>If it's possible, use <see cref="CreateTable{T}"/> because this method is more expensive.</remarks>
		/// <seealso cref="CreateTable{T}"/>
		[NotNull]
		private IBlackboardTable CreateTable([NotNull] Type valueType)
		{
			Profiler.BeginSample("EventBasedBlackboard.CreateTable");

			BlackboardDebug.Log($"[Blackboard] Create blackboard table of type '{valueType.FullName}'");

			Type tableType = typeof(BlackboardTable<>).MakeGenericType(valueType);
			var table = (IBlackboardTable)Activator.CreateInstance(tableType);
			m_tables.Add(valueType, table);

			Profiler.EndSample();

			return table;
		}
	}
}
