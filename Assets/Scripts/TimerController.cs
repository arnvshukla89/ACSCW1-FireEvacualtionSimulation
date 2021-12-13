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
       private float time;

       private void Awake() {
           Instance =this;
       }
       private void Start() {
          SimulationTiming.text="Time: 00:00:000";
       }
       private void FixedUpdate() {

           if(ChangeCharacters.Instance.numberofPlayers>0){
            time = ThreeExitMove.Instance.timer;
         DisplayTime(time);
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
