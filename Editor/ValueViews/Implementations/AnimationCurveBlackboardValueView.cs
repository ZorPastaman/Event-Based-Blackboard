// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class AnimationCurveBlackboardValueView : BlackboardValueView<AnimationCurve>
	{
		public override AnimationCurve DrawValue(string label, AnimationCurve value)
		{
			if (value == null)
			{
				value = new AnimationCurve();
			}

			return EditorGUILayout.CurveField(label, value);
		}
	}
}
