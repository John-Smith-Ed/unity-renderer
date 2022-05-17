

    using UnityEngine;
    using System.Collections;
    using ABEY;

    using WebSocketSharp;
    // this will be used as our main
    // Game controller for the whole app

    public class ABEYController : MonoSingleton<ABEYController>{

        [Header("Testing")]
        [SerializeField] DummyDataScriptable dummyData;

       // Vector3 sceneWorldPos = Utils.GridToWorldPosition(sceneData.basePosition.x, sceneData.basePosition.y);
       //gameObject.transform.position = PositionUtils.WorldToUnityPosition(sceneWorldPos);

        
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
            
            yield return FakeSetUp();
            yield return null;
            while(!DCLCharacterController.i.enabled){
                yield return null;
            }
            yield return null;
            //close hole position Vector3(17.3299999,104.57,3.67000008)
            // current player start position {\"x\":14.808116051111426,\"y\":206,\"z\":-4.2183475919324565}
            Debug.Log("SHOULD TELEPORT");
          // Vector3(21,120.579994,-3.77999997)
            DCLCharacterController.i.SetPosition(new Vector3(18f, 122f,-10.7f));
           //  OnMessage(MakeMessage("Teleport", "{\"x\":21,\"y\":120.579994,\"z\":-3.77999997} "));
        }

        IEnumerator FakeSetUp(){
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
            

            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting ABEYWORLD ready...\",\"showTips\":true}"));


            OnMessage(MakeMessage("LoadParcelScenes", dummyData.GetData("LoadParcelScenes")));
            OnMessage(MakeMessage("SetKernelConfiguration", dummyData.GetData("SetKernelConfiguration")));
            OnMessage(MakeMessage("SetRenderProfile", "{\"id\":0}"));
            OnMessage(MakeMessage("SetFeatureFlagConfiguration",  dummyData.GetData("SetFeatureFlagConfiguration")));
            OnMessage(MakeMessage("UpdateRealmsInfo",  dummyData.GetData("UpdateRealmsInfo")));
         
            OnMessage(MakeMessage("CreateGlobalScene", dummyData.GetData("CreateGlobalScene")));
            OnMessage(MakeMessage("LoadParcelScenes", dummyData.GetData("LoadParcelScenes2")));
             

            WaitForSeconds pause = new WaitForSeconds(3f);
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting ABEYWORLD ready...\",\"showTips\":true}"));
            yield return pause;
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting ABEYWORLD ready...\",\"showTips\":true}"));
            yield return pause;
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting ABEYWORLD ready...\",\"showTips\":true}"));
            yield return pause;
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting ABEYWORLD ready...\",\"showTips\":true}"));
            yield return pause;
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting ABEYWORLD ready...\",\"showTips\":true}"));
            yield return pause;
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting ABEYWORLD ready...\",\"showTips\":true}"));
            yield return pause;
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting ABEYWORLD ready...\",\"showTips\":true}"));
            
            
            
            
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":1, \"configuration\":{\"active\":true,\"visible\":true},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":3, \"configuration\":{\"active\":true,\"visible\":true},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":4, \"configuration\":{\"active\":true,\"visible\":false},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":29,\"configuration\":{\"active\":true,\"visible\":false},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":30,\"configuration\":{\"active\":true,\"visible\":false},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":31,\"configuration\":{\"active\":true,\"visible\":true},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":5, \"configuration\":{\"active\":true,\"visible\":false},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":6, \"configuration\":{\"active\":true,\"visible\":true},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":32,\"configuration\":{\"active\":true,\"visible\":false},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":7, \"configuration\":{\"active\":true,\"visible\":true},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":8, \"configuration\":{\"active\":true,\"visible\":true},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":9, \"configuration\":{\"active\":true,\"visible\":true},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":14,\"configuration\":{\"active\":true,\"visible\":false},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":16,\"configuration\":{\"active\":true,\"visible\":false},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":17,\"configuration\":{\"active\":true,\"visible\":false},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":26,\"configuration\":{\"active\":false,\"visible\":false},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":27,\"configuration\":{\"active\":false,\"visible\":true},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":2, \"configuration\":{\"active\":true,\"visible\":true},\"extraPayload\":null}"));
      //      OnMessage("ConfigureHUDElement", "{\"hudElementId\":11,\"configuration\":{\"active\":true,\"visible\":true},\"extraPayload\":"{\"enableVoiceChat\":true,\"enableQuestPanel\":false}\"}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":10,\"configuration\":{\"active\":true,\"visible\":true},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":18,\"configuration\":{\"active\":true,\"visible\":false},\"extraPayload\":null}"));
            OnMessage(MakeMessage("ConfigureHUDElement", "{\"hudElementId\":20,\"configuration\":{\"active\":true,\"visible\":false},\"extraPayload\":null}"));
       //     OnMessage("ConfigureHUDElement", "{"hudElementId":22,"configuration":{"active":true,"visible":false},"extraPayload":null}
       //     OnMessage("ConfigureHUDElement", "{"hudElementId":23,"configuration":{"active":true,"visible":true},"extraPayload":null}
            
            
            

            OnMessage(MakeMessage("SetRotation","{\"x\":15.903898964861497,\"y\":106,\"z\":-5.222989489695095,\"cameraTarget\":{\"x\":14,\"y\":106,\"z\":40}}"));
            OnMessage(MakeMessage("ActivateRendering", null)); 
            OnMessage(MakeMessage("SetLoadingScreen","{\"isVisible\":false,\"message\":\"Loading scenes 99%\",\"showTips\":false}"));
            OnMessage(MakeMessage("ActivateRendering", null)); 

           
            
OnMessage(MakeMessage("LoadProfile", dummyData.GetData("LoadProfile")));
                

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
    
