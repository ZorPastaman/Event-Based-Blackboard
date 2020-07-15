// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using UnityEngine;
using Zor.EventBasedBlackboard.Debugging;

namespace Zor.EventBasedBlackboard.Components.Main.Flushers
{
	/// <summary>
	/// Derive this component to implement your Flusher for <see cref="Zor.EventBasedBlackboard.Components.Main.BlackboardContainerComponent"/>.
	/// </summary>
	/// <remarks>
	/// You need to call <see cref="Flush"/> for flushing.
	/// </remarks>
	public abstract class BlackboardContainerFlusher : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField] private BlackboardContainerComponent m_BlackboardContainer;
		[SerializeField, Tooltip("If it's true, blackboard will flush until it's not dirty;\nif it's false, blackboard will flush only once.")]
		private bool m_ForceSinglePass;
#pragma warning restore CS0649

		/// <summary>
		/// Call this method for flushing
		/// </summary>
		protected void Flush()
		{
			m_BlackboardContainer.blackboard.Flush(m_ForceSinglePass);
		}

		protected virtual void OnEnable()
		{
			if (m_BlackboardContainer == null)
			{
				BlackboardDebug.LogError("[BlackboardContainerFlusher] Blackboard Container is null. Flusher is off", this);
				enabled = false;
			}
		}
	}
}
