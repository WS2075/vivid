using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

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

    [SerializeField]
    private PlayableDirector playable;

    [SerializeField]
    private GameObject GameClear;
    private Animator ClearAnime;

    [SerializeField]
    private GameObject GameOver;
    private Animator OverAnime;

    public bool isPause;
    private bool isSpaen;
    private bool isClear;
    private bool isOver;
    TimeMgr timeMgr;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        isSpaen = false;
        isClear = false;
        isOver = false;
        timeMgr = GetComponent<TimeMgr>();
        SoundManager.instance.PlayBGM(SoundManager.BGM_TYPE.BGM_GAME);

        scrPlayer = player.GetComponent<Player>();

        ClearAnime = GameClear.GetComponent<Animator>();
        OverAnime = GameOver.GetComponent<Animator>();
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

            isClear = true;
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

        if(isClear)
        {
            GameClear.SetActive(true);
            ClearAnime.SetBool("isClear", true);
        }

        if(isOver)
        {
            GameOver.SetActive(true);
            OverAnime.SetBool("isOver", true);
        }

    }

    public void isPaused()
    {
        isPause = !isPause;
        pause.SetActive(isPause);
        
        if(isPause)
        {
            timeMgr.SetGameSpeed(0.0f);
            playable.Pause();
            Debug.Log(timeMgr.GetGameSpeed());
        }
        else
        {
            timeMgr.SetGameSpeed(1.0f);
            playable.Resume();
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
        else
        {
            isOver = true;
        }
    }
}
