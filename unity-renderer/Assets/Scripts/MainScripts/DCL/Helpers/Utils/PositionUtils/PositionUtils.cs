using UnityEngine;

namespace DCL.Helpers
{
    public class PositionUtils
    {
        public static Vector3 UnityToWorldPosition(Vector3 pos) { return pos + ABEYController.i.CommonScriptables.worldOffset; }

        public static Vector3 WorldToUnityPosition(Vector3 pos) { return pos - ABEYController.i.CommonScriptables.worldOffset; }
    }
}