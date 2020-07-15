// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class SbyteBlackboardValueView : BlackboardValueView<sbyte>
	{
		public override sbyte DrawValue(string label, sbyte value)
		{
			return (sbyte)Mathf.Clamp(EditorGUILayout.IntField(label, value), sbyte.MinValue, sbyte.MaxValue);
		}
	}
}
