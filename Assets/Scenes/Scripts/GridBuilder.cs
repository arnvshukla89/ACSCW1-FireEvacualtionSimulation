     using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using static utils;
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

    
  public void setValue(int x, int z, TGridBuilderObject value){
      if(x >= 0 && z >= 0 && x < width && z < height)
      {
      // GridMatrix[x, y] = Mathf.Clamp(value, HEAT_MAP_MIN_VALUE, HEAT_MAP_MAX_VALUE);
      GridMatrix[x, z] = value;
      if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, z = z });
      }
}
  public void setValue(Vector3 worldPosition, TGridBuilderObject value){
      int x,y,z;
      GetXZ(worldPosition,out x,out y,out z);
      setValue(x,z,value);

  }
  public void TriggeredGridObjectChange(int x, int z){
  if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, z = z });

  }
  public void GetXZ(Vector3 worldPosition, out int x, out int z, out int y){
      x= Mathf.FloorToInt((worldPosition-originalPosition).x/cellSize);
      z= Mathf.FloorToInt((worldPosition-originalPosition).z/cellSize);
      y= Mathf.FloorToInt((worldPosition-originalPosition).y/cellSize);

  }
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
      int x,y,z;
      GetXZ(worldPosition,out y,out x, out z);
      return getValue(x,z);
      //addition
  }
}

