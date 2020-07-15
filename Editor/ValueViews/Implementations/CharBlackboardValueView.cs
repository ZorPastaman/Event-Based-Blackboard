// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class CharBlackboardValueView : BlackboardValueView<char>
	{
		public override char DrawValue(string label, char value)
		{
			string result = EditorGUILayout.TextField(label, value.ToString());
			return !string.IsNullOrEmpty(result) ? result[0] : default;
		}
	}
}
