namespace ABEY {
    using UnityEngine;
    using System.Collections.Generic;
    /// <summary>
    /// This is just a quick hack for removing all use of resources so we can 
    /// really start on making this app better
    /// </summary>
    [CreateAssetMenu(fileName = "PrefabRefs", menuName = "ABEY/PrefabRefs", order = 0)]
    class PrefabRefsScriptableObject : ScriptableObject {
        
        [SerializeField] List<GameObject> refs;

        public GameObject GetPrefab(string name){
            Debug.Log($"GetPrefab {name} ");
            if(name.Contains("/")){
                string[] n = name.Split('/');
                name = n[n.Length-1];
            }
            GameObject go = refs.Find(g => g.name==name);
            Debug.Log($"GetPrefab {name} found: {go}");
            return go;
        }
    }
}