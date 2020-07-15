// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class BoundsIntBlackboardValueView : BlackboardValueView<BoundsInt>
	{
		public override BoundsInt DrawValue(string label, BoundsInt value)
		{
			return EditorGUILayout.BoundsIntField(label, value);
		}
	}
}
