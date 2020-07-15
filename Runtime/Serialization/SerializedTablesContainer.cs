// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using System.Collections.Generic;
using UnityEngine;
using Zor.EventBasedBlackboard.Core;

namespace Zor.EventBasedBlackboard.Serialization
{
	/// <summary>
	/// Container of serialized properties for a <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/>.
	/// </summary>
	[CreateAssetMenu(menuName = "Event Based Blackboard/Serialized Tables Container", fileName = "SerializedTablesContainer", order = 445)]
	public sealed class SerializedTablesContainer : SerializedContainer
	{
#pragma warning disable CS0649
		[SerializeField, HideInInspector] private SerializedTable_Base[] m_SerializedTables;
#pragma warning restore CS0649

		/// <inheritdoc/>
		/// <remarks>
		/// This method doesn't call <see cref="Zor.EventBasedBlackboard.Core.Blackboard.Flush"/>
		/// </remarks>
		public override void Apply(Blackboard blackboard)
		{
			for (int i = 0, count = m_SerializedTables.Length; i < count; ++i)
			{
				SerializedTable_Base table = m_SerializedTables[i];

				if (table != null)
				{
					table.Apply(blackboard);
				}
			}
		}

		/// <inheritdoc/>
		public override void GetKeys(List<(string, Type)> keys)
		{
			for (int i = 0, count = m_SerializedTables.Length; i < count; ++i)
			{
				m_SerializedTables[i].GetKeys(keys);
			}
		}
	}
}
