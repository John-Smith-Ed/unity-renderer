namespace ABEY {
    using UnityEngine;

    [CreateAssetMenu(fileName = "EnvironmentConfigScriptable", menuName = "ABEY/EnvironmentConfigScriptable", order = 0)]
    public class EnvironmentConfigScriptable : ScriptableObject {
        
        [SerializeField] bool    runningTests           = false;
        [SerializeField] bool    debug                  = true;
        [SerializeField] Vector3 mordor                 = new Vector3(10000, 10000, 10000);
        [SerializeField] int     mordorScalar           = 10000;
        [SerializeField] float   uninitializedFloat     = 999999f;
        [SerializeField] string  avatarGlobalSceneId    = "dcl-gs-avatars";


        public bool    RunningTests           => runningTests;
        public bool    Debug                  => debug;
        public Vector3 Mordor                 => mordor;
        public int     MordorScalar           => mordorScalar;
        public float   UninitializedFloat     => uninitializedFloat;
        public string  AvatarGlobalSceneId    => avatarGlobalSceneId;


    }
}