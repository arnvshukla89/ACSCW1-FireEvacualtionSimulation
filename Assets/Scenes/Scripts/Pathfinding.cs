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
   public List<PathNode> FindPath(int startX, int startZ, int endX, int endZ){
      PathNode startNode=builder.getValue(startX,startZ);
      PathNode endNode=builder.getValue(endX, endZ);
      openList= new List<PathNode>{ startNode };
      closedList= new List<PathNode>(); 

     for(int x=0; x< builder.GetWidth(); x++){
         for(int z=0; z < builder.GetHeight(); z++){
             PathNode pathNode = builder.getValue(x, z);
             pathNode.gCost= 999999;
             pathNode.CalculateFCost();
             pathNode.cameFromNode=null; 


         }
     } 
     startNode.gCost = 0;
     startNode.hCost = calculateDistance(startNode, endNode);
     startNode.CalculateFCost();

     while(openList.Count > 0){
        PathNode currentNode =GetLowestFCostNode(openList);
        if(currentNode == endNode){
            return calculatePath(endNode);
        }
        openList.Remove(currentNode);
        closedList.Add(currentNode);
foreach (PathNode neighbourNode in GetNeighbourList(currentNode)){
    if(closedList.Contains(neighbourNode)) continue;
  if(!neighbourNode.isWalkable){
      closedList.Add(neighbourNode);
      continue;

  }
  int tentativeGCost = currentNode.gCost + calculateDistance(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost) {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = calculateDistance(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode)) {
                        openList.Add(neighbourNode);
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
