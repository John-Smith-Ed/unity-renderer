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
    * none socket
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
        // Please, use `Bridges` as a bridge name, avoid adding messages here. The system will use `Bridges` as the default bridge name.
        messageTypeToBridgeName["SetDebug"] = "Main";
        messageTypeToBridgeName["SetSceneDebugPanel"] = "Main";
        messageTypeToBridgeName["ShowFPSPanel"] = "Main";
        messageTypeToBridgeName["HideFPSPanel"] = "Main";
        messageTypeToBridgeName["SetEngineDebugPanel"] = "Main";
        messageTypeToBridgeName["SendSceneMessage"] = "Main";
        messageTypeToBridgeName["LoadParcelScenes"] = "Main";
        messageTypeToBridgeName["UnloadScene"] = "Main";
        messageTypeToBridgeName["Reset"] = "Main";
        messageTypeToBridgeName["CreateGlobalScene"] = "Main";
        messageTypeToBridgeName["BuilderReady"] = "Main";
        messageTypeToBridgeName["UpdateParcelScenes"] = "Main";
        messageTypeToBridgeName["LoadProfile"] = "Main";
        messageTypeToBridgeName["AddUserProfileToCatalog"] = "Main";
        messageTypeToBridgeName["AddUserProfilesToCatalog"] = "Main";
        messageTypeToBridgeName["RemoveUserProfilesFromCatalog"] = "Main";
        messageTypeToBridgeName["ActivateRendering"] = "Main";
        messageTypeToBridgeName["DeactivateRendering"] = "Main";
        messageTypeToBridgeName["ForceActivateRendering"] = "Main";
        messageTypeToBridgeName["AddWearablesToCatalog"] = "Main";
        messageTypeToBridgeName["WearablesRequestFailed"] = "Main";
        messageTypeToBridgeName["RemoveWearablesFromCatalog"] = "Main";
        messageTypeToBridgeName["ClearWearableCatalog"] = "Main";
        messageTypeToBridgeName["InitializeFriends"] = "Main";
        messageTypeToBridgeName["UpdateFriendshipStatus"] = "Main";
        messageTypeToBridgeName["UpdateUserPresence"] = "Main";
        messageTypeToBridgeName["FriendNotFound"] = "Main";
        messageTypeToBridgeName["AddMessageToChatWindow"] = "Main";
        messageTypeToBridgeName["UpdateMinimapSceneInformation"] = "Main";
        messageTypeToBridgeName["UpdateHotScenesList"] = "Main";
        messageTypeToBridgeName["SetRenderProfile"] = "Main";
        messageTypeToBridgeName["CrashPayloadRequest"] = "Main";
        messageTypeToBridgeName["SetDisableAssetBundles"] = "Main";
        messageTypeToBridgeName["DumpRendererLockersInfo"] = "Main";
        messageTypeToBridgeName["PublishSceneResult"] = "Main";
        messageTypeToBridgeName["BuilderProjectInfo"] = "Main";
        messageTypeToBridgeName["BuilderInWorldCatalogHeaders"] = "Main";
        messageTypeToBridgeName["RequestedHeaders"] = "Main";
        messageTypeToBridgeName["AddAssets"] = "Main";
        messageTypeToBridgeName["RunPerformanceMeterTool"] = "Main";
        messageTypeToBridgeName["InstantiateBotsAtWorldPos"] = "Main";
        messageTypeToBridgeName["InstantiateBotsAtCoords"] = "Main";
        messageTypeToBridgeName["StartBotsRandomizedMovement"] = "Main";
        messageTypeToBridgeName["StopBotsMovement"] = "Main";
        messageTypeToBridgeName["RemoveBot"] = "Main";
        messageTypeToBridgeName["ClearBots"] = "Main";
        messageTypeToBridgeName["ToggleSceneBoundingBoxes"] = "Main";
        messageTypeToBridgeName["TogglePreviewMenu"] = "Main";
        messageTypeToBridgeName["ToggleSceneSpawnPoints"] = "Main";
        messageTypeToBridgeName["Teleport"] = "CharacterController";
        messageTypeToBridgeName["SetRotation"] = "CameraController";
        messageTypeToBridgeName["ShowNotificationFromJson"] = "HUDController";
        messageTypeToBridgeName["ConfigureHUDElement"] = "HUDController";
        messageTypeToBridgeName["ShowTermsOfServices"] = "HUDController";
        messageTypeToBridgeName["RequestTeleport"] = "HUDController";
        messageTypeToBridgeName["ShowAvatarEditorInSignUp"] = "HUDController";
        messageTypeToBridgeName["SetUserTalking"] = "HUDController";
        messageTypeToBridgeName["SetUsersMuted"] = "HUDController";
        messageTypeToBridgeName["ShowWelcomeNotification"] = "HUDController";
        messageTypeToBridgeName["UpdateBalanceOfMANA"] = "HUDController";
        messageTypeToBridgeName["SetPlayerTalking"] = "HUDController";
        messageTypeToBridgeName["SetVoiceChatEnabledByScene"] = "HUDController";
        messageTypeToBridgeName["TriggerSelfUserExpression"] = "HUDController";
        messageTypeToBridgeName["AirdroppingRequest"] = "HUDController";
        
        messageTypeToBridgeName["GetMousePosition"] = "BuilderController";
        messageTypeToBridgeName["SelectGizmo"] = "BuilderController";
        messageTypeToBridgeName["ResetObject"] = "BuilderController";
        messageTypeToBridgeName["ZoomDelta"] = "BuilderController";
        messageTypeToBridgeName["SetPlayMode"] = "BuilderController";
        messageTypeToBridgeName["TakeScreenshot"] = "BuilderController";
        messageTypeToBridgeName["ResetBuilderScene"] = "BuilderController";
        messageTypeToBridgeName["SetBuilderCameraPosition"] = "BuilderController";
        messageTypeToBridgeName["SetBuilderCameraRotation"] = "BuilderController";
        messageTypeToBridgeName["ResetBuilderCameraZoom"] = "BuilderController";
        messageTypeToBridgeName["SetGridResolution"] = "BuilderController";
        messageTypeToBridgeName["OnBuilderKeyDown"] = "BuilderController";
        messageTypeToBridgeName["UnloadBuilderScene"] = "BuilderController";
        messageTypeToBridgeName["SetSelectedEntities"] = "BuilderController";
        messageTypeToBridgeName["GetCameraTargetBuilder"] = "BuilderController";
        messageTypeToBridgeName["PreloadFile"] = "BuilderController";
        messageTypeToBridgeName["SetBuilderConfiguration"] = "BuilderController";

        messageTypeToBridgeName["SetTutorialEnabled"] = "TutorialController";
        messageTypeToBridgeName["SetTutorialEnabledForUsersThatAlreadyDidTheTutorial"] = "TutorialController";
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
                        ABEY.LogWriter.Write("Comms", $"Message {msg.type}: {msg.payload}", 30);
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