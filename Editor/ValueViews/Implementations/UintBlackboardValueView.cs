// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class UintBlackboardValueView : BlackboardValueView<uint>
	{
		public override uint DrawValue(string label, uint value)
		{
			return (uint)Mathf.Clamp(EditorGUILayout.IntField(label, (int)value), uint.MinValue, uint.MaxValue);
		}
	}
}
