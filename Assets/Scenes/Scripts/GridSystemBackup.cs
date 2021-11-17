using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridSystemBackup : MonoBehaviour
{
   [SerializeField] private Transform WallTransform;
[SerializeField] private Transform Character;
private MoveChar characterMovement;
 public static Pathfinding Instance { get; private set; }
   private GridBuilder<PathNode> grid;
    private Pathfinding pathfinding;
     private const float speed = 5f;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    // public PathfindingVisual pathfindingVisual;
  private utils Utils;
  public GameObject char1;
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
characterMovement = new MoveChar();
buildaWall();

   }

private void Update() {
    if(Input.GetMouseButtonDown(0)){
buildaCharacter(Character,Mouse3D.GetMouseWorldPosition());
    }
 
   /*  if(Input.GetMouseButtonDown(1)){
         Vector3 mouseWorldPosition = utils.GetMouseWorldPosition();
          Debug.Log("Mouse world position"+mouseWorldPosition);
         pathfinding.GetGridBuilder().GetXZ(mouseWorldPosition , out int x,out int y, out int z); 
         pathfinding.GetNode(x, z).SetIsWalkable(!pathfinding.GetNode(x, z).isWalkable);
     }*/
   }

private void FixedUpdate() {
      char1 = GameObject.Find("Blocky_Dude_Red_Mobile_2(Clone)"); 
     
         //if(Input.GetKeyDown("space")){
             if(Input.GetKey(KeyCode.RightArrow)){
             for(int i=0; i<5; i++){
   Vector3 startWorldPosition = char1.transform.position;
   Vector3 endWorldPosition = new Vector3(47.5f, 0, 17.5f);
 pathfinding.GetGridBuilder().GetXZ(startWorldPosition , out int startX, out int startZ);
pathfinding.GetGridBuilder().GetXZ(endWorldPosition, out int endX, out int endZ);
 List<PathNode> path = pathfinding.FindPath(startX,startZ,endX,endZ);  
            if (path != null) {
                //for (int i=0; i<path.Count - 1; i++) {
                   Debug.Log("This is the pathnode list if not null"+ path);
      List<Vector3> vectorPath = new List<Vector3>();
      foreach (PathNode pathnode in path)
      {
         vectorPath.Add(new Vector3(pathnode.x ,0, pathnode.z)* pathfinding.GetGridBuilder().GetCellSize() + new Vector3(1,0,1) *  pathfinding.GetGridBuilder().GetCellSize() * .5f); 
      }
      Debug.Log("This is the vectorpath list"+ vectorPath);

      //----------------------------------------------------------------------//
 if (vectorPath != null) {
Debug.Log("this is pathVectorList vector list:" + vectorPath.Count);
  Debug.Log("Pathvector list is not null");
  currentPathIndex = 0;
            Vector3 target = endWorldPosition;
            Debug.Log("Target position" + target);
            Debug.Log("Current position" + startWorldPosition);
            Debug.Log("Distance b/w current and target position" + Vector3.Distance(startWorldPosition, target));
            if (Vector3.Distance(startWorldPosition, target) > 1f) {
                Vector3 moveDir = (target - startWorldPosition).normalized;
Debug.Log("moveDir" + moveDir);
 //char1.transform.position = char1.transform.position + moveDir * speed * Time.deltaTime; 
     char1.transform.position += moveDir * speed * Time.deltaTime;        
            } 
 }
                     
      //-----------------------------------------------------------------------//
                  //  Debug.DrawLine(new Vector3(path[i].x, 0, path[i].z) * 5f + new Vector3(1,0,1) * 2.5f, new Vector3(path[i+1].x,0,path[i+1].z) * 5f + new Vector3(1,0,1) * 2.5f, Color.green, 5f);
               }
            
            }
         }
}
   public void buildaCharacter(Transform prefab, Vector3 PrefabPosition){
         //Vector3 blockchar = ((new Vector3(5,0,5) * 5f) + (new Vector3(1,0,1) * 2.5f));
        pathfinding.GetGridBuilder().GetXZ(PrefabPosition, out int x, out int z);
   //Debug.Log(Mouse3D.GetMouseWorldPosition());
   //PathNode pathNode =grid.getValue(x,z);
  //Debug.Log("This is pathnode" + pathNode);
  // if(pathNode.CanBuild()){
  //Transform builtT = Instantiate(Character,grid.GetWorldPosition(x,z), Quaternion.Euler(Vector3.up * 0));
  Instantiate(prefab,grid.GetWorldPosition(x,z)+ (new Vector3(1,0,1) * 2.5f), Quaternion.Euler(Vector3.up * 90));
   //pathNode.SetTransform(builtT);
   //}

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
