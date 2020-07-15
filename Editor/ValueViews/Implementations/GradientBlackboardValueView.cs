// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class GradientBlackboardValueView : BlackboardValueView<Gradient>
	{
		public override Gradient DrawValue(string label, Gradient value)
		{
			return EditorGUILayout.GradientField(label, value);
		}
	}
}
