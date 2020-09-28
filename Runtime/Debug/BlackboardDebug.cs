// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Zor.EventBasedBlackboard.Debugging
{
	/// <summary>
	/// Class for logging the blackboard system.
	/// </summary>
	/// <remarks>
	/// All log levels except exception are switchable by defines. They are defined as constants in this class.
	/// </remarks>
	public static class BlackboardDebug
	{
		/// <summary>
		/// This define allows to log every change of the blackboard system. It can be too extensive. Use it only if
		/// you have problems in the blackboard system and don't understand an origin of them at all.
		/// </summary>
		public const string BlackboardLogDetailsDefine = "EVENT_BLACKBOARD_LOG_DETAILS";
		public const string BlackboardLogDefine = "EVENT_BLACKBOARD_LOG";
		public const string BlackboardLogWarningDefine = "EVENT_BLACKBOARD_LOG_WARNING";
		public const string BlackboardLogErrorDefine = "EVENT_BLACKBOARD_LOG_ERROR";

		private const string Format = "[EventBasedBlackboard] {0}.";

		[Conditional(BlackboardLogDetailsDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogDetails(string message)
		{
			UnityEngine.Debug.LogFormat(Format, message);
		}

		[Conditional(BlackboardLogDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Log(string message)
		{
			UnityEngine.Debug.LogFormat(Format, message);
		}

		[Conditional(BlackboardLogWarningDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogWarning(string message, UnityEngine.Object context)
		{
			UnityEngine.Debug.LogWarningFormat(context, Format, message);
		}

		[Conditional(BlackboardLogErrorDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogError(string message)
		{
			UnityEngine.Debug.LogErrorFormat(Format, message);
		}

		[Conditional(BlackboardLogErrorDefine), MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogError(string message, UnityEngine.Object context)
		{
			UnityEngine.Debug.LogErrorFormat(context, Format, message);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogException(Exception exception)
		{
			UnityEngine.Debug.LogException(exception);
		}
	}
}
