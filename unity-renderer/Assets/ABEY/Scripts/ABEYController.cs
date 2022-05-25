

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

        // Do not modify the base call
        // true passed will ensure this does not destroy on a scene change 
        private void Awake(){
            base.Awake(true);
            // can add code here if needed for Awake
        }

        // hack for now to start the player where we want
        IEnumerator Start(){
            
            FakeSetUp();
            yield return null;
           // while(!DCLCharacterController.i.enabled){
           //     yield return null;
           // }
            yield return new WaitForSeconds(5f);
            //close hole position Vector3(17.3299999,104.57,3.67000008)
            // current player start position {\"x\":14.808116051111426,\"y\":206,\"z\":-4.2183475919324565}
            Debug.Log("SHOULD TELEPORT");
          // Vector3(21,120.579994,-3.77999997)
          DCLCharacterController.i.enabled=true;
         DCLCharacterController.i.Teleport(new Vector3(18f, 122f,-10.7f));
            //
           //  OnMessage(MakeMessage("Teleport", "{\"x\":21,\"y\":120.579994,\"z\":-3.77999997} "));
        }

        void FakeSetUp(){
            //AbeyCommunicationBridge.OpenNftDialog(string contactAddress, string comment, string tokenId);
            //AbeyCommunicationBridge.OpenExternalUrl(string url);
            //AbeyCommunicationBridge.EntityComponentDestroy(string name);
            //AbeyCommunicationBridge.SharedComponentAttach(string id, string name);
            //AbeyCommunicationBridge.Query(Protocol.QueryPayload payload);
            //AbeyCommunicationBridge.SharedComponentUpdate(string id, string json);
            //AbeyCommunicationBridge.SharedComponentDispose(string id);
            //AbeyCommunicationBridge.SharedComponentCreate(int classId, string id);
            //AbeyCommunicationBridge.EntityComponentCreateOrUpdate(int classId, string json);
            //AbeyCommunicationBridge.SetEntityParent(string parentId);
            //AbeyCommunicationBridge.SetEntityId(string id);
            //AbeyCommunicationBridge.SetSceneId(string id);
            //AbeyCommunicationBridge.SetTag(string id);
            //AbeyCommunicationBridge.CreateEntity();
            //AbeyCommunicationBridge.RemoveEntity();
            //AbeyCommunicationBridge.SceneReady();
/*
            AbeyCommunicationBridge.SetSceneId("wc5");
            AbeyCommunicationBridge.CreateEntity();            
            AbeyCommunicationBridge.SceneReady();
            AbeyCommunicationBridge.OpenExternalUrl("http://google.com");
*/

/*----------- NOTES
            // These do not work as they should but are there to turn of and on the camera is a really retard way that should not be happening
                - ActivateRendering
                - DeactivateRendering

            // looks like it has a bunch of settings and the players avatar but it does not attach it
            - LoadProfile

            // turns of UI overlay with loading info, this for sure needs to be done right but not a huge issue right now
            - SetLoadingScreen
---*/
/*
            OnMessage(MakeMessage("Reset",null)); 
            OnMessage(MakeMessage("DeactivateRendering",null)); 

            CreateAllHuds();

            OnMessage(MakeMessage("LoadParcelScenes",dummyData.GetData("LoadParcelScenes")));

            OnMessage(MakeMessage("SetLoadingScreen", dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
            */
        /*    
            OnMessage(MakeMessage("SetLoadingScreen", dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
            OnMessage(MakeMessage("SetLoadingScreen", dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
            OnMessage(MakeMessage("SetLoadingScreen", dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
            OnMessage(MakeMessage("SetLoadingScreen", dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
            OnMessage(MakeMessage("SetLoadingScreen", dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
        */
        /*
            OnMessage(MakeMessage("SetKernelConfiguration", dummyData.GetData("SetKernelConfiguration")));
            OnMessage(MakeMessage("SetLoadingScreen",  dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
           // OnMessage(MakeMessage("SetLoadingScreen",  dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
            
            OnMessage(MakeMessage("SetFeatureFlagConfiguration",dummyData.GetData("SetFeatureFlagConfiguration")));
            
            
            OnMessage(MakeMessage("CreateGlobalScene", dummyData.GetData("CreateGlobalScene")));
            OnMessage(MakeMessage("UpdateRealmsInfo", dummyData.GetData("UpdateRealmsInfo")));

         //   OnMessage(MakeMessage("SetRotation","{\"x\":15.903898964861497,\"y\":106,\"z\":-5.222989489695095,\"cameraTarget\":{\"x\":14,\"y\":106,\"z\":40}}"));
            OnMessage(MakeMessage("ActivateRendering", null)); 
            OnMessage(MakeMessage("SetLoadingScreen", dummyData.GetLoadingScreen(false, "Loading scenes 99%", false)));
            OnMessage(MakeMessage("ActivateRendering", null)); 

            OnMessage(MakeMessage("LoadProfile", dummyData.GetData("LoadProfile")));
            

                OnMessage(MakeMessage("LoadParcelScenes", dummyData.GetData("LoadParcelScenes2")));

                OnMessage(MakeMessage("SendSceneMessage", "Cg5kY2wtZ3MtYXZhdGFycxIFc2NlbmVqAA=="));
       OnMessage(MakeMessage("ForceActivateRendering", null));
            OnMessage(MakeMessage("ReportFocusOn",null));
            DCLCharacterController.i.enabled=true;
*/
        }

        // Fake socket listen
        public void OnMessage(string message) {
            ((AbeyCommunicationBridge)DCL.Main.i.KernelCommunication).FakeMessage(message);
        }

        public string MakeMessage(string type, string payload) => JsonUtility.ToJson(new DCLWebSocketService.Message{
               type     = type,
               payload  = payload
        });

       
        

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
    
