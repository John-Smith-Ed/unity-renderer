

    using UnityEngine;
    using System.Collections;
    using ABEY;

    using WebSocketSharp;
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
            FakeSetUp();
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

            OnMessage(MakeMessage("Reset",null)); 
            OnMessage(MakeMessage("DeactivateRendering",null)); 
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting things ready...\",\"showTips\":true}"));
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting things ready...\",\"showTips\":true}"));
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting things ready...\",\"showTips\":true}"));
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting things ready...\",\"showTips\":true}"));
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting things ready...\",\"showTips\":true}"));
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting things ready...\",\"showTips\":true}"));
            OnMessage(MakeMessage("SetKernelConfiguration", "{\"proceduralSkyboxConfig\":{\"configToLoad\":\"Generic_Skybox\",\"lifecycleDuration\":60,\"fixedTime\":-1,\"disableReflection\":false,\"updateReflectionTime\":-1},\"enableNewTutorialCamera\":true,\"renderProfile\":0,\"pois\":[{\"x\":0,\"y\":0},{\"x\":-62,\"y\":-61},{\"x\":-83,\"y\":0},{\"x\":-62,\"y\":62},{\"x\":0,\"y\":83},{\"x\":61,\"y\":62},{\"x\":61,\"y\":-61},{\"x\":0,\"y\":-81},{\"x\":-3,\"y\":-33},{\"x\":81,\"y\":0},{\"x\":-55,\"y\":143},{\"x\":58,\"y\":2},{\"x\":61,\"y\":-27},{\"x\":-49,\"y\":-41},{\"x\":36,\"y\":46},{\"x\":-71,\"y\":-38},{\"x\":-129,\"y\":-141},{\"x\":52,\"y\":2},{\"x\":-39,\"y\":58},{\"x\":59,\"y\":133},{\"x\":57,\"y\":8},{\"x\":-40,\"y\":-49},{\"x\":-12,\"y\":-39},{\"x\":-9,\"y\":73},{\"x\":87,\"y\":18},{\"x\":67,\"y\":-21},{\"x\":-75,\"y\":73},{\"x\":-15,\"y\":-22},{\"x\":-32,\"y\":-44},{\"x\":52,\"y\":16},{\"x\":-55,\"y\":1},{\"x\":-25,\"y\":103},{\"x\":52,\"y\":10},{\"x\":12,\"y\":46},{\"x\":-5,\"y\":-16},{\"x\":105,\"y\":-21},{\"x\":-11,\"y\":-30},{\"x\":-49,\"y\":-49},{\"x\":113,\"y\":-7},{\"x\":52,\"y\":-71},{\"x\":-43,\"y\":53},{\"x\":63,\"y\":2},{\"x\":-134,\"y\":-121},{\"x\":28,\"y\":45},{\"x\":137,\"y\":34},{\"x\":-43,\"y\":57},{\"x\":16,\"y\":83},{\"x\":59,\"y\":114},{\"x\":-40,\"y\":33},{\"x\":-69,\"y\":77},{\"x\":-48,\"y\":58},{\"x\":-35,\"y\":-42},{\"x\":24,\"y\":-126},{\"x\":-148,\"y\":-35},{\"x\":-109,\"y\":-89},{\"x\":66,\"y\":16},{\"x\":56,\"y\":22},{\"x\":-1,\"y\":-35},{\"x\":-43,\"y\":64},{\"x\":-101,\"y\":-70},{\"x\":139,\"y\":-79},{\"x\":22,\"y\":-17},{\"x\":-74,\"y\":-29},{\"x\":-75,\"y\":46},{\"x\":-66,\"y\":-29},{\"x\":45,\"y\":9},{\"x\":-150,\"y\":43},{\"x\":-21,\"y\":-40},{\"x\":127,\"y\":-133},{\"x\":-2,\"y\":-49},{\"x\":13,\"y\":-10},{\"x\":25,\"y\":2},{\"x\":33,\"y\":2},{\"x\":75,\"y\":-12},{\"x\":77,\"y\":-12},{\"x\":11,\"y\":95},{\"x\":60,\"y\":49},{\"x\":-19,\"y\":-6},{\"x\":-29,\"y\":-4},{\"x\":56,\"y\":7},{\"x\":-149,\"y\":-143},{\"x\":69,\"y\":21},{\"x\":55,\"y\":94},{\"x\":52,\"y\":93},{\"x\":52,\"y\":96},{\"x\":51,\"y\":99},{\"x\":56,\"y\":99},{\"x\":58,\"y\":92},{\"x\":98,\"y\":-88},{\"x\":-137,\"y\":96}],\"comms\":{\"commRadius\":4,\"voiceChatEnabled\":false},\"profiles\":{\"nameValidCharacterRegex\":\"[a-zA-Z0-9]g\",\"nameValidRegex\":\"^[a-zA-Z0-9]+$\"},\"gifSupported\":false,\"network\":\"mainnet\",\"validWorldRanges\":[{\"xMin\":-150,\"yMin\":-150,\"xMax\":150,\"yMax\":150},{\"xMin\":62,\"yMin\":151,\"xMax\":162,\"yMax\":158},{\"xMin\":151,\"yMin\":144,\"xMax\":162,\"yMax\":150},{\"xMin\":151,\"yMin\":59,\"xMax\":163,\"yMax\":143}],\"kernelVersion\":\"1.0.0-2302764182.commit-cf8d532\",\"rendererVersion\":\"1.0.35629-20220510163806.commit-ef34c50\"}"));
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting things ready...\",\"showTips\":true}"));
            OnMessage(MakeMessage("SetLoadingScreen", "{\"isVisible\":true,\"message\":\"Getting things ready...\",\"showTips\":true}"));
         //   OnMessage("SetFeatureFlagConfiguration", "{\"flags\":{\"asset_bundles\":true,\"avatar_lods\":true,\"avatar_skins\":true,\"banned_users\":true,\"builder-dev\":true,\"builder_in_world\":true,\"cull-opaque-heuristic\":true,\"emotes_customization\":true,\"explorev2\":true,\"max_visible_peers\":true,\"parcel-denylist\":true,\"pick_realm_algorithm_config\":true,\"procedural_skybox\":true,\"rollout-unity-renderer-version\":true,\"social_bar_v1\":true,\"third_party_collections\":true,\"tutorial\":true,\"unsafe-request\":true,\"wearable_asset_bundles\":true},\"variants\":{\"banned_users\":{\"name\":\"banned_users\",\"payload\":{\"type\":\"json\",\"value\":\"{\"0x2d923d4846b958b19662c1b3d2c686b4b8b2aadf\": [{\"type\": \"VOICE_CHAT_AND_CHAT\", \"expiration\": 1642611240000}]}\"},\"enabled\":true},\"builder-dev\":{\"name\":\"builder-dev\",\"payload\":{\"type\":\"string\",\"value\":\"azul\"},\"enabled\":true},\"max_visible_peers\":{\"name\":\"max_visible_peers\",\"payload\":{\"type\":\"string\",\"value\":\"100\"},\"enabled\":true},\"parcel-denylist\":{\"name\":\"parcel-denylist\",\"payload\":{\"type\":\"string\",\"value\":\"-27,-47\"},\"enabled\":true},\"pick_realm_algorithm_config\":{\"name\":\"pick_realm_algorithm_config\",\"payload\":{\"type\":\"json\",\"value\":\"[\n   {\n      \"type\": \"LARGE_LATENCY\",\n      \"config\": {\n         \"largeLatencyThreshold\": 300\n      }\n   },\n   {\n      \"type\": \"ALL_PEERS_SCORE\"\n   },\n   {\n      \"type\": \"CLOSE_PEERS_SCORE\"\n   },\n   {\n      \"type\": \"LOAD_BALANCING\"\n   }\n]\"},\"enabled\":true},\"rollout-unity-renderer-version\":{\"name\":\"rollout-unity-renderer-version\",\"payload\":{\"type\":\"json\",\"value\":\"{\"resolved\": \"https://cdn.decentraland.org/@dcl/unity-renderer/1.0.7792\", \"version\": \"1.0.7792\" }\"},\"enabled\":true}}}"));
            OnMessage(MakeMessage("SetRenderProfile", "{\"id\":0}"));
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
        //    OnMessage("SetKernelConfiguration", "{"proceduralSkyboxConfig":{"configToLoad":"Generic_Skybox","lifecycleDuration":60,"fixedTime":-1,"disableReflection":false,"updateReflectionTime":-1},"enableNewTutorialCamera":true,"renderProfile":0,"pois":[{"x":0,"y":0},{"x":-62,"y":-61},{"x":-83,"y":0},{"x":-62,"y":62},{"x":0,"y":83},{"x":61,"y":62},{"x":61,"y":-61},{"x":0,"y":-81},{"x":-3,"y":-33},{"x":81,"y":0},{"x":-55,"y":143},{"x":58,"y":2},{"x":61,"y":-27},{"x":-49,"y":-41},{"x":36,"y":46},{"x":-71,"y":-38},{"x":-129,"y":-141},{"x":52,"y":2},{"x":-39,"y":58},{"x":59,"y":133},{"x":57,"y":8},{"x":-40,"y":-49},{"x":-12,"y":-39},{"x":-9,"y":73},{"x":87,"y":18},{"x":67,"y":-21},{"x":-75,"y":73},{"x":-15,"y":-22},{"x":-32,"y":-44},{"x":52,"y":16},{"x":-55,"y":1},{"x":-25,"y":103},{"x":52,"y":10},{"x":12,"y":46},{"x":-5,"y":-16},{"x":105,"y":-21},{"x":-11,"y":-30},{"x":-49,"y":-49},{"x":113,"y":-7},{"x":52,"y":-71},{"x":-43,"y":53},{"x":63,"y":2},{"x":-134,"y":-121},{"x":28,"y":45},{"x":137,"y":34},{"x":-43,"y":57},{"x":16,"y":83},{"x":59,"y":114},{"x":-40,"y":33},{"x":-69,"y":77},{"x":-48,"y":58},{"x":-35,"y":-42},{"x":24,"y":-126},{"x":-148,"y":-35},{"x":-109,"y":-89},{"x":66,"y":16},{"x":56,"y":22},{"x":-1,"y":-35},{"x":-43,"y":64},{"x":-101,"y":-70},{"x":139,"y":-79},{"x":22,"y":-17},{"x":-74,"y":-29},{"x":-75,"y":46},{"x":-66,"y":-29},{"x":45,"y":9},{"x":-150,"y":43},{"x":-21,"y":-40},{"x":127,"y":-133},{"x":-2,"y":-49},{"x":13,"y":-10},{"x":25,"y":2},{"x":33,"y":2},{"x":75,"y":-12},{"x":77,"y":-12},{"x":11,"y":95},{"x":60,"y":49},{"x":-19,"y":-6},{"x":-29,"y":-4},{"x":56,"y":7},{"x":-149,"y":-143},{"x":69,"y":21},{"x":55,"y":94},{"x":52,"y":93},{"x":52,"y":96},{"x":51,"y":99},{"x":56,"y":99},{"x":58,"y":92},{"x":98,"y":-88},{"x":-137,"y":96}],"comms":{"commRadius":4,"voiceChatEnabled":true},"profiles":{"nameValidCharacterRegex":"[a-zA-Z0-9]g","nameValidRegex":"^[a-zA-Z0-9]+$"},"gifSupported":false,"network":"mainnet","validWorldRanges":[{"xMin":-150,"yMin":-150,"xMax":150,"yMax":150},{"xMin":62,"yMin":151,"xMax":162,"yMax":158},{"xMin":151,"yMin":144,"xMax":162,"yMax":150},{"xMin":151,"yMin":59,"xMax":163,"yMax":143}],"kernelVersion":"1.0.0-2302764182.commit-cf8d532","rendererVersion":"1.0.35629-20220510163806.commit-ef34c50"}"));
       //     OnMessage("ConfigureHUDElement", "{"hudElementId":22,"configuration":{"active":true,"visible":false},"extraPayload":null}
       //     OnMessage("ConfigureHUDElement", "{"hudElementId":23,"configuration":{"active":true,"visible":true},"extraPayload":null}
            OnMessage(MakeMessage("CreateGlobalScene", "{\"id\":\"dcl-gs-avatars\",\"name\":\"Avatars\",\"baseUrl\":\"https://play.decentraland.zone\",\"isPortableExperience\":false,\"contents\":[]}"));

            OnMessage(MakeMessage("SetRotation","{\"x\":15.903898964861497,\"y\":106,\"z\":-5.222989489695095,\"cameraTarget\":{\"x\":14,\"y\":106,\"z\":40}}"));
            OnMessage(MakeMessage("ActivateRendering", null)); 
            OnMessage(MakeMessage("SetLoadingScreen","{\"isVisible\":false,\"message\":\"Loading scenes 99%\",\"showTips\":false}"));
            OnMessage(MakeMessage("ActivateRendering", null)); 
        }

        // Fake socket listen
        public void OnMessage(string message) {
            lock (AbeyCommunicationBridge.queuedMessages){ 
                
                DCLWebSocketService.Message finalMessage = JsonUtility.FromJson<DCLWebSocketService.Message>(message);

                AbeyCommunicationBridge.queuedMessages.Enqueue(finalMessage);
                AbeyCommunicationBridge.queuedMessagesDirty = true;
                
            }
        }

        public string MakeMessage(string type, string payload) => JsonUtility.ToJson(new DCLWebSocketService.Message{
               type     = type,
               payload  = payload
        });

       
        
     
     
    
    
    }
    
