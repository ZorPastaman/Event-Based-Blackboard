// Copyright (c) 2019-2020 Vladimir Popov zor1994@gmail.com https://github.com/ZorPastaman/Event-Based-Blackboard

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnityEngine;
using Zor.EventBasedBlackboard.Core;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Zor.EventBasedBlackboard.Tests
{
	public static class BlackboardTests
	{
		[Test]
		public static void SetGenericValueTests()
		{
			var blackboard = new Blackboard();

			SetGenericValueTest(blackboard, randomCurve);
			SetGenericValueTest(blackboard, randomBool);
			SetGenericValueTest(blackboard, randomBounds);
			SetGenericValueTest(blackboard, randomBoundsInt);
			SetGenericValueTest(blackboard, randomByte);
			SetGenericValueTest(blackboard, randomChar);
			SetGenericValueTest(blackboard, randomColor);
			SetGenericValueTest(blackboard, randomComponent);
			SetGenericValueTest(blackboard, randomDouble);
			SetGenericValueTest(blackboard, Random.value);
			SetGenericValueTest(blackboard, randomGameObject);
			SetGenericValueTest(blackboard, randomGradient);
			SetGenericValueTest(blackboard, randomInt);
			SetGenericValueTest(blackboard, randomLayerMask);
			SetGenericValueTest(blackboard, randomLong);
			SetGenericValueTest(blackboard, randomMatrix4x4);
			SetGenericValueTest(blackboard, randomObject);
			SetGenericValueTest(blackboard, randomPropertyName);
			SetGenericValueTest(blackboard, randomQuaternion);
			SetGenericValueTest(blackboard, randomRect);
			SetGenericValueTest(blackboard, randomRectInt);
			SetGenericValueTest(blackboard, randomSbyte);
			SetGenericValueTest(blackboard, randomShort);
			SetGenericValueTest(blackboard, randomString);
			SetGenericValueTest(blackboard, randomTransform);
			SetGenericValueTest(blackboard, randomUint);
			SetGenericValueTest(blackboard, randomUlong);
			SetGenericValueTest(blackboard, randomUshort);
			SetGenericValueTest(blackboard, Random.insideUnitCircle);
			SetGenericValueTest(blackboard, randomVector2Int);
			SetGenericValueTest(blackboard, Random.insideUnitSphere);
			SetGenericValueTest(blackboard, randomVector3Int);
			SetGenericValueTest(blackboard, randomVector4);
		}

		[Test]
		public static void SetObjectValueTests()
		{
			var blackboard = new Blackboard();

			SetObjectValueTest(blackboard, randomCurve);
			SetObjectValueTest(blackboard, randomBool);
			SetObjectValueTest(blackboard, randomBounds);
			SetObjectValueTest(blackboard, randomBoundsInt);
			SetObjectValueTest(blackboard, randomByte);
			SetObjectValueTest(blackboard, randomChar);
			SetObjectValueTest(blackboard, randomColor);
			SetObjectValueTest(blackboard, randomComponent);
			SetObjectValueTest(blackboard, randomDouble);
			SetObjectValueTest(blackboard, Random.value);
			SetObjectValueTest(blackboard, randomGameObject);
			SetObjectValueTest(blackboard, randomGradient);
			SetObjectValueTest(blackboard, randomInt);
			SetObjectValueTest(blackboard, randomLayerMask);
			SetObjectValueTest(blackboard, randomLong);
			SetObjectValueTest(blackboard, randomMatrix4x4);
			SetObjectValueTest(blackboard, randomObject);
			SetObjectValueTest(blackboard, randomPropertyName);
			SetObjectValueTest(blackboard, randomQuaternion);
			SetObjectValueTest(blackboard, randomRect);
			SetObjectValueTest(blackboard, randomRectInt);
			SetObjectValueTest(blackboard, randomSbyte);
			SetObjectValueTest(blackboard, randomShort);
			SetObjectValueTest(blackboard, randomString);
			SetObjectValueTest(blackboard, randomTransform);
			SetObjectValueTest(blackboard, randomUint);
			SetObjectValueTest(blackboard, randomUlong);
			SetObjectValueTest(blackboard, randomUshort);
			SetObjectValueTest(blackboard, Random.insideUnitCircle);
			SetObjectValueTest(blackboard, randomVector2Int);
			SetObjectValueTest(blackboard, Random.insideUnitSphere);
			SetObjectValueTest(blackboard, randomVector3Int);
			SetObjectValueTest(blackboard, randomVector4);
		}

		[Test]
		public static void GetGenericPropertiesTests()
		{
			var blackboard = new Blackboard();

			GetGenericPropertiesTest(blackboard, () => randomCurve);
			GetGenericPropertiesTest(blackboard, () => randomBool);
			GetGenericPropertiesTest(blackboard, () => randomBounds);
			GetGenericPropertiesTest(blackboard, () => randomBoundsInt);
			GetGenericPropertiesTest(blackboard, () => randomByte);
			GetGenericPropertiesTest(blackboard, () => randomChar);
			GetGenericPropertiesTest(blackboard, () => randomColor);
			GetGenericPropertiesTest(blackboard, () => randomComponent);
			GetGenericPropertiesTest(blackboard, () => randomDouble);
			GetGenericPropertiesTest(blackboard, () => Random.value);
			GetGenericPropertiesTest(blackboard, () => randomGameObject);
			GetGenericPropertiesTest(blackboard, () => randomGradient);
			GetGenericPropertiesTest(blackboard, () => randomInt);
			GetGenericPropertiesTest(blackboard, () => randomLayerMask);
			GetGenericPropertiesTest(blackboard, () => randomLong);
			GetGenericPropertiesTest(blackboard, () => randomMatrix4x4);
			GetGenericPropertiesTest(blackboard, () => randomObject);
			GetGenericPropertiesTest(blackboard, () => randomPropertyName);
			GetGenericPropertiesTest(blackboard, () => randomQuaternion);
			GetGenericPropertiesTest(blackboard, () => randomRect);
			GetGenericPropertiesTest(blackboard, () => randomRectInt);
			GetGenericPropertiesTest(blackboard, () => randomSbyte);
			GetGenericPropertiesTest(blackboard, () => randomShort);
			GetGenericPropertiesTest(blackboard, () => randomString);
			GetGenericPropertiesTest(blackboard, () => randomTransform);
			GetGenericPropertiesTest(blackboard, () => randomUint);
			GetGenericPropertiesTest(blackboard, () => randomUlong);
			GetGenericPropertiesTest(blackboard, () => randomUshort);
			GetGenericPropertiesTest(blackboard, () => Random.insideUnitCircle);
			GetGenericPropertiesTest(blackboard, () => randomVector2Int);
			GetGenericPropertiesTest(blackboard, () => Random.insideUnitSphere);
			GetGenericPropertiesTest(blackboard, () => randomVector3Int);
			GetGenericPropertiesTest(blackboard, () => randomVector4);
		}
		
		[Test]
		public static void GetObjectPropertiesTests()
		{
			var blackboard = new Blackboard();

			GetObjectPropertiesTest(blackboard, () => randomCurve);
			GetObjectPropertiesTest(blackboard, () => randomBool);
			GetObjectPropertiesTest(blackboard, () => randomBounds);
			GetObjectPropertiesTest(blackboard, () => randomBoundsInt);
			GetObjectPropertiesTest(blackboard, () => randomByte);
			GetObjectPropertiesTest(blackboard, () => randomChar);
			GetObjectPropertiesTest(blackboard, () => randomColor);
			GetObjectPropertiesTest(blackboard, () => randomComponent);
			GetObjectPropertiesTest(blackboard, () => randomDouble);
			GetObjectPropertiesTest(blackboard, () => Random.value);
			GetObjectPropertiesTest(blackboard, () => randomGameObject);
			GetObjectPropertiesTest(blackboard, () => randomGradient);
			GetObjectPropertiesTest(blackboard, () => randomInt);
			GetObjectPropertiesTest(blackboard, () => randomLayerMask);
			GetObjectPropertiesTest(blackboard, () => randomLong);
			GetObjectPropertiesTest(blackboard, () => randomMatrix4x4);
			GetObjectPropertiesTest(blackboard, () => randomObject);
			GetObjectPropertiesTest(blackboard, () => randomPropertyName);
			GetObjectPropertiesTest(blackboard, () => randomQuaternion);
			GetObjectPropertiesTest(blackboard, () => randomRect);
			GetObjectPropertiesTest(blackboard, () => randomRectInt);
			GetObjectPropertiesTest(blackboard, () => randomSbyte);
			GetObjectPropertiesTest(blackboard, () => randomShort);
			GetObjectPropertiesTest(blackboard, () => randomString);
			GetObjectPropertiesTest(blackboard, () => randomTransform);
			GetObjectPropertiesTest(blackboard, () => randomUint);
			GetObjectPropertiesTest(blackboard, () => randomUlong);
			GetObjectPropertiesTest(blackboard, () => randomUshort);
			GetObjectPropertiesTest(blackboard, () => Random.insideUnitCircle);
			GetObjectPropertiesTest(blackboard, () => randomVector2Int);
			GetObjectPropertiesTest(blackboard, () => Random.insideUnitSphere);
			GetObjectPropertiesTest(blackboard, () => randomVector3Int);
			GetObjectPropertiesTest(blackboard, () => randomVector4);
		}
		
		[Test]
		public static void ContainsGenericValueTests()
		{
			var blackboard = new Blackboard();

			ContainsGenericValueTest(blackboard, randomCurve);
			ContainsGenericValueTest(blackboard, randomBool);
			ContainsGenericValueTest(blackboard, randomBounds);
			ContainsGenericValueTest(blackboard, randomBoundsInt);
			ContainsGenericValueTest(blackboard, randomByte);
			ContainsGenericValueTest(blackboard, randomChar);
			ContainsGenericValueTest(blackboard, randomColor);
			ContainsGenericValueTest(blackboard, randomComponent);
			ContainsGenericValueTest(blackboard, randomDouble);
			ContainsGenericValueTest(blackboard, Random.value);
			ContainsGenericValueTest(blackboard, randomGameObject);
			ContainsGenericValueTest(blackboard, randomGradient);
			ContainsGenericValueTest(blackboard, randomInt);
			ContainsGenericValueTest(blackboard, randomLayerMask);
			ContainsGenericValueTest(blackboard, randomLong);
			ContainsGenericValueTest(blackboard, randomMatrix4x4);
			ContainsGenericValueTest(blackboard, randomObject);
			ContainsGenericValueTest(blackboard, randomPropertyName);
			ContainsGenericValueTest(blackboard, randomQuaternion);
			ContainsGenericValueTest(blackboard, randomRect);
			ContainsGenericValueTest(blackboard, randomRectInt);
			ContainsGenericValueTest(blackboard, randomSbyte);
			ContainsGenericValueTest(blackboard, randomShort);
			ContainsGenericValueTest(blackboard, randomString);
			ContainsGenericValueTest(blackboard, randomTransform);
			ContainsGenericValueTest(blackboard, randomUint);
			ContainsGenericValueTest(blackboard, randomUlong);
			ContainsGenericValueTest(blackboard, randomUshort);
			ContainsGenericValueTest(blackboard, Random.insideUnitCircle);
			ContainsGenericValueTest(blackboard, randomVector2Int);
			ContainsGenericValueTest(blackboard, Random.insideUnitSphere);
			ContainsGenericValueTest(blackboard, randomVector3Int);
			ContainsGenericValueTest(blackboard, randomVector4);
		}

		[Test]
		public static void ContainsObjectValueTests()
		{
			var blackboard = new Blackboard();

			ContainsObjectValueTest(blackboard, randomCurve);
			ContainsObjectValueTest(blackboard, randomBool);
			ContainsObjectValueTest(blackboard, randomBounds);
			ContainsObjectValueTest(blackboard, randomBoundsInt);
			ContainsObjectValueTest(blackboard, randomByte);
			ContainsObjectValueTest(blackboard, randomChar);
			ContainsObjectValueTest(blackboard, randomColor);
			ContainsObjectValueTest(blackboard, randomComponent);
			ContainsObjectValueTest(blackboard, randomDouble);
			ContainsObjectValueTest(blackboard, Random.value);
			ContainsObjectValueTest(blackboard, randomGameObject);
			ContainsObjectValueTest(blackboard, randomGradient);
			ContainsObjectValueTest(blackboard, randomInt);
			ContainsObjectValueTest(blackboard, randomLayerMask);
			ContainsObjectValueTest(blackboard, randomLong);
			ContainsObjectValueTest(blackboard, randomMatrix4x4);
			ContainsObjectValueTest(blackboard, randomObject);
			ContainsObjectValueTest(blackboard, randomPropertyName);
			ContainsObjectValueTest(blackboard, randomQuaternion);
			ContainsObjectValueTest(blackboard, randomRect);
			ContainsObjectValueTest(blackboard, randomRectInt);
			ContainsObjectValueTest(blackboard, randomSbyte);
			ContainsObjectValueTest(blackboard, randomShort);
			ContainsObjectValueTest(blackboard, randomString);
			ContainsObjectValueTest(blackboard, randomTransform);
			ContainsObjectValueTest(blackboard, randomUint);
			ContainsObjectValueTest(blackboard, randomUlong);
			ContainsObjectValueTest(blackboard, randomUshort);
			ContainsObjectValueTest(blackboard, Random.insideUnitCircle);
			ContainsObjectValueTest(blackboard, randomVector2Int);
			ContainsObjectValueTest(blackboard, Random.insideUnitSphere);
			ContainsObjectValueTest(blackboard, randomVector3Int);
			ContainsObjectValueTest(blackboard, randomVector4);
		}
		
		[Test]
		public static void GetGenericCountTests()
		{
			var blackboard = new Blackboard();

			GetGenericCountTest(blackboard, () => randomCurve);
			GetGenericCountTest(blackboard, () => randomBool);
			GetGenericCountTest(blackboard, () => randomBounds);
			GetGenericCountTest(blackboard, () => randomBoundsInt);
			GetGenericCountTest(blackboard, () => randomByte);
			GetGenericCountTest(blackboard, () => randomChar);
			GetGenericCountTest(blackboard, () => randomColor);
			GetGenericCountTest(blackboard, () => randomComponent);
			GetGenericCountTest(blackboard, () => randomDouble);
			GetGenericCountTest(blackboard, () => Random.value);
			GetGenericCountTest(blackboard, () => randomGameObject);
			GetGenericCountTest(blackboard, () => randomGradient);
			GetGenericCountTest(blackboard, () => randomInt);
			GetGenericCountTest(blackboard, () => randomLayerMask);
			GetGenericCountTest(blackboard, () => randomLong);
			GetGenericCountTest(blackboard, () => randomMatrix4x4);
			GetGenericCountTest(blackboard, () => randomObject);
			GetGenericCountTest(blackboard, () => randomPropertyName);
			GetGenericCountTest(blackboard, () => randomQuaternion);
			GetGenericCountTest(blackboard, () => randomRect);
			GetGenericCountTest(blackboard, () => randomRectInt);
			GetGenericCountTest(blackboard, () => randomSbyte);
			GetGenericCountTest(blackboard, () => randomShort);
			GetGenericCountTest(blackboard, () => randomString);
			GetGenericCountTest(blackboard, () => randomTransform);
			GetGenericCountTest(blackboard, () => randomUint);
			GetGenericCountTest(blackboard, () => randomUlong);
			GetGenericCountTest(blackboard, () => randomUshort);
			GetGenericCountTest(blackboard, () => Random.insideUnitCircle);
			GetGenericCountTest(blackboard, () => randomVector2Int);
			GetGenericCountTest(blackboard, () => Random.insideUnitSphere);
			GetGenericCountTest(blackboard, () => randomVector3Int);
			GetGenericCountTest(blackboard, () => randomVector4);
		}
		
		[Test]
		public static void GetObjectCountTests()
		{
			var blackboard = new Blackboard();

			GetObjectCountTest(blackboard, () => randomCurve);
			GetObjectCountTest(blackboard, () => randomBool);
			GetObjectCountTest(blackboard, () => randomBounds);
			GetObjectCountTest(blackboard, () => randomBoundsInt);
			GetObjectCountTest(blackboard, () => randomByte);
			GetObjectCountTest(blackboard, () => randomChar);
			GetObjectCountTest(blackboard, () => randomColor);
			GetObjectCountTest(blackboard, () => randomComponent);
			GetObjectCountTest(blackboard, () => randomDouble);
			GetObjectCountTest(blackboard, () => Random.value);
			GetObjectCountTest(blackboard, () => randomGameObject);
			GetObjectCountTest(blackboard, () => randomGradient);
			GetObjectCountTest(blackboard, () => randomInt);
			GetObjectCountTest(blackboard, () => randomLayerMask);
			GetObjectCountTest(blackboard, () => randomLong);
			GetObjectCountTest(blackboard, () => randomMatrix4x4);
			GetObjectCountTest(blackboard, () => randomObject);
			GetObjectCountTest(blackboard, () => randomPropertyName);
			GetObjectCountTest(blackboard, () => randomQuaternion);
			GetObjectCountTest(blackboard, () => randomRect);
			GetObjectCountTest(blackboard, () => randomRectInt);
			GetObjectCountTest(blackboard, () => randomSbyte);
			GetObjectCountTest(blackboard, () => randomShort);
			GetObjectCountTest(blackboard, () => randomString);
			GetObjectCountTest(blackboard, () => randomTransform);
			GetObjectCountTest(blackboard, () => randomUint);
			GetObjectCountTest(blackboard, () => randomUlong);
			GetObjectCountTest(blackboard, () => randomUshort);
			GetObjectCountTest(blackboard, () => Random.insideUnitCircle);
			GetObjectCountTest(blackboard, () => randomVector2Int);
			GetObjectCountTest(blackboard, () => Random.insideUnitSphere);
			GetObjectCountTest(blackboard, () => randomVector3Int);
			GetObjectCountTest(blackboard, () => randomVector4);
		}
		
		[Test]
		public static void RemoveGenericValueTests()
		{
			var blackboard = new Blackboard();

			RemoveGenericValueTest(blackboard, randomCurve);
			RemoveGenericValueTest(blackboard, randomBool);
			RemoveGenericValueTest(blackboard, randomBounds);
			RemoveGenericValueTest(blackboard, randomBoundsInt);
			RemoveGenericValueTest(blackboard, randomByte);
			RemoveGenericValueTest(blackboard, randomChar);
			RemoveGenericValueTest(blackboard, randomColor);
			RemoveGenericValueTest(blackboard, randomComponent);
			RemoveGenericValueTest(blackboard, randomDouble);
			RemoveGenericValueTest(blackboard, Random.value);
			RemoveGenericValueTest(blackboard, randomGameObject);
			RemoveGenericValueTest(blackboard, randomGradient);
			RemoveGenericValueTest(blackboard, randomInt);
			RemoveGenericValueTest(blackboard, randomLayerMask);
			RemoveGenericValueTest(blackboard, randomLong);
			RemoveGenericValueTest(blackboard, randomMatrix4x4);
			RemoveGenericValueTest(blackboard, randomObject);
			RemoveGenericValueTest(blackboard, randomPropertyName);
			RemoveGenericValueTest(blackboard, randomQuaternion);
			RemoveGenericValueTest(blackboard, randomRect);
			RemoveGenericValueTest(blackboard, randomRectInt);
			RemoveGenericValueTest(blackboard, randomSbyte);
			RemoveGenericValueTest(blackboard, randomShort);
			RemoveGenericValueTest(blackboard, randomString);
			RemoveGenericValueTest(blackboard, randomTransform);
			RemoveGenericValueTest(blackboard, randomUint);
			RemoveGenericValueTest(blackboard, randomUlong);
			RemoveGenericValueTest(blackboard, randomUshort);
			RemoveGenericValueTest(blackboard, Random.insideUnitCircle);
			RemoveGenericValueTest(blackboard, randomVector2Int);
			RemoveGenericValueTest(blackboard, Random.insideUnitSphere);
			RemoveGenericValueTest(blackboard, randomVector3Int);
			RemoveGenericValueTest(blackboard, randomVector4);
		}

		[Test]
		public static void RemoveObjectValueTests()
		{
			var blackboard = new Blackboard();

			RemoveObjectValueTest(blackboard, randomCurve);
			RemoveObjectValueTest(blackboard, randomBool);
			RemoveObjectValueTest(blackboard, randomBounds);
			RemoveObjectValueTest(blackboard, randomBoundsInt);
			RemoveObjectValueTest(blackboard, randomByte);
			RemoveObjectValueTest(blackboard, randomChar);
			RemoveObjectValueTest(blackboard, randomColor);
			RemoveObjectValueTest(blackboard, randomComponent);
			RemoveObjectValueTest(blackboard, randomDouble);
			RemoveObjectValueTest(blackboard, Random.value);
			RemoveObjectValueTest(blackboard, randomGameObject);
			RemoveObjectValueTest(blackboard, randomGradient);
			RemoveObjectValueTest(blackboard, randomInt);
			RemoveObjectValueTest(blackboard, randomLayerMask);
			RemoveObjectValueTest(blackboard, randomLong);
			RemoveObjectValueTest(blackboard, randomMatrix4x4);
			RemoveObjectValueTest(blackboard, randomObject);
			RemoveObjectValueTest(blackboard, randomPropertyName);
			RemoveObjectValueTest(blackboard, randomQuaternion);
			RemoveObjectValueTest(blackboard, randomRect);
			RemoveObjectValueTest(blackboard, randomRectInt);
			RemoveObjectValueTest(blackboard, randomSbyte);
			RemoveObjectValueTest(blackboard, randomShort);
			RemoveObjectValueTest(blackboard, randomString);
			RemoveObjectValueTest(blackboard, randomTransform);
			RemoveObjectValueTest(blackboard, randomUint);
			RemoveObjectValueTest(blackboard, randomUlong);
			RemoveObjectValueTest(blackboard, randomUshort);
			RemoveObjectValueTest(blackboard, Random.insideUnitCircle);
			RemoveObjectValueTest(blackboard, randomVector2Int);
			RemoveObjectValueTest(blackboard, Random.insideUnitSphere);
			RemoveObjectValueTest(blackboard, randomVector3Int);
			RemoveObjectValueTest(blackboard, randomVector4);
		}
		
		[Test]
		public static void ClearGenericValueTests()
		{
			var blackboard = new Blackboard();

			ClearGenericValueTest(blackboard, () => randomCurve);
			ClearGenericValueTest(blackboard, () => randomBool);
			ClearGenericValueTest(blackboard, () => randomBounds);
			ClearGenericValueTest(blackboard, () => randomBoundsInt);
			ClearGenericValueTest(blackboard, () => randomByte);
			ClearGenericValueTest(blackboard, () => randomChar);
			ClearGenericValueTest(blackboard, () => randomColor);
			ClearGenericValueTest(blackboard, () => randomComponent);
			ClearGenericValueTest(blackboard, () => randomDouble);
			ClearGenericValueTest(blackboard, () => Random.value);
			ClearGenericValueTest(blackboard, () => randomGameObject);
			ClearGenericValueTest(blackboard, () => randomGradient);
			ClearGenericValueTest(blackboard, () => randomInt);
			ClearGenericValueTest(blackboard, () => randomLayerMask);
			ClearGenericValueTest(blackboard, () => randomLong);
			ClearGenericValueTest(blackboard, () => randomMatrix4x4);
			ClearGenericValueTest(blackboard, () => randomObject);
			ClearGenericValueTest(blackboard, () => randomPropertyName);
			ClearGenericValueTest(blackboard, () => randomQuaternion);
			ClearGenericValueTest(blackboard, () => randomRect);
			ClearGenericValueTest(blackboard, () => randomRectInt);
			ClearGenericValueTest(blackboard, () => randomSbyte);
			ClearGenericValueTest(blackboard, () => randomShort);
			ClearGenericValueTest(blackboard, () => randomString);
			ClearGenericValueTest(blackboard, () => randomTransform);
			ClearGenericValueTest(blackboard, () => randomUint);
			ClearGenericValueTest(blackboard, () => randomUlong);
			ClearGenericValueTest(blackboard, () => randomUshort);
			ClearGenericValueTest(blackboard, () => Random.insideUnitCircle);
			ClearGenericValueTest(blackboard, () => randomVector2Int);
			ClearGenericValueTest(blackboard, () => Random.insideUnitSphere);
			ClearGenericValueTest(blackboard, () => randomVector3Int);
			ClearGenericValueTest(blackboard, () => randomVector4);
		}
		
		[Test]
		public static void ClearObjectValueTests()
		{
			var blackboard = new Blackboard();

			ClearObjectValueTest(blackboard, () => randomCurve);
			ClearObjectValueTest(blackboard, () => randomBool);
			ClearObjectValueTest(blackboard, () => randomBounds);
			ClearObjectValueTest(blackboard, () => randomBoundsInt);
			ClearObjectValueTest(blackboard, () => randomByte);
			ClearObjectValueTest(blackboard, () => randomChar);
			ClearObjectValueTest(blackboard, () => randomColor);
			ClearObjectValueTest(blackboard, () => randomComponent);
			ClearObjectValueTest(blackboard, () => randomDouble);
			ClearObjectValueTest(blackboard, () => Random.value);
			ClearObjectValueTest(blackboard, () => randomGameObject);
			ClearObjectValueTest(blackboard, () => randomGradient);
			ClearObjectValueTest(blackboard, () => randomInt);
			ClearObjectValueTest(blackboard, () => randomLayerMask);
			ClearObjectValueTest(blackboard, () => randomLong);
			ClearObjectValueTest(blackboard, () => randomMatrix4x4);
			ClearObjectValueTest(blackboard, () => randomObject);
			ClearObjectValueTest(blackboard, () => randomPropertyName);
			ClearObjectValueTest(blackboard, () => randomQuaternion);
			ClearObjectValueTest(blackboard, () => randomRect);
			ClearObjectValueTest(blackboard, () => randomRectInt);
			ClearObjectValueTest(blackboard, () => randomSbyte);
			ClearObjectValueTest(blackboard, () => randomShort);
			ClearObjectValueTest(blackboard, () => randomString);
			ClearObjectValueTest(blackboard, () => randomTransform);
			ClearObjectValueTest(blackboard, () => randomUint);
			ClearObjectValueTest(blackboard, () => randomUlong);
			ClearObjectValueTest(blackboard, () => randomUshort);
			ClearObjectValueTest(blackboard, () => Random.insideUnitCircle);
			ClearObjectValueTest(blackboard, () => randomVector2Int);
			ClearObjectValueTest(blackboard, () => Random.insideUnitSphere);
			ClearObjectValueTest(blackboard, () => randomVector3Int);
			ClearObjectValueTest(blackboard, () => randomVector4);
		}

		[Test]
		public static void ClearAllTest()
		{
			ClearAllTest(new Blackboard(), () => randomCurve,
				() => randomBool,
				() => randomBounds,
				() => randomBoundsInt,
				() => randomByte,
				() => randomChar,
				() => randomColor,
				() => randomComponent,
				() => randomDouble,
				() => Random.value,
				() => randomGameObject,
				() => randomGradient,
				() => randomInt,
				() => randomLayerMask,
				() => randomLong,
				() => randomMatrix4x4,
				() => randomObject,
				() => randomPropertyName,
				() => randomQuaternion,
				() => randomRect,
				() => randomRectInt,
				() => randomSbyte,
				() => randomShort,
				() => randomString,
				() => randomTransform,
				() => randomUint,
				() => randomUlong,
				() => randomUshort,
				() => Random.insideUnitCircle,
				() => randomVector2Int,
				() => Random.insideUnitSphere,
				() => randomVector3Int,
				() => randomVector4);
		}
		
		[Test]
		public static void OnChangedGenericValueTests()
		{
			var blackboard = new Blackboard();

			OnChangedGenericValueTest(blackboard, AnimationCurve.Constant(0f, 0f, 0f), AnimationCurve.Constant(1f, 1f, 1f));
			OnChangedGenericValueTest(blackboard, false, true);
			OnChangedGenericValueTest(blackboard, new Bounds(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f)), new Bounds(new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f)));
			OnChangedGenericValueTest(blackboard, new BoundsInt(0, 0, 0, 0, 0, 0), new BoundsInt(1, 1, 1, 1, 1, 1));
			OnChangedGenericValueTest(blackboard, (byte)0, (byte)100);
			OnChangedGenericValueTest(blackboard, 'A', 'y');
			OnChangedGenericValueTest(blackboard, Color.blue, Color.red);
			OnChangedGenericValueTest(blackboard, (Component) new GameObject("Test").transform, (Component) new GameObject("Test").transform);
			OnChangedGenericValueTest(blackboard, 1.0, 5.0);
			OnChangedGenericValueTest(blackboard, 1f, 5f);
			OnChangedGenericValueTest(blackboard, new GameObject("Test"), new GameObject("Test"));
			OnChangedGenericValueTest(blackboard, 0, 5);
			OnChangedGenericValueTest(blackboard, (LayerMask)1, (LayerMask)15);
			OnChangedGenericValueTest(blackboard, 0L, 5L);
			OnChangedGenericValueTest(blackboard, (Object) new GameObject("Test"), (Object) new GameObject("Test"));
			OnChangedGenericValueTest(blackboard, new PropertyName(65), new PropertyName(12));
			OnChangedGenericValueTest(blackboard, new Quaternion(0f, 0f, 0f, 0f), new Quaternion(100f, 100f, 100f, 100f));
			OnChangedGenericValueTest(blackboard, new Rect(0f, 0f, 0f, 0f), new Rect(1f, 1f, 1f, 1f));
			OnChangedGenericValueTest(blackboard, new RectInt(0, 0, 0, 0), new RectInt(1, 1, 1, 1));
			OnChangedGenericValueTest(blackboard, (sbyte)0, (sbyte)100);
			OnChangedGenericValueTest(blackboard, (short)0, (short)100);
			OnChangedGenericValueTest(blackboard, "Wow", "No");
			OnChangedGenericValueTest(blackboard, new GameObject("Test").transform, new GameObject("Test").transform);
			OnChangedGenericValueTest(blackboard, 1u, 6u);
			OnChangedGenericValueTest(blackboard, 1UL, 6UL);
			OnChangedGenericValueTest(blackboard, (ushort)1, (ushort)55);
			OnChangedGenericValueTest(blackboard, new Vector2(0f, 0f), new Vector2(1f, 1f));
			OnChangedGenericValueTest(blackboard, new Vector2Int(0, 0), new Vector2Int(1, 1));
			OnChangedGenericValueTest(blackboard, new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f));
			OnChangedGenericValueTest(blackboard, new Vector3Int(0, 0, 0), new Vector3Int(1, 1, 1));
			OnChangedGenericValueTest(blackboard, new Vector4(0f, 0f, 0f, 0f), new Vector4(1f, 1f, 1f, 1f));
		}
		
		[Test]
		public static void OnChangedObjectValueTests()
		{
			var blackboard = new Blackboard();

			OnChangedObjectValueTest(blackboard, AnimationCurve.Constant(0f, 0f, 0f), AnimationCurve.Constant(1f, 1f, 1f));
			OnChangedObjectValueTest(blackboard, false, true);
			OnChangedObjectValueTest(blackboard, new Bounds(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f)), new Bounds(new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f)));
			OnChangedObjectValueTest(blackboard, new BoundsInt(0, 0, 0, 0, 0, 0), new BoundsInt(1, 1, 1, 1, 1, 1));
			OnChangedObjectValueTest(blackboard, (byte)0, (byte)100);
			OnChangedObjectValueTest(blackboard, 'A', 'y');
			OnChangedObjectValueTest(blackboard, Color.blue, Color.red);
			OnChangedObjectValueTest(blackboard, (Component) new GameObject("Test").transform, (Component) new GameObject("Test").transform);
			OnChangedObjectValueTest(blackboard, 1.0, 5.0);
			OnChangedObjectValueTest(blackboard, 1f, 5f);
			OnChangedObjectValueTest(blackboard, new GameObject("Test"), new GameObject("Test"));
			OnChangedObjectValueTest(blackboard, 0, 5);
			OnChangedObjectValueTest(blackboard, (LayerMask)1, (LayerMask)15);
			OnChangedObjectValueTest(blackboard, 0L, 5L);
			OnChangedObjectValueTest(blackboard, (Object) new GameObject("Test"), (Object) new GameObject("Test"));
			OnChangedObjectValueTest(blackboard, new PropertyName(65), new PropertyName(12));
			OnChangedObjectValueTest(blackboard, new Quaternion(0f, 0f, 0f, 0f), new Quaternion(100f, 100f, 100f, 100f));
			OnChangedObjectValueTest(blackboard, new Rect(0f, 0f, 0f, 0f), new Rect(1f, 1f, 1f, 1f));
			OnChangedObjectValueTest(blackboard, new RectInt(0, 0, 0, 0), new RectInt(1, 1, 1, 1));
			OnChangedObjectValueTest(blackboard, (sbyte)0, (sbyte)100);
			OnChangedObjectValueTest(blackboard, (short)0, (short)100);
			OnChangedObjectValueTest(blackboard, "Wow", "No");
			OnChangedObjectValueTest(blackboard, new GameObject("Test").transform, new GameObject("Test").transform);
			OnChangedObjectValueTest(blackboard, 1u, 6u);
			OnChangedObjectValueTest(blackboard, 1UL, 6UL);
			OnChangedObjectValueTest(blackboard, (ushort)1, (ushort)55);
			OnChangedObjectValueTest(blackboard, new Vector2(0f, 0f), new Vector2(1f, 1f));
			OnChangedObjectValueTest(blackboard, new Vector2Int(0, 0), new Vector2Int(1, 1));
			OnChangedObjectValueTest(blackboard, new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f));
			OnChangedObjectValueTest(blackboard, new Vector3Int(0, 0, 0), new Vector3Int(1, 1, 1));
			OnChangedObjectValueTest(blackboard, new Vector4(0f, 0f, 0f, 0f), new Vector4(1f, 1f, 1f, 1f));
		}

		[Test]
		public static void OnTypedChangedGenericValueTests()
		{
			var blackboard = new Blackboard();

			OnTypedChangedGenericValueTest(blackboard, AnimationCurve.Constant(0f, 0f, 0f), AnimationCurve.Constant(1f, 1f, 1f));
			OnTypedChangedGenericValueTest(blackboard, false, true);
			OnTypedChangedGenericValueTest(blackboard, new Bounds(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f)), new Bounds(new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f)));
			OnTypedChangedGenericValueTest(blackboard, new BoundsInt(0, 0, 0, 0, 0, 0), new BoundsInt(1, 1, 1, 1, 1, 1));
			OnTypedChangedGenericValueTest(blackboard, (byte)0, (byte)100);
			OnTypedChangedGenericValueTest(blackboard, 'A', 'y');
			OnTypedChangedGenericValueTest(blackboard, Color.blue, Color.red);
			OnTypedChangedGenericValueTest(blackboard, (Component) new GameObject("Test").transform, (Component) new GameObject("Test").transform);
			OnTypedChangedGenericValueTest(blackboard, 1.0, 5.0);
			OnTypedChangedGenericValueTest(blackboard, 1f, 5f);
			OnTypedChangedGenericValueTest(blackboard, new GameObject("Test"), new GameObject("Test"));
			OnTypedChangedGenericValueTest(blackboard, 0, 5);
			OnTypedChangedGenericValueTest(blackboard, (LayerMask)1, (LayerMask)15);
			OnTypedChangedGenericValueTest(blackboard, 0L, 5L);
			OnTypedChangedGenericValueTest(blackboard, (Object) new GameObject("Test"), (Object) new GameObject("Test"));
			OnTypedChangedGenericValueTest(blackboard, new PropertyName(65), new PropertyName(12));
			OnTypedChangedGenericValueTest(blackboard, new Quaternion(0f, 0f, 0f, 0f), new Quaternion(100f, 100f, 100f, 100f));
			OnTypedChangedGenericValueTest(blackboard, new Rect(0f, 0f, 0f, 0f), new Rect(1f, 1f, 1f, 1f));
			OnTypedChangedGenericValueTest(blackboard, new RectInt(0, 0, 0, 0), new RectInt(1, 1, 1, 1));
			OnTypedChangedGenericValueTest(blackboard, (sbyte)0, (sbyte)100);
			OnTypedChangedGenericValueTest(blackboard, (short)0, (short)100);
			OnTypedChangedGenericValueTest(blackboard, "Wow", "No");
			OnTypedChangedGenericValueTest(blackboard, new GameObject("Test").transform, new GameObject("Test").transform);
			OnTypedChangedGenericValueTest(blackboard, 1u, 6u);
			OnTypedChangedGenericValueTest(blackboard, 1UL, 6UL);
			OnTypedChangedGenericValueTest(blackboard, (ushort)1, (ushort)55);
			OnTypedChangedGenericValueTest(blackboard, new Vector2(0f, 0f), new Vector2(1f, 1f));
			OnTypedChangedGenericValueTest(blackboard, new Vector2Int(0, 0), new Vector2Int(1, 1));
			OnTypedChangedGenericValueTest(blackboard, new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f));
			OnTypedChangedGenericValueTest(blackboard, new Vector3Int(0, 0, 0), new Vector3Int(1, 1, 1));
			OnTypedChangedGenericValueTest(blackboard, new Vector4(0f, 0f, 0f, 0f), new Vector4(1f, 1f, 1f, 1f));
		}

		private static void SetGenericValueTest<T>(Blackboard blackboard, T value)
		{
			var propertyName = new BlackboardPropertyName(typeof(T).FullName);
			UnityEngine.Debug.Log($"Blackboard set generic value test. PropertyName: {propertyName}. Value: {value}");
			blackboard.SetValue(propertyName, value);
			Assert.IsFalse(blackboard.Contains<T>(propertyName),
				$"Blackboard has value before flush. PropertyName: {propertyName}");
			blackboard.Flush();
			Assert.IsTrue(blackboard.TryGetValue(propertyName, out T blackboardValue)
				&& EqualityComparer<T>.Default.Equals(blackboardValue, value),
				$"Blackboard doesn't have the same value that was set. PropertyName: {propertyName}. Value: {value}. BlackboardValue: {blackboardValue}");
		}

		private static void SetObjectValueTest<T>(Blackboard blackboard, T value)
		{
			SetObjectValueTest(blackboard, typeof(T), value);
		}
		
		private static void SetObjectValueTest(Blackboard blackboard, Type valueType, object value)
		{
			var propertyName = new BlackboardPropertyName(valueType.FullName);
			UnityEngine.Debug.Log($"Blackboard set object value test. PropertyName: {propertyName}. Value: {value}");
			blackboard.SetValue(propertyName, value);
			Assert.IsFalse(blackboard.Contains(valueType, propertyName),
				$"Blackboard has value before flush. PropertyName: {propertyName}");
			blackboard.Flush();
			Assert.IsTrue(blackboard.TryGetValue(propertyName, out object blackboardValue)
				&& blackboardValue.Equals(value),
				$"Blackboard doesn't have the same value that was set. PropertyName: {propertyName}. Value: {value}. BlackboardValue: {blackboardValue}");
		}

		private static void GetGenericPropertiesTest<T>(Blackboard blackboard, Func<T> getValue)
		{
			int length = Random.Range(2, 100);
			var values = new T[length];

			for (int i = 0; i < length; ++i)
			{
				values[i] = getValue();
			}
			
			GetGenericPropertiesTest(blackboard, values);
		}

		private static void GetGenericPropertiesTest<T>(Blackboard blackboard, T[] values)
		{
			int length = values.Length;
			var propertyNames = new BlackboardPropertyName[length];

			for (int i = 0; i < length; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName($"{typeof(T).FullName} {i}");
			}

			var log = new StringBuilder("Blackboard get generic properties test.\n");

			for (int i = 0; i < length; ++i)
			{
				log.Append($"\t{{{propertyNames[i]}, {values[i]}}}\n");
			}

			UnityEngine.Debug.Log(log);

			for (int i = 0; i < length; ++i)
			{
				blackboard.SetValue(propertyNames[i], values[i]);
			}

			var properties = new List<KeyValuePair<BlackboardPropertyName, T>>();
			blackboard.GetProperties(properties);
			Assert.IsTrue(properties.Count == 0, "Blackboard has properties before flush");
			blackboard.Flush();
			blackboard.GetProperties(properties);
			Assert.AreEqual(length, properties.Count, "Blackboard has wrong number of properties");

			for (int i = 0; i < length; ++i)
			{
				KeyValuePair<BlackboardPropertyName, T> property = properties[i];
				BlackboardPropertyName propertyName = property.Key;
				T value = property.Value;
				int index = Array.IndexOf(propertyNames, propertyName);
				Assert.IsTrue(index >= 0, $"Property named {propertyName} wasn't set but found");
				T expected = values[index];
				Assert.AreEqual(expected, value, $"Got value {value} but expected {expected}");
			}
		}
		
		private static void GetObjectPropertiesTest<T>(Blackboard blackboard, Func<T> getValue)
		{
			int length = Random.Range(2, 100);
			var values = new object[length];

			for (int i = 0; i < length; ++i)
			{
				values[i] = getValue();
			}
			
			GetObjectPropertiesTest(blackboard, typeof(T), values);
		}
		
		private static void GetObjectPropertiesTest(Blackboard blackboard, Type valueType, object[] values)
		{
			int length = values.Length;
			var propertyNames = new BlackboardPropertyName[length];

			for (int i = 0; i < length; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName($"{valueType.FullName} {i}");
			}

			var log = new StringBuilder("Blackboard get object properties test.\n");

			for (int i = 0; i < length; ++i)
			{
				log.Append($"\t{{{propertyNames[i]}, {values[i]}}}\n");
			}

			UnityEngine.Debug.Log(log);

			for (int i = 0; i < length; ++i)
			{
				blackboard.SetValue(valueType, propertyNames[i], values[i]);
			}

			var properties = new List<KeyValuePair<BlackboardPropertyName, object>>();
			blackboard.GetProperties(valueType, properties);
			Assert.AreEqual(0, properties.Count, "Blackboard has properties before flush");
			blackboard.Flush();
			blackboard.GetProperties(valueType, properties);
			Assert.AreEqual(length, properties.Count, "Blackboard has wrong number of properties");

			for (int i = 0; i < length; ++i)
			{
				KeyValuePair<BlackboardPropertyName, object> property = properties[i];
				BlackboardPropertyName propertyName = property.Key;
				object value = property.Value;
				int index = Array.IndexOf(propertyNames, propertyName);
				Assert.IsTrue(index >= 0, $"Property named {propertyName} wasn't set but found");
				object expected = values[index];
				Assert.AreEqual(expected, value, $"Got value {value} but expected {expected}");
			}
		}

		private static void ContainsGenericValueTest<T>(Blackboard blackboard, T value)
		{
			var propertyName = new BlackboardPropertyName(typeof(T).FullName);
			UnityEngine.Debug.Log($"Blackboard contains generic value test. PropertyName: {propertyName}. Value: {value}");
			blackboard.SetValue(propertyName, value);
			Assert.IsFalse(blackboard.Contains<T>(propertyName),
				$"Blackboard has value before flush. PropertyName: {propertyName}");
			blackboard.Flush();
			Assert.IsTrue(blackboard.Contains<T>(propertyName),
				$"Blackboard doesn't have value after flush. PropertyName: {propertyName}");
		}

		private static void ContainsObjectValueTest<T>(Blackboard blackboard, T value)
		{
			ContainsObjectValueTest(blackboard, typeof(T), value);
		}

		private static void ContainsObjectValueTest(Blackboard blackboard, Type valueType, object value)
		{
			var propertyName = new BlackboardPropertyName(valueType.FullName);
			UnityEngine.Debug.Log($"Blackboard contains object value test. PropertyName: {propertyName}. Value: {value}");
			blackboard.SetValue(valueType, propertyName, value);
			Assert.IsFalse(blackboard.Contains(valueType, propertyName),
				$"Blackboard has value before flush. PropertyName: {propertyName}");
			blackboard.Flush();
			Assert.IsTrue(blackboard.Contains(valueType, propertyName),
				$"Blackboard doesn't have value after flush. PropertyName: {propertyName}");
		}

		private static void GetGenericCountTest<T>(Blackboard blackboard, Func<T> getValue)
		{
			int length = Random.Range(2, 100);
			var values = new T[length];

			for (int i = 0; i < length; ++i)
			{
				values[i] = getValue();
			}

			GetGenericCountTest(blackboard, values);
		}

		private static void GetGenericCountTest<T>(Blackboard blackboard, T[] values)
		{
			int length = values.Length;
			var propertyNames = new BlackboardPropertyName[length];

			for (int i = 0; i < length; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName($"{typeof(T).FullName} {i}");
			}

			var log = new StringBuilder("Blackboard get generic count test.\n");

			for (int i = 0; i < length; ++i)
			{
				log.Append($"\t{{{propertyNames[i]}, {values[i]}}}\n");
			}

			UnityEngine.Debug.Log(log);

			Assert.AreEqual(-1, blackboard.GetCount<T>(), "Blackboard doesn't have count -1 before set value");

			for (int i = 0; i < length; ++i)
			{
				blackboard.SetValue(propertyNames[i], values[i]);
			}

			Assert.AreEqual(0, blackboard.GetCount<T>(), "Blackboard doesn't have count 0 before flush");
			blackboard.Flush();
			Assert.AreEqual(length, blackboard.GetCount<T>(), "Blackboard has wrong number of values");
		}

		private static void GetObjectCountTest<T>(Blackboard blackboard, Func<T> getValue)
		{
			int length = Random.Range(2, 100);
			var values = new object[length];

			for (int i = 0; i < length; ++i)
			{
				values[i] = getValue();
			}

			GetObjectCountTest(blackboard, typeof(T), values);
		}

		private static void GetObjectCountTest(Blackboard blackboard, Type valueType, object[] values)
		{
			int length = values.Length;
			var propertyNames = new BlackboardPropertyName[length];

			for (int i = 0; i < length; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName($"{valueType.FullName} {i}");
			}

			var log = new StringBuilder("Blackboard get object count test.\n");

			for (int i = 0; i < length; ++i)
			{
				log.Append($"\t{{{propertyNames[i]}, {values[i]}}}\n");
			}

			Assert.AreEqual(-1, blackboard.GetCount(valueType), "Blackboard doesn't have count -1 before set value");

			for (int i = 0; i < length; ++i)
			{
				blackboard.SetValue(valueType, propertyNames[i], values[i]);
			}

			Assert.AreEqual(0, blackboard.GetCount(valueType), "Blackboard doesn't have count 0 before flush");
			blackboard.Flush();
			Assert.AreEqual(length, blackboard.GetCount(valueType), "Blackboard has wrong number of values");
		}

		private static void RemoveGenericValueTest<T>(Blackboard blackboard, T value)
		{
			var propertyName = new BlackboardPropertyName(typeof(T).FullName);
			UnityEngine.Debug.Log($"Blackboard remove generic value test. PropertyName: {propertyName}. Value: {value}");
			blackboard.SetValue(propertyName, value);
			Assert.IsFalse(blackboard.Contains<T>(propertyName), $"Blackboard has value before flush. PropertyName: {propertyName}");
			blackboard.Flush();
			Assert.IsTrue(blackboard.Contains<T>(propertyName), $"Blackboard doesn't have value after flush. PropertyName: {propertyName}");
			blackboard.Remove<T>(propertyName);
			Assert.IsTrue(blackboard.Contains<T>(propertyName), $"Blackboard doesn't have value after remove before flush. PropertyName: {propertyName}");
			blackboard.Flush();
			Assert.IsFalse(blackboard.Contains<T>(propertyName), $"Blackboard has value after remove and flush. PropertyName: {propertyName}");
		}

		private static void RemoveObjectValueTest<T>(Blackboard blackboard, T value)
		{
			RemoveObjectValueTest(blackboard, typeof(T), value);
		}

		private static void RemoveObjectValueTest(Blackboard blackboard, Type valueType, object value)
		{
			var propertyName = new BlackboardPropertyName(valueType.FullName);
			UnityEngine.Debug.Log($"Blackboard remove generic value test. PropertyName: {propertyName}. Value: {value}");
			blackboard.SetValue(valueType, propertyName, value);
			Assert.IsFalse(blackboard.Contains(valueType, propertyName), $"Blackboard has value before flush. PropertyName: {propertyName}");
			blackboard.Flush();
			Assert.IsTrue(blackboard.Contains(valueType, propertyName), $"Blackboard doesn't have value after flush. PropertyName: {propertyName}");
			blackboard.Remove(valueType, propertyName);
			Assert.IsTrue(blackboard.Contains(valueType, propertyName), $"Blackboard doesn't have value after remove before flush. PropertyName: {propertyName}");
			blackboard.Flush();
			Assert.IsFalse(blackboard.Contains(valueType, propertyName), $"Blackboard has value after remove and flush. PropertyName: {propertyName}");
		}

		private static void ClearGenericValueTest<T>(Blackboard blackboard, Func<T> getValue)
		{
			int length = Random.Range(2, 100);
			var values = new T[length];

			for (int i = 0; i < length; ++i)
			{
				values[i] = getValue();
			}

			ClearGenericValueTest(blackboard, values);
		}

		private static void ClearGenericValueTest<T>(Blackboard blackboard, T[] values)
		{
			int length = values.Length;
			var propertyNames = new BlackboardPropertyName[length];

			for (int i = 0; i < length; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName($"{typeof(T).FullName} {i}");
			}

			var log = new StringBuilder("Blackboard clear generic value test.\n");

			for (int i = 0; i < length; ++i)
			{
				log.Append($"\t{{{propertyNames[i]}, {values[i]}}}\n");
			}

			UnityEngine.Debug.Log(log);

			Assert.IsFalse(blackboard.Contains<T>(), "Blackboard has table before set value");

			for (int i = 0; i < length; ++i)
			{
				blackboard.SetValue(propertyNames[i], values[i]);
			}

			Assert.IsTrue(blackboard.Contains<T>(), "Blackboard doesn't have table after set value");
			blackboard.Flush();
			Assert.AreEqual(length, blackboard.GetCount<T>(), "Blackboard has wrong number of values after flush");
			blackboard.Clear<T>();
			Assert.AreEqual(length, blackboard.GetCount<T>(), "Blackboard has wrong number of values after clear");
			blackboard.Flush();
			Assert.AreEqual(0, blackboard.GetCount<T>(), "Blackboard has wrong number of values after final flush");
		}
		
		private static void ClearObjectValueTest<T>(Blackboard blackboard, Func<T> getValue)
		{
			int length = Random.Range(2, 100);
			var values = new object[length];

			for (int i = 0; i < length; ++i)
			{
				values[i] = getValue();
			}

			ClearObjectValueTest(blackboard, typeof(T), values);
		}

		private static void ClearObjectValueTest(Blackboard blackboard, Type valueType, object[] values)
		{
			int length = values.Length;
			var propertyNames = new BlackboardPropertyName[length];

			for (int i = 0; i < length; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName($"{valueType.FullName} {i}");
			}

			var log = new StringBuilder("Blackboard clear object value test.\n");

			for (int i = 0; i < length; ++i)
			{
				log.Append($"\t{{{propertyNames[i]}, {values[i]}}}\n");
			}

			UnityEngine.Debug.Log(log);

			Assert.IsFalse(blackboard.Contains(valueType), "Blackboard has table before set value");

			for (int i = 0; i < length; ++i)
			{
				blackboard.SetValue(valueType, propertyNames[i], values[i]);
			}

			Assert.IsTrue(blackboard.Contains(valueType), "Blackboard doesn't have table after set value");
			blackboard.Flush();
			Assert.AreEqual(length, blackboard.GetCount(valueType), "Blackboard has wrong number of values after flush");
			blackboard.Clear(valueType);
			Assert.AreEqual(length, blackboard.GetCount(valueType), "Blackboard has wrong number of values after clear");
			blackboard.Flush();
			Assert.AreEqual(0, blackboard.GetCount(valueType), "Blackboard has wrong number of values after final flush");
		}

		private static void ClearAllTest(Blackboard blackboard, params Func<object>[] getValues)
		{
			int length = getValues.Length;
			var values = new object[length];

			for (int i = 0; i < length; ++i)
			{
				values[i] = getValues[i]();
			}

			ClearAllTest(blackboard, values);
		}

		private static void ClearAllTest(Blackboard blackboard, object[] values)
		{
			int length = values.Length;
			var propertyNames = new BlackboardPropertyName[length];

			for (int i = 0; i < length; ++i)
			{
				propertyNames[i] = new BlackboardPropertyName($"{values[i].GetType()} {i}");
			}

			var log = new StringBuilder("Blackboard clear all values test.\n");

			for (int i = 0; i < length; ++i)
			{
				log.Append($"\t{{{propertyNames[i]}, {values[i]}}}\n");
			}

			UnityEngine.Debug.Log(log);

			for (int i = 0; i < length; ++i)
			{
				object value = values[i];
				blackboard.SetValue(value.GetType(), propertyNames[i], value);
			}

			Assert.AreEqual(0, blackboard.propertiesCount, "Blackboard has values before flush");
			blackboard.Flush();
			Assert.AreEqual(length, blackboard.propertiesCount, "Blackboard has wrong number of properties after flush");
			blackboard.Clear();
			Assert.AreEqual(length, blackboard.propertiesCount, "Blackboard has wrong number of properties after clear");
			blackboard.Flush();
			Assert.AreEqual(0, blackboard.propertiesCount, "Blackboard has wrong number of properties after final flush");
		}

		private static void OnChangedGenericValueTest<T>(Blackboard blackboard, T firstValue, T secondValue)
		{
			var propertyName = new BlackboardPropertyName(typeof(T).FullName);
			UnityEngine.Debug.Log($"On generic value changed test. PropertyName: {propertyName}. FirstValue: {firstValue}. SecondValue: {secondValue}");

			bool changed = false;
			Action onChanged = () => { changed = true; };

			blackboard.Subscribe<T>(propertyName, onChanged);
			blackboard.SetValue(propertyName, firstValue);
			Assert.AreEqual(false, changed, "Blackboard sent first onChanged before flush");
			blackboard.Flush();
			Assert.AreEqual(true, changed, "Blackboard didn't send first onChanged after flush");
			changed = false;
			blackboard.SetValue(propertyName, secondValue);
			Assert.AreEqual(false, changed, "Blackboard sent second onChanged before flush");
			blackboard.Flush();
			Assert.AreEqual(true, changed, "Blackboard didn't send second onChanged after flush");
			changed = false;
			blackboard.SetValue(propertyName, firstValue);
			blackboard.SetValue(propertyName, secondValue);
			blackboard.Flush();
			Assert.AreEqual(false, changed, "Blackboard sent changed after set to previous value");
			blackboard.Remove<T>(propertyName);
			Assert.AreEqual(false, changed, "Blackboard sent onChanged after remove before flush");
			blackboard.Flush();
			Assert.AreEqual(true, changed, "Blackboard didn't send onChanged after remove and flush");
			changed = false;
			blackboard.SetValue(propertyName, firstValue);
			blackboard.Remove<T>(propertyName);
			blackboard.Flush();
			Assert.AreEqual(false, changed, "Blackboard sent changed after set and remove");
			blackboard.Unsubscribe<T>(propertyName, onChanged);
			blackboard.SetValue(propertyName, firstValue);
			blackboard.Flush();
			Assert.AreEqual(false, changed, "Blackboard sent onChanged after unsubscribe");
		}

		private static void OnChangedObjectValueTest<T>(Blackboard blackboard, T firstValue, T secondValue)
		{
			OnChangedObjectValueTest(blackboard, typeof(T), firstValue, secondValue);
		}

		private static void OnChangedObjectValueTest(Blackboard blackboard, Type valueType, object firstValue, object secondValue)
		{
			var propertyName = new BlackboardPropertyName(valueType.FullName);
			UnityEngine.Debug.Log($"On object value changed test. PropertyName: {propertyName}. FirstValue: {firstValue}. SecondValue: {secondValue}");

			bool changed = false;
			Action onChanged = () => { changed = true; };

			blackboard.Subscribe(valueType, propertyName, onChanged);
			blackboard.SetValue(valueType, propertyName, firstValue);
			Assert.AreEqual(false, changed, "Blackboard sent first onChanged before flush");
			blackboard.Flush();
			Assert.AreEqual(true, changed, "Blackboard didn't send first onChanged after flush");
			changed = false;
			blackboard.SetValue(valueType, propertyName, secondValue);
			Assert.AreEqual(false, changed, "Blackboard sent second onChanged before flush");
			blackboard.Flush();
			Assert.AreEqual(true, changed, "Blackboard didn't send second onChanged after flush");
			changed = false;
			blackboard.SetValue(propertyName, firstValue);
			blackboard.SetValue(propertyName, secondValue);
			blackboard.Flush();
			Assert.AreEqual(false, changed, "Blackboard sent changed after set to previous value");
			blackboard.Remove(valueType, propertyName);
			Assert.AreEqual(false, changed, "Blackboard sent onChanged after remove before flush");
			blackboard.Flush();
			Assert.AreEqual(true, changed, "Blackboard didn't send onChanged after remove and flush");
			changed = false;
			blackboard.SetValue(propertyName, firstValue);
			blackboard.Remove(valueType, propertyName);
			blackboard.Flush();
			Assert.AreEqual(false, changed, "Blackboard sent changed after set and remove");
			blackboard.Unsubscribe(valueType, propertyName, onChanged);
			blackboard.SetValue(valueType, propertyName, firstValue);
			blackboard.Flush();
			Assert.AreEqual(false, changed, "Blackboard sent onChanged after unsubscribe");
		}

		private enum ChangedState
		{
			Default,
			Changed,
			Removed
		}

		private static void OnTypedChangedGenericValueTest<T>(Blackboard blackboard, T firstValue, T secondValue)
		{
			var propertyName = new BlackboardPropertyName(typeof(T).FullName);
			UnityEngine.Debug.Log($"On typed generic value changed test. PropertyName: {propertyName}. FirstValue: {firstValue}. SecondValue: {secondValue}");

			var changedState = ChangedState.Default;
			Action<BlackboardChangeInfo<T>> onChanged = info =>
			{
				changedState = info.removed ? ChangedState.Removed : ChangedState.Changed;
			};

			blackboard.Subscribe(propertyName, onChanged);
			blackboard.SetValue(propertyName, firstValue);
			Assert.AreEqual(ChangedState.Default, changedState, "Blackboard sent first Changed before flush");
			blackboard.Flush();
			Assert.AreEqual(ChangedState.Changed, changedState, "Blackboard didn't send first Changed after flush");
			changedState = ChangedState.Default;
			blackboard.SetValue(propertyName, secondValue);
			Assert.AreEqual(ChangedState.Default, changedState, "Blackboard sent second Changed before flush");
			blackboard.Flush();
			Assert.AreEqual(ChangedState.Changed, changedState, "Blackboard didn't send second Changed after flush");
			changedState = ChangedState.Default;
			blackboard.SetValue(propertyName, firstValue);
			blackboard.SetValue(propertyName, secondValue);
			blackboard.Flush();
			Assert.AreEqual(ChangedState.Default, changedState, "Blackboard sent changed after set to previous value");
			blackboard.Remove<T>(propertyName);
			Assert.AreEqual(ChangedState.Default, changedState, "Blackboard sent Changed or Removed after remove before flush");
			blackboard.Flush();
			Assert.AreEqual(ChangedState.Removed, changedState, "Blackboard didn't send Removed after remove and flush");
			changedState = ChangedState.Default;
			blackboard.SetValue(propertyName, firstValue);
			blackboard.Remove<T>(propertyName);
			blackboard.Flush();
			Assert.AreEqual(ChangedState.Default, changedState, "Blackboard sent changed after set and remove");
			blackboard.Unsubscribe(propertyName, onChanged);
			blackboard.SetValue(propertyName, firstValue);
			blackboard.Flush();
			Assert.AreEqual(ChangedState.Default, changedState, "Blackboard sent Changed after unsubscribe");
		}

		private static AnimationCurve randomCurve =>
			AnimationCurve.Constant(Random.value, Random.value, Random.value);

		private static bool randomBool => Random.Range(0, 2) == 1;

		private static Bounds randomBounds => new Bounds(Random.insideUnitSphere, Random.insideUnitSphere);

		private static BoundsInt randomBoundsInt => new BoundsInt(randomInt, randomInt, randomInt,
			randomInt, randomInt, randomInt);

		private static byte randomByte => Convert.ToByte(Random.Range(byte.MinValue, byte.MaxValue + 1));

		private static char randomChar => Convert.ToChar(Random.Range('A', 'z'));

		private static Color randomColor =>
			new Color(Random.value, Random.value, Random.value, Random.value);

		private static Component randomComponent => randomTransform;

		private static double randomDouble => Random.value;

		private static GameObject randomGameObject => new GameObject("Test");

		private static Gradient randomGradient => new Gradient();

		private static int randomInt => Random.Range(int.MaxValue, int.MaxValue);

		private static LayerMask randomLayerMask =>
			Random.Range(int.MinValue, int.MaxValue) + Mathf.RoundToInt(Random.value);

		private static long randomLong => randomInt;

		private static Matrix4x4 randomMatrix4x4 =>
			new Matrix4x4(randomVector4, randomVector4, randomVector4, randomVector4);

		private static Object randomObject => randomGameObject;

		private static PropertyName randomPropertyName => randomString;

		private static Quaternion randomQuaternion => Random.rotation;

		private static Rect randomRect => new Rect(Random.insideUnitCircle, Random.insideUnitCircle);

		private static RectInt randomRectInt => new RectInt(randomInt, randomInt, randomInt, randomInt);

		private static sbyte randomSbyte => Convert.ToSByte(Random.Range(sbyte.MinValue, sbyte.MaxValue + 1));

		private static short randomShort => Convert.ToInt16(Random.Range(short.MinValue, short.MaxValue + 1));

		private static string randomString
		{
			get
			{
				int length = Random.Range(5, 50);
				var answer = new char[length];

				for (int i = 0; i < length; ++i)
				{
					answer[i] = randomChar;
				}

				return new string(answer);
			}
		}

		private static Transform randomTransform => randomGameObject.transform;

		private static uint randomUint => Convert.ToUInt32(Random.Range(0, int.MaxValue));

		private static ulong randomUlong => randomUint;

		private static ushort randomUshort => Convert.ToUInt16(Random.Range(0, ushort.MaxValue));

		private static Vector2Int randomVector2Int => new Vector2Int(randomInt, randomInt);

		private static Vector3Int randomVector3Int => new Vector3Int(randomInt, randomInt, randomInt);

		private static Vector4 randomVector4 => new Vector4(Random.value, Random.value, Random.value, Random.value);
	}
}
