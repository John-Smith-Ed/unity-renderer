

    using UnityEngine;
    using System.Collections;
    using ABEY;

    using WebSocketSharp;
    // this will be used as our main
    // Game controller for the whole app

    public class ABEYController : MonoSingleton<ABEYController>{

       // Vector3 sceneWorldPos = Utils.GridToWorldPosition(sceneData.basePosition.x, sceneData.basePosition.y);
       //gameObject.transform.position = PositionUtils.WorldToUnityPosition(sceneWorldPos);

        [Header("Testing")]
        [SerializeField] DummyDataScriptable dummyData; 
        
        [Header("Configs")]
        [SerializeField] ConfigScriptable abeyConfig;
        // short access to configs
        public ConfigScriptable                     Config                  => abeyConfig;
        public ApplicationConfigScriptable          ApplicationConfig       => abeyConfig.ApplicationConfig;
        public EnvironmentConfigScriptable          EnvironmentConfig       => abeyConfig.EnvironmentConfig;
        public EndPointConfigScriptable             EndPointConfig          => abeyConfig.EndPointConfig;
        public InputConfigScriptable                InputConfig             => abeyConfig.InputConfig;        
        public AudioEventsScriptable                AudioEvents             => abeyConfig.AudioEvents;
        public PlayerConfigScriptable               PlayerConfig            => abeyConfig.PlayerConfig;
        public ParcelConfigScriptable               ParcelConfig            => abeyConfig.ParcelConfig;    
        public TestConfigScriptable                 TestConfig              => abeyConfig.TestConfig;        
        public AssetManagerConfigScriptable         AssetManagerConfig      => abeyConfig.AssetManagerConfig;
        public MessageThrottlingConfigScriptable    MessageThrottlingConfig => abeyConfig.MessageThrottlingConfig;
        public UIConfigScriptable                   UIConfig                => abeyConfig.UIConfig;
        public NFTDataFetchingConfigScriptable      NFTDataFetchingConfig   => abeyConfig.NFTDataFetchingConfig;
        public PhysicsLayersConfigScriptable        PhysicsLayersConfig     => abeyConfig.PhysicsLayersConfig;
        
        [Header("Prefab Refs")]
        [SerializeField] PrefabRefsScriptableObject prefabRefs;
        public GameObject GetPrefab(string name) => prefabRefs.GetPrefab(name);

        // Do not modify the base call
        // true passed will ensure this does not destroy on a scene change 
        private void Awake(){
            base.Awake(true);
            // can add code here if needed for Awake
        }

        // hack for now to start the player where we want
        IEnumerator Start(){
            
            yield return null;
          //  HUDController.i.ConfigureHUDElement(DCL.HUDElementID.LOADING, new HUDConfiguration(){active=true, visible=true}, null);
            while(!DCLCharacterController.i.enabled){
                yield return null;
            }
        //    yield return new WaitForSeconds(5f);
            //close hole position Vector3(17.3299999,104.57,3.67000008)
            // current player start position {\"x\":14.808116051111426,\"y\":206,\"z\":-4.2183475919324565}
            Debug.Log("SHOULD TELEPORT");
            // Vector3(21,120.579994,-3.77999997)
          //  DCLCharacterController.i.enabled=true;
            DCLCharacterController.i.Teleport(new Vector3(18f, 122f,-10.7f));
        }

       

        // Fake socket listen
        public void OnMessage(Message message) {
            ((AbeyCommunicationBridge)DCL.Main.i.KernelCommunication).FakeMessage(message);
        }

        public Message MakeMessage(string type, string payload) => new Message{
               type     = type,
               payload  = payload
        };

       
        

        void CreateAllHuds(){
            // this is so weird, all these elements are in the project, so why do you need the backend creating them?
            // i would understand if they were assetbundles stored on the backend and downloaded but they are here, should be built into the scene 
            for(int i=1; i < (int)DCL.HUDElementID.COUNT; i++){                
                OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(i, true, i==(int)DCL.HUDElementID.WORLD_CHAT_WINDOW, null) ));
            }
            // this one had some data passed in the request we captured so running it again - note the request captured ran alot of duplicated messages
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement((int)DCL.HUDElementID.PROFILE_HUD, true,false, new DummyDataScriptable.ExtraPayload(){enableVoiceChat=true,enableQuestPanel=false}) ));
            
        }
     
    
    
    }
    
