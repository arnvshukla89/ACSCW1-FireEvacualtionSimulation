using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer 
{
    private static List<Timer> activeTimerList;
    private static GameObject initGameObject;

    private static void Inititliseif(){
        if(initGameObject ==null){
            initGameObject = new GameObject("Timer_initGameObject");
            activeTimerList = new List<Timer>();
             
        }
    } 

    private static void RemoveT(Timer timer){
        Inititliseif();
        activeTimerList.Remove(timer);
    }
    private static void StopTimer(string timerName){
for (int i=0; i<activeTimerList.Count; i++){
    if(activeTimerList[i].timerName == timerName){
     activeTimerList[i].Destroyit();
     i--;   

    }
}
    }
    public static Timer Create(Action action, float timer, string timerName=null){
        Inititliseif();
        GameObject gameObject = new GameObject("FunctionTimer", typeof(Mono));
        Timer T =new Timer(action, timer,timerName,gameObject);
       // GameObject gameObject = new GameObject("Timer",typeof(Mono));
        gameObject.GetComponent<Mono>().onUpdate = T.Update;
        activeTimerList.Add(T);
        return T;
    }
    public class Mono : MonoBehaviour {
        public Action onUpdate;
        private void Update() {
            if(onUpdate != null) onUpdate();
        }  
    }
    

    private bool isDestroyed;
    private float timr;
    private string timerName;
    private Action action; 
    private GameObject gameObject;
    public Timer(Action action, float timr,string timerName,GameObject gameObject){
this.action =action;
this.timr = timr;
this.gameObject =gameObject;
this.timerName =timerName;
isDestroyed = false;
    }
   private void Update(){
       if(!isDestroyed){
timr -= Time.deltaTime;
if (timr<0){
    action();
    Destroyit();
}
       }
   } 
private void Destroyit(){
    isDestroyed = true;
    UnityEngine.Object.Destroy(gameObject);
    RemoveT(this);
}   
}
