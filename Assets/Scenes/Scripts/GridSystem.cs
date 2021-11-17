using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GridSystem : MonoBehaviour
{
[SerializeField] private Transform WallTransform;
[SerializeField] private Transform Character;
[SerializeField] private MovingCharacter charM;
[SerializeField] private Transform Fire;
 public static Pathfinding Instance { get; private set; }
   private GridBuilder<PathNode> grid;
    private Pathfinding pathfinding;
     private const float speed = 1f;
    private int currentPathIndex;
    private Vector3 startP = Vector3.zero;
     private Vector3 endP = Vector3.zero;
  private utils Utils;
    public Pathfinding GetPath(){
       return pathfinding;
   }
   
//public static GridSystem Instance { get; private set;}
   private void Awake() {
      //Instance = this;
       int gridWidth = 10;
       int gridHeight = 20;
       float cellSize =5f;

       //grid = new GridBuilder<PathNode>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridBuilder<PathNode> g, int x , int z) => new PathNode(g , x , z));
       grid = new GridBuilder<PathNode>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridBuilder<PathNode> g, int x , int z) => new PathNode(g , x , z));
pathfinding= new Pathfinding(grid);
buildaWall();

   }


private void Update() {
     if(Input.GetMouseButtonDown(0)){
buildaCharacter(Character,Mouse3D.GetMouseWorldPosition());
    }
         if(Input.GetMouseButtonDown(1)){
BuildFire(Fire, Mouse3D.GetMouseWorldPosition());
    }
 if(Input.GetKeyDown("space")){
 charM.SetTargetPosition(new Vector3(47.5f, 0, 17.5f));    
 } 
   }
   public void buildaCharacter(Transform prefab, Vector3 PrefabPosition){
         //Vector3 blockchar = ((new Vector3(5,0,5) * 5f) + (new Vector3(1,0,1) * 2.5f));
        pathfinding.GetGridBuilder().GetXZ(PrefabPosition, out int x, out int z);
   //Debug.Log(Mouse3D.GetMouseWorldPosition());
   PathNode pathNode =grid.getValue(x,z);
  //Debug.Log("This is pathnode" + pathNode);
   if(pathNode.CanBuild()){
  Transform builtT = Instantiate(prefab,grid.GetWorldPosition(x,z)+(new Vector3(1,0,1) * 2.5f), Quaternion.Euler(Vector3.up * 90));
  //Instantiate(prefab,grid.GetWorldPosition(x,z)+ (new Vector3(1,0,1) * 2.5f), Quaternion.Euler(Vector3.up * 90));
   pathNode.SetTransform(builtT);
    pathfinding.GetNode(x,z).SetIsWalkable(false);  
   }

   }
      public void BuildFire(Transform prefab, Vector3 PrefabPosition){
         //Vector3 blockchar = ((new Vector3(5,0,5) * 5f) + (new Vector3(1,0,1) * 2.5f));
        pathfinding.GetGridBuilder().GetXZ(PrefabPosition, out int x, out int z);
   //Debug.Log(Mouse3D.GetMouseWorldPosition());
   PathNode pathNode =grid.getValue(x,z);
  //Debug.Log("This is pathnode" + pathNode);
   if(pathNode.CanBuild()){
  Transform builtT = Instantiate(prefab,grid.GetWorldPosition(x,z)+(new Vector3(1,0,1) * 2.5f), Quaternion.Euler(Vector3.up * 90));
   pathNode.SetTransform(builtT);
    pathfinding.GetNode(x,z).SetIsWalkable(false); 
    pathfinding.GetNode(x+1,z).SetIsWalkable(false);
    pathfinding.GetNode(x,z+1).SetIsWalkable(false);    
    pathfinding.GetNode(x-1,z).SetIsWalkable(false); 
     pathfinding.GetNode(x,z-1).SetIsWalkable(false); 
   }

   }

   public void buildaWall(){
int h= grid.GetHeight();
int w =grid.GetWidth();
for(int i=0 ; i< w; i++){
    buildWall(i , 0);
}
for(int i=1 ; i< h; i++){
    buildWall(0 , i);
}
for(int i=1 ; i< w; i++){
    buildWall(i ,h-1);
}
for(int i= h-1 ; i > h-16; i--){
    buildWall(w-1, i);
    
    buildWall(w-1,1);
}
   }
public void buildWall(int x, int z){ 
if(x!= 0 && z!= 0){    
      Vector3 vec1 = new Vector3(x,0,z) * 5f + new Vector3(1,0,1) * 2.5f;
      
        Debug.Log("First condition" + "Vector vaue is :" + vec1);
        pathfinding.GetGridBuilder().GetXZ(vec1, out int xcord, out int zcord);
   PathNode pathNode =grid.getValue(xcord,zcord);
   //Debug.Log("First condition" + pathNode);
   if(pathNode.CanBuild()){
  Transform builtTransform = Instantiate(WallTransform,grid.GetWorldPosition(xcord,zcord), Quaternion.identity);
   pathNode.SetTransform(builtTransform);
   pathfinding.GetNode(xcord,zcord).SetIsWalkable(false);  
   }
}
else if(z == 0){
 Vector3 vec1 = new Vector3(x, 0, 0) * 5f + new Vector3(1,0,1) * 2.5f;
  //Debug.Log("Second condition" + "Vector vaue is :" + vec1);
   pathfinding.GetGridBuilder().GetXZ(vec1, out int xcord, out int zcord);
   PathNode pathNode =grid.getValue(xcord,zcord);
   if(pathNode.CanBuild()){
  Transform builtTransform = Instantiate(WallTransform,grid.GetWorldPosition(xcord,zcord), Quaternion.identity);
   pathfinding.GetNode(xcord,zcord).SetIsWalkable(false); 
    pathNode.SetTransform(builtTransform);
   }
}
else if(x == 0){
Vector3 vec1 = new Vector3(0, 0, z) * 5f + new Vector3(1,0,1) * 2.5f;
//Debug.Log("Third condition" + "Vector vaue is :" + vec1);
   pathfinding.GetGridBuilder().GetXZ(vec1, out int xcord, out int zcord);
   PathNode pathNode =grid.getValue(xcord,zcord);
   if(pathNode.CanBuild()){
  Transform builtTransform = Instantiate(WallTransform,grid.GetWorldPosition(xcord,zcord), Quaternion.identity);
   pathfinding.GetNode(xcord, zcord).SetIsWalkable(false); 
    pathNode.SetTransform(builtTransform);
   }
}
}
public void buildGround(){
Vector3 vec1 = new Vector3(0, 0, 0) * 5f + new Vector3(1,0,1) * 2.5f;
   pathfinding.GetGridBuilder().GetXZ(vec1, out int xcord, out int zcord);
   PathNode pathNode =grid.getValue(xcord,zcord);
   if(pathNode.CanBuild()){
  Transform builtTransform = Instantiate(WallTransform,grid.GetWorldPosition(xcord,zcord), Quaternion.identity);
   pathfinding.GetNode(xcord, zcord).SetIsWalkable(false); 
    pathNode.SetTransform(builtTransform);
}
}

}
