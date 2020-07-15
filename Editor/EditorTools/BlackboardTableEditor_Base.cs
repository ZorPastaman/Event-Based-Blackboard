// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Zor.EventBasedBlackboard.Core;

namespace Zor.EventBasedBlackboard.EditorTools
{
	/// <summary>
	/// Base class for <see cref="BlackboardTableEditor{T}"/>.
	/// </summary>
	internal abstract class BlackboardTableEditor_Base
	{
		protected static readonly GUIContent s_RemoveButtonIcon = EditorGUIUtility.IconContent("TreeEditor.Trash");
		protected static readonly GUILayoutOption[] s_RemoveButtonOptions = { GUILayout.Width(32f) };

		/// <summary>
		/// Value type of the drawn property.
		/// </summary>
		[NotNull]
		public abstract Type valueType { get; }

		/// <summary>
		/// Draws an editor for <see cref="BlackboardTable{T}"/> in <paramref name="blackboard"/>.
		/// </summary>
		/// <param name="blackboard">For its <see cref="BlackboardTable{T}"/> the editor is drawn.</param>
		public abstract void Draw(Blackboard blackboard);
	}
}
