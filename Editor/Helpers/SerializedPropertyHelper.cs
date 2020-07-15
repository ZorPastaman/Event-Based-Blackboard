﻿// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;

namespace Zor.EventBasedBlackboard
{
	internal static class SerializedPropertyHelper
	{
		/// <summary>
		/// A little hack to remove a serialized property from a serialized array. The hack is needed because it's
		/// impossible to know how many times it's needed to call
		/// <see cref="SerializedProperty.DeleteArrayElementAtIndex"/> to completely remove a property from the array.
		/// </summary>
		/// <param name="serializedProperty">Array property what the property is deleted from.</param>
		/// <param name="index">At this index the deletion is processed.</param>
		public static void CompletelyRemove([NotNull] SerializedProperty serializedProperty, int index)
		{
			int size = serializedProperty.arraySize;

			do
			{
				serializedProperty.DeleteArrayElementAtIndex(index);
			} while (serializedProperty.arraySize == size);
		}
	}
}

