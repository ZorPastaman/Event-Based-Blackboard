// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class BoundsBlackboardValueView : BlackboardValueView<Bounds>
	{
		public override Bounds DrawValue(string label, Bounds value)
		{
			return EditorGUILayout.BoundsField(label, value);
		}
	}
}
