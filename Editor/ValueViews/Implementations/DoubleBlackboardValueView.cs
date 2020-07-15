// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class DoubleBlackboardValueView : BlackboardValueView<double>
	{
		public override double DrawValue(string label, double value)
		{
			return EditorGUILayout.DoubleField(label, value);
		}
	}
}
