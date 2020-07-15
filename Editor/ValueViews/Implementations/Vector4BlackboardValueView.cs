// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class Vector4BlackboardValueView : BlackboardValueView<Vector4>
	{
		public override Vector4 DrawValue(string label, Vector4 value)
		{
			return EditorGUILayout.Vector4Field(label, value);
		}
	}
}
