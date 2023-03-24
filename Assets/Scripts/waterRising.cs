using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterRising : MonoBehaviour
{
    // Start is called before the first frame update    
    //public Transform water;
    public Vector3 starPos;
    private void Awake() {
        starPos = transform.position;
        
    }
    private void Update() {
        if(transform.position.y<=starPos.y+19.7f){
            Vector3 upForce = Vector3.up;
            upForce.Set(transform.position.x,transform.position.y + 0.002f,transform.position.z);
            transform.position = upForce;
        }
    }
}
