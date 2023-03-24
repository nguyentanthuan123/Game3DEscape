using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadFireBoth : MonoBehaviour
{
    public GameObject fireSpread;
    public int spreadTime = 2;
    float timecountdown;
    float spreadDist = -3;
    public Vector3 spreadValue;
    Vector3 savePosLeft;
    Vector3 savePosRight;
    Vector3 savePosCenter;
    int direction;
    int loopSpread = 0;
    int copyDirection = 1;
    public int spreadSize;
    bool isFirstSpreadLeft;
    bool isFirstSpreadRight;
    bool isFirstSpreadCenter;
    // Start is called before the first frame update
    void Start()
    {
        timecountdown = spreadTime;
        spreadValue = new Vector3(0, 0, spreadDist);
    }

    // Update is called once per frame
    void Update()
    {
        timecountdown -= Time.deltaTime;
        if (timecountdown <= 0)
        {
            spawnFire();
            timecountdown = spreadTime;
        }
    }
    void spawnFire()
    {
        //0 is forward, 1 is left, 2 is right
        switch (direction)
        {
            case 0:
                spreadValue = new Vector3(0, 0, spreadDist);
                if (!isFirstSpreadCenter)
                {
                    this.transform.position = savePosCenter + spreadValue;
                }
                savePosCenter = this.transform.position;
                isFirstSpreadCenter = false; break;

            case 1:
                spreadValue = new Vector3(spreadDist, 0, 0);
                if (!isFirstSpreadLeft)
                {
                    this.transform.position = savePosLeft + spreadValue;
                }
                savePosLeft = this.transform.position;
                isFirstSpreadLeft = false; break;

            case 2:
                spreadValue = new Vector3(-spreadDist, 0, 0);
                if (!isFirstSpreadRight)
                {
                    this.transform.position = savePosRight + spreadValue;
                }
                savePosRight = this.transform.position;
                isFirstSpreadRight = false; break;
        }
        // if(isFirstSpreadCenter)
        Instantiate(fireSpread, this.transform.position, Quaternion.identity);

        if (direction != 0)
        {
            copyDirection = direction; // tao bien luu de tranh lap lai direction ve 0
            if (loopSpread > spreadSize)
            {
                direction = 0;
                loopSpread = 0;
            }
            else
            {
                if (copyDirection == 1)
                {
                    direction = 2;
                }
                else
                {
                    direction = 1;
                }
                loopSpread++;
            }
        }
        else if (copyDirection == 1)
        {
            direction = 2;
        }
        else
        {
            direction = 1;
        }
    }
}
