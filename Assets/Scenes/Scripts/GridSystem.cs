using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GridSystem : MonoBehaviour
{
[SerializeField] private Transform WallTransformX;
[SerializeField] private Transform WallTransformZ;
[SerializeField] private Transform Corner;
[SerializeField] private Transform Corner2;
[SerializeField] private Transform Corner3;
[SerializeField] private Transform Corner4;
[SerializeField] private BuildWall wall;
[SerializeField] private BuildWall wall1;
[SerializeField] private BuildWall wall2;
[SerializeField] private ThreeExitMove charM;
[SerializeField] private Transform Fire;
private List<GameObject> player;
 public static GridSystem Instance { get; private set; }
   private GridBuilder<PathNode> grid;
   private GridBuilder<PathNode> grid1;
private GridBuilder<PathNode> grid2;
    private Pathfinding pathfinding;
   private Pathfinding pathfinding1;
   private Pathfinding pathfinding2;
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
      Instance = this;
       int gridWidth = 35;
       int gridHeight = 25;
       float cellSize =5f;

       //grid = new GridBuilder<PathNode>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridBuilder<PathNode> g, int x , int z) => new PathNode(g , x , z));
       grid = new GridBuilder<PathNode>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridBuilder<PathNode> g, int x , int z) => new PathNode(g , x , z));
       //grid1 = new GridBuilder<PathNode>(25, 10, cellSize, new Vector3(75,0,0), (GridBuilder<PathNode> g1, int x1 , int z1) => new PathNode(g1 , x1 , z1));
//grid2=new GridBuilder<PathNode>(20, 15, cellSize, new Vector3(75,0,50), (GridBuilder<PathNode> g2, int x2 , int z2) => new PathNode(g2 , x2 , z2));
pathfinding= new Pathfinding(grid);
pathfinding1 =new Pathfinding(grid);
pathfinding2 = new Pathfinding(grid);
wall = new BuildWall(grid,pathfinding);
player =new List<GameObject>();
buildLandscape();
   }

private void Update() {
    /* if(Input.GetMouseButtonDown(0)){
buildaCharacter(Character,Mouse3D.GetMouseWorldPosition());
player.Add(GameObject.Find("Blocky_Dude_Red_Mobile_2"));
    }*/
         if(Input.GetMouseButtonDown(1)){
BuildFire(Fire, Mouse3D.GetMouseWorldPosition());
    } 
   }
private void FixedUpdate() {
  // Debug.Log("number of players counting"+ player.Count);
   // if(Input.GetKeyDown("space")){
 //charM.SetTargetPosition(new Vector3(2.5f, 0, 32.5f),new Vector3(97.5f, 0, 2.5f),new Vector3(172.5f, 0, 72.5f));
 //Timer.Create(() => charM.SetTargetPosition(new Vector3(2.5f, 0, 32.5f),new Vector3(97.5f, 0, 2.5f),new Vector3(172.5f, 0, 72.5f)),1f);    
   // charM.SetTargetPosition(new Vector3(2.5f, 0, 32.5f),new Vector3(97.5f, 0, 2.5f),new Vector3(172.5f, 0, 72.5f));
    //}
 //if(player.Count>0){
  //charM.SetTargetPosition(new Vector3(2.5f, 0, 32.5f),new Vector3(97.5f, 0, 2.5f),new Vector3(172.5f, 0, 72.5f));  
 //}
}
   public Transform buildaCharacter(Transform prefab, Vector3 PrefabPosition){
         //Vector3 blockchar = ((new Vector3(5,0,5) * 5f) + (new Vector3(1,0,1) * 2.5f));
        pathfinding.GetGridBuilder().GetXZ(PrefabPosition, out int x, out int z);
   //Debug.Log(Mouse3D.GetMouseWorldPosition());
   PathNode pathNode =grid.getValue(x,z);
  //Debug.Log("This is pathnode" + pathNode);
   if(pathNode.CanBuild()){
  Transform builtT = Instantiate(prefab,grid.GetWorldPosition(x,z)+(new Vector3(1,0,1) * 2.5f), Quaternion.Euler(Vector3.up * 90));
  //Instantiate(prefab,grid.GetWorldPosition(x,z)+ (new Vector3(1,0,1) * 2.5f), Quaternion.Euler(Vector3.up * 90));
   pathNode.SetTransform(builtT);
  // pathfinding.GetNode(x,z).SetIsWalkable(false);  
  return builtT; 
   }
return null;
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
   // pathfinding.GetNode(x-1,z).SetIsWalkable(false); 
     //pathfinding.GetNode(x,z-1).SetIsWalkable(false); 
   }

   }
public void buildLandscape(){
wall.createWallSeqX(WallTransformX,(float)5,0,15);
  wall.createCorner(Corner3,new Vector3(85,0,0),0);
  wall.createWallSeqX(WallTransformX,(float)105,0,12);
  wall.createWallSeqZ(WallTransformZ,0,5,2);
  wall.createCorner(Corner,new Vector3(0,0,0),0);
   wall.createCorner(Corner2,new Vector3(0,0,20),0);
  wall.createWallSeqZ(WallTransformZ,0,40,15);
   wall.createCorner(Corner,new Vector3(0,0,35),0);
  wall.createCorner(Corner2,new Vector3(0,0,120),0);
  wall.createWallSeqX(WallTransformX,5,(float)122.73,32);
  wall.createCorner(Corner3,new Vector3((float)170,(float)-0.2,0),0);
  wall.createWallSeqZ(WallTransformZ,(float)172.65,5,11);
  wall.createWallSeqZ(WallTransformZ,(float)172.65,75,8);
  wall.createCorner(Corner4,new Vector3((float)172.5,0,120),0);
  //wall2.createWallSeqX(WallTransformX,80,(float)122.73,60);
   //wall.createWall(WallTransformZ,5,0,(float)0.1,new Vector3((float)0.5,3,35));

///Building rooms
  wall.createWallSeqX(WallTransformX,(float)5,(float)22.5,7);
   wall.createWallSeqX(WallTransformX,(float)55,(float)22.5,5);
    wall.createWallSeqZ(WallTransformZ,(float)87.7,5,2);
    wall.createCorner(Corner4,new Vector3((float)87.7,0,20),0);
     wall.createWallSeqX(WallTransformX,(float)5,(float)35,4);
     wall.createCorner(Corner3,new Vector3((float)30,0,35),0);
     wall.createWallSeqZ(WallTransformZ,(float)32.66,45,5);
      wall.createWallSeqZ(WallTransformZ,(float)32.66,85,6);
//room3
      wall.createWallSeqX(WallTransformX,(float)35,(float)85,4);
      wall.createWallSeqX(WallTransformX,(float)70,(float)85,6);
      wall.createWallSeqZ(WallTransformZ,(float)105,85,6);
      wall.createWallSeqZ(WallTransformZ,(float)115,90,6);
      wall.createWallSeqX(WallTransformX,(float)120,(float)85,3);
      wall.createWallSeqX(WallTransformX,(float)145,(float)85,4);
       wall.createCorner(Corner,new Vector3((float)115,0,85),0);
//Cafeteria
wall.createWallSeqZ(WallTransformZ,(float)55,40,4);
wall.createCorner(Corner,new Vector3((float)55,0,35),0);
wall.createWallSeqX(WallTransformX,(float)60,(float)35,8);
//wall.createWallSeqX(WallTransformX,(float)115,(float)35,3);
 wall.createCorner(Corner3,new Vector3((float)115,0,35),0);
 wall.createWallSeqZ(WallTransformZ,(float)118,40,4);
 wall.createWallSeqX(WallTransformX,(float)55,(float)65,12);

//room4
wall.createWallSeqX(WallTransformX,(float)130,(float)60,6);
 wall.createWallSeqZ(WallTransformZ,(float)130,15,8);
 wall.createWallSeqX(WallTransformX,(float)105,(float)15,5);
  wall.createWallSeqZ(WallTransformZ,(float)105,10,0);
}


}
