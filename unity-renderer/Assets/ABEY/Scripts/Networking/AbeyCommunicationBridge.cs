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

    public class AbeyCommunicationBridge : IKernelCommunication {

        public class Message {
            public string type;
            public string payload;
            public override string ToString() { return string.Format("type = {0}... payload = {1}...", type, payload); }
        }

        WebSocket ws;

        // this is a cached list of gameobjects that handle messages
        // i need to see if there is a better way, current this is ok but its the least prefomant way to do it
        // its stupid simple though
        Dictionary<string, GameObject> bridgeGameObjects = new Dictionary<string, GameObject>();

        // Public to be able to modify it from `explorer-desktop` <- I dont understand the thinking going on in the head of thses coders.....
        // this is a message map to gameobject, it enforce who gets messeges and what, kind of removes the dynamicness and makes bridgeGameObjects look lazy coding as shit
        public Dictionary<string, string> messageTypeToBridgeName = new Dictionary<string, string>(); 


        // ugly for now but is a huge fix - GameObject.Find like SendMessage is fucking slow, even docs for unity advise not using it unless you need to and know why
        GameObject _hudControllerGO = GameObject.Find("HUDController");
        GameObject _mainGO          = GameObject.Find("Main");
        GameObject hudControllerGO  => _hudControllerGO==null ? GameObject.Find("HUDController") : _hudControllerGO;
        GameObject mainGO           => _mainGO==null ? GameObject.Find("Main") : _mainGO;
       

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
            InitMessageTypeToBridgeName(); // this Bridge idea is really bad, and the nameing here has nothing to do whith the Bridge Desgin used else where, all brides need to go, its way over complacated then it needs to be

            ws.EmitOnPing   = true;
            ws.OnMessage    += OnMessage; 
            ws.OnError      += OnError;         
            ws.OnClose      += OnClose; 
            ws.Connect();
        }

        public void FakeMessage(string message){
            Debug.Log($"<color=yelloe>Fake Text</color> {message}");
            ProcessJsonMessage(message);
        }

        void OnMessage(object sender, MessageEventArgs e){            
            if (e.IsText==true) {
                Debug.Log($"<color=green>Socket Text</color> {e.Data}");
                ProcessJsonMessage(e.Data);
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

    

        void InitMessageTypeToBridgeName() {
            /*A.B THIS IS THE WORST POSIBLE WAY TO HANDLE THIS, 
            Game Objects do get cached though, which offsets how bad this is
            Unity SendMessage is then used to pass the call on and is super slow
            it is however the simplest way yo make the messaging as dynamic as possible

            logic flow
            messageTypeToBridgeName["method"] = "GameObject Name (has to match in scene)";
            1. check if we added the GameObject to the Dictionary
                a. if not search 'ALL' objects in scene til we find first object with name that matches
                b. add found object or null to the cache Dictionary
            2. then use SendMessege on game object to call the method on one of its componenets - this uses reflection under the hood and other things which make it so heavy
            
            */
            // Please, use `Bridges` as a bridge name, avoid adding messages here. The system will use `Bridges` as the default bridge name.
            // see Assets\Scripts\MainScripts\DCL\Environment\Factories\ServiceLocatorFactory\ServiceLocatorFactory.cs for some of the bridge reg of classes       
  
            //DebugBridge
            messageTypeToBridgeName["SetDebug"]                         = "Main"; // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            messageTypeToBridgeName["SetSceneDebugPanel"]               = "Main"; // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            messageTypeToBridgeName["ShowFPSPanel"]                     = "Main"; // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            messageTypeToBridgeName["HideFPSPanel"]                     = "Main"; // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            messageTypeToBridgeName["SetEngineDebugPanel"]              = "Main"; // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            messageTypeToBridgeName["CrashPayloadRequest"]              = "Main"; // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            messageTypeToBridgeName["SetDisableAssetBundles"]           = "Main"; // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            messageTypeToBridgeName["DumpRendererLockersInfo"]          = "Main"; // DebugBridge Assets\Scripts\MainScripts\DCL\Controllers\DebugController\DebugBridge.cs
            //SceneController
            messageTypeToBridgeName["SendSceneMessage"]                 = "Main"; // SceneControllerBridge Environment.Model [new ServiceLocator()]. -> SceneController Assets\Scripts\MainScripts\DCL\WorldRuntime\SceneController.cs
            messageTypeToBridgeName["CreateGlobalScene"]                = "Main"; // SceneControllerBridge Environment.Model [new ServiceLocator()]. -> SceneController Assets\Scripts\MainScripts\DCL\WorldRuntime\SceneController.cs
            messageTypeToBridgeName["LoadParcelScenes"]                 = "Main"; // SceneControllerBridge Environment.Model [new ServiceLocator()]. -> SceneController Assets\Scripts\MainScripts\DCL\WorldRuntime\SceneController.cs
            messageTypeToBridgeName["UpdateParcelScenes"]               = "Main"; // SceneControllerBridge Environment.Model [new ServiceLocator()]. -> SceneController Assets\Scripts\MainScripts\DCL\WorldRuntime\SceneController.cs
            messageTypeToBridgeName["UnloadScene"]                      = "Main"; // SceneControllerBridge Environment.Model [new ServiceLocator()]. -> SceneController Assets\Scripts\MainScripts\DCL\WorldRuntime\SceneController.cs
            messageTypeToBridgeName["Reset"]                            = "Main";
            //UserProfileController
            messageTypeToBridgeName["LoadProfile"]                      = "Main"; // UserProfileController Assets\Scripts\MainScripts\DCL\UserProfile\UserProfileController.cs
            messageTypeToBridgeName["AddUserProfileToCatalog"]          = "Main"; // UserProfileController Assets\Scripts\MainScripts\DCL\UserProfile\UserProfileController.cs
            messageTypeToBridgeName["AddUserProfilesToCatalog"]         = "Main"; // UserProfileController Assets\Scripts\MainScripts\DCL\UserProfile\UserProfileController.cs
            messageTypeToBridgeName["RemoveUserProfilesFromCatalog"]    = "Main"; // UserProfileController Assets\Scripts\MainScripts\DCL\UserProfile\UserProfileController.cs
            //RenderingController
            messageTypeToBridgeName["ActivateRendering"]                = "Main"; // RenderingController Assets\Scripts\MainScripts\DCL\Controllers\Rendering\RenderingController.cs
            messageTypeToBridgeName["DeactivateRendering"]              = "Main"; // RenderingController Assets\Scripts\MainScripts\DCL\Controllers\Rendering\RenderingController.cs
            messageTypeToBridgeName["ForceActivateRendering"]           = "Main"; // RenderingController Assets\Scripts\MainScripts\DCL\Controllers\Rendering\RenderingController.cs
            //CatalogController
            messageTypeToBridgeName["AddWearablesToCatalog"]            = "Main"; // CatalogController Assets\Scripts\MainScripts\DCL\Controllers\CatalogController\CatalogController.cs
            messageTypeToBridgeName["WearablesRequestFailed"]           = "Main"; // CatalogController Assets\Scripts\MainScripts\DCL\Controllers\CatalogController\CatalogController.cs
            messageTypeToBridgeName["RemoveWearablesFromCatalog"]       = "Main"; // CatalogController Assets\Scripts\MainScripts\DCL\Controllers\CatalogController\CatalogController.cs
            messageTypeToBridgeName["ClearWearableCatalog"]             = "Main"; // CatalogController Assets\Scripts\MainScripts\DCL\Controllers\CatalogController\CatalogController.cs
            //FriendsController
            messageTypeToBridgeName["InitializeFriends"]                = "Main"; // FriendsController Assets\Scripts\MainScripts\DCL\Controllers\FriendsController\FriendsController.cs
            messageTypeToBridgeName["UpdateFriendshipStatus"]           = "Main"; // FriendsController Assets\Scripts\MainScripts\DCL\Controllers\FriendsController\FriendsController.cs
            messageTypeToBridgeName["UpdateUserPresence"]               = "Main"; // FriendsController Assets\Scripts\MainScripts\DCL\Controllers\FriendsController\FriendsController.cs
            messageTypeToBridgeName["FriendNotFound"]                   = "Main"; // FriendsController Assets\Scripts\MainScripts\DCL\Controllers\FriendsController\FriendsController.cs
            //ChatController
            messageTypeToBridgeName["AddMessageToChatWindow"]           = "Main"; // ChatController Assets\Scripts\MainScripts\DCL\Controllers\ChatController\ChatController.cs
            //MinimapMetadataController
            messageTypeToBridgeName["UpdateMinimapSceneInformation"]    = "Main"; // MinimapMetadataController
            //HotScenesController
            messageTypeToBridgeName["UpdateHotScenesList"]              = "Main"; // HotScenesController Assets\Scripts\MainScripts\DCL\Controllers\HotScenesController\HotScenesController.cs

            //RenderProfileBridge <- not used or should not be, comments say in favor of skybox
            messageTypeToBridgeName["SetRenderProfile"]                 = "Main"; // RenderProfileBridge
            
            messageTypeToBridgeName["PublishSceneResult"]               = "Main";
            messageTypeToBridgeName["BuilderProjectInfo"]               = "Main";
            messageTypeToBridgeName["BuilderInWorldCatalogHeaders"]     = "Main";
            messageTypeToBridgeName["RequestedHeaders"]                 = "Main";
            messageTypeToBridgeName["AddAssets"]                        = "Main";
            messageTypeToBridgeName["RunPerformanceMeterTool"]          = "Main";
            messageTypeToBridgeName["InstantiateBotsAtWorldPos"]        = "Main";
            messageTypeToBridgeName["InstantiateBotsAtCoords"]          = "Main";
            messageTypeToBridgeName["StartBotsRandomizedMovement"]      = "Main";
            messageTypeToBridgeName["StopBotsMovement"]                 = "Main";
            messageTypeToBridgeName["RemoveBot"]                        = "Main";
            messageTypeToBridgeName["ClearBots"]                        = "Main";
            messageTypeToBridgeName["ToggleSceneBoundingBoxes"]         = "Main";
            messageTypeToBridgeName["TogglePreviewMenu"]                = "Main";
            messageTypeToBridgeName["ToggleSceneSpawnPoints"]           = "Main";
            

            messageTypeToBridgeName["Teleport"]     = "CharacterController";

            messageTypeToBridgeName["SetRotation"]  = "CameraController";

            messageTypeToBridgeName["ShowNotificationFromJson"]     = "HUDController";
            messageTypeToBridgeName["ConfigureHUDElement"]          = "HUDController";
            messageTypeToBridgeName["ShowTermsOfServices"]          = "HUDController";
            messageTypeToBridgeName["RequestTeleport"]              = "HUDController";
            messageTypeToBridgeName["ShowAvatarEditorInSignUp"]     = "HUDController";
            messageTypeToBridgeName["SetUserTalking"]               = "HUDController";
            messageTypeToBridgeName["SetUsersMuted"]                = "HUDController";
            messageTypeToBridgeName["ShowWelcomeNotification"]      = "HUDController";
            messageTypeToBridgeName["UpdateBalanceOfMANA"]          = "HUDController";
            messageTypeToBridgeName["SetPlayerTalking"]             = "HUDController";
            messageTypeToBridgeName["SetVoiceChatEnabledByScene"]   = "HUDController";
            messageTypeToBridgeName["TriggerSelfUserExpression"]    = "HUDController";
            messageTypeToBridgeName["AirdroppingRequest"]           = "HUDController";

            // in main but opens Builder so part of the builder group flow
            messageTypeToBridgeName["BuilderReady"]             = "Main"; // SceneControllerBridge Assets\Scripts\MainScripts\DCL\WorldRuntime\Bridge\SceneControllerBridge.cs note this just loads scene "Aditive" i.e on top of current loaded scene        
            messageTypeToBridgeName["GetMousePosition"]         = "BuilderController";
            messageTypeToBridgeName["SelectGizmo"]              = "BuilderController";
            messageTypeToBridgeName["ResetObject"]              = "BuilderController";
            messageTypeToBridgeName["ZoomDelta"]                = "BuilderController";
            messageTypeToBridgeName["SetPlayMode"]              = "BuilderController";
            messageTypeToBridgeName["TakeScreenshot"]           = "BuilderController";
            messageTypeToBridgeName["ResetBuilderScene"]        = "BuilderController";
            messageTypeToBridgeName["SetBuilderCameraPosition"] = "BuilderController";
            messageTypeToBridgeName["SetBuilderCameraRotation"] = "BuilderController";
            messageTypeToBridgeName["ResetBuilderCameraZoom"]   = "BuilderController";
            messageTypeToBridgeName["SetGridResolution"]        = "BuilderController";
            messageTypeToBridgeName["OnBuilderKeyDown"]         = "BuilderController";
            messageTypeToBridgeName["UnloadBuilderScene"]       = "BuilderController";
            messageTypeToBridgeName["SetSelectedEntities"]      = "BuilderController";
            messageTypeToBridgeName["GetCameraTargetBuilder"]   = "BuilderController";
            messageTypeToBridgeName["PreloadFile"]              = "BuilderController";
            messageTypeToBridgeName["SetBuilderConfiguration"]  = "BuilderController";

            messageTypeToBridgeName["SetTutorialEnabled"]                                   = "TutorialController";
            messageTypeToBridgeName["SetTutorialEnabledForUsersThatAlreadyDidTheTutorial"]  = "TutorialController";
        }

        

        void ProcessJsonMessage(string json){
            #if!ABEY && UNITY_EDITOR
            ABEY.LogWriter.Write("Comms", $"Message {json}", 30);
            #endif
            Debug.Log($"<color=orange>Message</color> {json}");
            ProcessJsonMessage(JsonUtility.FromJson<Message>(json));
        }
        void ProcessJsonMessage(Message msg){           
        
            switch (msg.type) {
                // Add to this list the messages that are used a lot and you want better performance
                case "SendSceneMessage":
                    DCL.Environment.i.world.sceneController.SendSceneMessage(msg.payload);
                    break;
                case "Reset":
                    DCL.Environment.i.world.sceneController.UnloadAllScenesQueued();
                    break;
                case "SetVoiceChatEnabledByScene":// The payload should be `string`, this will be changed in a `renderer-protocol` refactor
                    if (int.TryParse(msg.payload, out int value)) {
                        hudControllerGO.SendMessage(msg.type, value);
                    }
                    break;
                case "RunPerformanceMeterTool":// The payload should be `string`, this will be changed in a `renderer-protocol` refactor
                    if (float.TryParse(msg.payload, out float durationInSeconds))  {
                        mainGO.SendMessage(msg.type, durationInSeconds);
                    }
                    break;
                default:
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
                        bridgeObject.SendMessage(msg.type, msg.payload);
                    }
                    break;
            }                                 
                    
        }

        public void Dispose() {
            ws.OnMessage    -= OnMessage; 
            ws.OnError      -= OnError;         
            ws.OnClose      -= OnClose; 
            ws.Close();
        }

    }

}