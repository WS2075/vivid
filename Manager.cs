using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Manager : MonoBehaviour
{
    //‰¼
    [SerializeField]
    private GameObject item;

    [SerializeField]
    private GameObject pause;

    private bool isPause;
    TimeMgr timeMgr;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        timeMgr = GetComponent<TimeMgr>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            isPaused();
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Instantiate(item, new Vector3(800.0f, 0.0f, 0.0f), Quaternion.identity);
        }

        if (Keyboard.current.digit0Key.isPressed)
        {
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }
    }

    public void isPaused()
    {
        isPause = !isPause;
        pause.SetActive(isPause);
        
        if(isPause)
        {
            timeMgr.SetGameSpeed(0.0f);
            Debug.Log(timeMgr.GetGameSpeed());
        }
        else
        {
            timeMgr.SetGameSpeed(1.0f);
            Debug.Log(timeMgr.GetGameSpeed());
        }
    }

    public bool nowPause()
    {
        return isPause;
    }
}
