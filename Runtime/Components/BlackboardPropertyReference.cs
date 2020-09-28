// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.EventBasedBlackboard.Components.Main;

namespace Zor.EventBasedBlackboard.Components
{
	/// <summary>
	/// Useful serializable struct for referencing a property in <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/>
	/// in <see cref="Zor.EventBasedBlackboard.Components.Main.BlackboardContainerComponent"/>. It has a custom editor.
	/// </summary>
	/// <remarks>
	/// Usually it's used in <see cref="UnityEngine.Component"/>.
	/// </remarks>
	[Serializable]
	public struct BlackboardPropertyReference : IEquatable<BlackboardPropertyReference>
	{
		[SerializeField, NotNull] private BlackboardContainerComponent m_BlackboardContainer;
		[SerializeField, NotNull] private string m_PropertyName;

		public BlackboardPropertyReference([NotNull] BlackboardContainerComponent blackboardContainer,
			[NotNull] string propertyName)
		{
			m_BlackboardContainer = blackboardContainer;
			m_PropertyName = propertyName;
		}

		[NotNull]
		public BlackboardContainerComponent blackboardContainer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_BlackboardContainer;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_BlackboardContainer = value;
		}

		[NotNull]
		public string propertyName
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_PropertyName;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_PropertyName = value;
		}

		[Pure]
		public bool Equals(BlackboardPropertyReference other)
		{
			return Equals(m_BlackboardContainer, other.m_BlackboardContainer) &&
				string.Equals(m_PropertyName, other.m_PropertyName, StringComparison.InvariantCulture);
		}

		[Pure]
		public override bool Equals(object obj)
		{
			return obj is BlackboardPropertyReference other && Equals(other);
		}

		[Pure]
		public override int GetHashCode()
		{
			unchecked
			{
				return ((m_BlackboardContainer != null ? m_BlackboardContainer.GetHashCode() : 0) * 397) ^
					(m_PropertyName != null ? StringComparer.InvariantCulture.GetHashCode(m_PropertyName) : 0);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static bool operator ==(BlackboardPropertyReference left, BlackboardPropertyReference right)
		{
			return left.Equals(right);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public static bool operator !=(BlackboardPropertyReference left, BlackboardPropertyReference right)
		{
			return !left.Equals(right);
		}
	}
}
