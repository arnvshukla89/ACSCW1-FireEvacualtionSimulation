using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloor : MonoBehaviour
{
    public GridBuilder<PathNode> builder;
[SerializeField] private Transform floor;
public CreateFloor( GridBuilder<PathNode> builder){
   this.builder = builder;
}
   public void buildGround(Transform prefab, Vector3 Position,float height,float width){
Vector3 vec1 = Position * 5f + new Vector3(1,0,1) * 2.5f;
   builder.GetXZ(vec1, out int xcord, out int zcord);
  Instantiate(prefab,builder.GetWorldPosition(xcord,zcord), Quaternion.identity);
   prefab.transform.localScale = new Vector3(width*builder.GetCellSize(),(float)0.1 , height*builder.GetCellSize());

}
}
