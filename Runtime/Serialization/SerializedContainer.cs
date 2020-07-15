// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using System.Collections.Generic;
using UnityEngine;
using Zor.EventBasedBlackboard.Core;

namespace Zor.EventBasedBlackboard.Serialization
{
	/// <summary>
	/// <para>Serialized container of properties for <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/>.</para>
	/// <para>Inherit this if you want to get a custom functionality.</para>
	/// <para>If you need a common functionality, inherit <see cref="GeneratedValueSerializedTable{T}"/>
	/// or <see cref="SerializedValueSerializedTable{T}"/>.</para>
	/// </summary>
	public abstract class SerializedContainer : ScriptableObject
	{
		/// <summary>
		/// Applies its properties to <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboard">Applies its properties to this.</param>
		public abstract void Apply(Blackboard blackboard);

		/// <summary>
		/// Gets keys and their types and adds them to <paramref name="keys"/>.
		/// </summary>
		/// <param name="keys">Keys are added to this.</param>
		public abstract void GetKeys(List<(string, Type)> keys);
	}
}
