// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zor.EventBasedBlackboard.BlackboardValueViews;
using Zor.EventBasedBlackboard.Core;

namespace Zor.EventBasedBlackboard.EditorTools
{
	/// <summary>
	/// Draws an editor for <see cref="BlackboardTable{T}"/>.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	internal sealed class BlackboardTableEditor<T> : BlackboardTableEditor_Base
	{
		private static readonly List<KeyValuePair<BlackboardPropertyName, T>> s_properties =
			new List<KeyValuePair<BlackboardPropertyName, T>>();

		private readonly BlackboardValueView<T> m_blackboardValueView;

		/// <summary>
		/// Creates a <see cref="BlackboardTableEditor{T}"/> using <paramref name="blackboardValueView"/> for drawing.
		/// </summary>
		/// <param name="blackboardValueView">
		/// This is used for drawing a property in <see cref="BlackboardTable{T}"/>
		/// </param>
		public BlackboardTableEditor(BlackboardValueView<T> blackboardValueView)
		{
			m_blackboardValueView = blackboardValueView;
		}

		/// <inheritdoc/>
		public override Type valueType => typeof(T);

		/// <inheritdoc/>
		public override void Draw(Blackboard blackboard)
		{
			try
			{
				EditorGUILayout.LabelField(valueType.Name, EditorStyles.boldLabel);

				blackboard.GetProperties(s_properties);
				s_properties.Sort((left, right)
					=> string.CompareOrdinal(left.Key.name, right.Key.name));

				for (int i = 0, count = s_properties.Count; i < count; ++i)
				{
					KeyValuePair<BlackboardPropertyName, T> property = s_properties[i];

					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.BeginVertical();

					EditorGUI.BeginChangeCheck();

					BlackboardPropertyName key = property.Key;
					T newValue = m_blackboardValueView.DrawValue(key.name, property.Value);

					if (EditorGUI.EndChangeCheck())
					{
						blackboard.SetValue(key, newValue);
					}

					EditorGUILayout.EndVertical();

					if (GUILayout.Button(s_RemoveButtonIcon, s_RemoveButtonOptions))
					{
						blackboard.Remove<T>(key);
					}

					EditorGUILayout.EndHorizontal();
				}
			}
			finally
			{
				s_properties.Clear();
			}
		}
	}
}
