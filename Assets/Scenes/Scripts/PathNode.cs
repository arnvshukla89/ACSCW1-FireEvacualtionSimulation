using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GridBuilder<PathNode> builder;
    public int x;
    public int z;
    public int gCost;
    public int hCost;
    public int fCost;
    public bool isWalkable;
    public PathNode cameFromNode;

public PathNode (GridBuilder<PathNode> builder, int x, int z){
this.builder = builder;
this.x = x;
this.z = z;
isWalkable= true;
    }

public override string ToString()
    {
        return x+ "," + z;
    }
public void CalculateFCost(){
    fCost= gCost + hCost;
}

public void SetIsWalkable(bool isWalkable){
this.isWalkable = isWalkable;
builder.TriggeredGridObjectChange(x, z);

}
}
