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

    private GameObject fadeManager;
    private FadeManager scrFadeMgr;

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
    public bool isClear;
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

        fadeManager = GameObject.Find("FadeManager");
        scrFadeMgr = fadeManager.GetComponent<FadeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            isPaused();
        }

        //if (Keyboard.current.enterKey.wasPressedThisFrame)
        //{
        //    isClear = true;
        //}

        if (Keyboard.current.digit0Key.isPressed)
        {
            //SceneManager.LoadScene("Title", LoadSceneMode.Single);
            scrFadeMgr.FadeOutStart(0, 0, 0, 0, "Title");
        }

        if (!player.activeSelf && isSpaen == false)
        {
            isSpaen = true;
            StartCoroutine(ReSpawn());
        }

        if(isClear)
        {
            SoundManager.instance.PlayBGM(SoundManager.BGM_TYPE.BGM_CLEAR);
            GameClear.SetActive(true);
            ClearAnime.SetBool("isClear", true);

            if(ClearAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                if(Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    //SceneManager.LoadScene("Title", LoadSceneMode.Single);
                    scrFadeMgr.FadeOutStart(0, 0, 0, 0, "Title");
                }
            }
        }

        if(isOver)
        {
            SoundManager.instance.PlayBGM(SoundManager.BGM_TYPE.BGM_OVER);
            GameOver.SetActive(true);
            OverAnime.SetBool("isOver", true);

            if(OverAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    //SceneManager.LoadScene("Title", LoadSceneMode.Single);
                    scrFadeMgr.FadeOutStart(0, 0, 0, 0, "Title");
                }
            }
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
