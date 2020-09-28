﻿// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.EventBasedBlackboard.Core;
using Zor.EventBasedBlackboard.Debugging;
using Zor.EventBasedBlackboard.Helpers;

namespace Zor.EventBasedBlackboard.Serialization
{
	/// <summary>
	/// <para>Serialized table that has set keys and values.</para>
	/// <para>Inherit this class to get this functionality.</para>
	/// </summary>
	/// <typeparam name="T">Type of the value.</typeparam>
	public abstract class SerializedValueSerializedTable<T> : SerializedValueSerializedTable_Base
	{
#pragma warning disable CS0649
		[SerializeField] private string[] m_Keys;
		[SerializeField] private T[] m_Values;
#pragma warning restore CS0649

		/// <inheritdoc/>
		public sealed override Type valueType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => typeof(T);
		}

		/// <summary>
		/// How many properties are contained in this <see cref="ClassSerializedValueSerializedTable{T}"/>.
		/// </summary>
		public int propertiesCount
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => Mathf.Min(m_Keys.Length, m_Values.Length);
		}

		/// <summary>
		/// Gets a property as a pair of <see cref="string"/> and <typeparamref name="T"/>
		/// at the index <paramref name="index"/>.
		/// </summary>
		/// <param name="index"></param>
		/// <returns>
		/// A property as a pair of <see cref="string"/> and <typeparamref name="T"/>.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
		public (string, T) GetProperty(int index)
		{
			return (m_Keys[index], m_Values[index]);
		}

		/// <summary>
		/// Sets <paramref name="key"/> and <paramref name="value"/>
		/// as a property at the index <paramref name="index"/>.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="index"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetProperty([NotNull] string key, [NotNull] T value, int index)
		{
			m_Keys[index] = key;
			m_Values[index] = value;
		}

		/// <summary>
		/// Sets <paramref name="keys"/> and <paramref name="values"/> as a collection of properties.
		/// </summary>
		/// <param name="keys"></param>
		/// <param name="values"></param>
		/// <remarks>
		/// Ensure that <paramref name="keys"/> and <paramref name="values"/> are of the same length.
		/// </remarks>
		public void SetProperties([NotNull] string[] keys, [NotNull] T[] values)
		{
			m_Keys = keys;
			m_Values = values;
			EnsureEqualLength();
		}

		/// <inheritdoc/>
		public sealed override void Apply(Blackboard blackboard)
		{
			for (int i = 0, count = Mathf.Min(m_Keys.Length, m_Values.Length); i < count; ++i)
			{
				blackboard.SetValue(new BlackboardPropertyName(m_Keys[i]), m_Values[i]);
			}
		}

		/// <inheritdoc/>
		public override void GetKeys(List<(string, Type)> keys)
		{
			int count = m_Keys.Length;
			ListHelper.EnsureCapacity(keys, count);

			for (int i = 0; i < count; ++i)
			{
				keys.Add((m_Keys[i], valueType));
			}
		}

		protected override void OnValidate()
		{
			EnsureEqualLength();
		}

		private void EnsureEqualLength()
		{
			int keysLength = m_Keys.Length;
			int valuesLength = m_Values.Length;

			if (keysLength == valuesLength)
			{
				return;
			}

			BlackboardDebug.LogError($"[ClassSerializedValueSerializedTable] On '{name}' Keys and Values have different lengths. Autofixed");
			int length = Mathf.Min(keysLength, valuesLength);
			Array.Resize(ref m_Keys, length);
			Array.Resize(ref m_Values, length);
		}
	}
}
