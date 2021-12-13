using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeCharacters : MonoBehaviour {
//private GridBuilder grid;
//private Pathfinding pathfinding;
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
items.Add("Male");
items.Add("Female");
foreach(var item in items){
    dropdown.options.Add(new Dropdown.OptionData(){text = item});

}
DropdownItemSelected(dropdown);
dropdown.onValueChanged.AddListener(delegate{DropdownItemSelected(dropdown);});
}
public void DropdownItemSelected(Dropdown dropdown){
int index = dropdown.value;
if(index==1){
   var canvas = transform.GetComponent<Canvas>();
    if(Input.GetMouseButtonDown(0) && !canvas && !transform.name.Contains("player")){  
Transform girl =GridSystem.Instance.buildaCharacter(Character1,Mouse3D.GetMouseWorldPosition());
    if(girl!=null){
    CharTransform.Add(girl);
    }
    }

}
if(index==0){
    var canvas = transform.GetComponent<Canvas>();
    if(Input.GetMouseButtonDown(0) && !canvas && !transform.name.Contains("player") ){
Transform dude=GridSystem.Instance.buildaCharacter(Character2,Mouse3D.GetMouseWorldPosition());
   if(dude!= null){
   CharTransform.Add(dude);
   }
    }
} 
}
public int numberofPlayers{
    get {return CharTransform.Count;}
}
public List<Transform> characterL(){
    if(CharTransform !=null){
return CharTransform;
    }
    return null;
}
}