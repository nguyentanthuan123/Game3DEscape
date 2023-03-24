using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void play()
    {
        PlayerPrefs.SetInt("IsTwoGun", 1);
        SceneManager.LoadScene(1);
    }
    public void continueBtn()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }
    public void exitBtn()
    {
        Application.Quit();
    }
}
