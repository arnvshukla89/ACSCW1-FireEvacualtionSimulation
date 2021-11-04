using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridSystem : MonoBehaviour
{

   private GridBuilder<PathNode> grid;
    private Pathfinding pathfinding;
     public PathfindingVisual pathfindingVisual;
  public utils Utils;
   
 public static GridSystem Instance { get; private set;}

[SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();
[SerializeField] private Camera mainCamera;
   private void Awake() {
       Instance = this;
       int gridWidth = 40;
       int gridHeight = 20;
       float cellSize =5f;

       grid = new GridBuilder<PathNode>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridBuilder<PathNode> g, int x , int z) => new PathNode(g , x , z));
pathfinding= new Pathfinding(grid);
   }
 
private void Update() {
   Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
          if (Physics.Raycast(ray, out RaycastHit raycastHit, 2000f, mouseColliderLayerMask)){
             transform.position = raycastHit.point;
          }
        //  if(Input.GetMouseButtonDown(0)){
         Vector3 mouseWorldPosition = GetMousePosition3D();
         Debug.Log("Mouse world position"+mouseWorldPosition);
pathfinding.GetGridBuilder().GetXZ(mouseWorldPosition, out int x,out int y,out int z);
   List<PathNode> path = pathfinding.FindPath(5,0, 10,15);
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(path[i].x, 0, path[i].z) * 5f + new Vector3(1,0,1) * 2.5f, new Vector3(path[i+1].x,0,path[i+1].z) * 5f + new Vector3(1,0,1) * 2.5f, Color.green, 5f);
                }
            }
    //}
   /*  if(Input.GetMouseButtonDown(1)){
         Vector3 mouseWorldPosition = utils.GetMouseWorldPosition();
          Debug.Log("Mouse world position"+mouseWorldPosition);
         pathfinding.GetGridBuilder().GetXZ(mouseWorldPosition , out int x,out int y, out int z); 
         pathfinding.GetNode(x, z).SetIsWalkable(!pathfinding.GetNode(x, z).isWalkable);
     }*/
   }
public static Vector3 GetMousePosition3D() => Instance.GetMouseWorldPosition3D();
private Vector3 GetMouseWorldPosition3D() {
          Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
          if (Physics.Raycast(ray, out RaycastHit raycastHit, 2000f, mouseColliderLayerMask)){
              return raycastHit.point;
          } else{
              return Vector3.zero;
          }
        }
   public class GridObject{

       private GridBuilder<GridObject> grid;
       private int x;
       private int z;
       public GridObject(GridBuilder<GridObject> grid, int x , int z){
this.grid = grid;
this.x =x;
this.z = z;
   }
   public override string ToString(){
       return x + "," + z;
   }
}
}
