namespace ABEY {
    using UnityEngine;

    [CreateAssetMenu(fileName = "ParcelConfigScriptable", menuName = "ABEY/ParcelConfig", order = 0)]
    public class ParcelConfigScriptable : ScriptableObject {
        [SerializeField] float debugFloorHeight             = -0.1f;
        [SerializeField] float parcelSize                   = 16f;
        [SerializeField] float parcelBoundariesThreshold    = 0.01f;
        [SerializeField] float unloadDistance               = 12f;
        [SerializeField] bool  visualLoadingEnabled         = true;

        public float DebugFloorHeight             => debugFloorHeight;
        public float ParcelSize                   => parcelSize;
        public float ParcelBoundariesThreshold    => parcelBoundariesThreshold;
        public float UnloadDistance               => parcelSize * unloadDistance;
        public bool  VisualLoadingEnabled         => visualLoadingEnabled;
    }
}
