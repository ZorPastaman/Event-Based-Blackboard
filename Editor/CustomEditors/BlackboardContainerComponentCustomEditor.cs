// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using UnityEditor;
using Zor.EventBasedBlackboard.EditorTools;

namespace Zor.EventBasedBlackboard.Components.Main
{
	[CustomEditor(typeof(BlackboardContainerComponent))]
	public sealed class BlackboardContainerComponentCustomEditor : Editor
	{
		private bool m_constantRepaint;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!EditorApplication.isPlaying)
			{
				return;
			}

			EditorGUILayout.Separator();
			m_constantRepaint = EditorGUILayout.Toggle("Require Constant Repaint", m_constantRepaint);

			var blackboardContainer = (BlackboardContainerComponent)target;
			BlackboardEditor.DrawBlackboard(blackboardContainer.blackboard);
		}

		public override bool RequiresConstantRepaint()
		{
			return m_constantRepaint;
		}
	}
}
