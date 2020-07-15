// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using JetBrains.Annotations;
using UnityEditor;

namespace Zor.EventBasedBlackboard.BlackboardValueViews
{
	[UsedImplicitly]
	public sealed class UlongBlackboardValueView : BlackboardValueView<ulong>
	{
		public override ulong DrawValue(string label, ulong value)
		{
			long result = EditorGUILayout.LongField(label, (long)value);

			if (result < (long)ulong.MinValue)
			{
				result = (long)ulong.MinValue;
			}

			return (ulong)result;
		}
	}
}
