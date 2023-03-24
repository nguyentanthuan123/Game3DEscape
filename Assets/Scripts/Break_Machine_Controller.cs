using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_Machine_Controller : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject projectile;
    public Quaternion starQuaternion;
    public Vector3 toVector;
    public float lerpTime = 90f;
    int i = 0;


    private Quaternion curQuater;
    private void Awake() {
        starQuaternion = transform.rotation;
        lerpTime = 90f;
        fakeTime = 0;
    }
    public float fakeTime;
    // Update is called once per frame
    void Update()
    {   
        RotateMachine();
        fakeTime+=Time.deltaTime;
        if(fakeTime>=2 && i<30){
            Shoot();
            fakeTime=0;
        }
    }
    private void RotateMachine(){
        if(transform.rotation == Quaternion.Euler(toVector)){
            curQuater = starQuaternion;
        }
        else if(transform.rotation == starQuaternion){
            curQuater = Quaternion.Euler(toVector);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation,curQuater,Time.deltaTime*lerpTime);
    }
    public void Shoot(){
        GameObject cur;
        cur =GameObject.Instantiate(projectile, shootPoint.position, transform.rotation );
        cur.transform.localScale *= (Random.Range(4,20)*0.2f);
        cur.GetComponent<Rigidbody>().AddForce(shootPoint.forward * -(Random.Range(200,700)));
        i++;
    }
}
