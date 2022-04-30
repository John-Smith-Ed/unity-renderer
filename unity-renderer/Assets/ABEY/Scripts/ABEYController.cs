namespace ABEY{
//[DllImport("__Internal")]
    using UnityEngine;

    // this will be used as our main
    // Game controller for the whole app

    public class ABEYController : MonoSingleton<ABEYController>{

        
        // not in use right now, for this to work 
        // it can only be accessed after the awakes which is the correct way
        // the current code base is accessing things before the core bootstraps
        // moved to a fully static class so its ready when called early
       // [Header("Configs")]
       // [SerializeField] ConfigScriptable abeyConfig;
       // public ConfigScriptable Config => abeyConfig;

        // Do not modify the base call
        // true passed will ensure this does not destroy on a scene change 
        private void Awake(){
            base.Awake(true);
            // can add code here if needed for Awake
        }
    }
}