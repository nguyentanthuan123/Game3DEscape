using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class smallRoomController : MonoBehaviour
{
    public static smallRoomController instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject door;
    public GameObject answers;
    public GameObject answersSim;
    public GameObject answersPlace;
    public GameObject escape;
    public float answersMoveSpeed;
    public bool isHaveKey = false;
    bool isAnswers = false;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(isHaveKey && Vector3.Distance(door.transform.position, player.transform.position) < 5)
        {
            door.GetComponent<Animator>().SetBool("isOpen", true);
        }
        else
        {
            door.GetComponent<Animator>().SetBool("isOpen", false);
        }
        if (isAnswers == false)
        {
            if (Vector3.Distance(answers.transform.position, answersSim.transform.position) < 2)
            {
                answersSim.SetActive(true);
                isAnswers = true;
                Destroy(answers);
                escape.SetActive(true);
            }
        }    
        else
        {
            answersSimulate();
        }
    }
    void answersSimulate()
    {
        answersSim.transform.position = Vector3.Lerp(answersSim.transform.position, answersPlace.transform.position, answersMoveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 11)
        {
            isHaveKey = true;
            Destroy(other.gameObject);
        }
        roomController.instance.isTouchingDoor = true;
    }
    private void OnTriggerExit(Collider other)
    {
        roomController.instance.isTouchingDoor = false;
    }
}
