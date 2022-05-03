/**
 * We will need to rinfine this once we have all maped out but these are the current browser to unity messages done with DCL
 * there is a good change that most are not needed
 */
const $abeyworld = (function() {
    var game;
  
    function SendToUnity (gameObjectName, methodName, data) {
        this.game.SendMessage(gameObjectName, methodName, data);
    }
    
    // public interface
    return {
        // cache a ref to the unity player
        SetGameInstance: (instance) => this.game=instance,

         /* 
            the js inside this project for the webgl template seems to no be used and incorrect
            I found / pulled the files loaded in a live DCL game session
            the urls are just for ref to see what the js/unity-js interaction is 
            DCL refs from live run
            https://cdn.decentraland.org/@dcl/unity-renderer/1.0.32838-20220415090933.commit-43530d2/index.js
            https://cdn.decentraland.org/@dcl/unity-renderer/1.0.32838-20220415090933.commit-43530d2/unity.framework.js.br?v=1.0.32838-20220415090933.commit-43530d2
            https://cdn.decentraland.org/@dcl/kernel/1.0.0-2227136955.commit-c4944ec/index.js

            below are all the messaging I found and extracted
            I simplified it down to clean easy to under stand code
        */


        Teleport:                                               (data) => this.SendToUnity("CharacterController","Teleport",JSON.stringify(data)),//JSON.stringify({x:e,y:i,z:n})
        SetRotation:                                            (data) => this.SendToUnity("CameraController","SetRotation",JSON.stringify(data)),//JSON.stringify({x:e,y:i,z:n,cameraTarget:r})
        SendSceneMessage:                                       (data) => this.SendToUnity("SceneController","SendSceneMessage",data), //e
        SetTutorialEnabled:                                     (data) => this.SendToUnity("TutorialController","SetTutorialEnabled",JSON.stringify(data)),//JSON.stringify(e)
        SetTutorialEnabledForUsersThatAlreadyDidTheTutorial:    (data) => this.SendToUnity("TutorialController","SetTutorialEnabledForUsersThatAlreadyDidTheTutorial",JSON.stringify(data)),//JSON.stringify(e)
      
/***
 * Messages sent the the GameObject 'Main'
 */
        SetDebug:                       ()      => this.SendToUnity("Main","SetDebug"),
        LoadProfile:                    (data)  => this.SendToUnity("Main","LoadProfile",JSON.stringify(data)),//JSON.stringify(e)
        SetRenderProfile:               (data)  => this.SendToUnity("Main","SetRenderProfile",JSON.stringify(data)),//JSON.stringify({id:e})
        CreateGlobalScene:              (data)  => this.SendToUnity("Main","CreateGlobalScene",JSON.stringify(data)),//JSON.stringify(e)
        LoadParcelScenes:               (data)  => this.SendToUnity("Main","LoadParcelScenes",JSON.stringify(data)),//JSON.stringify(e[0])
        UnloadScene:                    (data)  => this.SendToUnity("Main","UnloadScene",data),//e
        SetSceneDebugPanel:             ()      => this.SendToUnity("Main","SetSceneDebugPanel"),
        ShowFPSPanel:                   ()      => this.SendToUnity("Main","ShowFPSPanel"),
        HideFPSPanel:                   ()      => this.SendToUnity("Main","HideFPSPanel"),
        SetEngineDebugPanel:            ()      => this.SendToUnity("Main","SetEngineDebugPanel"),
        SetDisableAssetBundles:         ()      => this.SendToUnity("Main","SetDisableAssetBundles"),
        CrashPayloadRequest:            ()      => this.SendToUnity("Main","CrashPayloadRequest"),
        ActivateRendering:              ()      => this.SendToUnity("Main","ActivateRendering"),
        DeactivateRendering:            ()      => this.SendToUnity("Main","DeactivateRendering"),
        BuilderReady:                   ()      => this.SendToUnity("Main","BuilderReady"),
        AddUserProfileToCatalog:        (data)  => this.SendToUnity("Main","AddUserProfileToCatalog",JSON.stringify(data)),//JSON.stringify(e)
        AddWearablesToCatalog:          (data)  => this.SendToUnity("Main","AddWearablesToCatalog",JSON.stringify(data)),//JSON.stringify({wearables:e,context:t})
        WearablesRequestFailed:         (data)  => this.SendToUnity("Main","WearablesRequestFailed",JSON.stringify(data)),//JSON.stringify({error:e,context:t})
        RemoveWearablesFromCatalog:     (data)  => this.SendToUnity("Main","RemoveWearablesFromCatalog",JSON.stringify(data)),//JSON.stringify(e)
        ClearWearableCatalog:           ()      => this.SendToUnity("Main","ClearWearableCatalog"),
        UpdateMinimapSceneInformation:  (data)  => this.SendToUnity("Main","UpdateMinimapSceneInformation",JSON.stringify(data)),//JSON.stringify(n)
        AddMessageToChatWindow:         (data)  => this.SendToUnity("Main","AddMessageToChatWindow",JSON.stringify(data)),//JSON.stringify(e)
        InitializeFriends:              (data)  => this.SendToUnity("Main","InitializeFriends",JSON.stringify(data)),//JSON.stringify(e)
        UpdateFriendshipStatus:         (data)  => this.SendToUnity("Main","UpdateFriendshipStatus",JSON.stringify(data)),//JSON.stringify(e)
        UpdateUserPresence:             (data)  => this.SendToUnity("Main","UpdateUserPresence",JSON.stringify(data)),//JSON.stringify(e)
        FriendNotFound:                 (data)  => this.SendToUnity("Main","FriendNotFound",JSON.stringify(data)),//JSON.stringify(e)
        UpdateHotScenesList:            (data)  => this.SendToUnity("Main","UpdateHotScenesList",JSON.stringify(data)),//JSON.stringify(n)
        ConnectionToRealmSuccess:       (data)  => this.SendToUnity("Main","ConnectionToRealmSuccess",JSON.stringify(data)),//JSON.stringify(e)
        ConnectionToRealmFailed:        (data)  => this.SendToUnity("Main","ConnectionToRealmFailed",JSON.stringify(data)),//JSON.stringify(e)
        UpdateGIFPointers:              (data)  => this.SendToUnity("Main","UpdateGIFPointers",JSON.stringify(data)),//JSON.stringify({id:e,width:t,height:n,pointers:r,frameDelays:o})
        FailGIFFetch:                   (data)  => this.SendToUnity("Main","FailGIFFetch",data),//e
        ForceActivateRendering:         ()      => this.SendToUnity("Main","ForceActivateRendering"),
        PublishSceneResult:             (data)  => this.SendToUnity("Main","PublishSceneResult",JSON.stringify(data)),//JSON.stringify(e)
        BuilderProjectInfo:             (data)  => this.SendToUnity("Main","BuilderProjectInfo",JSON.stringify(data)),//JSON.stringify({title:e,description:t,isNewEmptyProject:n})
        BuilderInWorldCatalogHeaders:   (data)  => this.SendToUnity("Main","BuilderInWorldCatalogHeaders",JSON.stringify(data)),//JSON.stringify(e)
        RequestedHeaders:               (data)  => this.SendToUnity("Main","RequestedHeaders",JSON.stringify(data)),//JSON.stringify(n)
        AddAssets:                      (data)  => this.SendToUnity("Main","AddAssets",JSON.stringify(data)),//JSON.stringify(e)
        DumpScenesLoadInfo:             ()      => this.SendToUnity("Main","DumpScenesLoadInfo"),
        DumpRendererLockersInfo:        ()      => this.SendToUnity("Main","DumpRendererLockersInfo"),
        RunPerformanceMeterTool:        (data)  => this.SendToUnity("Main","RunPerformanceMeterTool",data),//e
        InstantiateBotsAtWorldPos:      (data)  => this.SendToUnity("Main","InstantiateBotsAtWorldPos",JSON.stringify(data)),//JSON.stringify(e)
        InstantiateBotsAtCoords:        (data)  => this.SendToUnity("Main","InstantiateBotsAtCoords",JSON.stringify(data)),//JSON.stringify(e)
        StartBotsRandomizedMovement:    (data)  => this.SendToUnity("Main","StartBotsRandomizedMovement",JSON.stringify(data)),//JSON.stringify(e)
        StopBotsMovement:               ()      => this.SendToUnity("Main","StopBotsMovement"),
        RemoveBot:                      (data)  => this.SendToUnity("Main","RemoveBot",data),//e
        ClearBots:                      ()      => this.SendToUnity("Main","ClearBots"),
        ToggleSceneBoundingBoxes:       (data)  => this.SendToUnity("Main","ToggleSceneBoundingBoxes",JSON.stringify(data)),//JSON.stringify({sceneId:r,enabled:t})
        TogglePreviewMenu:              (data)  => this.SendToUnity("Main","TogglePreviewMenu",JSON.stringify(data)),//JSON.stringify({enabled:!0})

/***
 * Messages sent the the GameObject 'Bridges'
 */
        SetLoadingScreen:               (data)  => this.SendToUnity("Bridges","SetLoadingScreen",JSON.stringify(data)),//JSON.stringify(e)
        ReportFocusOn:                  ()      => this.SendToUnity("Bridges","ReportFocusOn"),
        ReportFocusOff:                 ()      => this.SendToUnity("Bridges","ReportFocusOff"),
        UnlockCursorBrowser:            (data)  => this.SendToUnity("Bridges","UnlockCursorBrowser",data),//e?1:0
        RequestWeb3ApiUse:              (data)  => this.SendToUnity("Bridges","RequestWeb3ApiUse",JSON.stringify(data)),//JSON.stringify({id:r,requestType:e,payload:t})
        SetKernelConfiguration:         (data)  => this.SendToUnity("Bridges","SetKernelConfiguration",JSON.stringify(data)),//JSON.stringify(e)
        SetFeatureFlagConfiguration:    (data)  => this.SendToUnity("Bridges","SetFeatureFlagConfiguration",JSON.stringify(data)),//JSON.stringify(e)
        UpdateRealmsInfo:               (data)  => this.SendToUnity("Bridges","UpdateRealmsInfo",JSON.stringify(data)),//JSON.stringify(e)
        SetENSOwnerQueryResult:         (data)  => this.SendToUnity("Bridges","SetENSOwnerQueryResult",JSON.stringify(data)),//JSON.stringify({searchInput:e,success:!1})
        SetENSOwnerQueryResult:         (data)  => this.SendToUnity("Bridges","SetENSOwnerQueryResult",JSON.stringify(data)),//JSON.stringify({searchInput:e,success:!0,profiles:n})
        UnpublishSceneResult:           (data)  => this.SendToUnity("Bridges","UnpublishSceneResult",JSON.stringify(data)),//JSON.stringify(e)
        InitializeQuests:               (data)  => this.SendToUnity("Bridges","InitializeQuests",JSON.stringify(data)),//JSON.stringify(e)
        UpdateQuestProgress:            (data)  => this.SendToUnity("Bridges","UpdateQuestProgress",JSON.stringify(data)),//JSON.stringify(e)


/***
 * Messages sent the the GameObject 'HUDController'
 */
        ShowNotificationFromJson:       (data)  => this.SendToUnity("HUDController","ShowNotificationFromJson",JSON.stringify(data)),//JSON.stringify(e)
        ConfigureHUDElement:            (data)  => this.SendToUnity("HUDController","ConfigureHUDElement",JSON.stringify(data)),//JSON.stringify({hudElementId:e,configuration:t,extraPayload:n?JSON.stringify(n):null})
        ShowWelcomeNotification:        ()      => this.SendToUnity("HUDController","ShowWelcomeNotification"),
        TriggerSelfUserExpression:      (data)  => this.SendToUnity("HUDController","TriggerSelfUserExpression",data),//e
        RequestTeleport:                (data)  => this.SendToUnity("HUDController","RequestTeleport",JSON.stringify(data)),//JSON.stringify(e)
        UpdateBalanceOfMANA:            (data)  => this.SendToUnity("HUDController","UpdateBalanceOfMANA",data),//e
        SetPlayerTalking:               (data)  => this.SendToUnity("HUDController","SetPlayerTalking",JSON.stringify(data)),//JSON.stringify(e)
        ShowAvatarEditorInSignUp:       ()      => this.SendToUnity("HUDController","ShowAvatarEditorInSignUp"),
        SetUserTalking:                 (data)  => this.SendToUnity("HUDController","SetUserTalking",JSON.stringify(data)),//JSON.stringify({userId:e,talking:t})
        SetUsersMuted:                  (data)  => this.SendToUnity("HUDController","SetUsersMuted",JSON.stringify(data)),//JSON.stringify({usersId:e,muted:t})
        SetVoiceChatEnabledByScene:     (data)  => this.SendToUnity("HUDController","SetVoiceChatEnabledByScene",data),//e?1:0


/***
 * Messages sent the the GameObject 'BuilderController'
 * The naming makes me think this are all for the plot builder stuff
 */
        SelectGizmo:                (data)  => this.SendToUnity("BuilderController", "SelectGizmo",data),//e
        ResetObject:                ()      => this.SendToUnity("BuilderController", "ResetObject"),
        ZoomDelta:                  (data)  => this.SendToUnity("BuilderController", "ZoomDelta",data),//e.toString()
        GetCameraTargetBuilder:     (data)  => this.SendToUnity("BuilderController", "GetCameraTargetBuilder",data),//e
        SetPlayMode:                (data)  => this.SendToUnity("BuilderController", "SetPlayMode",data),//e
        PreloadFile:                (data)  => this.SendToUnity("BuilderController", "PreloadFile",data),//e
        GetMousePosition:           (data)  => this.SendToUnity("BuilderController", "GetMousePosition",data),//`{"x":"${e}", "y": "${t}", "id": "${n}" }`
        TakeScreenshot:             (data)  => this.SendToUnity("BuilderController", "TakeScreenshot",data),//e
        SetBuilderCameraPosition:   (data)  => this.SendToUnity("BuilderController", "SetBuilderCameraPosition",data),//e.x+","+e.y+","+e.z
        SetBuilderCameraRotation:   (data)  => this.SendToUnity("BuilderController", "SetBuilderCameraRotation",data),//e+","+t
        ResetBuilderCameraZoom:     ()      => this.SendToUnity("BuilderController", "ResetBuilderCameraZoom"),
        SetGridResolution:          (data)  => this.SendToUnity("BuilderController", "SetGridResolution",JSON.stringify(data)),//JSON.stringify({position:e,rotation:t,scale:n})
        SetSelectedEntities:        (data)  => this.SendToUnity("BuilderController", "SetSelectedEntities",JSON.stringify(data)),//JSON.stringify({entities:e})
        ResetBuilderScene:          ()      => this.SendToUnity("BuilderController", "ResetBuilderScene"),
        OnBuilderKeyDown:           (data)  => this.SendToUnity("BuilderController", "OnBuilderKeyDown",data),//e
        SetBuilderConfiguration:    (data)  => this.SendToUnity("BuilderController", "SetBuilderConfiguration",JSON.stringify(data)),//JSON.stringify(e)

    };

})();