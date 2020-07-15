﻿// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using System.Collections.Generic;
using UnityEngine;
using Zor.EventBasedBlackboard.Core;

namespace Zor.EventBasedBlackboard.Serialization
{
	/// <summary>
	/// <para>Base class for a serialized table.</para>
	/// <para>Inherit this for a special functionality.</para>
	/// <para>If you need a common functionality, inherit <see cref="GeneratedValueSerializedTable{T}"/>
	/// or <see cref="SerializedValueSerializedTable{T}"/>.</para>
	/// </summary>
	public abstract class SerializedTable_Base : ScriptableObject
	{
		/// <summary>
		/// Type of serialized value in this table.
		/// </summary>
		public abstract Type valueType { get; }

		/// <summary>
		/// Applies its properties to <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboard">Applies its properties to this.</param>
		/// <remarks>
		/// Doesn't call <see cref="Zor.EventBasedBlackboard.Core.Blackboard.Flush"/>.
		/// </remarks>
		public abstract void Apply(Blackboard blackboard);

		/// <summary>
		/// Gets keys and their types and adds them to <paramref name="keys"/>.
		/// </summary>
		/// <param name="keys">Keys are added to this.</param>
		public abstract void GetKeys(List<(string, Type)> keys);
	}
}
