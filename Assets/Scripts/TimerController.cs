
/*-------------------------------------------

Class:TimerController
Functionality:Displaying movetime and response time
//---------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimerController : MonoBehaviour
{

public static TimerController Instance { get; private set; }
   [SerializeField] private TextMeshProUGUI SimulationTiming;
       private static float time;
       private float RTime=0f;

       private void Awake() {
           Instance =this;
       }
       private void Start() {
          SimulationTiming.text="Time: 00:00:000";
       }
       private void FixedUpdate() {
   
    if(GridSystem.Instance.Rtimerbool == true){
         RTime =GridSystem.Instance.Rtimer;
    DisplayTime(RTime);
    }else{
           if(ChangeCharacters.Instance.numberofPlayers>0 ){
            time = ThreeExitMove.Instance.timer + RTime; 
         DisplayTime(time);
       }
    }
      
      
}
    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float miniseconds = timeToDisplay % 1 * 1000;
        
  SimulationTiming.text = "Time:"+string.Format("{0:00}:{1:00}:{2:000}", minutes,  seconds, miniseconds);
    }
    
}
