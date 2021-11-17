using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{



   /* private V_UnitSkeleton unitSkeleton;
    private V_UnitAnimation unitAnimation;
    private AnimatedWalker animatedWalker;*/
   private const float speed = 40f;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;

  public static CharacterMovement Instance { get; private set; }

  private void Awake() {
       Instance = this;
  }
   // private void Start() {
        //Transform bodyTransform = transform.Find("Character(Copy)");
       // unitSkeleton = new V_UnitSkeleton(1f, bodyTransform.TransformPoint, (Mesh mesh) => bodyTransform.GetComponent<MeshFilter>().mesh = mesh);
        //unitAnimation = new V_UnitAnimation(unitSkeleton);
        //animatedWalker = new AnimatedWalker(unitAnimation, UnitAnimType.GetUnitAnimType("dMarine_Idle"), UnitAnimType.GetUnitAnimType("dMarine_Walk"), 1f, 1f);
   // }

    private void Update() {
        HandleMovement();
        //unitSkeleton.Update(Time.deltaTime);

       if (Input.GetMouseButtonDown(1)) {
            SetTargetPosition(new Vector3(47.5f, 0, 22.5f));
        }
    }
    
    public void HandleMovement() {
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            Debug.Log("Target position" + targetPosition);
            if (Vector3.Distance(transform.position, targetPosition) > 1f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                //animatedWalker.SetMoveVector(moveDir);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            } else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                   // animatedWalker.SetMoveVector(Vector3.zero);
                }
            }
        } else {
           // animatedWalker.SetMoveVector(Vector3.zero);
          Debug.Log("Pathvector list is null");
        }
    }

    private void StopMoving() {
        pathVectorList = null;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition) {
        currentPathIndex = 0;
        Debug.Log("Target and current location:" + targetPosition + " "+ GetPosition() );
        pathVectorList = null;//Pathfinding.Instance.FindPath(GetPosition(), targetPosition);
//Debug.Log("this is pathVectorList vector list:" + pathVectorList.Count);
        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);
        }
    }

    
}
