namespace ABEY {
    using UnityEngine;

    [CreateAssetMenu(fileName = "AssetManagerConfigScriptable", menuName = "ABEY/AssetManagerConfigScriptable", order = 0)]
    public class AssetManagerConfigScriptable : ScriptableObject {
        [SerializeField] int libraryCleanupThreshold = 10;

        public int LibraryCleanupThreshold => libraryCleanupThreshold;
    }
}