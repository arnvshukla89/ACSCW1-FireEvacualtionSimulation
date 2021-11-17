using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChar
{
     private const float speed = 40f;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
  // private Pathfinding pathfinding;
 /*  public MoveChar(Pathfinding pathfinding){
this.pathfinding = pathfinding;
   } */
    
    public void HandleMovement( Vector3 TransformP) {
        if (pathVectorList != null) {
            Debug.Log("Pathvector list is not null");
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            Debug.Log("Target position" + targetPosition);
           /* if (Vector3.Distance(transform.position, targetPosition) > 1f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            } */  if (Vector3.Distance(TransformP, targetPosition) > 1f) {
                Vector3 moveDir = (targetPosition - TransformP).normalized;

                float distanceBefore = Vector3.Distance(TransformP, targetPosition);
                TransformP = TransformP + moveDir * speed * Time.deltaTime;
            }else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                }
            }
        } else {
          Debug.Log("Pathvector list is null");
        }
    }
 
    private void StopMoving() {
        pathVectorList = null;
    }
    public void SetTargetPosition(Vector3 targetPosition, Vector3 CharPosition, Pathfinding pathfinding) {
       //this.pathfinding =pathfinding;
        currentPathIndex = 0;
        Debug.Log("Target and current location:" + targetPosition + " "+ CharPosition);
      // pathVectorList = pathfinding.FindP(transform.position, targetPosition);
       pathVectorList = FindP(CharPosition, targetPosition, pathfinding);
 if (pathVectorList != null) {
Debug.Log("this is pathVectorList vector list:" + "hello");
  Debug.Log("Pathvector list is not null");
            Vector3 target = pathVectorList[currentPathIndex];
            Debug.Log("Target position" + target);
            if (Vector3.Distance(CharPosition, target) > 1f) {
                Vector3 moveDir = (target - CharPosition).normalized;

                float distanceBefore = Vector3.Distance(CharPosition, target);
                CharPosition = CharPosition + moveDir * speed * Time.deltaTime;
            }else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                }
            }
 } else {
     Debug.Log("this is pathVectorList vector list:" + "hello its null");
 }
        if (pathVectorList != null && pathVectorList.Count > 1) {
            Debug.Log("this is pathVectorList vector list:" + "hello not null");
            pathVectorList.RemoveAt(0);
        }
    }

       public List<Vector3> FindP(Vector3 startWorldPosition, Vector3 endWorldPosition, Pathfinding pathfind){
       pathfind.GetGridBuilder().GetXZ(startWorldPosition , out int startX, out int startZ);
   pathfind.GetGridBuilder().GetXZ(endWorldPosition, out int endX, out int endZ);
       Debug.Log("values :" + startX + startZ + endX + endZ );
List<PathNode> path = pathfind.FindPath(startX, startZ, endX , endZ);
  if(path == null){
      Debug.Log("This is the pathnode list if null"+ path);
      return null;
  }else{
       Debug.Log("This is the pathnode list if not null"+ path);
      List<Vector3> vectorPath = new List<Vector3>();
      foreach (PathNode pathnode in path)
      {
         vectorPath.Add(new Vector3(pathnode.x ,0, pathnode.z)* pathfind.GetGridBuilder().GetCellSize() +Vector3.one *  pathfind.GetGridBuilder().GetCellSize() * .5f); 
      }
      Debug.Log("This is the vectorpath list"+ vectorPath);
      return vectorPath;
  }
   }
}
