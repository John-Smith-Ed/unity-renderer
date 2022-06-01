namespace ABEY {
    using UnityEngine;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This is just a quick hack for removing all use of resources so we can 
    /// really start on making this app better
    /// </summary>
    public abstract class RefScriptableObject<T> : ScriptableObject where T : Object {
        
        [SerializeField] List<T> refs;
        public T[]               Refs => refs.ToArray();

        public T GetRef(string name){
            Debug.Log($"GetRef {name} ");
            if(name.Contains('/')){
                string[] n = name.Split('/');
                name = n[n.Length-1];
            }
            
            foreach(T  r in refs){
                Debug.Log($"GetRef Checking {r.name} with {name}");
                if(r.name.Equals(name)){
                    Debug.Log($"GetRef {name} found: {r.name}");
                    return r;
                }
            } 
            Debug.LogError($"{name} IS NULL");
            Debug.Log($"GetRef {name} find done");
          //  Debug.Log($"GetPrefab {name} found: {go}");
            return null;
        }

        void OnValidate() {
            refs = refs.Distinct().ToList();
        }
    }
}