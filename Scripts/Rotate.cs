using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Rotate : VuforiaMonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0f,1f,0f);
    }
}
