// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Zor.EventBasedBlackboard.Components.Main.Flushers
{
	/// <summary>
	/// This flusher calls <see cref="Zor.EventBasedBlackboard.Components.Main.Flushers.BlackboardContainerFlusher.Flush"/>
	/// every time in <see cref="Coroutine"/> after <see cref="WaitForSeconds"/>.
	/// </summary>
	/// <remarks>
	/// <see cref="WaitForSeconds"/> is configured with <see cref="m_Seconds"/>.
	/// </remarks>
	[AddComponentMenu(BlackboardAddComponentConstants.FlushersFolder + "Blackboard Container Wait For Seconds Flusher")]
	public sealed class BlackboardContainerWaitForSecondsFlusher : BlackboardContainerCoroutineFlusher
	{
#pragma warning disable CS0649
		[SerializeField] private float m_Seconds;
#pragma warning restore CS0649

		public float seconds
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_Seconds;
			set
			{
				if (m_Seconds == value)
				{
					return;
				}

				m_Seconds = value;
				UpdateInstruction();
			}
		}

		protected override YieldInstruction instruction => new WaitForSeconds(m_Seconds);
	}
}
