 /*-------------------------------------------

Class:GridBuilder<T>
Functionality:Base class for building grid object
//---------------------------------------------------*/    
     
     using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class GridBuilder<TGridBuilderObject>
{
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs {
        public int x;
        public int z;
    }
  private int width;
  private int height;
private float cellSize;
private Vector3 originalPosition;

 /*-------------------------------------

   Functionality: Instantiate grid with grid nodes
   Methods:GridBuilder()
   Params:Width, height, cellsize, Gridnode objects with grid position rendered as text meshes
   --------------------------------------*/

  private TGridBuilderObject[,] GridMatrix;
  public GridBuilder(int width, int height, float cellSize,Vector3 originalPosition, Func<GridBuilder<TGridBuilderObject>,int,int,TGridBuilderObject> createGridObject){
      this.width=width;
      this.height=height;
      this.cellSize=cellSize;
      this.originalPosition=originalPosition;
      GridMatrix= new TGridBuilderObject[width, height];
     for(int x=0; x<GridMatrix.GetLength(0);x++){
         for(int z=0; z<GridMatrix.GetLength(1);z++){
             GridMatrix[x,z]= createGridObject(this,x,z);
         }
     }
     
      bool showDebug=true;
      if(showDebug){
      TextMesh[,] debugArray= new TextMesh[width, height];
     for (int x = 0; x<GridMatrix.GetLength(0); x++){
         for(int z = 0; z<GridMatrix.GetLength(1); z++){
        debugArray[x,z] = utils.CreateWorldText(GridMatrix[x, z]?.ToString(),null,GetWorldPosition(x,z)+ new Vector3(cellSize, 0 ,cellSize) * .5f,15,Color.white, TextAnchor.MiddleCenter,TextAlignment.Center);
            Debug.DrawLine(GetWorldPosition(x, z),GetWorldPosition(x,z+1),Color.white,200f);
             Debug.DrawLine(GetWorldPosition(x, z),GetWorldPosition(x+1,z),Color.white,200f);

         }
     }
      Debug.DrawLine(GetWorldPosition(0,height),GetWorldPosition(width,height),Color.white,200f);
      Debug.DrawLine(GetWorldPosition(width,0),GetWorldPosition(width,height),Color.white,200f);
   OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) => {
                debugArray[eventArgs.x, eventArgs.z].text = GridMatrix[eventArgs.x, eventArgs.z]?.ToString();
   };
      }
  }
  public int GetWidth() {
        return width;
    }

    public int GetHeight() {
        return height;
    }

    public float GetCellSize() {
        return cellSize;
    }

     /*-------------------------------------

   Functionality: set the value of tesmesh as grid coordinates
   Methods:setValue()
   Params:Width, x coordinate, y coordinate,value of the grid object
   params2: Vector3 postion,value of the grid object
   --------------------------------------*/
  public void setValue(int x, int z, TGridBuilderObject value){
      if(x >= 0 && z >= 0 && x < width && z < height)
      {
      GridMatrix[x, z] = value;
      if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, z = z });
      }
}
  public void setValue(Vector3 worldPosition, TGridBuilderObject value){
      int x,z;
      GetXZ(worldPosition,out x,out z);
      setValue(x,z,value);

  }

  public void TriggeredGridObjectChange(int x, int z){
  if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, z = z });

  }

   /*-------------------------------------

   Functionality: get X and Z coordinates of the grid
   Methods:GetXZ()
   params: Vector3 postion,x* coordinate, z* coordinate
   --------------------------------------*/
  public void GetXZ(Vector3 worldPosition, out int x, out int z){
      x= Mathf.FloorToInt((worldPosition-originalPosition).x/cellSize);
      z= Mathf.FloorToInt((worldPosition-originalPosition).z/cellSize);

  }
   /*-------------------------------------

   Functionality: get grid object 
   Methods:getValue()
   params: x grid coordinate, z grid coordinate
   params2: Vector3 world position
   return: gridnode object
   --------------------------------------*/
  public TGridBuilderObject getValue(int x,int z)   {
      if(x >= 0 && z >= 0 && x < width && z < height){
        return GridMatrix[x, z];  
      }
      else{
          return default(TGridBuilderObject);
      }
  }
      public Vector3 GetWorldPosition(int x, int z){
            return new Vector3(x, 0, z)* cellSize + originalPosition;
        }
  public TGridBuilderObject getValue(Vector3 worldPosition){
      int x,z;
      GetXZ(worldPosition,out x, out z);
      return getValue(x,z);
  }
}


