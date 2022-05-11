using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace ABEY{

    static public class LogWriter {

        public class LogQueue{
            public string file;
            public Queue<string> data = new Queue<string>();
        }

        static Dictionary<string, LogQueue> files = new Dictionary<string, LogQueue>();
        
        
        //static string Path => $"{Application.dataPath}/ABEY/Loger/Logs";
        static string Path => $"{Application.dataPath}/../Logs/ABEY";
        

        static string FilePath(string filename) => $"{Path}/{filename}.txt";

        static LogWriter(){
            Application.quitting+=WriteAll;
            if(!Directory.Exists(Path)){
                Directory.CreateDirectory(Path);
            }
        }

        static LogQueue CreateLog(string filename){
            
            string logPath="";
            if(files.ContainsKey(filename)){
                return files[filename];
            }else{
             //   #if UNITY_EDITOR
             //   logPath=AssetDatabase.GenerateUniqueAssetPath(FilePath(filename));
             //   #else
                logPath=FilePath(filename);
               // #endif
                using (FileStream sw = File.Create(logPath)){}
                LogQueue q = new LogQueue(){file=logPath};
                files.Add(filename, q);
                return q;
            }
        }
            
        public static void Write(string filename, string data, int writeAt=5){
            
            LogQueue queue = CreateLog(filename);
            queue.data.Enqueue(data);

            //write Queue
            if(queue.data.Count>=writeAt){
                WriteQueue( queue);  
            }
                
        }
        static void WriteQueue(LogQueue queue){
            // skiping the file right, this sometimes brakes the game, there is some weird 'sharing error' when writeing the log file
            // it does not always happen, this is just used to scrap info for areas that are loading api data
            //return;
           
               // #endif
            
            

            using (StreamWriter sw = File.AppendText(queue.file)) {
                while(queue.data.Count>0){
                    sw.WriteLine(queue.data.Dequeue());
                }            
            }
        }
        public static void WriteAll(){
            foreach (KeyValuePair<string, LogQueue> queue in files){
                WriteQueue(queue.Value);
            }
        }

    }
}