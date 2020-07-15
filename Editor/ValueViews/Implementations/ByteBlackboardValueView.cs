// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class ByteBlackboardValueView : BlackboardValueView<byte>
	{
		public override byte DrawValue(string label, byte value)
		{
			return (byte)Mathf.Clamp(EditorGUILayout.IntField(label, value), byte.MinValue, byte.MaxValue);
		}
	}
}
