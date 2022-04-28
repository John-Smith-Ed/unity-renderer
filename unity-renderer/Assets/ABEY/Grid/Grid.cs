using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABEY{

    public class Grid {
        //_Downloaded/QmHalloween01/QmHalloween01.glb
        
        int width;
        int height;
        float cellSize;
        int[,] gridArray;

        
        TextMesh[,] debugTextArray;

        public Grid(int width, int height, float cellSize = 16f){
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            gridArray = new int[width, height];
            debugTextArray = new TextMesh[width, height];
        }

        void DebugText(){
            for(int x=0;x<gridArray.GetLength(0);x++){
                for(int y=0;y<gridArray.GetLength(1);y++){
                    CreateWorldText(null, gridArray[x, y].ToString(), GetWorldPosition(x,y));
                    //create  text gridArray.ToString();
                    Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x,y+1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x+1,y), Color.white, 100f);
                }
            }            
            Debug.DrawLine(GetWorldPosition(0,height), GetWorldPosition(width,height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width,0), GetWorldPosition(width,height), Color.white, 100f);
        }

        public Vector3 GetWorldPosition(int x, int y){
            return new Vector3(x, 0, y) * cellSize;
        }

        public void SetValue(int x, int y, int value){
            if(x>=0 && y >=0 && x<width && y<height ){
                gridArray[x,y]=value;
                debugTextArray[x,y].text=value.ToString();
            }else{
                Debug.LogWarning($"Invlid Grid({x},{y})");
            }
            
        }

        TextMesh CreateWorldText(Transform parent, string text, Vector3 pos){
            return CreateWorldText(parent, text, pos, Color.white, 40, 10);
        }
        TextMesh CreateWorldText(Transform parent, string text, Vector3 pos, Color color, int fontSize, int order){
            // Create a new GO to house the text
            GameObject gameObject = new GameObject("World_text", typeof(TextMesh));
            gameObject.transform.SetParent(parent, false);
            gameObject.transform.localPosition = pos;
            // Get the TextMesh and set the values
            TextMesh textMesh   = gameObject.GetComponent<TextMesh>();
            textMesh.anchor     = TextAnchor.MiddleCenter;
            textMesh.alignment  = TextAlignment.Center;
            textMesh.text       = text;
            textMesh.fontSize   = fontSize;
            textMesh.color      = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder=order;
            return textMesh;
        }
    }
}