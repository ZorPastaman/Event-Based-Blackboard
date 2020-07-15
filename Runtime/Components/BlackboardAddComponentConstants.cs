// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

namespace Zor.EventBasedBlackboard.Components
{
	/// <summary>
	/// Collection of constant folders for <see cref="UnityEngine.AddComponentMenu"/>.
	/// </summary>
	/// <example>
	/// [AddComponentMenu(BlackboardAddComponentConstants.MainFolder + "Blackboard Container")]
	/// </example>
	public static class BlackboardAddComponentConstants
	{
		public const string BlackboardFolder = "Event Based Blackboard/";
		public const string MainFolder = BlackboardFolder + "Main/";
		public const string FlushersFolder = MainFolder + "Flushers/";
	}
}
