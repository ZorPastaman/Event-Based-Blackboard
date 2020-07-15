// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using UnityEngine;
using Zor.EventBasedBlackboard.Core;
using Zor.EventBasedBlackboard.Debugging;
using Zor.EventBasedBlackboard.Serialization;

namespace Zor.EventBasedBlackboard.Components.Main
{
	/// <summary>
	/// Container of <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/> for using that as
	/// <see cref="UnityEngine.Component"/>.
	/// </summary>
	/// <remarks>
	/// As <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/> requires flushing to update its values, use any
	/// <see cref="Zor.EventBasedBlackboard.Components.Main.Flushers.BlackboardContainerFlusher"/> to call
	/// <see cref="Zor.EventBasedBlackboard.Core.Blackboard.Flush"/> of
	/// <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/> in this component.
	/// </remarks>
	[AddComponentMenu(BlackboardAddComponentConstants.MainFolder + "Blackboard Container")]
	public sealed class BlackboardContainerComponent : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField, Tooltip("Array of serialized properties for Blackboard.\nIt is applied to Blackboard only on Awake.")]
		private SerializedContainer[] m_SerializedContainers;
#pragma warning restore CS0649

		private Blackboard m_blackboard;

		/// <summary>
		/// Contained <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/> or null if it's called on prefab.
		/// </summary>
		public Blackboard blackboard => m_blackboard;

		private void Awake()
		{
			m_blackboard = new Blackboard();

			for (int i = 0, count = m_SerializedContainers.Length; i < count; ++i)
			{
				SerializedContainer container = m_SerializedContainers[i];

				if (container == null)
				{
					BlackboardDebug.LogWarning($"[BlackboardContainerComponent] SerializedContainer at index '{i}' is null", this);
					continue;
				}

				container.Apply(m_blackboard);
			}

			m_blackboard.Flush();
		}

		[ContextMenu("Flush All")]
		private void FlushAll()
		{
			m_blackboard.Flush();
		}

		[ContextMenu("Flush Single Pass")]
		private void FlushSinglePass()
		{
			m_blackboard.Flush(true);
		}

		[ContextMenu("Log")]
		private void Log()
		{
			UnityEngine.Debug.Log(m_blackboard.ToString(), this);
		}
	}
}
