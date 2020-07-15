// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using UnityEngine;

namespace Zor.EventBasedBlackboard.Components.Main.Flushers
{
	/// <summary>
	/// This flusher calls <see cref="Zor.EventBasedBlackboard.Components.Main.Flushers.BlackboardContainerFlusher.Flush"/>
	/// every <see cref="LateUpdate"/>.
	/// </summary>
	[AddComponentMenu(BlackboardAddComponentConstants.FlushersFolder + "Blackboard Container Late Update Flusher")]
	public sealed class BlackboardContainerLateUpdateFlusher : BlackboardContainerFlusher
	{
		private void LateUpdate()
		{
			Flush();
		}
	}
}
