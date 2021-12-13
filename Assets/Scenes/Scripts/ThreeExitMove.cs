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
     //Debug.Log("Character list count is here" +CharacterList.Count);
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

 /*   IEnumerator handleMovementWithCollision(float speed){
if(collision == true){
    Debug.Log("This is collider and main body position:"+collidingwiththisbody+mainbody+"and this is the current moving body position"+GetPosition());
          if(collidingwiththisbody == GetPosition()|| mainbody == GetPosition() ){
//Timer.Create(() => HandleMovement(speed),10f);
yield return new WaitForSeconds(2f);
Debug.Log("Waiting time");
HandleMovement(speed);
collision =false;
collidingwiththisbody=Vector3.zero;
mainbody = Vector3.zero;
          } else{
HandleMovement(speed);
collision =false;
collidingwiththisbody=Vector3.zero;
mainbody = Vector3.zero;
          }     



}else{
HandleMovement(speed); 
collision=false;
collidingwiththisbody=Vector3.zero;
mainbody = Vector3.zero;
}

    } */


  public void OnCollisionEnter(Collision other ) {
        if(gameObject.transform.name.Contains("player") && other.gameObject.transform.name.Contains("player")){
            collisionmethodCounter++;
            Debug.Log("Collisionmethod counter"+collisionmethodCounter);
            if(gameObject.transform.position.x>other.gameObject.transform.position.x){
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
        }

    }
    }
      private void OnCollisionStay(Collision other) {
        if(gameObject.transform.name.Contains("player") && other.gameObject.transform.name.Contains("player")){
   //Debug.Log(gameObject.transform.name+"Collission happening" + other.gameObject.transform.name);  
   string x = gameObject.transform.name;
   string y = other.gameObject.transform.name;
    }
    }

    
     
     IEnumerator waitforsomeTime(float time ){
          yield return new WaitForSeconds(time);
     }

public void moveCharacters(Vector3 TargetPosition,Vector3 position, float speed){
Vector3 Direction = (TargetPosition-position).normalized;
position = position +Direction *speed*Time.deltaTime;
}

     IEnumerator HandleMovement(float speed) {
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            //Debug.Log("Current path Index in HandleMovement method before increment" + currentPathIndex);
            float distanceBefore = Vector3.Distance(rb.transform.position, targetPosition)*2;
            if (Vector3.Distance(transform.position, targetPosition) > 1f) {
                Vector3 moveDir = (targetPosition - rb.transform.position).normalized;
               // rb.transform.position = rb.transform.position + moveDir * speed * Time.deltaTime;
            // Debug.Log("this eucledean distance travelled:" + Mathf.Abs((Vector3.Distance(transform.position, targetPosition)*2)-distanceBefore));
             //-------------------------------------------------------///

oncollision = (object sender,OnCollisionEvent eventArgs) =>{
    Debug.Log("This is collider and main body position:"+eventArgs.colliderP+eventArgs.bodyP+"and this is the current moving body position"+rb.transform.position);
if(eventArgs.bodyP == rb.transform.position){

    Debug.Log("Inside the bodyP and current position"+eventArgs.bodyP+rb.transform.position);
    Debug.Log("Waiting");
    MovingonmethodCounter++;
    Debug.Log("Movingmethod counter"+MovingonmethodCounter);
    collision = true;
//Timer.Create(() => moveCharacters(rb.transform.position,moveDir,speed),1f);
}

};
if(collision ==true){
    yield return new WaitForSeconds(0.2f);
    collision = false;
}
rb.transform.position = rb.transform.position + moveDir * speed * Time.deltaTime; 


             ///----------------------------------------------------------------//
            
            if(rb.transform.name == max){
            time +=(Mathf.Abs((Vector3.Distance(rb.transform.position, targetPosition)*2)-distanceBefore)/speed);
            Debug.Log("this the time without round off:" + time);
            }
            //Debug.Log("this the time:" + timerMain);
            if(moveDir!= Vector3.zero){
                Quaternion CharRotation = Quaternion.LookRotation(moveDir,Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,CharRotation,rotationSpeed * Time.deltaTime); 

            }
               
            
            } else {
                currentPathIndex++;
              
                if (currentPathIndex >= pathVectorList.Count) {
                    EvacuationcounterGS =1;
                    CharacterList.Remove(gameObject.transform);
                    if(gameObject.transform.name==max){
                    time=0f;
                    }
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


    public void SetTargetPosition(Vector3 targetPosition, Vector3 targetPosition2,Vector3 targetPosition3) {
      // Debug.Log("set target positionfunction is called");
        currentPathIndex = 0;
       //FarthestCharacter = new Dictionary<string, int>();
        //Debug.Log("Current path Index in SetTargetPosition method" + currentPathIndex);
       // Debug.Log("Current position in SetTargetPosition method" + GetPosition());
        pathVectorList = Pathfinding.Instance.ShortestTarget(GetPosition(), targetPosition,targetPosition2,targetPosition3);
 //Debug.Log("pathVectorList in SetTargetPosition method" + pathVectorList.Count);
 if(GetName().Contains("player")){
     if(FarthestCharacter !=null && !FarthestCharacter.ContainsKey(GetName())){
     FarthestCharacter.Add(GetName(),pathVectorList.Count);
     }
     //Debug.Log("adding into dictionary");
     //Debug.Log(GetName()+"Added");
 }
 foreach (var item in FarthestCharacter)
 {
  // Debug.Log("This is inside the dictionary" + item.Key + item.Value);  
   //Debug.Log("Looping inside dictionary");
   //Debug.Log("FarthestList count" + FarthestCharacter.Count);
 }
 
  //Debug.Log("CharacterList count" + CharacterList.Count);
 if(FarthestCharacter.Count == CharacterList.Count){
   max = FarthestCharacter.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
   //Debug.Log("Character at farhest distance" + max);
    }
  
        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);      
     
         }  
  

    }
  
}
