using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class StringUnityEvent : UnityEvent<string> {}
[Serializable] public class DoubleUnityEvent : UnityEvent<double> {}
[Serializable] public class FloatUnityEvent : UnityEvent<float> {}
[Serializable] public class LongUnityEvent : UnityEvent<long> {}
[Serializable] public class IntUnityEvent : UnityEvent<int> {}
[Serializable] public class ActionUnityEvent : UnityEvent<Action> {} 
[Serializable] public class ColorUnityEvent : UnityEvent<Color> {}
[Serializable] public class BoolUnityEvent : UnityEvent<bool> { }
[Serializable] public class IntStringEvent : UnityEvent<int,string> {}
[Serializable] public class IntIntUnityEvent : UnityEvent<int,int> {}
[Serializable] public class Vector3UnityEvent : UnityEvent<Vector3> {}
[Serializable] public class TransformUnityEvent : UnityEvent<Transform> {}
[Serializable] public class TrackUnityEvent : UnityEvent<string,Dictionary<string, object>> {}

