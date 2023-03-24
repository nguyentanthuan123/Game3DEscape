using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neon_Script : MonoBehaviour
{
    
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        
    }
    public bool ElecShock;
    private void OnCollisionEnter(Collision other) {
        if(other.transform.tag=="Grabable" && isDrop == false){
            Neon_Break();
            ElecShock = false;
            Debug.Log(haveKey);
            if (haveKey)
            {
                Instantiate(key, particalDrop.transform.position,Quaternion.identity);
                isDrop = true;
            }
            else
            {
                Instantiate(fakeKey, particalDrop.transform.position, Quaternion.identity);
            }
        }
        
    }
    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer==4){
            if(ElecShock){
                if(GameObject.FindGameObjectWithTag("player").GetComponent<FPSControllerLPFP.FpsControllerLPFP>().isSurfing == true){
                    GameObject.FindGameObjectWithTag("player").GetComponent<Player_status>().health-=45*Time.deltaTime;
                }
            }
        }
    }
    public GameObject glasses;
    public GameObject key;
    public GameObject fakeKey;
    public Transform particalDrop;
    GameObject basementController;
    public bool haveKey = false;
    bool isDrop = false;
   // public GameObject key;
    public int minPosSpawnX;
    public int maxPosSpawnX;
    private void Neon_Break(){
        
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor",Color.gray);
        GameObject cur;
        Vector3 randPos=Vector3.zero;
        Vector3 randRotate=Vector3.zero;
        for(int i=0;i<=30;i++){
            randPos.x= Random.Range(minPosSpawnX,maxPosSpawnX)/10;
            randPos.y=particalDrop.position.y;
            randPos.z=particalDrop.position.z;
            cur = GameObject.Instantiate(glasses, randPos , Quaternion.identity );
            cur.transform.localScale *= (Random.Range(1,4)*0.1f);
            randRotate = particalDrop.forward;
            randRotate.x += Random.Range(-300,300)/10;
            randRotate.z += Random.Range(-900,900)/10;
            cur.GetComponent<Rigidbody>().AddForce(randRotate* -(Random.Range(5,20)));
        }
        //if(haveKey == false){
        //   // GameObject.Instantiate(key, randPos , Quaternion.identity );
        //    haveKey=true;
        //}
    }
}
