

    using UnityEngine;
    using System.Collections;
    using ABEY;
    using DCL.Skybox;

    using WebSocketSharp;
    // this will be used as our main
    // Game controller for the whole app

    public class ABEYController : MonoSingleton<ABEYController>{
        /**
        Not sure what the diff is with world to unity, grid to position makes sense, and so dose world to local
        what do they mean by 'grid -> world to *unity' position, never seen anyone add an extra made up process
        **/   
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
        public CommonConfigScriptable               CommonScriptables       => abeyConfig.CommonScriptables;
        public NotificationConfigScriptable         NotificationScriptables => abeyConfig.NotificationScriptables;
       
        
        [Header("Resources Refs")]
        [SerializeField] PrefabRefsScriptableObject                 prefabRefs;
        [SerializeField] InputAction_TriggerRefsScriptableObject    inputActionTriggerRefs;
        [SerializeField] InputAction_HoldRefsScriptableObject       inputActionHoldRefs;
        [SerializeField] ColorListRefsScriptableObject              colorListRefs;
        [SerializeField] Texture2DRefsScriptableObject              texture2DRefs;
        [SerializeField] TextAssetRefsScriptableObject              textAssetRefs;
        [SerializeField] MaterialRefsScriptableObject               matetialRefs;
        [SerializeField] ShaderRefsScriptableObject                 shaderRefs;
        [SerializeField] ShaderVariantRefsScriptableObject          shaderVariantRefs;
        [SerializeField] OthersScriptableObject                     otherRefs;
        [SerializeField] SkyboxConfigurationRefsScriptableObject    skyboxConfigurationRefs;

        public GameObject           GetPrefab(string name)              => prefabRefs.GetPrefab(name);
        public SkyboxConfiguration  GetskyboxConfiguration(string name)  => skyboxConfigurationRefs.GetRef(name);
        public InputAction_Trigger  GetInputActionTrigger(string name)  => inputActionTriggerRefs.GetRef(name);
        public InputAction_Hold     GetInputActionHold(string name)     => inputActionHoldRefs.GetRef(name); 
        public ColorList            GetColorList(string name)           => colorListRefs.GetRef(name);   
        public Texture2D            GetTexture2D(string name)           => texture2DRefs.GetRef(name); 
        public TextAsset            GetTextAsset(string name)           => textAssetRefs.GetRef(name);  
        public Material             GetMaterial(string name)            => matetialRefs.GetRef(name);
        public Shader               GetShader(string name)              => shaderRefs.GetRef(name);
        public ShaderVariantCollection GetShaderVariant(string name)    => shaderVariantRefs.GetRef(name);
        public OthersScriptableObject OtherRefs                         => otherRefs;

        public SkyboxConfigurationRefsScriptableObject SkyboxConfiguration => skyboxConfigurationRefs;

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
            while(DCLCharacterController.i==null || !DCLCharacterController.i.enabled){
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

        void Update() {
            ((AbeyCommunicationBridge)DCL.Main.i.KernelCommunication).ProccessQueue();
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
    
