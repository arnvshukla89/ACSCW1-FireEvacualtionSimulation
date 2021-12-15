/*-------------------------------------------

Class:ThreeExitMove
Functionality:Class to move the character according to A* pathfinding, detect collision, calculate move time , rotate characters
//---------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class ThreeExitMove : MonoBehaviour
{
 [SerializeField] private Transform Character1;
[SerializeField] private Transform Character2;

      private int currentPathIndex;
    private List<Vector3> pathVectorList;
    private List<Transform> CharacterList;
    private Rigidbody rb;
    private float rotationSpeed =720f;
     
     private static float time;
     public float timer{ get { return time; } }
     private static int Evacuationcounter = 0;
     private static bool timerbool=false;
     private int collisionmethodCounter =0;
     private int MovingonmethodCounter =0;
     private static Dictionary<string, int> FarthestCharacter =new Dictionary<string, int>();
public int EvacuationcounterGS{
    get {return Evacuationcounter;}
    set{ Evacuationcounter += value;}
}
    private TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime;
    private List<Vector3> CollidingObjects= new List<Vector3>();
      private static string max = null;
    private static bool collision =false;
    private static Vector3 collidingwiththisbody;
    private static Vector3 mainbody;
private bool simulationstarted = false;
public bool SimulationGetSet{
    get {return simulationstarted;}
    set{ simulationstarted = value;}
}
    private bool ButtonClicked;

    public event EventHandler<OnCollisionEvent> oncollision;
public static ThreeExitMove Instance { get; private set; }

 public class OnCollisionEvent : EventArgs {
        public Vector3 bodyP;
        public Vector3 colliderP;
    }

  private void Awake() {
      Instance =this;
        ButtonClicked = false;
        if(Input.GetKeyDown("space")){  
     SimulationGetSet = true;    
     Evacuationcounter=0;   
        }
    }

    private void start(){  
    }
    private void Update() {
        if(transform.name.Contains("player")){
     CharacterList = ChangeCharacters.Instance.characterL();

        }
     
    }

    private void FixedUpdate() {
         rb = GetComponent<Rigidbody>();
         if(GetName().Contains("player")){   
     StartCoroutine(HandleMovement(5f));
      // HandleMovement(5f);
   //StartCoroutine(handleMovementWithCollision(5f));
         }
        if(Input.GetKeyDown("space")){ 
            if(GetName().Contains("player")){
           SetTargetPosition(new Vector3(2.5f, 0, 32.5f),new Vector3(97.5f, 0, 2.5f),new Vector3(172.5f, 0, 72.5f));
            }
          // Timer.Create(() => SetTargetPosition(new Vector3(2.5f, 0, 32.5f),new Vector3(97.5f, 0, 2.5f),new Vector3(172.5f, 0, 72.5f)),1f);
        }
        
        
        
    }

/*-------------------------------------

   Functionality: detects collision and triggers the event 'oncollision' which is subscribed in the handle movement method
   Methods:OnCollisionEnter()
   Params:Collision gameobject
   --------------------------------------*/

  public void OnCollisionEnter(Collision other ) {
        if(gameObject.transform.name.Contains("player") && other.gameObject.transform.name.Contains("player")){
            collisionmethodCounter++;
            Debug.Log("Collisionmethod counter"+collisionmethodCounter);
           // if(gameObject.transform.position.x>other.gameObject.transform.position.x){
            Debug.Log("About the first body" +gameObject.transform.name+gameObject.transform.position);
              Debug.Log("About the second colliding body" +other.gameObject.transform.name+other.gameObject.transform.position);
              Debug.Log("Collision counter");
                 Debug.Log(gameObject.transform.name+"Collission happening" + other.gameObject.transform.name); 
string body = gameObject.transform.name; 
string collider = other.gameObject.transform.name;
Vector3 colliderP =other.gameObject.transform.position;
Vector3 bodyP =gameObject.transform.position;
oncollision?.Invoke(this,new OnCollisionEvent{ bodyP = bodyP, colliderP = colliderP});
/*if(colliderP ==rb.transform.position){
}
collidingwiththisbody = colliderP;
mainbody =bodyP;*/
collision = true;
        //}

    }
    }

    
     
     IEnumerator waitforsomeTime(float time ){
          yield return new WaitForSeconds(time);
     }


/*-------------------------------------

   Functionality: Move characters, rotate characters,move timer calculations
   Methods:HandleMovement()
   Params:float speed
   --------------------------------------*/
     IEnumerator HandleMovement(float speed) {
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            float distanceBefore = Vector3.Distance(rb.transform.position, targetPosition)*2;
            if (Vector3.Distance(transform.position, targetPosition) > 1f) {
                Vector3 moveDir = (targetPosition - rb.transform.position).normalized;
if(collision ==true){
    yield return new WaitForSeconds(0.1f);
    collision = false;
    if(rb.transform.name == max){
        time += 0.1f;
    }
}
rb.transform.position = rb.transform.position + moveDir * speed * Time.deltaTime; 


            
            if(rb.transform.name == max){
            time +=(Mathf.Abs((Vector3.Distance(rb.transform.position, targetPosition)*2)-distanceBefore)/speed);
            Debug.Log("this the time without round off:" + time);
            }
            if(moveDir!= Vector3.zero){
                Quaternion CharRotation = Quaternion.LookRotation(moveDir,Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,CharRotation,rotationSpeed * Time.deltaTime); 

            }
               
            
            } else {
                currentPathIndex++;
              
                if (currentPathIndex >= pathVectorList.Count) {
                    EvacuationcounterGS =1;
                    CharacterList.Remove(gameObject.transform);
                    Destroy(gameObject);
                      if(CharacterList.Count==0){
                        timerbool =false;
                    }
                    StopMoving();

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
        return rb.transform.position;
    }
      public String GetName() {
        return transform.name;
    }

/*-------------------------------------

   Functionality: Set target position for the characters, select the character which will be exiting last
   Methods:SetTargetPosition()
   Params:Vector3 Target Position1,Vector3 Target Position2, Vector3 Target Position3
   --------------------------------------*/
    public void SetTargetPosition(Vector3 targetPosition, Vector3 targetPosition2,Vector3 targetPosition3) {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.ShortestTarget(GetPosition(), targetPosition,targetPosition2,targetPosition3);


 if(GetName().Contains("player")){
     if(FarthestCharacter !=null && pathVectorList!=null&&!FarthestCharacter.ContainsKey(GetName())){
     FarthestCharacter.Add(GetName(),pathVectorList.Count);
     }
 }
 foreach (var item in FarthestCharacter)
 {
  
 }
 
  
 if(FarthestCharacter.Count == CharacterList.Count){
   max = FarthestCharacter.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
    }
  
        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);      
     
         }  
  

    }
  
}
