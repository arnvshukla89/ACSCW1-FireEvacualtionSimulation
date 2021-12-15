/*-------------------------------------------

Class:Pathfinding
Functionality:A* Path finding implementation, Exit selection based on heuristic value
//---------------------------------------------------*/

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
public Pathfinding(GridBuilder<PathNode> builder){
       Instance = this;
      this.builder = builder;
   }
   public GridBuilder<PathNode> GetGridBuilder(){
       return builder;
   } 

    /*-------------------------------------

   Functionality: Selection of shortest exit from three exits based on heuristic value
   Methods:ShortestTarget()
   Params:Start Position, Target1 Position,Target2 Position,Target3 Position
   --------------------------------------*/
     
     public List<Vector3> ShortestTarget (Vector3 StartPosition, Vector3 T1, Vector3 T2, Vector3 T3){
    builder.GetXZ(StartPosition , out int startX, out int startZ);
    builder.GetXZ(T1, out int endX1, out int endZ1);
    builder.GetXZ(T2, out int endX2, out int endZ2);
    builder.GetXZ(T3, out int endX3, out int endZ3); 
     PathNode startNode =builder.getValue(startX,startZ);
     PathNode Target1 =builder.getValue(endX1,endZ1); 
     PathNode Target2 =builder.getValue(endX2,endZ2); 
     PathNode Target3 =builder.getValue(endX3,endZ3);
int d1= calculateDistance(startNode,Target1);
int d2= calculateDistance(startNode,Target2);
int d3= calculateDistance(startNode,Target3);
     if(d1<d2 && d1<d3){
         List<Vector3> p1 =FindPath(StartPosition,T1);
        if(p1!=null){
            return p1;
            }
        else if(p1 == null){
            if(d2<d3){
            List<Vector3> p2 =FindPath(StartPosition,T2);    
            if(p2!=null){
            return p2;
            }else{
             return(FindPath(StartPosition,T3));   
            }
            }else if(d3<d2){
            List<Vector3> p3=FindPath(StartPosition,T3);
            if(p3!=null){
           return p3;
            }else{
                return(FindPath(StartPosition,T2));
            }
            }
        } else{
            return null;
        }
        return null;
     }else if (d2<d1 && d2<d3){
             List<Vector3> p2 =FindPath(StartPosition,T2);
        if(p2!=null){
            return p2;
            }
        else if(p2 == null){
            if(d1<d3){
            List<Vector3> p1 =FindPath(StartPosition,T1);    
            if(p1!=null){
            return p1;
            }else{
             return(FindPath(StartPosition,T3));   
            }
            }else if(d3<d1){
            List<Vector3> p3=FindPath(StartPosition,T3);
            if(p3!=null){
           return p3;
            }else{
                return(FindPath(StartPosition,T1));
            }
            }
        } else{
            return null;
        }
        return null;
     }else if (d3<d1 && d3<d2){
      List<Vector3> p3 =FindPath(StartPosition,T3);
        if(p3!=null){
            return p3;
            }
        else if(p3 == null){
            if(d1<d2){
            List<Vector3> p1 =FindPath(StartPosition,T1);    
            if(p1!=null){
            return p1;
            }else{
             return(FindPath(StartPosition,T2));   
            }
            }else if(d2<d1){
            List<Vector3> p2=FindPath(StartPosition,T2);
            if(p2!=null){
           return p2;
            }else{
                return(FindPath(StartPosition,T1));
            }
            }
        } else{
            return null;
        }
     
     }else{
         return null;
     }
     return null;        
     }
      /*-------------------------------------

   Functionality: Calls the Findpath() method and returns a list of vector3 
   Methods:FindPath()
   Params:Start Position, Target1 Position
   Return: Path List<Vector3>
   --------------------------------------*/
      public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition){
       builder.GetXZ(startWorldPosition , out int startX, out int startZ);
   builder.GetXZ(endWorldPosition, out int endX, out int endZ);
List<PathNode> path = FindPath(startX, startZ, endX , endZ);
  if(path == null){
      return null;
  }else{
      List<Vector3> vectorPath = new List<Vector3>();
      foreach (PathNode pathnode in path)
      {
         vectorPath.Add(new Vector3(pathnode.x ,0, pathnode.z)* builder.GetCellSize() +Vector3.one *  builder.GetCellSize() * .5f); 
      }
      return vectorPath;
  }
   }

     /*-------------------------------------

   Functionality: Calls the Findpath() method and returns a list of vector3 
   Methods:FindPath()
   Params:X value of Start Pathnode ,Z value of Start Pathnode ,X value of Target Pathnode, Z value of Target Pathnode 
   Return: Path List<Pathnodes>
   Generic: Pathnode object
   Source: CodeMonkey.com
   --------------------------------------*/

   public List<PathNode> FindPath(int startX, int startZ, int endX, int endZ){
      PathNode startNode=builder.getValue(startX,startZ);
      PathNode endNode=builder.getValue(endX,endZ);
         if (startNode == null || endNode == null) {
            return null;
        }
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

  /*-------------------------------------

   Functionality: Search for the immediate neighbours of the current Node
   Methods:GetNeighbourList
   Params:Current Node value
   Return: Neighbour nodes List<Pathnodes>
   Generic: Pathnode object
   Source: CodeMonkey.com
   --------------------------------------*/
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
   /*-------------------------------------

   Functionality: Calculate Heuristic value
   Methods:calculateDistance()
   Params:Current and Target Node value
   Return: Heuristic Distance in int
   Source: CodeMonkey.com
   --------------------------------------*/
private int calculateDistance(PathNode a, PathNode b){
       int xDistance = Mathf.Abs(a.x - b.x);
       int zDistance = Mathf.Abs(a.z - b.z);
       int remaining = Mathf.Abs(xDistance - zDistance);
       return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, zDistance) + MOVE_STRAIGHT_COST * remaining;
   }

     /*-------------------------------------

   Functionality: Calculate Heuristic value
   Methods:GetLowestFCostNode()
   Params:List of Grid nodes
   Return: Pathnode with lowest value
   Source: CodeMonkey.com
   --------------------------------------*/ 

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
