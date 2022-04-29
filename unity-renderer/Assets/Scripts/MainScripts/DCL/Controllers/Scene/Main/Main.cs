using System;
using System.Collections.Generic;
using DCL.Components;
using DCL.Controllers;
using DCL.Helpers;
using DCL.SettingsCommon;
using UnityEngine;
using UnityEngine.Serialization;

namespace DCL
{
    /// <summary>
    /// This is the InitialScene entry point.
    /// Most of the application subsystems should be initialized from this class Awake() event.
    /// </summary>
    public class Main : MonoBehaviour
    {
        [SerializeField] private bool disableSceneDependencies;
        public static Main i { get; private set; }

        public PoolableComponentFactory componentFactory;

        private PerformanceMetricsController performanceMetricsController;
        protected IKernelCommunication kernelCommunication;

        protected PluginSystem pluginSystem;
        
        protected virtual void Awake()
        {
            if (i != null)
            {
                Utils.SafeDestroy(this);
                return;
            }

            i = this;

            if (!disableSceneDependencies)
                InitializeSceneDependencies();
            
            //TODO: This local unity stuff - Look at later not needed to change now
            Settings.CreateSharedInstance(new DefaultSettingsFactory());

            if (!Configuration.EnvironmentSettings.RUNNING_TESTS)
            {
                performanceMetricsController = new PerformanceMetricsController();
                SetupServices();

                DataStore.i.HUDs.loadingHUD.visible.OnChange += OnLoadingScreenVisibleStateChange;
            }
            
#if UNITY_STANDALONE || UNITY_EDITOR
            Application.quitting += () => DataStore.i.common.isApplicationQuitting.Set(true);
#endif

            SetupPlugins();
            InitializeCommunication();

            // TODO(Brian): This is a temporary fix to address elevators issue in the xmas event.
            // We should re-enable this later as produces a performance regression.
            if (!Configuration.EnvironmentSettings.RUNNING_TESTS)
                Environment.i.platform.cullingController.SetAnimationCulling(false);
        }

        protected virtual void InitializeCommunication()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Debug.Log("DCL Unity Build Version: " + DCL.Configuration.ApplicationSettings.version);
            Debug.unityLogger.logEnabled = false;

            kernelCommunication = new NativeBridgeCommunication(Environment.i.world.sceneController);
#else
            // TODO(Brian): Remove this branching once we finish migrating all tests out of the
            //              IntegrationTestSuite_Legacy base class.
            if (!Configuration.EnvironmentSettings.RUNNING_TESTS)
            {
                kernelCommunication = new WebSocketCommunication(DebugConfigComponent.i.webSocketSSL);
            }
#endif
        }

        void OnLoadingScreenVisibleStateChange(bool newVisibleValue, bool previousVisibleValue)
        {
            if (newVisibleValue)
            {
                // Prewarm shader variants
                Resources.Load<ShaderVariantCollection>("ShaderVariantCollections/shaderVariants-selected").WarmUp();
                DataStore.i.HUDs.loadingHUD.visible.OnChange -= OnLoadingScreenVisibleStateChange;
            }
        }

        protected virtual void SetupPlugins()
        {
            pluginSystem = PluginSystemFactory.Create();
        }

        protected virtual void SetupServices()
        {
            Environment.Setup(ServiceLocatorFactory.CreateDefault());
        }

        protected virtual void Start()
        {
            // this event should be the last one to be executed after initialization
            // it is used by the kernel to signal "EngineReady" or something like that
            // to prevent race conditions like "SceneController is not an object",
            // aka sending events before unity is ready
            DCL.Interface.WebInterface.SendSystemInfoReport();

            // We trigger the Decentraland logic once everything is initialized.
            DCL.Interface.WebInterface.StartDecentraland();
        }

        protected virtual void Update()
        {
            performanceMetricsController?.Update();
        }
        
        [RuntimeInitializeOnLoadMethod]
        static void RunOnStart()
        {
            Application.wantsToQuit += ApplicationWantsToQuit;
        }
        private static bool ApplicationWantsToQuit()
        {
            if (i != null)
                i.Dispose();
    
            return true;
        }

        protected virtual void Dispose()
        {
            DataStore.i.HUDs.loadingHUD.visible.OnChange -= OnLoadingScreenVisibleStateChange;

            DataStore.i.common.isApplicationQuitting.Set(true);

            pluginSystem?.Dispose();

            if (!Configuration.EnvironmentSettings.RUNNING_TESTS)
                Environment.Dispose();
            
            kernelCommunication?.Dispose();
        }
        
        protected virtual void InitializeSceneDependencies()
        {
           gameObject.AddComponent<UserProfileController>(); // Awake() - this looks to just create a data container to hold the user info once signed, also has a really bad singleton "dont ever do this!"
           gameObject.AddComponent<RenderingController>();
           gameObject.AddComponent<CatalogController>();
           gameObject.AddComponent<MinimapMetadataController>();
           gameObject.AddComponent<ChatController>();
           gameObject.AddComponent<FriendsController>();
           gameObject.AddComponent<HotScenesController>();
           gameObject.AddComponent<GIFProcessingBridge>();
           gameObject.AddComponent<RenderProfileBridge>();
           gameObject.AddComponent<AssetCatalogBridge>();
           gameObject.AddComponent<ScreenSizeWatcher>();
           gameObject.AddComponent<SceneControllerBridge>();

            // Creates empty object and attaches [BuilderInWorldBridge]
            //BuilderInWorldBridge has no mono lifecycle methods
            MainSceneFactory.CreateBuilderInWorldBridge(gameObject);

            //Loads [Bridges] prefab from Resources and Instantiates it
            //[Bridges] - Components
            //[KernelConfigurationBridge] has no mono lifecycle methods  !has one method SetKernelConfiguration() this is overkill for a mono and needs to be designed better
            //[FocusStateBridge] Awake loads a script able from resouces that holds only a bool ??? WTF! needs to be designed better
            //[RealmsInfoBridge] has no mono lifecycle methods
            //[MouseCatcherBridge] has no mono lifecycle methods - has a method exposed for browser - this is weird, there should be one manager for browser stuff this code is so bad
            //[QuestsBridge] does some interaction with [QuestsController] singleton, hope its for UI connection else why only interact with a few things??? And only uses OnDestroy, so clean up with object is removed
            //[UsersSearchBridge] Has Awake but it only implements a really bad singleton "dont ever do this" and it implemnets [IUsersSearchBridge]
            //[DataStoreBridge] has no mono lifecycle methods - decodes json and passes to [DataStore]
            //[LoadingBridge] has no mono lifecycle methods updates loading UI
            MainSceneFactory.CreateBridges();


           MainSceneFactory.CreateMouseCatcher();
           MainSceneFactory.CreatePlayerSystems();
           CreateEnvironment();
            MainSceneFactory.CreateAudioHandler();
            MainSceneFactory.CreateHudController();
            MainSceneFactory.CreateSettingsController();
            MainSceneFactory.CreateNavMap();
            MainSceneFactory.CreateEventSystem();
            MainSceneFactory.CreateInteractionHoverCanvas();
        }

        protected virtual void CreateEnvironment() => MainSceneFactory.CreateEnvironment();
    }
}