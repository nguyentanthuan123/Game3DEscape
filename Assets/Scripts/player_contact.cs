using System.Collections;
using System.Linq;
using System;

using System.Collections.Generic;
using UnityEngine;

public class player_contact : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera player_cam;
    public GameObject holding_Obj;
    public GameObject handling_Obj;
    public Transform holding_possision;
    public bool isHolding;
    public bool isHandling;
    public Animator anim;
    public float speed;
    public float throwPower;
    public float maxPower;
    void Awake() {
        isHandling = false;
        isHolding = false;
        holding_Obj = null;
        anim=gameObject.GetComponent<Animator>();
        throwPower=10f;
        maxPower = 100f;
    }
    // Update is called once per frame
    void Update()
    {
        if(holding_Obj != null) {
            holding_Obj.transform.SetParent(holding_possision) ;
            holding_Obj.transform.position = holding_possision.position;
            holding_Obj.transform.rotation = holding_possision.rotation;
            isHolding = true;
        }
        if(handling_Obj != null) {
            //gameObject.transform.SetParent(handling_Obj.transform);
            //holding_possision.position
            //isHolding = true ;
            gameObject.GetComponentInParent<FPSControllerLPFP.FpsControllerLPFP>().LockMoving = true;
            isHandling =true;
        }
        if(holding_Obj==null&&handling_Obj==null){
            isHandling=false;
            isHolding = false;
            anim.SetBool("IsGrabling",false) ;
        }
        Contact();
        ThrowObj();
    }
    
    public void Contact(){          
        if(Input.GetButtonDown("Fire1") && (!isHolding||!isHandling) ){
            var ray  = player_cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit,3f)&& hit.transform.gameObject.tag == "Grabable"){
                anim.SetBool("IsGrabling",true) ;
                holding_Obj = hit.transform.gameObject;
            }
            else if(Physics.Raycast(ray, out hit,3f)&& hit.transform.gameObject.tag == "handle"){
                anim.SetBool("IsGrabling",true) ;
                handling_Obj = hit.transform.gameObject;
            }
        }
        if(Input.GetButtonUp("Fire1") && (holding_Obj != null||handling_Obj!=null) ){
            if(holding_Obj!=null){
                holding_Obj.transform.SetParent(null) ;
            }
            if(handling_Obj!=null){
                gameObject.GetComponentInParent<FPSControllerLPFP.FpsControllerLPFP>().LockMoving=false;
            }
            holding_Obj = null;
            handling_Obj = null;
            // Debug.Log(throwPower);
            throwPower = 10f;
        }
    }
    public void ThrowObj(){
        if(Input.GetKey(KeyCode.F) && holding_Obj != null){
            //if(isHandling) return;
            throwPower += Time.deltaTime * 30f;  
            //Debug.Log(throwPower);
            if(throwPower>maxPower) throwPower = maxPower;
        }
        if(Input.GetKeyUp(KeyCode.F ) && holding_Obj != null){
            //if(isHandling) return;
            var ray  = player_cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
            holding_Obj.GetComponent<Rigidbody>().AddForce( (ray.GetPoint(20)-holding_possision.position).normalized * throwPower,ForceMode.VelocityChange);
            holding_Obj.transform.SetParent(null) ;
            holding_Obj = null;
            throwPower = 10f;
        }
    }    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer==6){
            gameObject.GetComponent<Player_status>().health-=20;
            
        }
    }
    private void OnCollisionStay(Collision other) {
        if(other.gameObject.layer==6){
            gameObject.GetComponent<Player_status>().health-=10*Time.deltaTime;
        }
    }
}
