//non socket
using System;
using DCL;
using DCL.Interface;
using DCL.Models;
using Ray = DCL.Models.Ray;

//socket
//using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using DCL;
using UnityEngine;
using System.Threading.Tasks;
using WebSocketSharp;

namespace ABEY{

    public class Message {

        public string type;
        public string payload;
        public override string ToString() { return string.Format("type = {0}... payload = {1}...", type, payload); }
    }

    public class AbeyCommunicationBridge : IKernelCommunication {

        

        WebSocket ws;

        
        // ugly for now but is a huge fix - GameObject.Find like SendMessage is fucking slow, even docs for unity advise not using it unless you need to and know why
        GameObject _hudControllerGO = GameObject.Find("HUDController");
        GameObject _mainGO          = GameObject.Find("Main");
        GameObject hudControllerGO  => _hudControllerGO==null ? GameObject.Find("HUDController") : _hudControllerGO;
        GameObject mainGO           => Main.i.gameObject;
       

        /*****************
        * non socket
        * TODO: what are these really for and can we be smarter
        */
       static string currentEntityId;
       static string currentSceneId;
       static string currentTag;
       static IMessageQueueHandler queueHandler;
    
        public AbeyCommunicationBridge(IMessageQueueHandler _queueHandler){
            // do we need to queue messages, not sure why they cant be processed when recieved
            // currently its not used here, there is spots in this code that add to it
            // need to weed it all out
            queueHandler = _queueHandler; 

            ws = new WebSocket("ws://localhost:5000/abw");             
            ws.EmitOnPing   = true;
            ws.OnMessage    += OnMessage; 
            ws.OnError      += OnError;         
            ws.OnClose      += OnClose; 
            ws.Connect();

            DataStore.i.wsCommunication.url = "ws://localhost:5000/abw";
            DataStore.i.wsCommunication.communicationReady.Set(true);

            Application.quitting += () => Dispose();
        }

        public void FakeMessage(Message message){
            Debug.LogError($"<color=yellow>Fake Text</color> {message}");
            ProcessJsonMessage(message);
        }
        public void FakeMessage(string message){
            Debug.LogError($"<color=yellow>Fake Text</color> {message}");
            ProcessJsonMessage(message);
        }

        void OnMessage(object sender, MessageEventArgs e){            
            if (e.IsText==true) {
                //Debug.Log($"<color=green>Socket Text</color> {e.Data}");
                Message m = JsonUtility.FromJson<Message>(e.Data);
                if(m==null){
                    Debug.LogWarning($"Message is null -> {e.Data}"); 
                }
                ProcessJsonMessage(m);
                return;
            }
            if (e.IsBinary) {            
                Debug.Log($"<color=orange>Binary</color> {e.RawData.ToString()}");
                //  ProcessBinaryMessage(e.RawData); 
                return;
            }
            if (e.IsPing) {
                // Do something to notify that a ping has been received.
                Debug.Log($"<color=blue>Ping</color>");
                return;
            }
        }

        void OnError(object sender, ErrorEventArgs e){}
        void OnClose(object sender, CloseEventArgs e){}

        public void OpenNftDialog(string contactAddress, string comment, string tokenId){
            QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();

            Protocol.OpenNftDialog payload = new Protocol.OpenNftDialog {
                contactAddress  = contactAddress,
                comment         = comment,
                tokenId         = tokenId
            };

            queuedMessage.payload   = payload;
            queuedMessage.method    = MessagingTypes.OPEN_NFT_DIALOG;

            queueHandler.EnqueueSceneMessage(queuedMessage);
        }

        public void OpenExternalUrl(string url){
            QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();

            Protocol.OpenExternalUrl payload = new Protocol.OpenExternalUrl{
                url = url
            };

            queuedMessage.payload   = payload;
            queuedMessage.method    = MessagingTypes.OPEN_EXTERNAL_URL;

            queueHandler.EnqueueSceneMessage(queuedMessage);
        }

        public void EntityComponentDestroy(string name){
            QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();

            Protocol.EntityComponentDestroy payload = new Protocol.EntityComponentDestroy{
                entityId = currentEntityId,
                name     = name
            };

            queuedMessage.payload   = payload;
            queuedMessage.method    = MessagingTypes.ENTITY_COMPONENT_DESTROY;

            queueHandler.EnqueueSceneMessage(queuedMessage);
        }

        public void SharedComponentAttach(string id, string name){
            QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();

            Protocol.SharedComponentAttach payload = new Protocol.SharedComponentAttach{
                entityId    = currentEntityId,
                id          = id,
                name        = name
            };

            queuedMessage.payload = payload;
            queuedMessage.method = MessagingTypes.SHARED_COMPONENT_ATTACH;

            queueHandler.EnqueueSceneMessage(queuedMessage);
        }

        public static void Query(Protocol.QueryPayload payload){
            QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();

            string queryId = Convert.ToString(payload.raycastPayload.id);

            RaycastType raycastType = (RaycastType) payload.raycastPayload.raycastType;

            Ray ray = new Ray(){
                origin      = payload.raycastPayload.origin,
                direction   = payload.raycastPayload.direction,
                distance    = payload.raycastPayload.distance
            };

            queuedMessage.method = MessagingTypes.QUERY;
            queuedMessage.payload = new QueryMessage(){
                payload = new RaycastQuery(){
                    id          = queryId,
                    raycastType = raycastType,
                    ray         = ray,
                    sceneId     = currentSceneId
                }
            };

            queueHandler.EnqueueSceneMessage(queuedMessage);
        }

        public static void SharedComponentUpdate(string id, string json){
            QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();

            Protocol.SharedComponentUpdate payload =
                new Protocol.SharedComponentUpdate{
                    componentId = id,
                    json        = json
                };

            queuedMessage.payload   = payload;
            queuedMessage.method    = MessagingTypes.SHARED_COMPONENT_UPDATE;

            queueHandler.EnqueueSceneMessage(queuedMessage);
        }

        public static void SharedComponentDispose(string id) {
            QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();

            Protocol.SharedComponentDispose payload =
                new Protocol.SharedComponentDispose{
                    id = id
                };

            queuedMessage.payload   = payload;
            queuedMessage.method    = MessagingTypes.SHARED_COMPONENT_DISPOSE;

            queueHandler.EnqueueSceneMessage(queuedMessage);
        }

        
        public static void SharedComponentCreate(int classId, string id) {
            QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();

            Protocol.SharedComponentCreate payload =
                new Protocol.SharedComponentCreate{
                    id = id,
                    classId = classId
                };

            queuedMessage.payload = payload;
            queuedMessage.method = MessagingTypes.SHARED_COMPONENT_CREATE;

            queueHandler.EnqueueSceneMessage(queuedMessage);
        }

        public static void EntityComponentCreateOrUpdate(int classId, string json) {
            QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();

            Protocol.EntityComponentCreateOrUpdate payload =
                new Protocol.EntityComponentCreateOrUpdate{
                    entityId = currentEntityId,
                    classId = classId,
                    json = json
                };

            queuedMessage.payload = payload;
            queuedMessage.method = MessagingTypes.ENTITY_COMPONENT_CREATE_OR_UPDATE;

            queueHandler.EnqueueSceneMessage(queuedMessage);
        }

        public static void SetEntityParent(string parentId) {
            QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();

            Protocol.SetEntityParent payload =
                new Protocol.SetEntityParent {
                    entityId = currentEntityId,
                    parentId = parentId
                };

            queuedMessage.payload = payload;
            queuedMessage.method = MessagingTypes.ENTITY_REPARENT;

            queueHandler.EnqueueSceneMessage(queuedMessage);
        }

        public static void SetEntityId(string id) { currentEntityId = id; }

        public static void SetSceneId(string id) { currentSceneId = id; }

        public static void SetTag(string id) { currentTag = id; }

        public static void CreateEntity(){
            QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();

            Protocol.CreateEntity payload =
                new Protocol.CreateEntity{
                    entityId = currentEntityId
                };

            queuedMessage.payload = payload;
            queuedMessage.method = MessagingTypes.ENTITY_CREATE;

            queueHandler.EnqueueSceneMessage(queuedMessage);
        }

        public void RemoveEntity(){
            QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();
            Protocol.RemoveEntity payload =
                new Protocol.RemoveEntity() {
                    entityId = currentEntityId
                };

            queuedMessage.payload = payload;
            queuedMessage.method = MessagingTypes.ENTITY_DESTROY;

            queueHandler.EnqueueSceneMessage(queuedMessage);
        }

        public void SceneReady(){
            QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();
            queuedMessage.method = MessagingTypes.INIT_DONE;
            queuedMessage.payload = new Protocol.SceneReady();
            queueHandler.EnqueueSceneMessage(queuedMessage);
            
            ProcessJsonMessage(new Message(){type="SceneReady"});
        }

        internal static QueuedSceneMessage_Scene GetSceneMessageInstance() {
            var sceneMessagesPool = queueHandler.sceneMessagesPool;

            if (!sceneMessagesPool.TryDequeue(out QueuedSceneMessage_Scene message)) {
                message = new QueuedSceneMessage_Scene();
            }

            message.sceneId = currentSceneId;
            message.tag = currentTag;
            message.type = QueuedSceneMessage.Type.SCENE_MESSAGE;

            return message;
        }

        /**********
        * ws stuff
        */

       
        private bool requestStop = false;

      
        [System.NonSerialized]
        public static Queue<DCLWebSocketService.Message> queuedMessages = new Queue<DCLWebSocketService.Message>();

        [System.NonSerialized]
        public static volatile bool queuedMessagesDirty;

        public bool isServerReady => true;

    
        

        void ProcessJsonMessage(string json){
            #if!ABEY && UNITY_EDITOR
            ABEY.LogWriter.Write("Comms", $"Message {json}", 30);
            #endif
            //Debug.Log($"<color=orange>Message</color> {json}");
            ProcessJsonMessage(JsonUtility.FromJson<Message>(json));
        }
        void ProcessJsonMessage(Message msg){           
            Debug.Log($"<color=orange>ProcessJsonMessage</color> : {msg.type}");
            switch (msg.type) {
                // Add to this list the messages that are used a lot and you want better performance
                case "SendSceneMessage"             : HandleMessage.SendSceneMessage(msg.payload);              break;
                case "Reset"                        : HandleMessage.Reset();                                    break;
                case "SetVoiceChatEnabledByScene"   : HandleMessage.SetVoiceChatEnabledByScene(msg);            break;
                case "RunPerformanceMeterTool"      : HandleMessage.RunPerformanceMeterTool(msg);               break;
                //HUDController
                case "ConfigureHUDElement"          : HandleHUDMessage.ConfigureHUDElement(msg.payload);        break;
                case "ShowTermsOfServices"          : HandleHUDMessage.ShowTermsOfServices(msg.payload);        break;
                case "RequestTeleport"              : HandleHUDMessage.RequestTeleport(msg.payload);            break;
                case "ShowAvatarEditorInSignUp"     : HandleHUDMessage.ShowAvatarEditorInSignUp();              break;
                case "SetUserTalking"               : HandleHUDMessage.SetUserTalking(msg.payload);             break;
                case "SetUsersMuted"                : HandleHUDMessage.SetUsersMuted(msg.payload);              break;
                case "UpdateBalanceOfMANA"          : HandleHUDMessage.UpdateBalanceOfMANA(msg.payload);        break;
                case "SetPlayerTalking"             : HandleHUDMessage.SetPlayerTalking(msg.payload);           break;
                case "TriggerSelfUserExpression"    : HandleHUDMessage.TriggerSelfUserExpression(msg.payload);  break;
                case "AirdroppingRequest"           : HandleHUDMessage.AirdroppingRequest(msg.payload);         break;

                default                             : HandleMessage.Default(msg); break;
            }                                 
                    
        }

        public void Dispose() {
            Debug.Log("Dispose");
            ws.OnMessage    -= OnMessage; 
            ws.OnError      -= OnError;         
            ws.OnClose      -= OnClose; 
            ws.Close();
        }

        public void ProccessQueue(){
            // due to the use of unity's SendMessage with sockets we run into the bug of running SendMessage in the wrong thread which fails quitely
            // for now we will use the same slow ass queueing proccess they did till I rewrite this garbage
            if(HandleMessage.messages.Count>0){
                HandleMessage.Process(HandleMessage.messages.Dequeue());
            }if(HandleHUDMessage.messages.Count>0){
                HandleHUDMessage.ConfigureHUDElementMessage h =  HandleHUDMessage.messages.Dequeue();
                HUDController.i.ConfigureHUDElement( h.hudElementId,  h.configuration,  h.extraPayload);
            }
            
        }

    }

    // The main messeging handling
    public class HandleMessage{
        // due to the use of unity's SendMessage with sockets we run into the bug of running SendMessage in the wrong thread which fails quitely
        // for now we will use the same slow ass queueing proccess they did till I rewrite this garbage
        public static Queue<Message> messages = new Queue<Message>();

        // this is a cached list of gameobjects that handle messages
        // i need to see if there is a better way, current this is ok but its the least prefomant way to do it
        // its stupid simple though
        static Dictionary<string, GameObject> bridgeGameObjects = new Dictionary<string, GameObject>();

        // Public to be able to modify it from `explorer-desktop` <- I dont understand the thinking going on in the head of these coders.....
        // this is a message map to gameobject, it enforce who gets messeges and what, kind of removes the dynamicness and makes bridgeGameObjects look lazy coding as shit
        // this is the slow message handling messegs
        public static Dictionary<string, string> messageTypeToBridgeName = new Dictionary<string, string>(){
            //DebugBridge
            {"SetDebug",                         "Main"}, // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            {"SetSceneDebugPanel",               "Main"}, // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            {"ShowFPSPanel",                     "Main"}, // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            {"HideFPSPanel",                     "Main"}, // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            {"SetEngineDebugPanel",              "Main"}, // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            {"CrashPayloadRequest",              "Main"}, // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            {"SetDisableAssetBundles",           "Main"}, // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            {"DumpRendererLockersInfo",          "Main"}, // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            //SceneController
            {"CreateGlobalScene",                "Main"}, // SceneControllerBridge Environment.Model [new ServiceLocator()]. -> SceneController Assets\Scripts\MainScripts\DCL\WorldRuntime\SceneController.cs
            {"LoadParcelScenes",                 "Main"}, // SceneControllerBridge Environment.Model [new ServiceLocator()]. -> SceneController Assets\Scripts\MainScripts\DCL\WorldRuntime\SceneController.cs
            {"UpdateParcelScenes",               "Main"}, // SceneControllerBridge Environment.Model [new ServiceLocator()]. -> SceneController Assets\Scripts\MainScripts\DCL\WorldRuntime\SceneController.cs
            {"UnloadScene",                      "Main"}, // SceneControllerBridge Environment.Model [new ServiceLocator()]. -> SceneController Assets\Scripts\MainScripts\DCL\WorldRuntime\SceneController.cs
            //UserProfileController
            {"LoadProfile",                      "Main"}, // UserProfileController Assets\Scripts\MainScripts\DCL\UserProfile\UserProfileController.cs
            {"AddUserProfileToCatalog",          "Main"}, // UserProfileController Assets\Scripts\MainScripts\DCL\UserProfile\UserProfileController.cs
            {"AddUserProfilesToCatalog",         "Main"}, // UserProfileController Assets\Scripts\MainScripts\DCL\UserProfile\UserProfileController.cs
            {"RemoveUserProfilesFromCatalog",    "Main"}, // UserProfileController Assets\Scripts\MainScripts\DCL\UserProfile\UserProfileController.cs
            //RenderingController
            {"ActivateRendering",                "Main"}, // RenderingController Assets\Scripts\MainScripts\DCL\Controllers\Rendering\RenderingController.cs
            {"DeactivateRendering",              "Main"}, // RenderingController Assets\Scripts\MainScripts\DCL\Controllers\Rendering\RenderingController.cs
            {"ForceActivateRendering",           "Main"}, // RenderingController Assets\Scripts\MainScripts\DCL\Controllers\Rendering\RenderingController.cs
            //CatalogController
            {"AddWearablesToCatalog",            "Main"}, // CatalogController Assets\Scripts\MainScripts\DCL\Controllers\CatalogController\CatalogController.cs
            {"WearablesRequestFailed",           "Main"}, // CatalogController Assets\Scripts\MainScripts\DCL\Controllers\CatalogController\CatalogController.cs
            {"RemoveWearablesFromCatalog",       "Main"}, // CatalogController Assets\Scripts\MainScripts\DCL\Controllers\CatalogController\CatalogController.cs
            {"ClearWearableCatalog",             "Main"}, // CatalogController Assets\Scripts\MainScripts\DCL\Controllers\CatalogController\CatalogController.cs
            //FriendsController
            {"InitializeFriends",                "Main"}, // FriendsController Assets\Scripts\MainScripts\DCL\Controllers\FriendsController\FriendsController.cs
            {"UpdateFriendshipStatus",           "Main"}, // FriendsController Assets\Scripts\MainScripts\DCL\Controllers\FriendsController\FriendsController.cs
            {"UpdateUserPresence",               "Main"}, // FriendsController Assets\Scripts\MainScripts\DCL\Controllers\FriendsController\FriendsController.cs
            {"FriendNotFound",                   "Main"}, // FriendsController Assets\Scripts\MainScripts\DCL\Controllers\FriendsController\FriendsController.cs
            //ChatController
            {"AddMessageToChatWindow",           "Main"}, // ChatController Assets\Scripts\MainScripts\DCL\Controllers\ChatController\ChatController.cs
            //MinimapMetadataController
            {"UpdateMinimapSceneInformation",    "Main"}, // MinimapMetadataController
            //HotScenesController
            {"UpdateHotScenesList",              "Main"}, // HotScenesController Assets\Scripts\MainScripts\DCL\Controllers\HotScenesController\HotScenesController.cs
            //RenderProfileBridge <- not used or should not be, comments say in favor of skybox
            {"SetRenderProfile",                 "Main"}, // RenderProfileBridge
            {"PublishSceneResult",               "Main"},
            {"BuilderProjectInfo",               "Main"},
            {"BuilderInWorldCatalogHeaders",     "Main"},
            {"RequestedHeaders",                 "Main"},
            {"AddAssets",                        "Main"},
            {"InstantiateBotsAtWorldPos",        "Main"},
            {"InstantiateBotsAtCoords",          "Main"},
            {"StartBotsRandomizedMovement",      "Main"},
            {"StopBotsMovement",                 "Main"},
            {"RemoveBot",                        "Main"},
            {"ClearBots",                        "Main"},
            {"ToggleSceneBoundingBoxes",         "Main"},
            {"TogglePreviewMenu",                "Main"},
            {"ToggleSceneSpawnPoints",           "Main"},
            //HUDController
            {"ShowWelcomeNotification",         "HUDController"},
            {"ShowNotificationFromJson",        "HUDController"},

            {"Teleport",     "CharacterController"},
            {"SetRotation",  "CameraController"},
            //BuilderController
            // in main but opens Builder so part of the builder group flow
            {"BuilderReady",             "Main"}, // SceneControllerBridge Assets\Scripts\MainScripts\DCL\WorldRuntime\Bridge\SceneControllerBridge.cs note this just loads scene "Aditive" i.e on top of current loaded scene        
            {"GetMousePosition",         "BuilderController"},
            {"SelectGizmo",              "BuilderController"},
            {"ResetObject",              "BuilderController"},
            {"ZoomDelta",                "BuilderController"},
            {"SetPlayMode",              "BuilderController"},
            {"TakeScreenshot",           "BuilderController"},
            {"ResetBuilderScene",        "BuilderController"},
            {"SetBuilderCameraPosition", "BuilderController"},
            {"SetBuilderCameraRotation", "BuilderController"},
            {"ResetBuilderCameraZoom",   "BuilderController"},
            {"SetGridResolution",        "BuilderController"},
            {"OnBuilderKeyDown",         "BuilderController"},
            {"UnloadBuilderScene",       "BuilderController"},
            {"SetSelectedEntities",      "BuilderController"},
            {"GetCameraTargetBuilder",   "BuilderController"},
            {"PreloadFile",              "BuilderController"},
            {"SetBuilderConfiguration",  "BuilderController"},
            {"SetTutorialEnabled",                                  "TutorialController"},
            {"SetTutorialEnabledForUsersThatAlreadyDidTheTutorial",  "TutorialController"}
        }; 

        // Direct message handing calls
        public static void SendSceneMessage(string payload) => DCL.Environment.i.world.sceneController.SendSceneMessage(payload);
        public static void Reset() => DCL.Environment.i.world.sceneController.UnloadAllScenesQueued();
        
        public static void SetVoiceChatEnabledByScene(Message msg){
            if (int.TryParse(msg.payload, out int value)) {
                HUDController.i.taskbarHud?.SetVoiceChatEnabledByScene((value!=0));
            }
        }
        public static void RunPerformanceMeterTool(Message msg){
            if (float.TryParse(msg.payload, out float durationInSeconds))  {
                Main.i.gameObject.SendMessage(msg.type, durationInSeconds);
            }
        }
        
        // due to the use of unity's SendMessage with sockets we run into the bug of running SendMessage in the wrong thread which fails quitely
        // for now we will use the same slow ass queueing proccess they did till I rewrite this garbage
        public static void Default(Message msg){
            messages.Enqueue(msg);
        }
        // The slow way - if not direct check for in dict and then run slow gameobject serching send/reflection
        public static void Process(Message msg){
            Debug.Log($"DEFAULT: {msg.type}");
            string bridgeName = "Bridges"; // Default bridge
            messageTypeToBridgeName.TryGetValue(msg.type, out bridgeName);
            if(bridgeName==null){
                Debug.Log($"{msg.type} No Object for this message");
                bridgeName = "Bridges";
            }
            // See if we cached the object, if not Search entire scene 'All gameObjects' and cache it
            if (bridgeGameObjects.TryGetValue(bridgeName, out GameObject bridgeObject) == false) {
                bridgeObject = GameObject.Find(bridgeName);
                if(bridgeObject==null){Debug.Log($"No GO in scene for name {bridgeName}"); }
                bridgeGameObjects.Add(bridgeName, bridgeObject); // caches null if not found - this could cause a bug if does not exsits yet
            }

            // use the slow 'send' message to call the method
            if (bridgeObject != null) {
                Debug.Log($"SendMessage({msg.type}, {msg.payload})");
                bridgeObject.SendMessage(msg.type, msg.payload);
            }
        }
    }


    // HUD messeging handling
    public class HandleHUDMessage{
        public static Queue<ConfigureHUDElementMessage> messages = new Queue<ConfigureHUDElementMessage>();

        [System.Serializable]
        public class ConfigureHUDElementMessage {
            public HUDElementID hudElementId;
            public HUDConfiguration configuration;
            public string extraPayload;
        }

        public static void ConfigureHUDElement(string payload)  {
            ConfigureHUDElementMessage message = JsonUtility.FromJson<ConfigureHUDElementMessage>(payload);
            if(message==null){
                Debug.LogError($"failed {payload}");
            }
            messages.Enqueue(message);
            //HUDController.i.ConfigureHUDElement(message.hudElementId, message.configuration, message.extraPayload);
        }

        public static void TriggerSelfUserExpression(string id)             => UserProfile.GetOwnUserProfile().SetAvatarExpression(id); 
        public static void AirdroppingRequest(string payload)               => HUDController.i.airdroppingHud.AirdroppingRequested(JsonUtility.FromJson<AirdroppingHUDController.Model>(payload));
        public static void ShowTermsOfServices(string payload)              => HUDController.i.termsOfServiceHud?.ShowTermsOfService(JsonUtility.FromJson<TermsOfServiceHUDController.Model>(payload));
        public static void SetPlayerTalking(string talking)                 => HUDController.i.taskbarHud?.SetVoiceChatRecording("true".Equals(talking)); 
        public static void SetVoiceChatEnabledByScene(int enabledPayload)   => HUDController.i.taskbarHud?.SetVoiceChatEnabledByScene((enabledPayload != 0));
        public static void RequestTeleport(string teleportDataJson)         => HUDController.i.teleportHud?.RequestTeleport(teleportDataJson);
        public static void UpdateBalanceOfMANA(string balance)              => HUDController.i.profileHud?.SetManaBalance(balance);

        public static void SetUserTalking(string payload) {
            UserTalkingModel model = JsonUtility.FromJson<UserTalkingModel>(payload);
            HUDController.i.usersAroundListHud?.SetUserRecording(model.userId, model.talking);
        }

        public static void SetUsersMuted(string payload) {
            UserMutedModel model = JsonUtility.FromJson<UserMutedModel>(payload);
            HUDController.i.usersAroundListHud?.SetUsersMuted(model.usersId, model.muted);
        }

        

        public static void ShowAvatarEditorInSignUp(){
            if (HUDController.i.avatarEditorHud != null){
                DataStore.i.common.isSignUpFlow.Set(true);
                HUDController.i.avatarEditorHud?.SetVisibility(true);
            }
        }

    
    }
    




}