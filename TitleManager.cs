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
        MODE_OPTION,
        MODE_CREDIT,
        MODE_MAX
    };

    public MODE_STATE state;

    //private SEManager scrSEMgr;

    // Start is called before the first frame update
    void Start()
    {
        state = MODE_STATE.MODE_START;
        SoundManager.instance.PlayBGM(SoundManager.BGM_TYPE.BGM_TITLE);
        //scrSEMgr = GetComponent<SEManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case MODE_STATE.MODE_START:
                if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame)
                {
                    --state;
                    //scrSEMgr.SE2();
                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_CURSOR);
                    if (state >= MODE_STATE.MODE_MIN)
                    {
                        state = MODE_STATE.MODE_CREDIT;
                    }
                }
                if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame)
                {
                    ++state;
                    //scrSEMgr.SE2();
                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_CURSOR);
                }

                if (Keyboard.current.enterKey.isPressed)
                {
                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_OK);
                    SceneManager.LoadScene("Game", LoadSceneMode.Single);
                    //scrSEMgr.SE1();
                    //if (scrSEMgr.SECheck())
                    //{

                    //}
                }
                break;

            case MODE_STATE.MODE_OPTION:
                if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame)
                {
                    --state;
                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_CURSOR);
                    //scrSEMgr.SE2();
                }
                if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame)
                {
                    ++state;
                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_CURSOR);
                    //scrSEMgr.SE2();
                }

                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    //scrSEMgr.SE1();
                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_OK);
                    Debug.Log(state);
                }
                break;

            case MODE_STATE.MODE_CREDIT:
                if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame)
                {
                    --state;
                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_CURSOR);
                    //scrSEMgr.SE2();
                }
                if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame)
                {
                    ++state;
                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_CURSOR);
                    //scrSEMgr.SE2();
                    if (state <= MODE_STATE.MODE_MAX)
                    {
                        state = MODE_STATE.MODE_START;
                    }
                }

                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_OK);
                    //scrSEMgr.SE1();
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
