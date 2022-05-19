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
//using System.Net.Sockets;
//using DCL;
using UnityEngine;
//using WebSocketSharp.Server;

namespace ABEY{

public class AbeyCommunicationBridge : IKernelCommunication {

    public void Dispose() {}

    /*****************
    * non socket
    */
    private static string currentEntityId;
    private static string currentSceneId;
    private static string currentTag;
    private static IMessageQueueHandler queueHandler;
 
    public AbeyCommunicationBridge(IMessageQueueHandler _queueHandler){
        queueHandler = _queueHandler;    
        WebSocketMessageInit();
    }

    public static void OpenNftDialog(string contactAddress, string comment, string tokenId){
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

    public static void OpenExternalUrl(string url){
        QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();

        Protocol.OpenExternalUrl payload = new Protocol.OpenExternalUrl{
            url = url
        };

        queuedMessage.payload   = payload;
        queuedMessage.method    = MessagingTypes.OPEN_EXTERNAL_URL;

        queueHandler.EnqueueSceneMessage(queuedMessage);
    }

    public static void EntityComponentDestroy(string name){
        QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();

        Protocol.EntityComponentDestroy payload = new Protocol.EntityComponentDestroy{
            entityId = currentEntityId,
            name     = name
        };

        queuedMessage.payload   = payload;
        queuedMessage.method    = MessagingTypes.ENTITY_COMPONENT_DESTROY;

        queueHandler.EnqueueSceneMessage(queuedMessage);
    }

    public static void SharedComponentAttach(string id, string name){
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

    public static void RemoveEntity(){
        QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();
        Protocol.RemoveEntity payload =
            new Protocol.RemoveEntity() {
                entityId = currentEntityId
            };

        queuedMessage.payload = payload;
        queuedMessage.method = MessagingTypes.ENTITY_DESTROY;

        queueHandler.EnqueueSceneMessage(queuedMessage);
    }

    public static void SceneReady(){
        QueuedSceneMessage_Scene queuedMessage = GetSceneMessageInstance();
        queuedMessage.method = MessagingTypes.INIT_DONE;
        queuedMessage.payload = new Protocol.SceneReady();

        queueHandler.EnqueueSceneMessage(queuedMessage);
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

    private Coroutine updateCoroutine;
    private bool requestStop = false;

    private Dictionary<string, GameObject> bridgeGameObjects = new Dictionary<string, GameObject>();
    public Dictionary<string, string> messageTypeToBridgeName = new Dictionary<string, string>(); // Public to be able to modify it from `explorer-desktop`

    [System.NonSerialized]
    public static Queue<DCLWebSocketService.Message> queuedMessages = new Queue<DCLWebSocketService.Message>();

    [System.NonSerialized]
    public static volatile bool queuedMessagesDirty;

    public bool isServerReady => true;

  
    public void WebSocketMessageInit() {
        InitMessageTypeToBridgeName();

        updateCoroutine = CoroutineStarter.Start(ProcessMessages());
    }

    private void InitMessageTypeToBridgeName() {
        //A.B THIS IS THE WORST POSIBLE WAY TO HANDLE THIS, I Believe monos get cahced to offset how bad this is which might be exceptable after the fact but send message is super slow, more so when there is a ton of objects in scene
        //["method"] = "GameObject";
        // first it searches entire scene for gameobject if found puts it in a dict, this uses slow send message to call the method on any component that has a method with same name

        // Please, use `Bridges` as a bridge name, avoid adding messages here. The system will use `Bridges` as the default bridge name.
        // see Assets\Scripts\MainScripts\DCL\Environment\Factories\ServiceLocatorFactory\ServiceLocatorFactory.cs for some of the bridge reg of classes       
/* components on main to seach for these methods, some of these may add more componets
UserProfileController
RenderingController
CatalogController
MinimapMetadataControl
ChatController
FriendsController
HotScenesController
GIFProcessingBridge
RenderProfileBridge
AssetCatalogBridge
ScreenSizeWatcher
SceneControllerBridge
*/
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

    // ugly for now but is a huge fix
    GameObject _hudControllerGO = GameObject.Find("HUDController");
    GameObject _mainGO          = GameObject.Find("Main");
    GameObject hudControllerGO  => _hudControllerGO==null ? GameObject.Find("HUDController") : _hudControllerGO;
    GameObject mainGO           => _mainGO==null ? GameObject.Find("Main") : _mainGO;

    IEnumerator ProcessMessages()
    {
        // this is really dumb and super ineficient - fix above
        //var hudControllerGO = GameObject.Find("HUDController");
        //var mainGO = GameObject.Find("Main");

        while (!requestStop)
        {
            lock (queuedMessages)
            {
                if (queuedMessagesDirty)
                {
                    while (queuedMessages.Count > 0)
                    {
                        DCLWebSocketService.Message msg = queuedMessages.Dequeue();
                        #if!ABEY && UNITY_EDITOR
                        ABEY.LogWriter.Write("Comms", $"Message {msg.ToJson()}", 30);
                        #endif
                        switch (msg.type)
                        {
                            // Add to this list the messages that are used a lot and you want better performance
                            case "SendSceneMessage":
                                DCL.Environment.i.world.sceneController.SendSceneMessage(msg.payload);
                                break;
                            case "Reset":
                                DCL.Environment.i.world.sceneController.UnloadAllScenesQueued();
                                break;
                            case "SetVoiceChatEnabledByScene":
                                if (int.TryParse(msg.payload, out int value)) // The payload should be `string`, this will be changed in a `renderer-protocol` refactor
                                {
                                    hudControllerGO.SendMessage(msg.type, value);
                                }
                                break;
                            case "RunPerformanceMeterTool":
                                if (float.TryParse(msg.payload, out float durationInSeconds)) // The payload should be `string`, this will be changed in a `renderer-protocol` refactor
                                {
                                    mainGO.SendMessage(msg.type, durationInSeconds);
                                }
                                break;
                            default:
                                if (!messageTypeToBridgeName.TryGetValue(msg.type, out string bridgeName))
                                {
                                    bridgeName = "Bridges"; // Default bridge
                                }

                                if (bridgeGameObjects.TryGetValue(bridgeName, out GameObject bridgeObject) == false)
                                {
                                    bridgeObject = GameObject.Find(bridgeName);
                                    bridgeGameObjects.Add(bridgeName, bridgeObject);
                                }

                                if (bridgeObject != null)
                                {
                                    bridgeObject.SendMessage(msg.type, msg.payload);
                                }
                                break;
                        }

                        if (DCLWebSocketService.VERBOSE)
                        {
                            Debug.Log(
                                "<b><color=#0000FF>WebSocketCommunication</color></b> >>> Got it! passing message of type " +
                                msg.type);
                        }
                    }
                }
            }
            yield return null;
        }
    }


    }
}