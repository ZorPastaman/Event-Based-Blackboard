// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class Matrix4x4BlackboardValueView : BlackboardValueView<Matrix4x4>
	{
		public override Matrix4x4 DrawValue(string label, Matrix4x4 value)
		{
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.PrefixLabel(label);

			EditorGUILayout.BeginVertical();

			for (int i = 0; i < 4; ++i)
			{
				EditorGUILayout.BeginHorizontal();

				Vector4 row = value.GetRow(i);

				for (int j = 0; j < 4; j++)
				{
					row[j] = EditorGUILayout.FloatField(row[j]);
				}

				value.SetRow(i, row);

				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.EndVertical();

			EditorGUILayout.EndHorizontal();

			return value;
		}
	}
}
