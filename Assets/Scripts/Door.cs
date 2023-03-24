using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Door : MonoBehaviour
{
    public bool isOpen;
    public Transform door;
    public Light endLight;
    public TextMeshProUGUI conversation;
    private void Awake() {
        isOpen=false;
        endLight.intensity = 0;
    }
    private void Update() {
        if(isOpen){
            door.rotation = Quaternion.RotateTowards(door.rotation,Quaternion.Euler(new Vector3(0,-20,0)),Time.deltaTime*30f);
            if(endLight.intensity < 50)
                endLight.intensity += Time.deltaTime*10f;
        }
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 11){
            conversation.text="Door is Open. Let escape";
            isOpen=true;
        }
        if(endLight.intensity>=50){
            if(other.transform.tag=="Player"){
                conversation.text="Finally";
            }
        }
        if(other.transform.tag=="Player"&&!isOpen){
                conversation.text="It's Lock, should find key first";
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(isOpen && collision.gameObject.transform.tag.Equals("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
