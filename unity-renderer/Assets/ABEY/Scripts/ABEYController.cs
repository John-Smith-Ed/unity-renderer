

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
        public ConfigScriptable Config => abeyConfig;

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
            while(!DCLCharacterController.i.enabled){
                yield return null;
            }
            yield return null;
            //close hole position Vector3(17.3299999,104.57,3.67000008)
            // current player start position {\"x\":14.808116051111426,\"y\":206,\"z\":-4.2183475919324565}
            Debug.Log("SHOULD TELEPORT");
          // Vector3(21,120.579994,-3.77999997)
            DCLCharacterController.i.Teleport("{\"x\":18,\"y\":122,\"z\":-10.7} ");
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

            OnMessage(MakeMessage("Reset",null)); 
            OnMessage(MakeMessage("DeactivateRendering",null)); 
            OnMessage(MakeMessage("LoadParcelScenes",dummyData.GetData("LoadParcelScenes")));

            OnMessage(MakeMessage("SetLoadingScreen", dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
        /*    
            OnMessage(MakeMessage("SetLoadingScreen", dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
            OnMessage(MakeMessage("SetLoadingScreen", dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
            OnMessage(MakeMessage("SetLoadingScreen", dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
            OnMessage(MakeMessage("SetLoadingScreen", dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
            OnMessage(MakeMessage("SetLoadingScreen", dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
        */
            OnMessage(MakeMessage("SetKernelConfiguration", dummyData.GetData("SetKernelConfiguration")));
            OnMessage(MakeMessage("SetLoadingScreen",  dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
           // OnMessage(MakeMessage("SetLoadingScreen",  dummyData.GetLoadingScreen(true, "Getting ABEYWORLD ready...", true)));
            
            OnMessage(MakeMessage("SetFeatureFlagConfiguration",dummyData.GetData("SetFeatureFlagConfiguration")));
            
            OnMessage(MakeMessage("SetRenderProfile", "{\"id\":0}"));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(1, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(3, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(4, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(29, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(30, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(31, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(5, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(6, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(32, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(7, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(8, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(9, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(14, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(16, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(17, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(26, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(27, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(2, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(2, true,true, new DummyDataScriptable.ExtraPayload(){enableVoiceChat=true,enableQuestPanel=false}) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(10, true,true,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(18, true,false,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(20, true,false,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(22, true,false,null) ));
            OnMessage(MakeMessage("ConfigureHUDElement", dummyData.GetConfigureHUDElement(23, true,true,null) ));
            
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

        }

        // Fake socket listen
        public void OnMessage(string message) {
            #if ABEY
            lock (AbeyCommunicationBridge.queuedMessages){ 
                
                DCLWebSocketService.Message finalMessage = JsonUtility.FromJson<DCLWebSocketService.Message>(message);

                AbeyCommunicationBridge.queuedMessages.Enqueue(finalMessage);
                AbeyCommunicationBridge.queuedMessagesDirty = true;
                
            }
            #endif
        }

        public string MakeMessage(string type, string payload) => JsonUtility.ToJson(new DCLWebSocketService.Message{
               type     = type,
               payload  = payload
        });

       
        
     
     
    
    
    }
    
