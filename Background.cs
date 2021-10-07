using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-20.0f, 0, 0);
        if(transform.position.x < -1900)
        {
            transform.position = new Vector3(1900, 0, 0);
        }
    }
}
