using UnityEngine;
using UnityEngine.Audio;
using DCL;
using DCL.Skybox;
using DCL.Emotes;
using DCL.SettingsCommon;

[CreateAssetMenu(fileName = "OthersScriptableObject", menuName = "ABEY/OthersScriptableObject", order = 0)]
public class OthersScriptableObject : ScriptableObject {
    
    [SerializeField] UserProfile                    userProfile;
    [SerializeField] UserProfileDictionary          userProfilesCatalog;
    [SerializeField] EmbeddedEmotesSO               embeddedEmotes;
    [SerializeField] QualitySettingsData            qualitySettingsData;
    [SerializeField] AudioMixer                     audioMixer;
    [SerializeField] StringVariable                 currentPlayerInfoCardId;
    [SerializeField] NFTShapeFactory                nFTShapeFactory;
    [SerializeField] MinimapMetadata                minimapMetadata;
    [SerializeField] PerformanceMetricsDataVariable performanceMetricsDataVariable;
    [SerializeField] GPUSkinningThrottlingCurveSO   gPUSkinningThrottlingCurveSO;
    [SerializeField] PoolableComponentFactory       poolableComponentFactory;
    [SerializeField] SceneAssetPackDictionary       sceneAssetPackDictionary;
    [SerializeField] SceneObjectDictionary          sceneObjectDictionary;
    [SerializeField] StickersFactory                stickersFactory;
    [SerializeField] MaterialReferenceContainer     skyboxMaterialData;
    [SerializeField] BIWProjectReferences           projectReferences;    
    [SerializeField] BIWInputsReferences            inputsReferences;    
    [SerializeField] BIWGodModeDynamicVariables     godModeVariables;
    [SerializeField] BIWFirstPersonDynamicVariables firstPersonDynamicVariables;

    public UserProfile                      UserProfile                     => userProfile;
    public UserProfileDictionary            UserProfilesCatalog             => userProfilesCatalog;
    public EmbeddedEmotesSO                 EmbeddedEmotes                  => embeddedEmotes;
    public QualitySettingsData              QualitySettingsData             => qualitySettingsData;
    public AudioMixer                       AudioMixer                      => audioMixer;
    public StringVariable                   CurrentPlayerInfoCardId         => currentPlayerInfoCardId;
    public NFTShapeFactory                  NFTShapeFactory                 => nFTShapeFactory;
    public MinimapMetadata                  MinimapMetadata                 => minimapMetadata;
    public PerformanceMetricsDataVariable   PerformanceMetricsDataVariable  => performanceMetricsDataVariable;
    public GPUSkinningThrottlingCurveSO     GPUSkinningThrottlingCurveSO    => gPUSkinningThrottlingCurveSO;
    public PoolableComponentFactory         PoolableComponentFactory        => poolableComponentFactory;
    public SceneAssetPackDictionary         SceneAssetPackDictionary        => sceneAssetPackDictionary;
    public SceneObjectDictionary            SceneObjectDictionary           => sceneObjectDictionary;
    public StickersFactory                  StickersFactory                 => stickersFactory;
    public MaterialReferenceContainer       SkyboxMaterialData              => skyboxMaterialData;
    public BIWProjectReferences             ProjectReferences               => projectReferences;
    public BIWInputsReferences              InputsReferences                => inputsReferences;
    public BIWGodModeDynamicVariables       GodModeDynamicVariables         => godModeVariables;
    public BIWFirstPersonDynamicVariables   FirstPersonDynamicVariables     => firstPersonDynamicVariables;
}