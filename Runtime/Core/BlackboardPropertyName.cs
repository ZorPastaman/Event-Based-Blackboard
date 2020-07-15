﻿// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Zor.EventBasedBlackboard.Core
{
	/// <summary>
	/// Wrapper over <see cref="string"/> transforming it into <see cref="int"/> for faster comparisons.
	/// </summary>
	/// <remarks>
	/// It's used in <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/>.
	/// </remarks>
	public readonly struct BlackboardPropertyName : IEquatable<BlackboardPropertyName>
	{
		/// <summary>
		/// Dictionary of all unique strings that were used in <see cref="BlackboardPropertyName(string)"/>
		/// to their ids.
		/// </summary>
		private static readonly Dictionary<string, int> s_nameIds = new Dictionary<string, int>(100);
		/// <summary>
		/// List of all unique strings that were used in <see cref="BlackboardPropertyName(string)"/>.
		/// </summary>
		private static readonly List<string> s_names = new List<string>(100);

		/// <summary>
		/// Unique per string id.
		/// </summary>
		public readonly int id;

		/// <summary>
		/// Creates a <see cref="BlackboardPropertyName"/> with unique <see cref="id"/> per <paramref name="name"/>.
		/// </summary>
		/// <param name="name">For this, unique <see cref="id"/> is set.</param>
		public BlackboardPropertyName([NotNull] string name)
		{
			if (!s_nameIds.TryGetValue(name, out id))
			{
				id = s_names.Count;
				s_nameIds.Add(name, id);
				s_names.Add(name);
			}
		}

		/// <summary>
		/// Creates a <see cref="BlackboardPropertyName"/> with the specified <paramref name="id"></paramref>.
		/// </summary>
		/// <param name="id">Id to set.</param>
		public BlackboardPropertyName(int id)
		{
			this.id = id;
		}

		/// <summary>
		/// Name of the property.
		/// </summary>
		/// <returns>
		/// <para>
		/// Original string name of the property if the <see cref="BlackboardPropertyName"/> was created
		/// with <see cref="BlackboardPropertyName(string)"/>.
		/// </para>
		/// <para>
		/// If the <see cref="BlackboardPropertyName"/> was created with <see cref="BlackboardPropertyName(int)"/>,
		/// this may return <see cref="string.Empty"/> or a name if another <see cref="BlackboardPropertyName"/>
		/// was created with <see cref="BlackboardPropertyName(string)"/> and got the same <see cref="id"/>.
		/// </para>
		/// </returns>
		public string name => id >= 0 && id < s_names.Count ? s_names[id] : string.Empty;

		public override bool Equals(object obj)
		{
			return obj is BlackboardPropertyName other && Equals(other);
		}

		public bool Equals(BlackboardPropertyName other)
		{
			return other.id == id;
		}

		public override int GetHashCode()
		{
			return id;
		}

		public override string ToString()
		{
			return $"{id.ToString()}({name})";
		}
	}
}
