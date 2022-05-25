namespace ABEY {
    using UnityEngine;

    [CreateAssetMenu(fileName = "PlayerConfigScriptable", menuName = "ABEY/PlayerConfig", order = 0)]
    public class PlayerConfigScriptable :ScriptableObject{
        [SerializeField] float positionReportingDelay          = 0.1f; // In seconds
        [SerializeField] float worldRepositionMinimumDistance  = 100f;

        public float PositionReportingDelay          => positionReportingDelay;
        public float WorldRepositionMinimumDistance  => worldRepositionMinimumDistance;
    }
}


