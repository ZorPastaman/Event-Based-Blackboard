// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Zor.EventBasedBlackboard.Debugging;

namespace Zor.EventBasedBlackboard.Serialization
{
	[CustomEditor(typeof(GeneratedValueSerializedTable_Base), true)]
	public sealed class GeneratingValueSerializedTableEditor : Editor
	{
		private const string KeysPropertyName = "m_Keys";

		private SerializedProperty m_keysProperty;
		private ReorderableList m_list;

		private bool m_correctType;

		public override void OnInspectorGUI()
		{
			if (!m_correctType)
			{
				base.OnInspectorGUI();
				return;
			}

			m_list.DoLayoutList();
		}

		private void OnEnable()
		{
			Type targetType = target.GetType();

			if (!IsSubclassOfRightClass(targetType))
			{
				BlackboardDebug.LogError($"{targetType.FullName} is derived from {typeof(GeneratedValueSerializedTable_Base).FullName}. Instead it should be derived from {typeof(GeneratedValueSerializedTable<>).FullName}");
				return;
			}

			m_correctType = true;

			m_keysProperty = serializedObject.FindProperty(KeysPropertyName);

			m_list = new ReorderableList(serializedObject, m_keysProperty, false, true, true, true)
			{
				drawHeaderCallback = OnDrawHeader,
				elementHeightCallback = OnElementHeight,
				drawElementCallback = OnDrawElement,
				onAddCallback = OnAdd,
				onRemoveCallback = OnRemove
			};
		}

		private void OnDrawHeader(Rect rect)
		{
			EditorGUI.LabelField(rect, $"{((SerializedTable_Base)target).valueType.Name} ({m_keysProperty.arraySize})");
		}

		private float OnElementHeight(int index)
		{
			SerializedProperty key = m_keysProperty.GetArrayElementAtIndex(index);

			return EditorGUI.GetPropertyHeight(key, new GUIContent("Key"), true)
				+ EditorGUIUtility.standardVerticalSpacing * 2f;
		}

		private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
		{
			SerializedProperty key = m_keysProperty.GetArrayElementAtIndex(index);

			rect.x += 10f;
			rect.width -= 10f;
			rect.height = EditorGUI.GetPropertyHeight(key, new GUIContent("Key"),true);
			EditorGUI.PropertyField(rect, key, new GUIContent("Key"),true);

			serializedObject.ApplyModifiedProperties();
		}

		private void OnAdd(ReorderableList list)
		{
			m_keysProperty.arraySize++;

			serializedObject.ApplyModifiedProperties();
		}

		private void OnRemove(ReorderableList list)
		{
			int index = list.index;
			SerializedPropertyHelper.CompletelyRemove(m_keysProperty, index);

			serializedObject.ApplyModifiedProperties();
		}

		private static bool IsSubclassOfRightClass(Type toCheck)
		{
			while (toCheck != null && toCheck != typeof(object))
			{
				Type type = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;

				if (typeof(GeneratedValueSerializedTable<>) == type)
				{
					return true;
				}

				toCheck = toCheck.BaseType;
			}

			return false;
		}
	}
}
