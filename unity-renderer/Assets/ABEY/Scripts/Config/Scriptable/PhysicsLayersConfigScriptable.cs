namespace ABEY {
    #if UNITY_EDITOR
        using UnityEditor;
    #endif

    using UnityEngine;

    [CreateAssetMenu(fileName = "PhysicsLayersConfig", menuName = "ABEY/PhysicsLayersConfigScriptable", order = 0)]
    public class PhysicsLayersConfigScriptable : ScriptableObject {

        [SerializeField] int defaultLayer;
        [SerializeField] int onPointerEventLayer;
        [SerializeField] int characterLayer;
        [SerializeField] int characterOnlyLayer;
        [SerializeField] int friendsHUDPlayerMenu;
        [SerializeField] int playerInfoCardMenu;
        [SerializeField] int avatarTriggerMask;



        public int DefaultLayer          => defaultLayer;
        public int OnPointerEventLayer   => onPointerEventLayer;
        public int CharacterLayer        => characterLayer;
        public int CharacterOnlyLayer    => characterOnlyLayer;
        public int FriendsHUDPlayerMenu  => friendsHUDPlayerMenu;
        public int PlayerInfoCardMenu    => playerInfoCardMenu;
        public int AvatarTriggerMask     => avatarTriggerMask;

        public LayerMask PhysicsCastLayerMask => 1 << OnPointerEventLayer;
        public LayerMask PhysicsCastLayerMaskWithoutCharacter => (PhysicsCastLayerMask | (1 << defaultLayer)) & ~(1 << characterLayer) & ~(1 << characterOnlyLayer);

        // TODO: DELETE Awake - here for easy copy during re-write of shity code
        void Awake() {
            defaultLayer          = LayerMask.NameToLayer("Default");
            onPointerEventLayer   = LayerMask.NameToLayer("OnPointerEvent");
            characterLayer        = LayerMask.NameToLayer("CharacterController");
            characterOnlyLayer    = LayerMask.NameToLayer("CharacterOnly");
            friendsHUDPlayerMenu  = LayerMask.NameToLayer("FriendsHUDPlayerMenu");
            playerInfoCardMenu    = LayerMask.NameToLayer("PlayerInfoCardMenu");
            avatarTriggerMask     = LayerMask.GetMask("AvatarTriggerDetection");
        }
        
    }

    #if UNITY_EDITOR
    // TODO: MOVE TO EDITOR FOLDER - if this is new to you See Aaron or speak with someone who actually knows unity before changing  "and not just someone claiming to or someone that has made a game... they are not qualified!"
    // THIS allows for use to set by a humanreadable name vs remembering indexs
    
    [CustomEditor(typeof(PhysicsLayersConfigScriptable))]
    class PhysicsLayersConfigScriptableEditor : Editor {
        
        public override void OnInspectorGUI() {
            // for avatarTriggerMask - Masks are 32 bitmask so not just a simple int/index
            string[] options = new string[32];
            for(int i = 0; i < 32; i++) { // get layer names
                if(LayerMask.LayerToName(i).Length>0){
                    options[i] = LayerMask.LayerToName(i);
                }
            }


            SerializedProperty defaultLayer         = serializedObject.FindProperty("defaultLayer");
            SerializedProperty onPointerLayer       = serializedObject.FindProperty("onPointerEventLayer");
            SerializedProperty characterLayer       = serializedObject.FindProperty("characterLayer");
            SerializedProperty characterOnlyLayer   = serializedObject.FindProperty("characterOnlyLayer");
            SerializedProperty friendsHUDPlayerMenu = serializedObject.FindProperty("friendsHUDPlayerMenu");
            SerializedProperty playerInfoCardMenu   = serializedObject.FindProperty("playerInfoCardMenu");
            SerializedProperty avatarTriggerMask    = serializedObject.FindProperty("avatarTriggerMask");


            defaultLayer.intValue         = EditorGUILayout.LayerField("Default Layer",                 defaultLayer.intValue);
            onPointerLayer.intValue       = EditorGUILayout.LayerField("On Pointer Layer",              onPointerLayer.intValue);
            characterLayer.intValue       = EditorGUILayout.LayerField("Character Layer",               characterLayer.intValue);
            characterOnlyLayer.intValue   = EditorGUILayout.LayerField("Character Only Layer",          characterOnlyLayer.intValue);
            friendsHUDPlayerMenu.intValue = EditorGUILayout.LayerField("Friend HUD Player Menu Layer",  friendsHUDPlayerMenu.intValue);
            playerInfoCardMenu.intValue   = EditorGUILayout.LayerField("Player Info Card Menu Layer",   playerInfoCardMenu.intValue);
            avatarTriggerMask.intValue    = EditorGUILayout.MaskField("Avatar Trigger Mask Layer",      avatarTriggerMask.intValue, options);

            serializedObject.ApplyModifiedProperties();
            
        }
    }
    #endif
}

