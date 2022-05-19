using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour{

    public void CombineMeshes(){
        
        Quaternion oldRotation  = transform.rotation;
        Vector3 oldPositon      = transform.position;

        transform.rotation      = Quaternion.identity;
        transform.position      = Vector3.zero;

        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();
        
        Debug.Log($"name {filters.Length}");

        Mesh finalMesh = new Mesh();
        Matrix4x4 ourMatrix = transform.localToWorldMatrix;

        finalMesh.indexFormat       = UnityEngine.Rendering.IndexFormat.UInt32;
        CombineInstance[] combiners = new CombineInstance[filters.Length];

        for(int a=0; a<filters.Length;a++){
            if(filters[a].transform==transform){continue;} // skip self, this is were we are building to
            combiners[a].subMeshIndex   = 0;
            combiners[a].mesh           = filters[a].sharedMesh;
            combiners[a].transform      = filters[a].transform.localToWorldMatrix;
        }

        finalMesh.CombineMeshes(combiners);
        GetComponent<MeshFilter>().sharedMesh = finalMesh;

        transform.rotation      = oldRotation;
        transform.position      = oldPositon;


    }
}
