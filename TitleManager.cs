using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class TitleManager : MonoBehaviour
{
    public enum MODE_STATE
    {
        MODE_MIN,
        MODE_START,
        MODE_CREDIT,
        MODE_MAX
    };

    public MODE_STATE state;

    // Start is called before the first frame update
    void Start()
    {
        state = MODE_STATE.MODE_START;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case MODE_STATE.MODE_START:
                if (Keyboard.current.upArrowKey.wasPressedThisFrame)
                {
                    --state;
                    if(state >= MODE_STATE.MODE_MIN)
                    {
                        state = MODE_STATE.MODE_CREDIT;
                    }
                }
                if (Keyboard.current.downArrowKey.wasPressedThisFrame)
                {
                    ++state;
                }

                if (Keyboard.current.enterKey.isPressed)
                {
                    SceneManager.LoadScene("Game", LoadSceneMode.Single);
                }
                break;

            case MODE_STATE.MODE_CREDIT:
                if (Keyboard.current.upArrowKey.wasPressedThisFrame)
                {
                    --state;
                }
                if (Keyboard.current.downArrowKey.wasPressedThisFrame)
                {
                    ++state;
                    if(state <= MODE_STATE.MODE_MAX)
                    {
                        state = MODE_STATE.MODE_START;
                    }
                }

                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    Debug.Log(state);
                }
                break;
        }
    }

    public MODE_STATE GetModeStart()
    {
        return MODE_STATE.MODE_START;
    }

    public MODE_STATE GetModeCredit()
    {
        return MODE_STATE.MODE_CREDIT;
    }
}
