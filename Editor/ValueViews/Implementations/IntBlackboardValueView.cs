// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class IntBlackboardValueView : BlackboardValueView<int>
	{
		public override int DrawValue(string label, int value)
		{
			return EditorGUILayout.IntField(label, value);
		}
	}
}
