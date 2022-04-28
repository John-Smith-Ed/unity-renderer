using DCL.Helpers;
using UnityEngine;

public class MinimapMetadataController : MonoBehaviour
{
    private MinimapMetadata minimapMetadata => MinimapMetadata.GetMetadata();
    public static MinimapMetadataController i { get; private set; }

    public void Awake()
    {
        i = this;
        minimapMetadata.Clear();
    }

    public void UpdateMinimapSceneInformation(string scenesInfoJson)
    {
        var scenesInfo = Utils.ParseJsonArray<MinimapMetadata.MinimapSceneInfo[]>(scenesInfoJson);

        ABEY.LogWriter.Write("MiniMapServerJson",scenesInfoJson);

        foreach (var sceneInfo in scenesInfo)
        {
            minimapMetadata.AddSceneInfo(sceneInfo);
        }
    }
}