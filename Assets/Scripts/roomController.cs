using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class roomController : MonoBehaviour
{
    public static roomController instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject airCondition;
    public GameObject enegry;
    public GameObject power;
    public GameObject lighting;
    public GameObject fireSpread;
    public Text timeTxt;
    public Text message;
    public float enegryDistance;
    public float speedEnegry;
    public float timer;
    Animator airConditionAnim;
    Animator powerAnim;
    bool isPowerOn = true; // use for open animation
    bool isHeating = false; 
    public bool isDoorUnlocked = false;
    public bool isTouchingDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        airCondition = GameObject.FindGameObjectWithTag("airCondition");
    }

    // Update is called once per frame
    void Update()
    {
        setText();
        if (isPowerOn)
        {
            if (Vector3.Distance(enegry.transform.position, power.transform.position) < enegryDistance)
            {
                enegry.transform.position = Vector3.Lerp(enegry.transform.position, power.transform.position, speedEnegry * Time.deltaTime);
                turnOnPower();
                if (timer>0)
                {
                    StartCoroutine(timeCount());
                }
                else
                {
                    fireSpread.SetActive(true);
                }
            }
            else
            {
                turnOffPower();
            }
        }

    }
    void turnOnPower()
    {
        Player_status.instance.isOxigen = true;
        isHeating = true;
        powerAnim = power.GetComponent<Animator>();
        airConditionAnim = airCondition.GetComponent<Animator>();
        if (Vector3.Distance(enegry.transform.position, power.transform.position) <0.1)
        { 
            powerAnim.SetBool("isEnegryIn", true);
            lighting.SetActive(true);
            airConditionAnim.SetBool("turnOn", true);
        }
    }
    void turnOffPower()
    {
        Player_status.instance.isOxigen = false;
        isHeating = false;
        powerAnim = power.GetComponent<Animator>();
        airConditionAnim = airCondition.GetComponent<Animator>();
        if (Vector3.Distance(enegry.transform.position, power.transform.position) > 0.1)
        {
            powerAnim.SetBool("isEnegryIn", false);
            lighting.SetActive(false);
            airConditionAnim.SetBool("turnOn", false);
        }
    }
    IEnumerator timeCount()
    {    
        if(timer>0)
        {
            timer -= Time.deltaTime;
        }
        timeTxt.text ="0"+ (int)(timer/60)%60 + ":" + (int)timer%60;
        yield return null;
    }
    void setText()
    {      
        if (isTouchingDoor && smallRoomController.instance.isHaveKey == false)
        {
                message.text = "Find the golden key";

        }
        else if(smallRoomController.instance.isHaveKey == false)
        {
            if (isHeating)
            {
                message.text = "The Heater are going to explosive";
            }
            else
            {

                message.text = "Find enegry and place it to the holder";
            }         
        }
        else
        {
            message.text = " What is missing in the picture";
        }
    }
    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
