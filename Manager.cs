using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Manager : MonoBehaviour
{
    //‰¼
    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.iKey.wasPressedThisFrame)
        {
            Instantiate(item, new Vector3(800.0f, 0.0f, 0.0f), Quaternion.identity);
        }

        if (Keyboard.current.digit0Key.isPressed)
        {
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }
    }
}
