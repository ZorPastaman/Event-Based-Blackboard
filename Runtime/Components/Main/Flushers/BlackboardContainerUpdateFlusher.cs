// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using UnityEngine;

namespace Zor.EventBasedBlackboard.Components.Main.Flushers
{
	/// <summary>
	/// This flusher calls <see cref="Zor.EventBasedBlackboard.Components.Main.Flushers.BlackboardContainerFlusher.Flush"/>
	/// every <see cref="Update"/>.
	/// </summary>
	[AddComponentMenu(BlackboardAddComponentConstants.FlushersFolder +  "Blackboard Container Update Flusher")]
	public sealed class BlackboardContainerUpdateFlusher : BlackboardContainerFlusher
	{
		private void Update()
		{
			Flush();
		}
	}
}
