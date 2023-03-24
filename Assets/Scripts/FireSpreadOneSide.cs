using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpreadOneSide : MonoBehaviour
{
    public GameObject fireSpread;
    public Vector3 spreadValue;
    public float spreadTime;
    public int spreadSize;
    public int fireSize;
    public float spreadDist;
    int direction;
    int loopSpread=0;
    int copyDirection=1;
    float timecountdown;
    bool isFirstSpread = true;
    // Start is called before the first frame update
    void Start()
    {
        timecountdown = spreadTime;
        spreadValue = new Vector3(0,0,-spreadDist);
    }

    // Update is called once per frame
    void Update()
    {
        timecountdown -= Time.deltaTime;
        if(timecountdown <=0)
        {
            if(fireSize!=0)
            {
                spawnFire();
                fireSize--;
            }
            timecountdown = spreadTime;
        }
    }
    void spawnFire()
    {
        //0 is forward, 1 is left, 2 is right
        switch(direction)
        {
            case 0: spreadValue = new Vector3(0, 0, -spreadDist);   break;

            case 1: spreadValue = new Vector3(-spreadDist, 0, 0); break;

            case 2: spreadValue = new Vector3(spreadDist, 0, 0); break;
        }
        this.transform.position += spreadValue;
        Instantiate(fireSpread, this.transform.position, Quaternion.identity);
        if (direction != 0)
        {
            copyDirection = direction; // tao bien luu de tranh lap lai direction ve 0
            if(loopSpread > spreadSize)
            {
                direction = 0;
                loopSpread=0;
                if (isFirstSpread)
                {
                    spreadSize = spreadSize * 2;
                }
                isFirstSpread = false;
            }
            else
            {
                loopSpread++;
            }
        }
        else if(copyDirection == 1)
        {
            direction = 2;          
        }
        else
        {
            direction = 1;
        }      
    }
}
