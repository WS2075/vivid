using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Player scrPlayer;

    //仮
    [SerializeField]
    private GameObject item;

    [SerializeField]
    private GameObject pause;

    private bool isPause;
    private bool isSpaen;
    TimeMgr timeMgr;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        isSpaen = false;
        timeMgr = GetComponent<TimeMgr>();
        SoundManager.instance.PlayBGM(SoundManager.BGM_TYPE.BGM_GAME);

        scrPlayer = player.GetComponent<Player>();
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

        if (!player.activeSelf && isSpaen == false)
        {
            isSpaen = true;
            StartCoroutine(ReSpawn());
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

    private IEnumerator ReSpawn()
    {
        if (scrPlayer.remaining > 0)
        {
            yield return new WaitForSeconds(2.0f);
            player.SetActive(true);
            scrPlayer.ReSpawn();
            isSpaen = false;
            Debug.Log("ReSpawn");
        }
    }
}
