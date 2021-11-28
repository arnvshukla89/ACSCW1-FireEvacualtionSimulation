using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCharacters : MonoBehaviour {
//private GridBuilder grid;
//private Pathfinding pathfinding;
[SerializeField] private Transform Character;
[SerializeField] private Transform Character1;
[SerializeField] private Transform Character2;
List<Transform> CharTransform =new List<Transform>();
public static ChangeCharacters Instance { get; private set; }

private void Awake(){
Instance =this;
}
private void Update() {
var dropdown = transform.GetComponent<Dropdown>();
dropdown.options.Clear();
List<string> items =new List<string>();
items.Add("Old Person");
items.Add("Female");
items.Add("Male");

foreach(var item in items){
    dropdown.options.Add(new Dropdown.OptionData(){text = item});
}
DropdownItemSelected(dropdown);
//Debug.Log("Value of index in start method" + index);
dropdown.onValueChanged.AddListener(delegate{DropdownItemSelected(dropdown);});
}
public void DropdownItemSelected(Dropdown dropdown){
int index = dropdown.value;
if(index==0){
   //var canvas = transform.GetComponent<Canvas>();
   // if(Input.GetMouseButtonDown(0) && !canvas){
//GridSystem.Instance.buildaCharacter(Character,Mouse3D.GetMouseWorldPosition());  
    //}
     //Debug.Log("Inside Index =0" + "do nothing");
}
if(index==1){
   var canvas = transform.GetComponent<Canvas>();
    if(Input.GetMouseButtonDown(0) && !canvas){
Transform girl =GridSystem.Instance.buildaCharacter(Character1,Mouse3D.GetMouseWorldPosition());
    if(girl!=null){
    CharTransform.Add(girl);
    }
    Debug.Log("Number of Character transform List if index =1" + CharTransform.Count);
    }
      //Debug.Log("Inside Index =1");

}
if(index==2){
    var canvas = transform.GetComponent<Canvas>();
    if(Input.GetMouseButtonDown(0) && !canvas){
Transform dude=GridSystem.Instance.buildaCharacter(Character2,Mouse3D.GetMouseWorldPosition());
   if(dude!= null){
   CharTransform.Add(dude);
   }
    }
    Debug.Log("Number of Character transform List if index =2" + CharTransform.Count + CharTransform);
}
}
public List<Transform> characterL(){
    if(CharTransform !=null){
       // Debug.Log("CharTransform count" + CharTransform.Count);
return CharTransform;
    }
    return null;
}
}