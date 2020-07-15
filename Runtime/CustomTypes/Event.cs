// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;

namespace Zor.EventBasedBlackboard.CustomTypes
{
	/// <summary>
	/// Event struct for <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/>.
	/// </summary>
	/// <remarks>
	/// <para>This is an empty struct which comparisons are always false.</para>
	/// <para>Call <see cref="Zor.EventBasedBlackboard.Core.Blackboard.SetValue{T}"/> with an <see cref="Event"/>
	/// and receive a callback subscribed to its property.</para>
	/// </remarks>
	public readonly struct Event : IEquatable<Event>
	{
		public override int GetHashCode()
		{
			return 0;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public bool Equals(Event other)
		{
			return false;
		}

		public override string ToString()
		{
			return "Event";
		}
	}
}
