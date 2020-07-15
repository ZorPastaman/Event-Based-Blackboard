// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using UnityEngine;

namespace Zor.EventBasedBlackboard.Components.Main.Flushers
{
	/// <summary>
	/// This flusher calls <see cref="Zor.EventBasedBlackboard.Components.Main.Flushers.BlackboardContainerFlusher.Flush"/>
	/// every time in <see cref="Coroutine"/> after <see cref="WaitForFixedUpdate"/>.
	/// </summary>
	[AddComponentMenu(BlackboardAddComponentConstants.FlushersFolder + "Blackboard Container Wait For Fixed Update Flusher")]
	public sealed class BlackboardContainerWaitForFixedUpdateFlusher : BlackboardContainerCoroutineFlusher
	{
		protected override YieldInstruction instruction => new WaitForFixedUpdate();
	}
}
