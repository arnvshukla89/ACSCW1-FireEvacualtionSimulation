/*-------------------------------------------

Class:NumberofPlayers
Functionality:Display the number of occupants and number of people evacuated on screen
//---------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NumberofPlayers : MonoBehaviour
{
    public ChangeCharacters changechar;
    public ThreeExitMove move;
    public TextMeshProUGUI HeadCount;
    public TextMeshProUGUI EvacuatedCount;


private void awake(){
   HeadCount.text ="Total Occupants:"; 
}
    private void Update() {
         HeadCount.text = "Total Occupants:" + ChangeCharacters.Instance.numberofPlayers.ToString();
         EvacuatedCount.text = "Total Evacuated:" + ThreeExitMove.Instance.EvacuationcounterGS.ToString();
    }

}
