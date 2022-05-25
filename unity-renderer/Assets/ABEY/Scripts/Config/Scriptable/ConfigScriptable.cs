namespace ABEY {

    using UnityEngine;

    [CreateAssetMenu(fileName = "ConfigScriptable", menuName = "ABEY/ConfigScriptable", order = 0)]
    public class ConfigScriptable : ScriptableObject {

        [SerializeField] ApplicationConfigScriptable        applicationConfig;
        [SerializeField] EnvironmentConfigScriptable        environmentConfig;
        [SerializeField] EndPointConfigScriptable           endPointConfig;
        [SerializeField] InputConfigScriptable              inputConfig;
        [SerializeField] AudioEventsScriptable              audioEvents;
        [SerializeField] ParcelConfigScriptable             parcelConfig;
        [SerializeField] PlayerConfigScriptable             playerConfig;
        [SerializeField] TestConfigScriptable               testConfig;
        [SerializeField] AssetManagerConfigScriptable       assetManagerConfig;
        [SerializeField] MessageThrottlingConfigScriptable  messageThrottlingConfig;

        public ApplicationConfigScriptable          ApplicationConfig       => applicationConfig;
        public EnvironmentConfigScriptable          EnvironmentConfig       => environmentConfig;
        public EndPointConfigScriptable             EndPointConfig          => endPointConfig;
        public InputConfigScriptable                InputConfig             => inputConfig;
        public AudioEventsScriptable                AudioEvents             => audioEvents;
        public ParcelConfigScriptable               ParcelConfig            => parcelConfig;
        public PlayerConfigScriptable               PlayerConfig            => playerConfig;
        public TestConfigScriptable                 TestConfig              => TestConfig;
        public AssetManagerConfigScriptable         AssetManagerConfig      => assetManagerConfig;
        public MessageThrottlingConfigScriptable    MessageThrottlingConfig => messageThrottlingConfig;
        


        
    
    }

}

