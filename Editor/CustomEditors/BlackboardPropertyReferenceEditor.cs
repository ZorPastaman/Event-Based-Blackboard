// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using UnityEditor;
using UnityEngine;
using Zor.EventBasedBlackboard.EditorTools;

namespace Zor.EventBasedBlackboard.Components
{
	[CustomPropertyDrawer(typeof(BlackboardPropertyReference))]
	public sealed class BlackboardPropertyReferenceEditor : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return BlackboardPropertyReferenceDrawer.GetPropertyHeight(property, label);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			BlackboardPropertyReferenceDrawer.OnGUI(position, property, label);
		}
	}
}
