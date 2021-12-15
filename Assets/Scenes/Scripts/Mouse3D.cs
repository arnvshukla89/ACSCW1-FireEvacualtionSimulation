/*-------------------------------------------

Class:Mouse3D
Functionality:return the world position for mouse in 3D
Source:Codemonkey.com
//---------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mouse3D : MonoBehaviour {

    public static Mouse3D Instance { get; private set; }

    [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask)) {
            transform.position = raycastHit.point;
        }
    }

    public static Vector3 GetMouseWorldPosition() => Instance.GetMouseWorldPosition_Instance();
  /*-------------------------------------

   Functionality: Uses raycast to detect collision on the ground and render the mouse click position.
   Methods:GetMouseWorldPosition_Instance
   Params:
   --------------------------------------*/
    private Vector3 GetMouseWorldPosition_Instance() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(ray);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask)) {
            return raycastHit.point;
        } else {
            return Vector3.zero;
        }
    }

}
