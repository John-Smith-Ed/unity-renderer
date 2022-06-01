namespace ABEY {
    //Replace CommonScriptableObjects
    // the "Resouces API Should not be user - They abused it with improper use of scriptables"

    using UnityEngine;

    [CreateAssetMenu(fileName = "NotificationConfig", menuName = "ABEY/NotificationConfig", order = 0)]
    public class NotificationConfigScriptable : ScriptableObject {

        [SerializeField] FloatVariable newApprovedFriendsValue;
        [SerializeField] FloatVariable pendingChatMessagesValue;
        [SerializeField] FloatVariable pendingFriendRequestsValue;

        public FloatVariable newApprovedFriends      => newApprovedFriendsValue;
        public FloatVariable pendingChatMessages     => pendingChatMessagesValue;
        public FloatVariable pendingFriendRequests   => pendingFriendRequestsValue;


        //TODO:DELETE AWAKE - used just for the bootstraping to rewrite bad code
        // Just incase you dont know how scriptables work, Awake is called when you create a new copy - i.e in the editor
        void Awake() {
            newApprovedFriendsValue     = UnityEditor.AssetDatabase.LoadAssetAtPath<FloatVariable>("Assets/NResources/ScriptableObjects/NotificationBadge_NewApprovedFriends.asset");    
            pendingChatMessagesValue    = UnityEditor.AssetDatabase.LoadAssetAtPath<FloatVariable>("Assets/NResources/ScriptableObjects/NotificationBadge_PendingChatMessages.asset");    
            pendingFriendRequestsValue  = UnityEditor.AssetDatabase.LoadAssetAtPath<FloatVariable>("Assets/NResources/ScriptableObjects/NotificationBadge_PendingFriendRequests.asset");
        }
    
    }

}