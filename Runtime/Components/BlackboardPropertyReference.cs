// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using Zor.EventBasedBlackboard.Components.Main;

namespace Zor.EventBasedBlackboard.Components
{
	/// <summary>
	/// Useful serializable struct for referencing a property in <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/>
	/// in <see cref="Zor.EventBasedBlackboard.Components.Main.BlackboardContainerComponent"/>.
	/// </summary>
	/// <remarks>
	/// Usually it's used in <see cref="UnityEngine.Component"/>.
	/// </remarks>
	[Serializable]
	public struct BlackboardPropertyReference
	{
		public BlackboardContainerComponent blackboardContainer;
		public string propertyName;
	}
}
