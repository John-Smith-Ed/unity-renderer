namespace ABEY {
    using UnityEngine;

    [CreateAssetMenu(fileName = "MessageThrottlingConfig", menuName = "ABEY/MessageThrottlingConfig", order = 0)]
    public class MessageThrottlingConfigScriptable : ScriptableObject {
        //TODO: NOTE The division should be cached at some point here
        // however the trade off from their missuse of Resources greatly outweighs 
        // the impact that caused before so just doing the division each time for now is ok 
        
        [SerializeField] float sixtyFpsTime                     = 1.0f / 60.0f;
        [SerializeField] float globalFrameThrottlingTime        = 8.0f;
        [SerializeField] float loadParcelScenesThrottlingTime   = 4.0f;
     
        public float SixtyFpsTime                     => sixtyFpsTime; 
        public float GlobalFrameThrottlingTime        => sixtyFpsTime / globalFrameThrottlingTime;
        public float LoadParcelScenesThrottlingTime   => sixtyFpsTime / loadParcelScenesThrottlingTime;

    }
}