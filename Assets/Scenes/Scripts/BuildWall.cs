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
public void createWallSeqZ(Transform prefab,float StartX,float StartZ,float blockNumbers){
for (int i=0; i<=blockNumbers; i++){
createWallX(prefab,StartX,StartZ);
StartZ=StartZ + 5f;
}
}
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

