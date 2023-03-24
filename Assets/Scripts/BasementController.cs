using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasementController : MonoBehaviour
{
    #region singleton
    public static BasementController instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject[] keySpawn;
    int randomLocation;
    // Start is called before the first frame update
    void Start()
    {
        keySpawn = GameObject.FindGameObjectsWithTag("keySpawn");
        randomLocation = Random.Range(0, keySpawn.Length);
        keySpawn[randomLocation].GetComponent<Neon_Script>().haveKey = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
