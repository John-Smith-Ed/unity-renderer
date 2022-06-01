namespace ABEY {
    //Replace CommonScriptableObjects
    // the "Resouces API Should not be user - They abused it with improper use of scriptables"

    using UnityEngine;

    [CreateAssetMenu(fileName = "CommonConfigScriptable", menuName = "ABEY/CommonConfigScriptable", order = 0)]
    public class CommonConfigScriptable : ScriptableObject {
        [SerializeField] Vector3Variable            playerUnityPositionValue;
        [SerializeField] Vector3Variable            playerWorldPositionValue;
        [SerializeField] Vector3Variable            playerUnityEulerAnglesValue;
        [SerializeField] Vector3Variable            worldOffsetValue;
        [SerializeField] Vector2IntVariable         playerCoordsValue;
        [SerializeField] BooleanVariable            playerIsOnMovingPlatformValue;
        [SerializeField] QuaternionVariable         movingPlatformRotationDeltaValue;
        [SerializeField] StringVariable             sceneIDValue;
        [SerializeField] FloatVariable              minimapZoomValue;
        [SerializeField] Vector3NullableVariable    characterForwardValue;
        [SerializeField] Vector3Variable            cameraForwardValue;
        [SerializeField] Vector3Variable            cameraPositionValue;
        [SerializeField] Vector3Variable            cameraRightValue;
        [SerializeField] BooleanVariable            cameraIsBlendingValue;
        [SerializeField] BooleanVariable            cameraBlockedValue;
        [SerializeField] BooleanVariable            playerInfoCardVisibleStateValue;
        [SerializeField] RendererState              rendererStateValue;
        [SerializeField] BooleanVariable            focusStateValue;
        [SerializeField] ReadMessagesDictionary     lastReadChatMessagesDictionary;
        [SerializeField] LongVariable               lastReadChatMessagesValue;
        [SerializeField] BooleanVariable            allUIHiddenValue;
        [SerializeField] BooleanVariable            builderInWorldNotNecessaryUIVisibilityStatusValue;
        [SerializeField] LatestOpenChatsList        latestOpenChatsValue;
        [SerializeField] CameraMode                 cameraModeValue;
        [SerializeField] BooleanVariable            cameraModeInputLockedValue;
        [SerializeField] BooleanVariable            isProfileHUDOpenValue;
        [SerializeField] BooleanVariable            isFullscreenHUDOpenValue;
        [SerializeField] BooleanVariable            isTaskbarHUDInitializedValue;
        [SerializeField] BooleanVariable            tutorialActiveValue;
        [SerializeField] BooleanVariable            featureKeyTriggersBlockedValue;
        [SerializeField] BooleanVariable            emailPromptActiveValue;
        [SerializeField] BooleanVariable            voiceChatDisabledValue;
        [Header("Common Settings")]
        [SerializeField] BooleanVariable            shadowsDisabledValue;
        [SerializeField] BooleanVariable            detailObjectCullingDisabledValue;
        [SerializeField] BooleanVariable            dynamicSkyboxDisabledValue;


        public Vector3Variable           playerUnityPosition         => playerUnityPositionValue; //GetOrLoad(ref playerUnityPositionValue, "ScriptableObjects/PlayerUnityPosition");    
        public Vector3Variable           playerWorldPosition         => playerWorldPositionValue; //GetOrLoad(ref playerWorldPositionValue, "ScriptableObjects/PlayerWorldPosition");    
        public Vector3Variable           playerUnityEulerAngles      => playerUnityEulerAnglesValue; //GetOrLoad(ref playerUnityEulerAnglesValue, "ScriptableObjects/PlayerUnityEulerAngles");
        public Vector3Variable           worldOffset                 => worldOffsetValue; //GetOrLoad(ref worldOffsetValue, "ScriptableObjects/WorldOffset");
        public Vector2IntVariable        playerCoords                => playerCoordsValue; //GetOrLoad(ref playerCoordsValue, "ScriptableObjects/PlayerCoords");
        public BooleanVariable           playerIsOnMovingPlatform    => playerIsOnMovingPlatformValue; //GetOrLoad(ref playerIsOnMovingPlatformValue, "ScriptableObjects/playerIsOnMovingPlatform");    
        public QuaternionVariable        movingPlatformRotationDelta => movingPlatformRotationDeltaValue; //GetOrLoad(ref movingPlatformRotationDeltaValue, "ScriptableObjects/MovingPlatformRotationDelta");    
        public StringVariable            sceneID                     => sceneIDValue; //GetOrLoad(ref sceneIDValue, "ScriptableObjects/SceneID");    
        public FloatVariable             minimapZoom                 => minimapZoomValue; //GetOrLoad(ref minimapZoomValue, "ScriptableObjects/MinimapZoom");    
        public Vector3NullableVariable   characterForward            => characterForwardValue; //GetOrLoad(ref characterForwardValue, "ScriptableObjects/CharacterForward");
        public Vector3Variable           cameraForward               => cameraForwardValue; //GetOrLoad(ref cameraForwardValue, "ScriptableObjects/CameraForward");
        public Vector3Variable           cameraPosition              => cameraPositionValue; //GetOrLoad(ref cameraPositionValue, "ScriptableObjects/CameraPosition");    
        public Vector3Variable           cameraRight                 => cameraRightValue; //GetOrLoad(ref cameraRightValue, "ScriptableObjects/CameraRight");    
        public BooleanVariable           cameraIsBlending            => cameraIsBlendingValue; //GetOrLoad(ref cameraIsBlendingValue, "ScriptableObjects/CameraIsBlending");
        public BooleanVariable           cameraBlocked               => cameraBlockedValue; //GetOrLoad(ref cameraBlockedValue, "ScriptableObjects/CameraBlocked");
        public BooleanVariable           playerInfoCardVisibleState  => playerInfoCardVisibleStateValue; //GetOrLoad(ref playerInfoCardVisibleStateValue, "ScriptableObjects/PlayerInfoCardVisibleState");
        public RendererState             rendererState               => rendererStateValue; //GetOrLoad(ref rendererStateValue, "ScriptableObjects/RendererState");
        public BooleanVariable           focusState                  => focusStateValue; //GetOrLoad(ref focusStateValue, "ScriptableObjects/FocusState");
        public ReadMessagesDictionary    lastReadChatMessages        => lastReadChatMessagesDictionary; //GetOrLoad(ref lastReadChatMessagesDictionary, "ScriptableObjects/LastReadChatMessages");    
        public LongVariable              lastReadWorldChatMessages   => lastReadChatMessagesValue; //GetOrLoad(ref lastReadChatMessagesValue, "ScriptableObjects/LastReadWorldChatMessages");
        public BooleanVariable           allUIHidden                 => allUIHiddenValue; //GetOrLoad(ref allUIHiddenValue, "ScriptableObjects/AllUIHidden");    
        public BooleanVariable           builderInWorldNotNecessaryUIVisibilityStatus => builderInWorldNotNecessaryUIVisibilityStatusValue; // GetOrLoad(ref builderInWorldNotNecessaryUIVisibilityStatusValue, "ScriptableObjects/BuilderInWorldUIHidden");    
        public LatestOpenChatsList       latestOpenChats             => latestOpenChatsValue; //GetOrLoad(ref latestOpenChatsValue, "ScriptableObjects/LatestOpenChats");
        public CameraMode                cameraMode                  => cameraModeValue; //GetOrLoad(ref cameraModeValue, "ScriptableObjects/CameraMode");
        public BooleanVariable           cameraModeInputLocked       => cameraModeInputLockedValue; //GetOrLoad(ref cameraModeInputLockedValue, "ScriptableObjects/CameraModeInputLocked");
        public BooleanVariable           isProfileHUDOpen            => isProfileHUDOpenValue; //GetOrLoad(ref isProfileHUDOpenValue, "ScriptableObjects/IsProfileHUDOpen");    
        public BooleanVariable           isFullscreenHUDOpen         => isFullscreenHUDOpenValue; //GetOrLoad(ref isFullscreenHUDOpenValue, "ScriptableObjects/IsAvatarHUDOpen");
        public BooleanVariable           isTaskbarHUDInitialized     => isTaskbarHUDInitializedValue; //GetOrLoad(ref isTaskbarHUDInitializedValue, "ScriptableObjects/IsTaskbarHUDInitialized");
        public BooleanVariable           tutorialActive              => tutorialActiveValue; //GetOrLoad(ref tutorialActiveValue, "ScriptableObjects/TutorialActive");
        public BooleanVariable           featureKeyTriggersBlocked   => featureKeyTriggersBlockedValue; //GetOrLoad(ref featureKeyTriggersBlockedValue, "ScriptableObjects/FeatureKeyTriggersBlocked");
        public BooleanVariable           emailPromptActive           => emailPromptActiveValue; //GetOrLoad(ref emailPromptActiveValue, "ScriptableObjects/EmailPromptActive");   
        public BooleanVariable           voiceChatDisabled           => voiceChatDisabledValue; //GetOrLoad(ref voiceChatDisabledValue, "ScriptableObjects/VoiceChatDisabled");   

        //Common Settings
        public BooleanVariable shadowsDisabled              => shadowsDisabledValue;
        public BooleanVariable detailObjectCullingDisabled  => detailObjectCullingDisabledValue;
        public BooleanVariable dynamicSkyboxDisabled        => dynamicSkyboxDisabledValue;
   

        //TODO:DELETE AWAKE - used just for the bootstraping to rewrite bad code
        // Just incase you dont know how scriptables work, Awake is called when you create a new copy - i.e in the editor
        /*
      void Awake() {

        playerUnityPositionValue                              = UnityEditor.AssetDatabase.LoadAssetAtPath<Vector3Variable        >("Assets/NResources/ScriptableObjects/PlayerUnityPosition.asset");    
        playerWorldPositionValue                              = UnityEditor.AssetDatabase.LoadAssetAtPath<Vector3Variable        >("Assets/NResources/ScriptableObjects/PlayerWorldPosition.asset");    
        playerUnityEulerAnglesValue                           = UnityEditor.AssetDatabase.LoadAssetAtPath<Vector3Variable        >("Assets/NResources/ScriptableObjects/PlayerUnityEulerAngles.asset");
        worldOffsetValue                                      = UnityEditor.AssetDatabase.LoadAssetAtPath<Vector3Variable        >("Assets/NResources/ScriptableObjects/WorldOffset.asset");
        playerCoordsValue                                     = UnityEditor.AssetDatabase.LoadAssetAtPath<Vector2IntVariable     >("Assets/NResources/ScriptableObjects/PlayerCoords.asset");
        playerIsOnMovingPlatformValue                         = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/playerIsOnMovingPlatform.asset");    
        movingPlatformRotationDeltaValue                      = UnityEditor.AssetDatabase.LoadAssetAtPath<QuaternionVariable     >("Assets/NResources/ScriptableObjects/MovingPlatformRotationDelta.asset");    
        sceneIDValue                                          = UnityEditor.AssetDatabase.LoadAssetAtPath<StringVariable         >("Assets/NResources/ScriptableObjects/SceneID.asset");    
        minimapZoomValue                                      = UnityEditor.AssetDatabase.LoadAssetAtPath<FloatVariable          >("Assets/NResources/ScriptableObjects/MinimapZoom.asset");    
        characterForwardValue                                 = UnityEditor.AssetDatabase.LoadAssetAtPath<Vector3NullableVariable>("Assets/NResources/ScriptableObjects/CharacterForward.asset");
        cameraForwardValue                                    = UnityEditor.AssetDatabase.LoadAssetAtPath<Vector3Variable        >("Assets/NResources/ScriptableObjects/CameraForward.asset");
        cameraPositionValue                                   = UnityEditor.AssetDatabase.LoadAssetAtPath<Vector3Variable        >("Assets/NResources/ScriptableObjects/CameraPosition.asset");    
        cameraRightValue                                      = UnityEditor.AssetDatabase.LoadAssetAtPath<Vector3Variable        >("Assets/NResources/ScriptableObjects/CameraRight.asset");    
        cameraIsBlendingValue                                 = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/CameraIsBlending.asset");
        cameraBlockedValue                                    = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/CameraBlocked.asset");
        playerInfoCardVisibleStateValue                       = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/PlayerInfoCardVisibleState.asset");
        rendererStateValue                                    = UnityEditor.AssetDatabase.LoadAssetAtPath<RendererState          >("Assets/NResources/ScriptableObjects/RendererState.asset");
        focusStateValue                                       = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/FocusState.asset");
        lastReadChatMessagesDictionary                        = UnityEditor.AssetDatabase.LoadAssetAtPath<ReadMessagesDictionary >("Assets/NResources/ScriptableObjects/LastReadChatMessages.asset");    
        lastReadChatMessagesValue                             = UnityEditor.AssetDatabase.LoadAssetAtPath<LongVariable           >("Assets/NResources/ScriptableObjects/LastReadWorldChatMessages.asset");
        allUIHiddenValue                                      = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/AllUIHidden.asset");    
        builderInWorldNotNecessaryUIVisibilityStatusValue     = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/BuilderInWorldUIHidden.asset");    
        latestOpenChatsValue                                  = UnityEditor.AssetDatabase.LoadAssetAtPath<LatestOpenChatsList    >("Assets/NResources/ScriptableObjects/LatestOpenChats.asset");
        cameraModeValue                                       = UnityEditor.AssetDatabase.LoadAssetAtPath<CameraMode             >("Assets/NResources/ScriptableObjects/CameraMode.asset");
        cameraModeInputLockedValue                            = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/CameraModeInputLocked.asset");
        isProfileHUDOpenValue                                 = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/IsProfileHUDOpen.asset");    
        isFullscreenHUDOpenValue                              = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/IsAvatarHUDOpen.asset");
        isTaskbarHUDInitializedValue                          = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/IsTaskbarHUDInitialized.asset");
        tutorialActiveValue                                   = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/TutorialActive.asset");
        featureKeyTriggersBlockedValue                        = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/FeatureKeyTriggersBlocked.asset");
        emailPromptActiveValue                                = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/EmailPromptActive.asset");   
        voiceChatDisabledValue                                = UnityEditor.AssetDatabase.LoadAssetAtPath<BooleanVariable        >("Assets/NResources/ScriptableObjects/VoiceChatDisabled.asset");   

        }
        */
    
    }

}

