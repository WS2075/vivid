using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.Linq;
using QFSW.MOP2;

public class enemy : MonoBehaviour, ITimeControl
{
    public enum STATE
    {
        INVASION,
        FORMATION,
        BATTLE,
        WITHDRAW
    }

    public enum MOVE_TYPE
    {
        LEFT_TURN,
        RIGHT_TURN,
        WAVY,
        ROTATE
    }

    public enum BULLET_TYPE
    {
        NORMAL,
        SPREAD,
        HOMING
    }

    public enum ITEM
    {
        RECOVER,
        POWERUP,
        ONEUP
    }

    [Header("Status")]
    public int hp;
    public float speed;
    public float battletime;
    public bool isMiddle;
    public MOVE_TYPE move_type;
    public int score;
    private bool isActive;

    [Header("Item")]
    public bool isItem;
    public ITEM item_state;
    [SerializeField]
    private GameObject recoverItem;
    [SerializeField]
    private GameObject powUpItem;
    [SerializeField]
    private GameObject oneUpItem;


    [Header("Bullet")]
    public BULLET_TYPE bullet_type;
    public GameObject barrel;
    public GameObject normalBulletPrefab;
    public GameObject spreadBulletPrefab;
    public GameObject homingBulletPrefab;
    public float cooltime;
    private float default_cooltime;

    [Header("Bullet Pool")]
    [SerializeField]
    private ObjectPool NormalBulletPool;
    [SerializeField]
    private ObjectPool SpreadBulletPool;
    [SerializeField]
    private ObjectPool HomingBulletPool;

    [Header("Particle")]
    [SerializeField]
    private ObjectPool particlePool;

    [Header("No Touch")]
    public int startPosNum;
    public int endPosNum;
    public int formationPosNum;


    private STATE state;
    private Quaternion angle1, angle2;

    private enemy_bullet scrEnemybullet;

    public Transform startMarker;
    public Transform endMarker;
    public Transform formationMarker;
    
    private float distance;
    private float elapsedTime;
    private bool withdraw_phase2 = false;

    private GameObject mgr;
    private ScoreManager scrScoreMgr;

    private GameObject bulletCase;

    ////playable
    //private GameObject playableObj;
    //private PlayableDirector director;


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
        if(!isActive)
        {
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = STATE.INVASION;

        default_cooltime = cooltime;

        //scrEnemybullet = bulletPrefab.GetComponent<enemy_bullet>();

        mgr = GameObject.Find("GameManager");
        scrScoreMgr = mgr.GetComponent<ScoreManager>();

        startMarker = mgr.transform.GetChild(startPosNum);
        endMarker = mgr.transform.GetChild(endPosNum);
        formationMarker = mgr.transform.GetChild(formationPosNum);

        if(isMiddle)
        {
            GameObject newFormationMarker = new GameObject("FormationMarker");
            newFormationMarker.transform.position = Vector3.Lerp(mgr.transform.GetChild(formationPosNum).position,
                                                mgr.transform.GetChild(formationPosNum + 9).position, 0.5f);

            formationMarker = newFormationMarker.transform;
        }

        distance = Vector3.Distance(startMarker.position, endMarker.position);

        angle1 = Quaternion.AngleAxis(30.0f, new Vector3(0.0f, 0.0f, 1.0f));
        angle2 = Quaternion.AngleAxis(-30.0f, new Vector3(0.0f, 0.0f, 1.0f));

        bulletCase = GameObject.Find("BulletCase");
    }

    // Update is called once per frame
    void Update()
    {
        cooltime -= Time.deltaTime;

        elapsedTime += Time.deltaTime;
        float now_Location = (elapsedTime * speed) / distance;

        switch(state)
        {
            case STATE.INVASION:
                switch (move_type)
                {
                    case MOVE_TYPE.LEFT_TURN:
                        var center_L = Vector3.Lerp(startMarker.position, endMarker.position, 0.5f);
                        var point_L = new Vector3(center_L.x + (startMarker.position.y - center_L.y), center_L.y + (startMarker.position.x - center_L.x), 0.0f);

                        var P1_L = Vector3.Lerp(startMarker.position, point_L, now_Location);
                        var P2_L = Vector3.Lerp(point_L, endMarker.position, now_Location);

                        transform.position = Vector3.Lerp(P1_L, P2_L, now_Location);
                        break;

                    case MOVE_TYPE.RIGHT_TURN:
                        var center_R = Vector3.Lerp(startMarker.position, endMarker.position, 0.5f);
                        var point_R = new Vector3(center_R.x - (startMarker.position.y - center_R.y), center_R.y - (startMarker.position.x - center_R.x), 0.0f);

                        var P1_R = Vector3.Lerp(startMarker.position, point_R, now_Location);
                        var P2_R = Vector3.Lerp(point_R, endMarker.position, now_Location);

                        transform.position = Vector3.Lerp(P1_R, P2_R, now_Location);
                        break;

                    case MOVE_TYPE.ROTATE:
                        break;

                    case MOVE_TYPE.WAVY:
                        var center_W = Vector3.Lerp(startMarker.position, endMarker.position, 0.5f);
                        var center_WR = Vector3.Lerp(startMarker.position, center_W, 0.5f);
                        var center_WL = Vector3.Lerp(center_W, endMarker.position, 0.5f);

                        var point_WR = new Vector3(center_WR.x + (startMarker.position.y - center_WR.y) * 2, center_WR.y + (startMarker.position.x - center_WR.x) * 2, 0.0f);
                        var point_WL = new Vector3(center_WL.x - (center_W.y - center_WL.y) * 2, center_WL.y - (center_W.x - center_WL.x) * 2, 0.0f);

                        var P1_W = Vector3.Lerp(startMarker.position, point_WR, now_Location);
                        var P2_W = Vector3.Lerp(point_WR, center_W, now_Location);
                        var P3_W = Vector3.Lerp(center_W, point_WL, now_Location);
                        var P4_W = Vector3.Lerp(center_WL, endMarker.position, now_Location);

                        var P5_W = Vector3.Lerp(P1_W, P2_W, now_Location);
                        var P6_W = Vector3.Lerp(P2_W, P3_W, now_Location);
                        var P7_W = Vector3.Lerp(P3_W, P4_W, now_Location);

                        var P8_W = Vector3.Lerp(P5_W, P6_W, now_Location);
                        var P9_W = Vector3.Lerp(P6_W, P7_W, now_Location);

                        transform.position = Vector3.Lerp(P8_W, P9_W, now_Location);

                        break;
                }

                if (now_Location >= 1.0f)
                {
                    state = STATE.FORMATION;
                    SetStartEnd(endMarker, formationMarker);
                }
                break;

            case STATE.FORMATION:
                transform.position = Vector3.Lerp(endMarker.position, formationMarker.position, now_Location);

                if(now_Location >= 1.0f)
                {
                    state = STATE.BATTLE;
                }
                break;

            case STATE.BATTLE:
                battletime -= Time.deltaTime;
                if (battletime <= 0.0f)
                {
                    state = STATE.WITHDRAW;
                    SetStartEnd(formationMarker, endMarker);
                }
                break;

            case STATE.WITHDRAW:
                transform.position = Vector3.Lerp(formationMarker.position, endMarker.position, now_Location);
                if(!withdraw_phase2)
                {
                    if (now_Location >= 1.0f)
                    {
                        SetStartEnd(endMarker, startMarker);
                        withdraw_phase2 = true;
                        break;
                    }
                }
                

                if (withdraw_phase2)
                {
                    if(now_Location >= 1.0f)
                    {
                        //gameObject.SetActive(false);
                        isActive = false;
                        break;
                    }

                    switch (move_type)
                    {
                        case MOVE_TYPE.LEFT_TURN:
                            var center_L = Vector3.Lerp(endMarker.position, startMarker.position, 0.5f);
                            var point_L = new Vector3(center_L.x + (endMarker.position.y - center_L.y), center_L.y + (endMarker.position.x - center_L.x), 0.0f);

                            var P1_L = Vector3.Lerp(endMarker.position, point_L, now_Location);
                            var P2_L = Vector3.Lerp(point_L, startMarker.position, now_Location);

                            transform.position = Vector3.Lerp(P1_L, P2_L, now_Location);
                            break;

                        case MOVE_TYPE.RIGHT_TURN:
                            var center_R = Vector3.Lerp(endMarker.position, startMarker.position, 0.5f);
                            var point_R = new Vector3(center_R.x - (endMarker.position.y - center_R.y), center_R.y - (endMarker.position.x - center_R.x), 0.0f);

                            var P1_R = Vector3.Lerp(endMarker.position, point_R, now_Location);
                            var P2_R = Vector3.Lerp(point_R, startMarker.position, now_Location);

                            transform.position = Vector3.Lerp(P1_R, P2_R, now_Location);
                            break;

                        case MOVE_TYPE.ROTATE:
                            break;

                        case MOVE_TYPE.WAVY:
                            var center_W = Vector3.Lerp(endMarker.position, startMarker.position, 0.5f);
                            var center_WR = Vector3.Lerp(endMarker.position, center_W, 0.5f);
                            var center_WL = Vector3.Lerp(center_W, startMarker.position, 0.5f);

                            var point_WR = new Vector3(center_WR.x + (endMarker.position.y - center_WR.y) * 2, center_WR.y + (endMarker.position.x - center_WR.x) * 2, 0.0f);
                            var point_WL = new Vector3(center_WL.x - (center_W.y - center_WL.y) * 2, center_WL.y - (center_W.x - center_WL.x) * 2, 0.0f);

                            var P1_W = Vector3.Lerp(endMarker.position, point_WR, now_Location);
                            var P2_W = Vector3.Lerp(point_WR, center_W, now_Location);
                            var P3_W = Vector3.Lerp(center_W, point_WL, now_Location);
                            var P4_W = Vector3.Lerp(center_WL, startMarker.position, now_Location);

                            var P5_W = Vector3.Lerp(P1_W, P2_W, now_Location);
                            var P6_W = Vector3.Lerp(P2_W, P3_W, now_Location);
                            var P7_W = Vector3.Lerp(P3_W, P4_W, now_Location);

                            var P8_W = Vector3.Lerp(P5_W, P6_W, now_Location);
                            var P9_W = Vector3.Lerp(P6_W, P7_W, now_Location);

                            transform.position = Vector3.Lerp(P8_W, P9_W, now_Location);

                            break;
                    }
                }
                break;
        }

        if(cooltime <= 0.0f)
        {
            switch(bullet_type)
            {
                case BULLET_TYPE.NORMAL:
                    //Instantiate(normalBulletPrefab, barrel.transform.position, Quaternion.identity,bulletCase.transform);
                    CreateBullet(barrel.transform.position, Quaternion.identity);
                    cooltime = default_cooltime;
                    break;

                case BULLET_TYPE.SPREAD:
                    //Instantiate(spreadBulletPrefab, barrel.transform.position, Quaternion.identity, bulletCase.transform);
                    //Instantiate(spreadBulletPrefab, barrel.transform.position, angle1, bulletCase.transform);
                    //Instantiate(spreadBulletPrefab, barrel.transform.position, angle2, bulletCase.transform);
                    CreateBullet(barrel.transform.position, Quaternion.identity);
                    CreateBullet(barrel.transform.position, angle1);
                    CreateBullet(barrel.transform.position, angle2);
                    cooltime = default_cooltime;
                    break;

                case BULLET_TYPE.HOMING:
                    break;
            }
        }
    }

    public void SetStartEnd(Transform start_pos, Transform end_pos)
    {
        elapsedTime = 0.0f;
        distance = Vector3.Distance(start_pos.position, end_pos.position);
    }

    private void CreateBullet(Vector3 position, Quaternion angle)
    {
        switch(bullet_type)
        {
            case BULLET_TYPE.NORMAL:
                var newNormalBullet = NormalBulletPool.GetObject(position);
                newNormalBullet.transform.rotation = angle;
                break;

            case BULLET_TYPE.SPREAD:
                var newSpreadBullet = SpreadBulletPool.GetObject(position);
                newSpreadBullet.transform.rotation = angle;
                break;

            case BULLET_TYPE.HOMING:
                break;
        }
    }

    private void CreateParticle(Vector3 position)
    {
        var newParticle = particlePool.GetObject(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            Debug.Log("hit!!");
            SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_DAMAGE);
            hp -= other.gameObject.GetComponent<Bullet>().damege;
            
            if(hp <= 0)
            {
                //Destroy(gameObject);
                //particle.Play();
                if(isItem)
                {
                    switch(item_state)
                    {
                        case ITEM.RECOVER:
                            Instantiate(recoverItem, gameObject.transform.position, Quaternion.identity);
                            break;

                        case ITEM.POWERUP:
                            Instantiate(powUpItem, gameObject.transform.position, Quaternion.identity);
                            break;

                        case ITEM.ONEUP:
                            Instantiate(oneUpItem, gameObject.transform.position, Quaternion.identity);
                            break;
                    }
                }
                scrScoreMgr.addScore(score);
                CreateParticle(gameObject.transform.position);
                isActive = false;
            }

        }
    }
}
