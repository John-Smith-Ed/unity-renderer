using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinimapMetadata", menuName = "MinimapMetadata")]
public class MinimapMetadata : ScriptableObject
{

    /*
        ABEY IS
        "blank" -> road
        0 ->    public
        some kind of levels
        1 -> level 1
        2 -> level 2
        3 -> level 3
        4 -> level 4
        
     -- Map Key Redsign ABEY --
        MyParcel = 0,
        MyParcelsOnSale = 1,
        MyEstates = 2,
        MyEstatesOnSale = 3,
        WithAccess = 4,
        District = 5,
        Contribution = 6,
        Roads = 7,
        Plaza = 8,
        Taken = 9,
        OnSale = 10,
        Unowned = 11,
        Background = 12,
        Loading = 13

        Class1-> 21
        Class2-> 22
        Class3-> 23
        Class4-> 24



    */
    public enum TileType
    {
        MyParcel = 0,
        MyParcelsOnSale = 1,
        MyEstates = 2,
        MyEstatesOnSale = 3,
        WithAccess = 4,
        District = 5,
        Contribution = 6,
        Roads = 7,
        Plaza = 8,
        Taken = 9,
        OnSale = 10,
        Unowned = 11,
        Background = 12,
        Loading = 13
    }

    [Serializable]
    public class MinimapSceneInfo
    {
        public string name;
        public TileType type;
        public List<Vector2Int> parcels;

        public bool isPOI;
        public string owner;
        public string description;
        public string previewImageUrl;
    }

    public event Action<MinimapSceneInfo> OnSceneInfoUpdated;

    HashSet<MinimapSceneInfo> scenesInfo = new HashSet<MinimapSceneInfo>();
    Dictionary<Vector2Int, MinimapSceneInfo> sceneInfoMap = new Dictionary<Vector2Int, MinimapSceneInfo>();

    // TODO:A.B  delete - added so we can visualize the data in the editor
    [SerializeField] List<MinimapSceneInfo> infoMapList = new List<MinimapSceneInfo>();

    public MinimapSceneInfo GetSceneInfo(int x, int y)
    {
        if (sceneInfoMap.TryGetValue(new Vector2Int(x, y), out MinimapSceneInfo result))
            return result;

        return null;
    }

    public void AddSceneInfo(MinimapSceneInfo sceneInfo)
    {
        if (scenesInfo.Contains(sceneInfo))
            return;

        int parcelsCount = sceneInfo.parcels.Count;

        for (int i = 0; i < parcelsCount; i++)
        {
            if (sceneInfoMap.ContainsKey(sceneInfo.parcels[i]))
            {
                //NOTE(Brian): I intended at first to just return; here. But turns out kernel is sending
                //             overlapping coordinates, sending first gigantic 'Estate' and 'Roads' scenes to
                //             send the proper scenes later. This will be fixed when we implement the content v3 data
                //             plumbing.
                sceneInfoMap.Remove(sceneInfo.parcels[i]);
            }

            sceneInfoMap.Add(sceneInfo.parcels[i], sceneInfo);
            infoMapList.Add(sceneInfo);
        }

        scenesInfo.Add(sceneInfo);

        OnSceneInfoUpdated?.Invoke(sceneInfo);
    }

    public void Clear()
    {
        scenesInfo.Clear();
        sceneInfoMap.Clear();
    }

    // A.B Loads the scriptiable into a static ref
    // The scriptable has no exposed props so why resouces? create a new instance
    private static MinimapMetadata minimapMetadata;
    public static MinimapMetadata GetMetadata()
    {
        if (minimapMetadata == null)
        {
            minimapMetadata = Resources.Load<MinimapMetadata>("ScriptableObjects/MinimapMetadata");
        }

        return minimapMetadata;
    }
}