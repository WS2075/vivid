using System.Collections;
using System.Collections.Generic;
using System;
//using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;


public class enemyMgr : MonoBehaviour
{
    public GameObject obj;

    private float width;
    private float width_space;

    private float height;
    private float height_space;

    // Start is called before the first frame update
    void Start()
    {
        width = (float)Screen.width / 2;
        width_space = (float)Screen.width / 12;
        height = (float)Screen.height / 2;
        height_space = (float)Screen.height / 7;

        for(int x = 0; x < 13; x++)
        {
            for(int y = 0; y < 8; y++)
            {
                var parent = this.transform;
                Instantiate(obj, new Vector3(width - (x * width_space), height - (y * height_space), 0.0f), Quaternion.identity, parent);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Transform GetPoint(int num)
    {
        return this.transform.GetChild(num);
    }

}
 