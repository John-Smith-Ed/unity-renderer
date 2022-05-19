using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshCombiner))]
public class MeshCombinerEditor : Editor{

    MeshCombiner mc => target as MeshCombiner;

    void OnSceneGUI() {

        

        if(Handles.Button(mc.transform.position+Vector3.up * 5, Quaternion.LookRotation(Vector3.up), 1, 1, Handles.CylinderHandleCap)){
            CombineMeshes();
        }    
    }

    public void CombineMeshes(){
        
        Quaternion oldRotation  = mc.transform.rotation;
        Vector3 oldPositon      = mc.transform.position;

        mc.transform.rotation      = Quaternion.identity;
        mc.transform.position      = Vector3.zero;

        MeshFilter[] filters = mc.GetComponentsInChildren<MeshFilter>();
        
        Debug.Log($"name {filters.Length}");

        Mesh finalMesh = new Mesh();
        Matrix4x4 ourMatrix = mc.transform.localToWorldMatrix;

        finalMesh.indexFormat       = UnityEngine.Rendering.IndexFormat.UInt32;
        CombineInstance[] combiners = new CombineInstance[filters.Length];

        for(int a=0; a<filters.Length;a++){
            if(filters[a].transform==mc.transform){continue;} // skip self, this is were we are building to
            combiners[a].subMeshIndex   = 0;
            combiners[a].mesh           = filters[a].sharedMesh;
            combiners[a].transform      = filters[a].transform.localToWorldMatrix;
        }

        finalMesh.CombineMeshes(combiners);
        mc.GetComponent<MeshFilter>().sharedMesh = finalMesh;

        mc.transform.rotation      = oldRotation;
        mc.transform.position      = oldPositon;

        AssetDatabase.CreateAsset(finalMesh, $"Assets/ABEY/Testing/models/{mc.name}.asset");
        AssetDatabase.SaveAssets();


    }
}
