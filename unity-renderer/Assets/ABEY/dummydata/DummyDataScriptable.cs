using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DummyDataScriptable", menuName = "ABEY/DummyDataScriptable", order = 0)]
class DummyDataScriptable : ScriptableObject {

    [Serializable]
    public class LoadingScreenMsg{
        public bool isVisible;
        public string message;
        public bool showTips;
    }
    [Serializable]
    public class ConfigureHUDElement{
        public int hudElementId;
        public HUDConfigure configuration;
        public ExtraPayload extraPayload; 
    }
    [Serializable]
    public class HUDConfigure{
        public bool active;
        public bool visible;
    }
    [Serializable]
    public class ExtraPayload{
        public bool enableVoiceChat;
        public bool enableQuestPanel;
    }

   
    
    [SerializeField] TextAsset[] datas;

    public string GetData(string name) => datas.First(d => d.name==name)?.ToString();

    public string GetLoadingScreen(bool show, string msg, bool tips) {
        return JsonUtility.ToJson(new LoadingScreenMsg(){
            isVisible   = show,
            message     = msg,
            showTips    = tips
        });
    }

    public string GetConfigureHUDElement(int id, bool active,bool visible, ExtraPayload ext = null ) {
        return JsonUtility.ToJson(new ConfigureHUDElement(){
            hudElementId   = id,
            configuration  = new HUDConfigure(){active=active,visible=visible},
            extraPayload   = ext
        });
    }


}