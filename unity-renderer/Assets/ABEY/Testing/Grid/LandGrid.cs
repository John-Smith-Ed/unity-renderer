using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABEY{
    public class LandGrid : MonoBehaviour{

        [SerializeField] GameObject[] plots;

        Grid grid;

        GameObject  RandomPlot => plots[Random.Range(0, plots.Length)];

        void Awake() {
            grid = new Grid(6,6);

           
        }
        void Start(){
            for(int x=0;x<6;x++){
                for(int y=0;y<6;y++){
                    CreateLand(x,y);
                }
            }
        }

        void CreateLand(int x, int y){
            Instantiate(RandomPlot, grid.GetWorldPosition(x, y), Quaternion.identity, transform);
        }
    }
}
