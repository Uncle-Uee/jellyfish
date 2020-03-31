// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using UltEvents;
using UnityEngine;

namespace SOFlow.Utilities
{
	[Serializable] public class BoolEvent : UltEvent<bool>{}
	[Serializable] public class IntEvent : UltEvent<int>{}
	[Serializable] public class FloatEvent : UltEvent<float>{}
	[Serializable] public class StringEvent : UltEvent<string>{}
	[Serializable] public class Vector2Event : UltEvent<Vector2>{}
	[Serializable] public class Vector2IntEvent : UltEvent<Vector2Int>{}
	[Serializable] public class Vector3Event : UltEvent<Vector3>{}
	[Serializable] public class Vector3IntEvent : UltEvent<Vector3Int>{}
	[Serializable] public class ColorEvent : UltEvent<Color>{}
}