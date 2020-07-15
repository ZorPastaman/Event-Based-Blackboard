// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using Event = Zor.EventBasedBlackboard.CustomTypes.Event;

namespace Zor.EventBasedBlackboard.Serialization
{
	public sealed class EventSerializedTable : GeneratedValueSerializedTable<CustomTypes.Event>
	{
		protected override CustomTypes.Event GetValue()
		{
			return new CustomTypes.Event();
		}
	}
}
