using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class Boss : MonoBehaviour
{
    private enum BOSS_STATE
    {
        INVASION,
        BATTLE
    }

    private BOSS_STATE state;

    private GameObject Mgr;
    private Manager scrManager;

    [Header("Status")]
    public int MaxHP;
    [SerializeField]
    private int borderHP;
    public int NowHP;
    private bool isActive;
    private GameObject root;

    [SerializeField]
    private GameObject SecondBarrel;

    [Header("Move")]
    [SerializeField]
    private float speed;
    private Transform initialPos;
    private Transform StartPos;
    private Transform EndPos;

    [SerializeField]
    private Transform defaultPosition;

    [SerializeField]
    private Transform RightLimit;

    [SerializeField]
    private Transform LeftLimit;

    [SerializeField]
    private Transform RightToDef;

    [SerializeField]
    private Transform LeftToDef;

    private BoxCollider2D collider;
    private float distance;
    private float elapsedTime;


    public void OnControlTimeStart()
    {
        gameObject.SetActive(true);
        isActive = true;
        
    }

    public void OnControlTimeStop()
    {
        gameObject.SetActive(false);
    }

    public void SetTime(double time)
    {
        if (!isActive)
        {
            root.SetActive(false);
            //gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM_TYPE.BGM_BOSS);

        Mgr = GameObject.Find("GameManager");
        scrManager = Mgr.GetComponent<Manager>();

        state = BOSS_STATE.INVASION;
        NowHP = MaxHP;
        initialPos = gameObject.transform;
        collider = GetComponent<BoxCollider2D>();
        root = transform.root.gameObject;


        distance = Vector3.Distance(initialPos.localPosition, defaultPosition.localPosition);
        RightLimit.localPosition -= new Vector3(collider.size.x / 2, 0.0f, 0.0f);
        LeftLimit.localPosition += new Vector3(collider.size.x / 2, 0.0f, 0.0f);
        RightToDef.localPosition = Vector3.Lerp(RightLimit.localPosition, defaultPosition.localPosition, 0.5f);
        RightToDef.localPosition += new Vector3(0.0f, Screen.height / 4, 0.0f);

        LeftToDef.localPosition = Vector3.Lerp(LeftLimit.localPosition, defaultPosition.localPosition, 0.5f);
        LeftToDef.localPosition += new Vector3(0.0f, -Screen.height / 4, 0.0f);

        StartPos = RightLimit;
        EndPos = LeftLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (NowHP <= 0)
        {
            isActive = false;

            if (!isActive)
            {
                scrManager.isClear = true;
                root.SetActive(false);
                //gameObject.SetActive(false);
            }
        }

        if (!scrManager.isPause)
        {
            elapsedTime += Time.deltaTime;
            float now_Location = (elapsedTime * speed) / distance;

            switch (state)
            {
                case BOSS_STATE.INVASION:
                    transform.localPosition = Vector3.Lerp(initialPos.localPosition, defaultPosition.localPosition, now_Location);
                    if (now_Location >= 1.0f)
                    {
                        state = BOSS_STATE.BATTLE;
                        SetStartEnd(StartPos, EndPos);
                    }
                    break;

                case BOSS_STATE.BATTLE:
                    var P1 = Vector3.Lerp(StartPos.localPosition, RightToDef.localPosition, now_Location);
                    var P2 = Vector3.Lerp(RightToDef.localPosition, defaultPosition.localPosition, now_Location);
                    var P3 = Vector3.Lerp(defaultPosition.localPosition, LeftToDef.localPosition, now_Location);
                    var P4 = Vector3.Lerp(LeftToDef.localPosition, EndPos.localPosition, now_Location);

                    var P5 = Vector3.Lerp(P1, P2, now_Location);
                    var P6 = Vector3.Lerp(P2, P3, now_Location);
                    var P7 = Vector3.Lerp(P3, P4, now_Location);

                    var P8 = Vector3.Lerp(P5, P6, now_Location);
                    var P9 = Vector3.Lerp(P6, P7, now_Location);

                    transform.localPosition = Vector3.Lerp(P8, P9, now_Location);

                    if(now_Location >= 1.0f)
                    {
                        var buffer = RightToDef;
                        RightToDef = LeftToDef;
                        LeftToDef = buffer;

                        SetStartEnd(EndPos, StartPos);
                    }
                    break;
            }

            if(NowHP <= borderHP)
            {
                SecondBarrel.SetActive(true);
            }

        }
    }

    public int GetNowHP()
    {
        return NowHP;
    }

    public void Damage(int value)
    {
        NowHP -= value;
    }

    private void SetStartEnd(Transform startPos,Transform endPos)
    {
        StartPos = startPos;
        EndPos = endPos;

        elapsedTime = 0.0f;
        distance = Vector3.Distance(StartPos.position, EndPos.position);
    }
}
