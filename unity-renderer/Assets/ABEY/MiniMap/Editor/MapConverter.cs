using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapConverter : EditorWindow {



    [MenuItem("ABEY/Create Mini Map Scriptable", false, 2)]
    public static void BuildAbeyMiniMap(){
        return;
        string mapData          = File.ReadAllText("Assets/ABEY/MiniMap/Editor/Resources/ABW-Map-mapped.csv");     
        string[] tiles          = mapData.Split(',');           
        MinimapMetadata asset   = ScriptableObject.CreateInstance<MinimapMetadata>(); 

        
        for(int i=0; i< tiles.Length; i++){                
            Debug.Log($"Plot {i}");     

            MinimapMetadata.MinimapSceneInfo info = new MinimapMetadata.MinimapSceneInfo();
            info.name = "";
            info.type= ConvertTileType(tiles[i]);
            info.parcels = new List<Vector2Int>(){IndexToCord(i)};
            info.isPOI= false;
            info.owner = "ABEY";
            info.description = ConvertDescription(tiles[i]);
            info.previewImageUrl = "";
            asset.AddSceneInfo(info);
        }
    

        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/ABEY/MiniMap/abeyMap.asset");
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;

    }
    
    static MinimapMetadata.TileType ConvertTileType(string t){
        switch(t){
            case "1": return (MinimapMetadata.TileType)11;
            case "2": return (MinimapMetadata.TileType)11;
            case "3": return (MinimapMetadata.TileType)11;
            case "4": return (MinimapMetadata.TileType)11;
            case "7": return (MinimapMetadata.TileType)7;
            case "8": return (MinimapMetadata.TileType)8;
            default: return  (MinimapMetadata.TileType)5; //(District is 5) but this really is unkown
        }
    }

    static string ConvertDescription(string t){
        switch(t){
            case "1": return "Level 1";
            case "2": return "Level 2";
            case "3": return "Level 3";
            case "4": return "Level 4";
            case "7": return "Road";
            case "8": return "Plaza";
            default: return  $"Unknown for {t}";
        }
    }

    static Vector2Int IndexToCord(int i){
        //230x230?
        //-115 to 115
        int NUMBER_ITEMS_IN_ROW = 230;
         int col = (i % NUMBER_ITEMS_IN_ROW) - (int)(NUMBER_ITEMS_IN_ROW*0.5f);
         int row = (i / NUMBER_ITEMS_IN_ROW) - (int)(NUMBER_ITEMS_IN_ROW*0.5f);

        return new Vector2Int(
            col,
            row
        );
    }


}
