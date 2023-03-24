using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dive_Float : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsFloating;
    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        
    }
}
