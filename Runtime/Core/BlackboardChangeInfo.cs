﻿// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Zor.EventBasedBlackboard.Core
{
	/// <summary>
	/// Blackboard value change info used by subscribers of
	/// <see cref="Zor.EventBasedBlackboard.Core.Blackboard.Subscribe{T}(BlackboardPropertyName, Action{BlackboardChangeInfo{T}})"/>.
	/// </summary>
	/// <typeparam name="T">Type of changed value.</typeparam>
	public readonly struct BlackboardChangeInfo<T> : IEquatable<BlackboardChangeInfo<T>>
	{
		/// <summary>
		/// Current value if <see cref="removed"/> is false or default if <see cref="removed"/> is true.
		/// </summary>
		public readonly T value;
		/// <summary>
		/// True if value was removed and false if it was changed.
		/// </summary>
		public readonly bool removed;

		private BlackboardChangeInfo(T value, bool removed)
		{
			this.value = value;
			this.removed = removed;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static BlackboardChangeInfo<T> CreateRemoved()
		{
			return new BlackboardChangeInfo<T>(default, true);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static BlackboardChangeInfo<T> CreateChanged(T value)
		{
			return new BlackboardChangeInfo<T>(value, false);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		[Pure]
		public override bool Equals(object obj)
		{
			return obj is BlackboardChangeInfo<T> other && Equals(other);
		}

		[Pure]
		public bool Equals(BlackboardChangeInfo<T> other)
		{
			return removed == other.removed && EqualityComparer<T>.Default.Equals(value, other.value);
		}

		[Pure]
		public override string ToString()
		{
			return removed ? "Removed" : value.ToString();
		}
	}
}
