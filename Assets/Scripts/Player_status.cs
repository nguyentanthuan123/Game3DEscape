using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player_status : MonoBehaviour
{
    public static Player_status instance;
    // Start is called before the first frame update
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public float health;
    public float maxHealth = 100;
    public float power;
    public float maxPower = 100;
    public int damnge = 10;
    public bool isDead;
    public bool isOxigen;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI conversation;
    float timeDelay = 0.5f;
   private void Awake() {
       isDead=false;
       health = maxHealth;
       power = maxPower;
       instance = this;
   }

    // Update is called once per frame
    void Update()
    {
        StatusChange();
        if(health <=0){
            isDead=true;
        }
        
        if(!gameObject.GetComponent<FPSControllerLPFP.FpsControllerLPFP>().isSurfing)
            if((gameObject.GetComponent<FPSControllerLPFP.FpsControllerLPFP>()._isGrounded) && power<maxPower){
                power += Time.deltaTime*5;
                if(power > maxPower) power= maxPower;
            }    
        if(gameObject.GetComponent<FPSControllerLPFP.FpsControllerLPFP>().isSurfing)
        {
            if(gameObject.GetComponentInChildren<player_contact>().isHandling){
                power += Time.deltaTime*8;
                if(power > maxPower) power= maxPower;
            }
            else if(gameObject.GetComponent<FPSControllerLPFP.FpsControllerLPFP>().isMoving)
                power -= Time.deltaTime*5;
        }
    }
    void StatusChange(){
        if (health <= 0)
        {
            health = 0;
            gameOverPanel.SetActive(true);
            activateMouse();
            Time.timeScale = 0;
        }
        if(power<=0) {
            health-=10*Time.deltaTime;
            power =0;
        }
        if (!isOxigen)
        {
            //StartCoroutine(losePower());
           // FPSControllerLPFP.FpsControllerLPFP.instance.isSurfing = true;
        }
        statusText.text="Health: " + (int)health + "\nPower: " + (int)power;
    }
    void activateMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void damngePlayer(int damnge)
    {
        if (timeDelay <= 0)
        {
            health -= damnge;

        }
        
    }
    IEnumerator losePower()
    {
        if (power > 0)
        {
            power -= Time.deltaTime*5;
        }
        yield return null;
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag.Equals("fireSpread"))
        {
            health -= Time.deltaTime*5;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.tag.Equals("escape"))
        {
            activateMouse();
            winPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
