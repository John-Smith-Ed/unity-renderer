namespace ABEY {
    using UnityEngine;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This is just a quick hack for removing all use of resources so we can 
    /// really start on making this app better
    /// </summary>
    [CreateAssetMenu(fileName = "PrefabRefs", menuName = "ABEY/PrefabRefs", order = 0)]
    class PrefabRefsScriptableObject : ScriptableObject {
        
        [SerializeField] List<GameObject> refs;

        public GameObject GetPrefab(string name){
            Debug.Log($"GetPrefab {name} ");
            if(name.Contains('/')){
                Debug.LogWarning($"{name} Contains('/')");
                string[] n = name.Split('/');
                name = n[n.Length-1];
            }
            Debug.Log($"GetPrefab {name} Running find");
            Debug.Log($"refs {refs.Count} ");
            
            foreach(GameObject go in refs){
                Debug.Log($"GetPrefab Checking {go.name} with {name}");
                if(go.name.Equals(name)){
                    Debug.Log($"GetPrefab {name} found: {go.name}");
                    return go;
                }
            } 
            Debug.LogError($"{name} IS NULL");
            Debug.Log($"GetPrefab {name} find done");
          //  Debug.Log($"GetPrefab {name} found: {go}");
            return null;
        }

        void OnValidate() {
            refs = refs.Distinct().ToList();
        }
    }
}