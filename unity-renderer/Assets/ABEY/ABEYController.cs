
//[DllImport("__Internal")]
    using UnityEngine;
    using System.Collections;
   
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

        // hack for now to start the player where we want
        IEnumerator Start(){
            yield return null;
            while(!DCLCharacterController.i.enabled){
                yield return null;
            }
            yield return null;
            //close hole position Vector3(17.3299999,104.57,3.67000008)
            // current player start position {\"x\":14.808116051111426,\"y\":206,\"z\":-4.2183475919324565}
            Debug.Log("SHOULD TELEPORT");
           
            DCLCharacterController.i.Teleport("{\"x\":18,\"y\":122,\"z\":-10.7} ");
        }
    }
