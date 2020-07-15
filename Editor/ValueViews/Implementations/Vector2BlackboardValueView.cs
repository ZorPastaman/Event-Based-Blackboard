// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class Vector2BlackboardValueView : BlackboardValueView<Vector2>
	{
		public override Vector2 DrawValue(string label, Vector2 value)
		{
			return EditorGUILayout.Vector2Field(label, value);
		}
	}
}
