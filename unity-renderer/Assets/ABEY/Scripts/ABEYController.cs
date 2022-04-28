namespace ABEY{

    using UnityEngine;

    public class ABEYController : MonoSingleton<ABEYController>{

        [Header("Configs")]
        [SerializeField] ConfigScriptable abeyConfig;
        public ConfigScriptable Config => abeyConfig;

        // Do not modify the base call
        // true passed will ensure this does not destroy on a scene change 
        private void Awake(){
            base.Awake(true);

            // can add code here if needed for Awake
        }
    }
}