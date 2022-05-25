namespace ABEY {
    using UnityEngine;

    [CreateAssetMenu(fileName = "TestConfigScriptable", menuName = "ABEY/TestConfigScriptable", order = 0)]
    public class TestConfigScriptable : ScriptableObject {
        
        [SerializeField] int   visualTestsApprovedAffinity     = 95;
        [SerializeField] float visualTestsPixelsCheckThreshold = 5.0f;
        [SerializeField] int   visualTestsSnapshotWidth        = 1280;
        [SerializeField] int   visualTestsSnapshotHeight       = 720;

        public int   VisualTestsApprovedAffinity     => visualTestsApprovedAffinity;
        public float VisualTestsPixelsCheckThreshold => visualTestsPixelsCheckThreshold;
        public int   VisualTestsSnapshotWidth        => visualTestsSnapshotWidth;
        public int   VisualTestsSnapshotHeight       => visualTestsSnapshotHeight;

    }
}