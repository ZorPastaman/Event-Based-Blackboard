// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Zor.EventBasedBlackboard.Core;
using Zor.EventBasedBlackboard.Debugging;
using Zor.EventBasedBlackboard.Serialization;

namespace Zor.EventBasedBlackboard.Components.Main
{
	/// <summary>
	/// Container of <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/> for using that as
	/// <see cref="UnityEngine.Component"/>.
	/// </summary>
	/// <remarks>
	/// As <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/> requires flushing to update its values, use any
	/// <see cref="Zor.EventBasedBlackboard.Components.Main.Flushers.BlackboardContainerFlusher"/> to call
	/// <see cref="Zor.EventBasedBlackboard.Core.Blackboard.Flush"/> of
	/// <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/> in this component.
	/// </remarks>
	[AddComponentMenu(BlackboardAddComponentConstants.MainFolder + "Blackboard Container")]
	public sealed class BlackboardContainerComponent : MonoBehaviour
	{
#pragma warning disable CS0649
		[SerializeField, Tooltip("Array of serialized properties for Blackboard.\nIt is applied to Blackboard only on Awake.")]
		private SerializedContainer[] m_SerializedContainers;
#pragma warning restore CS0649

		private Blackboard m_blackboard;

		/// <summary>
		/// Contained <see cref="Zor.EventBasedBlackboard.Core.Blackboard"/>.
		/// </summary>
		[NotNull]
		public Blackboard blackboard
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_blackboard;
		}

		/// <summary>
		/// How many serialized containers this <see cref="BlackboardContainerComponent"/> depends on.
		/// </summary>
		public int serializedContainersCount
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
			get => m_SerializedContainers.Length;
		}

		/// <summary>
		/// Gets a <see cref="SerializedContainer"/> at the index <paramref name="index"/>.
		/// </summary>
		/// <param name="index"></param>
		/// <returns><see cref="SerializedContainer"/> at the index <paramref name="index"/>.</returns>
		/// <remarks>
		/// If you change a gotten <see cref="SetSerializedContainer"/>,
		/// you need to call <see cref="RecreateBlackboard"/> to apply changes.
		/// </remarks>
		[MethodImpl(MethodImplOptions.AggressiveInlining), NotNull, Pure]
		public SerializedContainer GetSerializedContainer(int index)
		{
			return m_SerializedContainers[index];
		}

		/// <summary>
		/// Sets the serialized container <paramref name="serializedContainer"/> at the index <paramref name="index"/>
		/// </summary>
		/// <param name="serializedContainer"></param>
		/// <param name="index"></param>
		/// <remarks>
		/// You need to call <see cref="RecreateBlackboard"/> to apply changes.
		/// </remarks>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetSerializedContainer([NotNull] SerializedContainer serializedContainer, int index)
		{
			m_SerializedContainers[index] = serializedContainer;
		}

		/// <summary>
		/// Sets the serialized containers <paramref name="serializedContainers"/>.
		/// </summary>
		/// <param name="serializedContainers"></param>
		/// <remarks>
		/// You need to call <see cref="RecreateBlackboard"/> to apply changes.
		/// </remarks>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetSerializedContainers([NotNull] SerializedContainer[] serializedContainers)
		{
			m_SerializedContainers = serializedContainers;
		}

		/// <summary>
		/// Creates a new <see cref="Blackboard"/> and applies current serialized containers to it.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining), ContextMenu("Recreate Blackboard")]
		public void RecreateBlackboard()
		{
			Awake();
		}

		private void Awake()
		{
			m_blackboard = new Blackboard();

			for (int i = 0, count = m_SerializedContainers.Length; i < count; ++i)
			{
				SerializedContainer container = m_SerializedContainers[i];

				if (container == null)
				{
					BlackboardDebug.LogWarning($"[BlackboardContainerComponent] SerializedContainer at index '{i}' is null", this);
					continue;
				}

				container.Apply(m_blackboard);
			}

			m_blackboard.Flush();
		}

		[ContextMenu("Flush All")]
		private void FlushAll()
		{
			m_blackboard.Flush();
		}

		[ContextMenu("Flush Single Pass")]
		private void FlushSinglePass()
		{
			m_blackboard.Flush(true);
		}

		[ContextMenu("Log")]
		private void Log()
		{
			Debug.Log(m_blackboard.ToString(), this);
		}
	}
}
