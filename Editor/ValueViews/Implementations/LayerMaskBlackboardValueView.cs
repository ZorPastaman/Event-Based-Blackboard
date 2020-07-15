// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class LayerMaskBlackboardValueView : BlackboardValueView<LayerMask>
	{
		public override LayerMask DrawValue(string label, LayerMask value)
		{
			return EditorGUILayout.MaskField(label, value, InternalEditorUtility.layers);
		}
	}
}
