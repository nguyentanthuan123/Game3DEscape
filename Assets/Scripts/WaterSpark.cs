using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpark : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem par;
    public bool IsFloating;
    public bool isHit = false;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer==4){
            Instantiate(par,other.GetContact(0).point,Quaternion.identity);
            isHit = true;
        }
    }
    private void OnCollisionStay(Collision other) {
        return;
    }
    private void OnCollisionExit(Collision other) {
    }
    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer==4){
            
            if(IsFloating){
                if(transform.position.y < other.gameObject.transform.position.y){
                    if(TryGetComponent<Rigidbody>(out Rigidbody rig)){
                        
                        rig.useGravity=false;
                    }
                    Vector3 upForce = Vector3.up;
                    upForce.Set(transform.position.x,transform.position.y + 0.1f,transform.position.z);
                    transform.position = upForce;
                }
                else if(TryGetComponent<Rigidbody>(out Rigidbody rig)){
                        rig.useGravity=true;
                    }
                if(gameObject.tag!="Player"){
                    
                    //Shaking();
                }   
            }
        }
    }
    private void Shaking(){
        Vector3 x = Vector3.zero;
        x.x= x.x += Random.Range(-1,1)*0.5f;
        x.z= x.z += Random.Range(-1,1)*0.5f;
        gameObject.GetComponent<Rigidbody>().AddForce(x*Time.deltaTime,ForceMode.Impulse);
        //transform.position.Set(x.x,x.y,x.z);

        Quaternion r = transform.rotation;
        r.x = r.x +=Random.Range(-1,1)*0.1f;
        r.y = r.y+=Random.Range(-1,1)*0.1f;
        r.z = r.z+=Random.Range(-1,1)*0.1f;
        transform.rotation.Set(r.x,r.y,r.z,r.w);
    }
}
