// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using JetBrains.Annotations;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	/// <summary>
	/// Interface for <see cref="BlackboardValueView{T}"/>.
	/// </summary>
	internal interface IBlackboardValueView
	{
		/// <summary>
		/// Value type of the drawn property.
		/// </summary>
		[NotNull]
		Type valueType { get; }
	}
}
