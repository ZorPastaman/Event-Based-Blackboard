// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Event = Zor.EventBasedBlackboard.CustomTypes.Event;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class EventBlackboardValueView : BlackboardValueView<CustomTypes.Event>
	{
		public override CustomTypes.Event DrawValue(string label, CustomTypes.Event value)
		{
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.PrefixLabel(label);

			if (GUILayout.Button("Invoke"))
			{
				GUI.changed = true;
			}

			EditorGUILayout.EndHorizontal();

			return new CustomTypes.Event();
		}
	}
}
