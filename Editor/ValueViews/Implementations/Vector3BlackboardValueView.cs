// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class Vector3BlackboardValueView : BlackboardValueView<Vector3>
	{
		public override Vector3 DrawValue(string label, Vector3 value)
		{
			return EditorGUILayout.Vector3Field(label, value);
		}
	}
}
