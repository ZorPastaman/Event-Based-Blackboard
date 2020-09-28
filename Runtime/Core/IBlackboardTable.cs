// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Zor.EventBasedBlackboard.Core
{
	internal interface IBlackboardTable
	{
		/// <summary>
		/// Type of values that are contained in the <see cref="IBlackboardTable"/>.
		/// </summary>
		[NotNull]
		Type valueType { get; }

		/// <summary>
		/// How many flushed values are contained in the <see cref="IBlackboardTable"/>.
		/// </summary>
		int count { get; }

		/// <summary>
		/// <para>True if there's at least one not flushed value.</para>
		/// <para>False if there's no not flushed value.</para>
		/// </summary>
		/// <seealso cref="Flush"/>
		bool dirty { get; }

		/// <summary>Tries to get and return a value of <paramref name="propertyName"/>.</summary>
		/// <param name="propertyName">Name of the value property to get.</param>
		/// <param name="value">If the property is found, this contains its value; otherwise
		/// this contains default value.</param>
		/// <returns>True if the property is found; false otherwise.</returns>
		/// <remarks>
		/// <para>Call this method with the same <paramref name="propertyName"/> that it was set with to get it.</para>
		/// </remarks>
		/// <seealso cref="SetValue"/>
		bool TryGetValue(BlackboardPropertyName propertyName, out object value);

		/// <summary>
		/// Sets a value of <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the value property to set.</param>
		/// <param name="value">Value to set.</param>
		/// <remarks>
		/// <para>If the value of <paramref name="propertyName"/> doesn't exist
		/// in the <see cref="IBlackboardTable"/>, this method may allocate because of resizing.</para>
		/// <para>To apply the change of the value and send callbacks subscribed to it, call <see cref="Flush"/>.</para>
		/// </remarks>
		/// <seealso cref="TryGetValue"/>
		void SetValue(BlackboardPropertyName propertyName, [CanBeNull] object value);

		/// <summary>
		/// Gets all properties and adds them to <paramref name="properties"/>.
		/// </summary>
		/// <param name="properties">Properties are added to this.</param>
		void GetProperties([NotNull] List<KeyValuePair<BlackboardPropertyName, object>> properties);

		/// <summary>
		/// Checks if the <see cref="IBlackboardTable"/> contains a property of <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to find.</param>
		/// <returns>
		/// True if the property of <paramref name="propertyName"/> is contained in the
		/// <see cref="IBlackboardTable"/>; false otherwise.
		/// </returns>
		bool Contains(BlackboardPropertyName propertyName);

		/// <summary>
		/// Removes a property of <paramref name="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property to remove.</param>
		/// <returns>True if the property is removed; false if it doesn't exist.</returns>
		/// <remarks>
		/// To apply the change of the value and send callbacks subscribed to it, call <see cref="Flush"/>.
		/// </remarks>
		bool Remove(BlackboardPropertyName propertyName);

		/// <summary>
		/// Clears of all properties.
		/// </summary>
		/// <remarks>
		/// To apply the change of the values and send callbacks subscribed to it, call <see cref="Flush"/>.
		/// </remarks>
		void Clear();

		/// <summary>
		/// Subscribes <paramref name="onChanged"/> to a value of <paramref name="propertyName"/> change.
		/// It's called when the value is changed in <see cref="Flush"/>.
		/// </summary>
		/// <param name="propertyName">Name of the property which change to subscribe to.</param>
		/// <param name="onChanged">Callback that is called when the value is changed.</param>
		/// <remarks>
		/// This method may allocate because of resizing of the list of callbacks.
		/// </remarks>
		/// <seealso cref="Unsubscribe"/>
		void Subscribe(BlackboardPropertyName propertyName, [NotNull] Action onChanged);

		/// <summary>
		/// Unsubscribes <paramref name="onChanged"/> from a value of <paramref name="propertyName"/> change.
		/// </summary>
		/// <param name="propertyName">Name of the property which change to unsubscribe from.</param>
		/// <param name="onChanged">Callback to unsubscribe.</param>
		/// <remarks>
		/// To unsubscribe successfully, use the same <paramref name="onChanged"/> that was used in
		/// <see cref="Subscribe"/>.
		/// </remarks>
		/// <seealso cref="Subscribe"/>
		void Unsubscribe(BlackboardPropertyName propertyName, [NotNull] Action onChanged);

		/// <summary>
		/// Applies all changes in values and creates a list of current changes.
		/// </summary>
		/// <returns>True if at least one value is flushed; false otherwise.</returns>
		/// <remarks>
		/// <para>To apply changes made in methods like
		/// <see cref="SetValue"/>, <see cref="Remove"/> and <see cref="Clear"/>, call this method.</para>
		/// <para>This method may allocate because of resizing of pools. When pools reach a required size, this
		/// method stops allocating.</para>
		/// <para>To send callbacks of changed values, call <see cref="SendCallbacks"/>.</para>
		/// </remarks>
		bool Flush();

		/// <summary>
		/// Send current callbacks of changed values.
		/// </summary>
		/// <remarks>
		/// The list of current callbacks is created in <see cref="Flush"/>.
		/// </remarks>
		void SendCallbacks();
	}
}
