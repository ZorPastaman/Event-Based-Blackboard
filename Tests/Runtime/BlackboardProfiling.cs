// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using UnityEngine;
using Zor.EventBasedBlackboard.Core;

namespace Zor.EventBasedBlackboard.Tests
{
	public sealed class BlackboardProfiling : MonoBehaviour
	{
		private const int ArrayLength = 100;

#pragma warning disable CS0649
		[SerializeField] private bool m_FlushEveryValue;
		[SerializeField] private bool m_FlushInArrayEnd;
		[SerializeField] private bool m_FlushInStepEnd;
		[SerializeField] private bool m_NewPropertyNamePerValue;
#pragma warning restore CS0649

		private Blackboard m_blackboard = new Blackboard();

		private BlackboardPropertyName[] m_propertyNames;

		private int[] m_ints;
		private double[] m_doubles;
		private float[] m_floats;
		private short[] m_shorts;

		private int m_step;

		private void Start()
		{
			m_propertyNames = CreateArray(new BlackboardPropertyName("0"),
				(value, index) => new BlackboardPropertyName(index.ToString()));

			m_ints = CreateArray(-ArrayLength / 2, (value, index) => value + 1);
			m_doubles = CreateArray(-(double)ArrayLength / 2, (value, index) => value + 1.0);
			m_floats = CreateArray(-(float)ArrayLength / 2, (value, index) => value + 1f);
			m_shorts = CreateArray((short)(-ArrayLength / 2), (value, index) => (short)(value + 1));
		}

		private void Update()
		{
			SetValues(m_ints);
			SetValues(m_doubles);
			SetValues(m_floats);
			SetValues(m_shorts);

			if (m_FlushInStepEnd)
			{
				m_blackboard.Flush();
			}

			m_step++;
		}

		private void SetValues<T>(T[] array)
		{
			for (int i = 0; i < ArrayLength; ++i)
			{
				int index = m_NewPropertyNamePerValue ? m_step + i : m_step;
				BlackboardPropertyName propertyName = m_propertyNames[index % ArrayLength];

				m_blackboard.SetValue(propertyName, array[i]);

				if (m_FlushEveryValue)
				{
					m_blackboard.Flush();
				}
			}

			if (m_FlushInArrayEnd)
			{
				m_blackboard.Flush();
			}
		}

		private static T[] CreateArray<T>(T initialValue, Func<T, int, T> getNext)
		{
			var array = new T[ArrayLength];
			array[0] = initialValue;

			for (int i = 1; i < ArrayLength; ++i)
			{
				array[i] = getNext(array[i - 1], i);
			}

			return array;
		}

		private void Reset()
		{
			m_blackboard = new Blackboard();
		}

		[ContextMenu("Log")]
		private void Log()
		{
			UnityEngine.Debug.Log(m_blackboard.ToString());
		}
	}
}
