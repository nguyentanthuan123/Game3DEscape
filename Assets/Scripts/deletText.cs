using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class deletText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<TextMeshProUGUI>().text!="")  
        {
            StartCoroutine("deleteText");
        }  
    }
    IEnumerator deleteText(){
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<TextMeshProUGUI>().text="";
    }

}
