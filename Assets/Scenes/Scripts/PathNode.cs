using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GridBuilder<PathNode> grid;
       public int x;
       public int z;
  public int gCost;
    public int hCost;
    public int fCost;
    public bool isWalkable;
    public PathNode cameFromNode;
       private Transform transform;
       public PathNode(GridBuilder<PathNode> grid, int x , int z){
this.grid = grid;
this.x =x;
this.z = z;
isWalkable=true;
   }
  public void CalculateFCost(){
    fCost= gCost + hCost;
}

public void SetIsWalkable(bool isWalkable){
this.isWalkable = isWalkable;
grid.TriggeredGridObjectChange(x, z);

} 
   public void SetTransform(Transform transform){
       this.transform = transform;
       grid.TriggeredGridObjectChange(x,z);
   }

   public void ClearTransform(){
       transform = null;
    grid.TriggeredGridObjectChange(x,z);
   }
   public bool CanBuild(){
       return transform == null;
   }
   public override string ToString(){
       return x + "," + z ;
   }
}
