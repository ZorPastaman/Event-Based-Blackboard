﻿// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using Zor.EventBasedBlackboard.Core;

namespace Zor.EventBasedBlackboard.BlackboardTableEditors
{
	/// <summary>
	/// Interface for a value wrapper used in <see cref="AddPopup"/>.
	/// </summary>
	internal interface IAddPopupValue
	{
		/// <summary>
		/// Type of the value.
		/// </summary>
		Type valueType { get; }

		/// <summary>
		/// Draws an editor for the value.
		/// </summary>
		/// <param name="label">Property label.</param>
		void DrawValue(string label);

		/// <summary>
		/// Sets the current value into <paramref name="blackboard"/> using <paramref name="key"/> as a property name.
		/// </summary>
		/// <param name="key">Property name.</param>
		/// <param name="blackboard">Sets current value into this.</param>
		void Set(string key, Blackboard blackboard);
	}
}
