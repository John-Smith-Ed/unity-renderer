namespace ABEY.ABEYEditor {
    

    using UnityEngine;
    using UnityEditor;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ConfigWindow : EditorWindow{

        static ConfigWindow window;
        ConfigScriptable config;
        Editor editingConfig;
        string activeConfig;

        Dictionary<string, SerializedProperty> configs= new Dictionary<string, SerializedProperty>();

        [MenuItem("ABEY/Config Window", false, 2)]
        public static void OpenWindow(){            
            System.Type inspectorType = System.Type.GetType("UnityEditor.SceneView,UnityEditor.dll");
            window = (ConfigWindow)EditorWindow.GetWindow<ConfigWindow>(new System.Type[] { inspectorType });
            window.titleContent = new GUIContent("Main Configs");
        }

        public ConfigWindow(){

        }

        void LoadConfig(){
            //TODO: handel no config or more then one - only one should exsist in prod but we can have more for testing other settings!
            if(config==null){
                string[] assetGUIDS = AssetDatabase.FindAssets("t:ConfigScriptable");
                string path = AssetDatabase.GUIDToAssetPath(assetGUIDS[0]);
                config = (ConfigScriptable)AssetDatabase.LoadAssetAtPath(path, typeof(ConfigScriptable));
            }
            
        }

        void OnGUI(){
            LoadConfig();
            SerializedObject so = new SerializedObject(config);

          
            SerializedProperty props = so.GetIterator();
            props.Next(true);
            LoadConfigs(props);
            DrawToolBar();

            if(editingConfig!=null){
                GUILayout.Label(editingConfig.name);
                editingConfig.DrawDefaultInspector();
            }
            
            
        }

        void LoadConfigs(SerializedProperty parent){
            configs.Clear();
            while(parent.Next(false)){
                SerializedProperty sp = parent.Copy();
                if(!sp.type.Contains("ConfigScriptable")){continue;}
                configs.Add(sp.name, sp);
            }
            if(editingConfig==null){
                KeyValuePair<string, SerializedProperty> item = configs.First();
                activeConfig    = item.Key;
                editingConfig   = Editor.CreateEditor(item.Value.objectReferenceValue);
            }
        }

        void DrawToolBar(){                        
            // Is responsive and will wrap
            GUIStyle style        = new GUIStyle(GUI.skin.button); // for size check and default style
            GUIStyle styleActive  = new GUIStyle(GUI.skin.button); // active button stype
            styleActive.normal.textColor = Color.green;
            float used=0f;
            
            GUILayout.BeginHorizontal();
            foreach(KeyValuePair<string, SerializedProperty> item in configs){
                
                used += style.CalcSize(new GUIContent(item.Key)).x;   
                if(used>=EditorGUIUtility.currentViewWidth){
                    used=0;
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
                if(GUILayout.Button(Capitalize(SplitCamelCase(item.Key)), (activeConfig == item.Key ? styleActive : style))){
                    activeConfig    = item.Key;
                    editingConfig   = Editor.CreateEditor(item.Value.objectReferenceValue);
                }
            }
            GUILayout.EndHorizontal();
        }

        public string Capitalize(string str) =>char.ToUpper(str[0]) + str.Substring(1);
        public string SplitCamelCase( string str ) {
            return System.Text.RegularExpressions.Regex.Replace( 
                System.Text.RegularExpressions.Regex.Replace( 
                    str, 
                    @"(\P{Ll})(\P{Ll}\p{Ll})", 
                    "$1 $2" 
                ), 
                @"(\p{Ll})(\P{Ll})", 
                "$1 $2" 
            );
        }

    }
}