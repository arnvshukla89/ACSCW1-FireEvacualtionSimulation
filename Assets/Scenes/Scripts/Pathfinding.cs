using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding{
   private const int MOVE_STRAIGHT_COST= 10;
      private const int MOVE_DIAGONAL_COST= 14;
 public static Pathfinding Instance { get; private set; }
    public GridBuilder<PathNode> builder;
    private List<PathNode> openList;
    private List<PathNode> closedList;
  /* public Pathfinding(int width, int height){
        Instance = this;
      builder = new GridBuilder<PathNode>(width, height, 5f, Vector3.zero,(GridBuilder<PathNode> g, int x, int y) => new PathNode(g,x,y));  
   }*/
public Pathfinding(GridBuilder<PathNode> builder){
       Instance = this;
      this.builder = builder;
   }
   public GridBuilder<PathNode> GetGridBuilder(){
       return builder;
   } 
      public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition){
       builder.GetXZ(startWorldPosition , out int startX, out int startZ);
   builder.GetXZ(endWorldPosition, out int endX, out int endZ);
       Debug.Log("values :" + startX + startZ + endX + endZ );
List<PathNode> path = FindPath(startX, startZ, endX , endZ);
  if(path == null){
      Debug.Log("This is the pathnode list if null"+ path);
      return null;
  }else{
       Debug.Log("This is the pathnode list if not null"+ path);
      List<Vector3> vectorPath = new List<Vector3>();
      foreach (PathNode pathnode in path)
      {
         vectorPath.Add(new Vector3(pathnode.x ,0, pathnode.z)* builder.GetCellSize() +Vector3.one *  builder.GetCellSize() * .5f); 
      }
      Debug.Log("This is the vectorpath list"+ vectorPath.Count);
      return vectorPath;
  }
   }

   public List<PathNode> FindPath(int startX, int startZ, int endX, int endZ){
      PathNode startNode=builder.getValue(startX,startZ);
      PathNode endNode=builder.getValue(endX,endZ);
      Debug.Log("startNode"+startNode);
       Debug.Log("endNode"+endNode);
         if (startNode == null || endNode == null) {
            return null;
        }
      openList= new List<PathNode>{ startNode };
       Debug.Log("openList"+openList.Count);
      closedList= new List<PathNode>(); 
 Debug.Log("closedList"+closedList.Count);
     for(int x=0; x< builder.GetWidth(); x++){
         for(int z=0; z < builder.GetHeight(); z++){
             PathNode pathNode = builder.getValue(x, z);
             pathNode.gCost= 999999;
             pathNode.CalculateFCost();
             pathNode.cameFromNode=null; 


         }
     } 
     startNode.gCost = 0;
      Debug.Log("startNode.gCost"+startNode.gCost);
     startNode.hCost = calculateDistance(startNode, endNode);
       Debug.Log("startNode.hCost"+startNode.hCost);
     startNode.CalculateFCost();

     while(openList.Count > 0){
        PathNode currentNode =GetLowestFCostNode(openList);
          Debug.Log("currentNode"+currentNode);
        if(currentNode == endNode){
           Debug.Log("calculatePath(endNode)"+calculatePath(endNode));
            return calculatePath(endNode);
        }
        openList.Remove(currentNode);
        closedList.Add(currentNode);
          Debug.Log("neighbour List"+GetNeighbourList(currentNode));
          Debug.Log("neighbour List"+GetNeighbourList(currentNode).Count);
foreach (PathNode neighbourNode in GetNeighbourList(currentNode)){
     Debug.Log("Inside the loop for iterating through neighbour list");
    if(closedList.Contains(neighbourNode)) continue;
  
  if(!neighbourNode.isWalkable){
      closedList.Add(neighbourNode);
      continue;
  }
  int tentativeGCost = currentNode.gCost + calculateDistance(currentNode, neighbourNode);
    Debug.Log(" current node gcost"+ currentNode.gCost);
  Debug.Log("calculateDistance current"+ calculateDistance(currentNode, neighbourNode));
   Debug.Log("tentativeGCost"+ tentativeGCost);

                if (tentativeGCost < neighbourNode.gCost) {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = calculateDistance(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode)) {
                        openList.Add(neighbourNode);
                        Debug.Log("openList again"+openList.Count);
                    }
                }

}

     }
     // out of nodes in the open list
   return null;
   }

private List<PathNode> GetNeighbourList(PathNode currentNode){
List<PathNode> neighbourList =new List<PathNode>();
if(currentNode.x -1 >= 0){
    //Left
neighbourList.Add(GetNode(currentNode.x - 1, currentNode.z));
//left down
if(currentNode.z -1 >= 0)neighbourList.Add(GetNode(currentNode.x - 1, currentNode.z -1));
//Left Up
if(currentNode.z +1 < builder.GetHeight())neighbourList.Add(GetNode(currentNode.x - 1, currentNode.z +1));
}
if(currentNode.x +1 < builder.GetWidth()){
    //Right
neighbourList.Add(GetNode(currentNode.x + 1, currentNode.z));
//Right down
if(currentNode.z -1 >= 0)neighbourList.Add(GetNode(currentNode.x + 1, currentNode.z -1));
//Right Up
if(currentNode.z +1 < builder.GetHeight())neighbourList.Add(GetNode(currentNode.x + 1, currentNode.z +1));
}
//down
if(currentNode.z - 1 >= 0) neighbourList.Add(GetNode(currentNode.x , currentNode.z -1));
//Up
if(currentNode.z +1 < builder.GetHeight())neighbourList.Add(GetNode(currentNode.x, currentNode.z +1));

return neighbourList;
}

 public PathNode GetNode(int x, int z) {
        return builder.getValue(x, z);
    }

   private List<PathNode> calculatePath(PathNode endNode){
    List<PathNode> path = new List<PathNode>();
    path.Add(endNode);
    PathNode currentNode= endNode;
while (currentNode.cameFromNode !=null){
path.Add(currentNode.cameFromNode);
currentNode= currentNode.cameFromNode;
}
path.Reverse();
return path;
   }
private int calculateDistance(PathNode a, PathNode b){
       int xDistance = Mathf.Abs(a.x - b.x);
       int zDistance = Mathf.Abs(a.z - b.z);
       int remaining = Mathf.Abs(xDistance - zDistance);
       return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, zDistance) + MOVE_STRAIGHT_COST * remaining;
   }

   private PathNode GetLowestFCostNode(List<PathNode> pathNodeList){
       PathNode lowestFCostNode = pathNodeList[0];
       for(int i=0 ; i < pathNodeList.Count ; i++){
           if(pathNodeList[i].fCost < lowestFCostNode.fCost){
               lowestFCostNode= pathNodeList[i];
           }
       }
return lowestFCostNode;
   }
}
