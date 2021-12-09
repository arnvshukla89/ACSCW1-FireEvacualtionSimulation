using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ThreeExitMove : MonoBehaviour
{
 [SerializeField] private Transform Character1;
[SerializeField] private Transform Character2;
      private int currentPathIndex;
    private List<Vector3> pathVectorList;
    private List<Transform> CharacterList;

    private float rotationSpeed =720f;
     public TextMeshProUGUI timeCounter;
     private static float time =0f;
     private static int Evacuationcounter = 0;
private static int collisioncounter =0;
public int EvacuationcounterGS{
    get {return Evacuationcounter;}
    set{ Evacuationcounter += value;}
}
    private TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime;
public Button play;
    private List<Vector3> CollidingObjects= new List<Vector3>();
    private bool collision;
private bool simulationstarted = false;
public bool SimulationGetSet{
    get {return simulationstarted;}
    set{ simulationstarted = value;}
}
    private bool ButtonClicked;
public static ThreeExitMove Instance { get; private set; }

  private void Awake() {
      Instance =this;
        collision= false;
        ButtonClicked = false;
        if(Input.GetKeyDown("space")){  
     SimulationGetSet = true;    
     Evacuationcounter=0;   
        }
    }

    private void start(){  
    }
    private void Update() {
     CharacterList = ChangeCharacters.Instance.characterL();
     Debug.Log("Character list count is here" +CharacterList.Count);
     
    }

    private void FixedUpdate() {
        if(CharacterList!=null){
        if(gameObject.name == "Blocky_Dude_Red_Mobile_2(Clone)"){
        //Timer.Create(() =>HandleMovement(25f),1f);
        //HandleMovement(25f);
        StartCoroutine(HandleMovement(15f));
        }
          else if (gameObject.name =="Blocky_Girl_Green_Mobile(Clone)"){
        //Timer.Create(() =>HandleMovement(15f),1f);
       // HandleMovement(15f);
       StartCoroutine(HandleMovement(10f));
        }
        else{
            Debug.Log("Game object name...its not able to find"  + gameObject.name);
        }
        if(Input.GetKeyDown("space")){     
           SetTargetPosition(new Vector3(2.5f, 0, 32.5f),new Vector3(97.5f, 0, 2.5f),new Vector3(172.5f, 0, 72.5f));
          // Timer.Create(() => SetTargetPosition(new Vector3(2.5f, 0, 32.5f),new Vector3(97.5f, 0, 2.5f),new Vector3(172.5f, 0, 72.5f)),1f);
        }
   // SimulationGetSet = false;
     // play.onClick.AddListener(SetTargetPosition(new Vector3(2.5f, 0, 32.5f),new Vector3(97.5f, 0, 2.5f),new Vector3(172.5f, 0, 72.5f)));   
        }
    }


    private void OnCollisionEnter(Collision other) {
        if((gameObject.name =="Blocky_Dude_Red_Mobile_2(Clone)" && other.gameObject.name =="Blocky_Dude_Red_Mobile_2(Clone)")||(gameObject.name =="Blocky_Dude_Red_Mobile_2(Clone)" && other.gameObject.name =="Blocky_Girl_Green_Mobile(Clone)")||(gameObject.name =="Blocky_Girl_Green_Mobile(Clone)" && other.gameObject.name =="Blocky_Girl_Green_Mobile(Clone)")||(gameObject.name =="Blocky_Girl_Green_Mobile(Clone)" && other.gameObject.name =="Blocky_Dude_Red_Mobile_2(Clone)")){
   Debug.Log(gameObject.name+"Collission happening" + other.gameObject.name);  
Vector3 collider2 = other.gameObject.transform.position;
Vector3 collider1 = gameObject.transform.position;
Debug.Log("Collider positions 2" + collider2);
Debug.Log("Transform position in collision" + transform.position);
if(collider2 == other.gameObject.transform.position){
collision =true;
}else{
    collision=false;
}
 }else{
     collision=false;
        }
    }
  
      IEnumerator WaitOneSecond()
      {
          yield return new WaitForSeconds(3f);
  Debug.Log("Delaying for 3 seconds");
         //character.SetActive (false);
      }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name =="Blocky_Dude_Red_Mobile_2(Clone)"){
        Debug.Log(gameObject.name+"Trigger happening" + other.gameObject.name);  
        }
    }

    
     IEnumerator HandleMovement(float speed) {
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            //Debug.Log("Current path Index in HandleMovement method before increment" + currentPathIndex);
            float distanceBefore = Vector3.Distance(transform.position, targetPosition)*2;
            if (Vector3.Distance(transform.position, targetPosition) > 1f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
              /*  if(CollidingObjects != null){
                    Debug.Log("Count of colliding objects inside Handle method"+CollidingObjects.Count);
                    foreach (Vector3 c in CollidingObjects)
                    {
                        Debug.Log("Colliding objects in handleMovement" + c);
                        Debug.Log("Transform position objects in handleMovement" + transform.position);
                         if(collision== true){
                             if(c==transform.position){
                  yield return new WaitForSeconds(1f);
                             }
                }
                    }
                    collision = false;
                }*/
                if(collision== true){
                   yield return new WaitForSeconds(0.2f);
                   collisioncounter++;
                   Debug.Log("Collision counter"+ collisioncounter);
                  collision = false; 
                }
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
                 time = Mathf.Abs(((Vector3.Distance(transform.position, targetPosition) *2)/speed)-distanceBefore/speed);
                 double timerMain = Math.Round(Convert.ToDouble(time),5);
             Debug.Log("this the eucledean distance:" + Vector3.Distance(transform.position, targetPosition));
            Debug.Log("this the time without round off:" + time);
            Debug.Log("this the time:" + timerMain);
            if(moveDir!= Vector3.zero){
                //transform.forward =moveDir;rotationSpeed
                Quaternion CharRotation = Quaternion.LookRotation(moveDir,Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,CharRotation,rotationSpeed * Time.deltaTime); 

            }
            
            } else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    EvacuationcounterGS =1;
                    Debug.Log("Evacuation Counting" +EvacuationcounterGS);
                    CharacterList.Remove(gameObject.transform);
                    Destroy(gameObject);
                    StopMoving();
                   // Debug.Log("Character list after removing the initial location of the character" + CharacterList.Count);

                }
            }
        } else {
            Debug.Log("Do nothing");
        }
    }


    private void StopMoving() {
        pathVectorList = null;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition, Vector3 targetPosition2,Vector3 targetPosition3) {
        currentPathIndex = 0;
        Debug.Log("Current path Index in SetTargetPosition method" + currentPathIndex);
        Debug.Log("Current position in SetTargetPosition method" + GetPosition());
        pathVectorList = Pathfinding.Instance.ShortestTarget(GetPosition(), targetPosition,targetPosition2,targetPosition3);
//        Debug.Log("pathVectorList in SetTargetPosition method" + pathVectorList.Count);
        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);
     
         }
         
    }
   public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }
}
