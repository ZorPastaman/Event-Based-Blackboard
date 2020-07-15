// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using Zor.EventBasedBlackboard.BlackboardValueViews;
using Zor.EventBasedBlackboard.Core;

namespace Zor.EventBasedBlackboard.BlackboardTableEditors
{
	/// <summary>
	/// Value wrapper used in <see cref="AddPopup"/>.
	/// </summary>
	/// <typeparam name="T">Type of the value.</typeparam>
	internal sealed class AddPopupValue<T> : IAddPopupValue
	{
		private readonly BlackboardValueView<T> m_valueView;
		private T m_value;

		/// <summary>
		/// Creates a wrapper over <paramref name="valueView"/>
		/// </summary>
		/// <param name="valueView">Value view of <typeparamref name="T"/> used in this wrapper.</param>
		public AddPopupValue(BlackboardValueView<T> valueView)
		{
			m_valueView = valueView;
		}

		/// <inheritdoc/>
		public Type valueType => typeof(T);

		/// <inheritdoc/>
		public void DrawValue(string label)
		{
			m_value = m_valueView.DrawValue(label, m_value);
		}

		/// <inheritdoc/>
		public void Set(string key, Blackboard blackboard)
		{
			blackboard.SetValue(new BlackboardPropertyName(key), m_value);
		}
	}
}
