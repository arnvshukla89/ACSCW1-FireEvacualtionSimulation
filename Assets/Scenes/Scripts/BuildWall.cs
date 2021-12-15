/*-------------------------------------------

Class:BuildWall
Functionality:Building walls on grid positions
//---------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWall : MonoBehaviour
{
public GridBuilder<PathNode> builder;
private Pathfinding pathfinding;

public BuildWall(GridBuilder<PathNode> builder,Pathfinding pathfinding){
 this.builder = builder;
 this.pathfinding = pathfinding;
}
public void createWallSeqX(Transform prefab,float StartX,float StartZ,float blockNumbers){
for (int i=0; i<=blockNumbers; i++){
createWallX(prefab,StartX,StartZ);
StartX = StartX + 5f;
}
}

  /*-------------------------------------

   Functionality: Spawn a wall on Z axis
   Methods:createWallSeqZ()
   Params:Transform object, Start X value, STart Z value, number of grid nodes to cover
   --------------------------------------*/
public void createWallSeqZ(Transform prefab,float StartX,float StartZ,float blockNumbers){
for (int i=0; i<=blockNumbers; i++){
createWallX(prefab,StartX,StartZ);
StartZ=StartZ + 5f;
}
}
/*-------------------------------------

   Functionality: Spawn a wall on X axis
   Methods:createWallSeqX()
   Params:Transform object, Start X value, STart Z value)
   --------------------------------------*/
public void createWallX(Transform prefab,float x, float z){
  Vector3 vec1 = new Vector3(x,0,z);   
  //Vector3 vec1 = new Vector3(x,0,z)* 5f+ (new Vector3(1,0,1) * 2.5f);
builder.GetXZ(vec1, out int xcord, out int zcord);
PathNode pathNode =builder.getValue(xcord,zcord);
//Debug.Log(pathNode);
if(pathNode.CanBuild()){
  Transform builtTransform = Instantiate(prefab,new Vector3(x,0,z),Quaternion.identity);
   pathNode.SetTransform(builtTransform);
   pathfinding.GetNode(xcord,zcord).SetIsWalkable(false);  
}
}
/*-------------------------------------

   Functionality: Spawn Corners
   Methods:createCorner()
   Params:Transform object, spaw position, rotation value
   --------------------------------------*/
public void createCorner(Transform prefab1,Vector3 position,float rotation){
 
   Vector3 vec1 = position;     
builder.GetXZ(vec1, out int xcord, out int zcord);
PathNode pathNode =builder.getValue(xcord,zcord);
//Debug.Log("First condition" + pathNode);
   if(pathNode.CanBuild()){
  Transform builtTransform = Instantiate(prefab1,position, Quaternion.Euler(Vector3.up * rotation));
   pathNode.SetTransform(builtTransform);
   pathfinding.GetNode(xcord,zcord).SetIsWalkable(false);  
   
   }
}
}

