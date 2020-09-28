// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Zor.EventBasedBlackboard.Components.Main.Flushers
{
	/// <summary>
	/// Derive this component to implement your Flusher for
	/// <see cref="Zor.EventBasedBlackboard.Components.Main.BlackboardContainerComponent"/>
	/// based on <see cref="UnityEngine.Coroutine"/>.
	/// </summary>
	public abstract class BlackboardContainerCoroutineFlusher : BlackboardContainerFlusher
	{
		private YieldInstruction m_instruction;
		private Coroutine m_process;

		/// <summary>
		/// <see cref="UnityEngine.YieldInstruction"/> which is processed before calling
		/// <see cref="Zor.EventBasedBlackboard.Components.Main.Flushers.BlackboardContainerCoroutineFlusher.Flush"/>.
		/// </summary>
		protected abstract YieldInstruction instruction { get; }

		/// <summary>
		/// Gets a new instruction from <see cref="instruction"/>.
		/// </summary>
		/// <remarks>
		/// It doesn't recreate a current coroutine.
		/// </remarks>
		[MethodImpl(MethodImplOptions.AggressiveInlining), ContextMenu("Update Instruction")]
		protected void UpdateInstruction()
		{
			m_instruction = instruction;
		}

		protected override void OnValidate()
		{
			m_instruction = instruction;
		}

		private void Awake()
		{
			m_instruction = instruction;
		}

		protected override void OnEnable()
		{
			base.OnEnable();

			if (!enabled)
			{
				return;
			}

			m_process = StartCoroutine(Process());
		}

		private void OnDisable()
		{
			if (m_process != null)
			{
				StopCoroutine(m_process);
				m_process = null;
			}
		}

		private IEnumerator Process()
		{
			while (true)
			{
				yield return m_instruction;
				Flush();
			}
		}
	}
}
