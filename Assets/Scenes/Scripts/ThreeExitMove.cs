using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeExitMove : MonoBehaviour
{
      private int currentPathIndex;
    private List<Vector3> pathVectorList;
    private List<Vector3> pathVectorList2;
    private List<Vector3> pathVectorList3;
    private GameObject person;
     private const float speed = 10f;

    private void Update() {
      /*  HandleMovement();
        if(Input.GetKeyDown("space")){ 
            SetTargetPosition(new Vector3(2.5f, 0, 32.5f),new Vector3(97.5f, 0, 2.5f),new Vector3(172.5f, 0, 72.5f));
        }*/
    }

    private void FixedUpdate() {
        HandleMovement();
        if(Input.GetKeyDown("space")){ 
            SetTargetPosition(new Vector3(2.5f, 0, 32.5f),new Vector3(97.5f, 0, 2.5f),new Vector3(172.5f, 0, 72.5f));
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name =="Blocky_Dude_Red_Mobile_2(Clone)"){
   Debug.Log(gameObject.name+"Collission happening" + other.gameObject.name);   
        } 
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name =="Blocky_Dude_Red_Mobile_2(Clone)"){
        Debug.Log(gameObject.name+"Trigger happening" + other.gameObject.name);  
        }
    }

  /*  private void HandleMovementRecursive(){
      if(pathVectorList!=null && pathVectorList2!=null && pathVectorList3!=null){
       int p1= pathVectorList.Count;
       int p2= pathVectorList2.Count;
       int p3= pathVectorList3.Count;

       Debug.Log("Values of p1,p2 and p3:" +p1+","+p2+","+p3+",");
       if(p1<p2 && p1<p3){
HandleMovement(pathVectorList);
       }
else if(p2<p1 && p2<p3){
HandleMovement(pathVectorList2);
       }
else if(p3<p1 && p3<p2){
HandleMovement(pathVectorList3);
}
else{
    Debug.Log("not conclusive");
}
      }   
    }*/
    
    private void HandleMovement() {
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            } else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                    Destroy(gameObject);
                }
            }
        } else {
            Debug.Log("Do nothing");
        }
    }

    private void StopMoving() {
        pathVectorList = null;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition, Vector3 targetPosition2,Vector3 targetPosition3) {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.ShortestTarget(GetPosition(), targetPosition,targetPosition2,targetPosition3);
        //pathVectorList2 = Pathfinding.Instance.FindPath(GetPosition(), targetPosition2);
       // pathVectorList3 = Pathfinding.Instance.FindPath(GetPosition(), targetPosition3);
        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);
     
         }
          /* 
         if (pathVectorList2 != null && pathVectorList2.Count > 1) {
            pathVectorList2.RemoveAt(0);
        }
         if (pathVectorList3 != null && pathVectorList3.Count > 1) {
            pathVectorList3.RemoveAt(0);
        }*/
    }
}
