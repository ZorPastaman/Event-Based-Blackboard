// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class StringBlackboardValueView : BlackboardValueView<string>
	{
		public override string DrawValue(string label, string value)
		{
			return EditorGUILayout.TextField(label, value);
		}
	}
}
