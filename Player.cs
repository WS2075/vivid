using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    public enum BULLET_TYPE
    {
        TYPE_A,
        TYPE_B,
        TYPE_C,
        TYPE_MAX
    };

    public int remaining;
    public int max_hp;
    private int now_hp;
    public float speed;
    private Vector3 move;
    private float powValue;
    private float cooltime;
    private bool isFire = false;
    Quaternion rotZ_180;
    private Rigidbody2D rd;

    public GameObject[] bulletPrefab;

    [System.NonSerialized]
    public BULLET_TYPE type = BULLET_TYPE.TYPE_A;
    private GameObject barrelforward;
    private GameObject barrelback;

    [SerializeField]
    private GameObject Mgr;
    private TimeMgr timeMgr;

    private Bullet ScrBullet;


    // Start is called before the first frame update
    void Start()
    {
        timeMgr = Mgr.GetComponent<TimeMgr>();

        ScrBullet = bulletPrefab[(int)BULLET_TYPE.TYPE_A].GetComponent<Bullet>();

        bulletPrefab[(int)BULLET_TYPE.TYPE_A].GetComponent<Bullet>().pow = Bullet.POW_STATE.STATE_1;
        bulletPrefab[(int)BULLET_TYPE.TYPE_B].GetComponent<Bullet>().pow = Bullet.POW_STATE.STATE_1;
        bulletPrefab[(int)BULLET_TYPE.TYPE_C].GetComponent<Bullet>().pow = Bullet.POW_STATE.STATE_1;

        cooltime = ScrBullet.GetCooltime();
        rotZ_180 = Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));

        //cooltime = ScrBulletA.GetCooltime();
        rd = GetComponent<Rigidbody2D>();

        barrelforward = transform.Find("barrel_forward").gameObject;
        barrelback = transform.Find("barrel_back").gameObject;

        move = new Vector3(0.0f,0.0f,0.0f);

        now_hp = max_hp;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isFire = true;
        }
        
        if(context.phase == InputActionPhase.Canceled)
        {
            isFire = false;
        }
    }

    public void OnBulletChange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            type++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cooltime -= Time.deltaTime;

        transform.Translate(move * speed * Time.deltaTime * timeMgr.GetGameSpeed());

        if (type == BULLET_TYPE.TYPE_MAX)
        {
            type = BULLET_TYPE.TYPE_A;
        }

        switch(type)
        {
            case BULLET_TYPE.TYPE_A:
                ScrBullet = bulletPrefab[(int)BULLET_TYPE.TYPE_A].GetComponent<Bullet>();
                break;
            case BULLET_TYPE.TYPE_B:
                ScrBullet = bulletPrefab[(int)BULLET_TYPE.TYPE_B].GetComponent<Bullet>();
                break;
            case BULLET_TYPE.TYPE_C:
                ScrBullet = bulletPrefab[(int)BULLET_TYPE.TYPE_C].GetComponent<Bullet>();
                break;
        }

        if (isFire)
        {
            if (timeMgr.GetGameSpeed() > 0.1f)
            {
                switch (type)
                {
                    case BULLET_TYPE.TYPE_A:
                        switch (ScrBullet.GetPowState())
                        {
                            case Bullet.POW_STATE.STATE_1:
                                //Debug.Log(ScrBulletA.GetPowState());
                                if (cooltime <= 0.0f)
                                {
                                    for (int i = 0; i <= (int)ScrBullet.GetPowState() + 1; i++)//ScrBulletA.GetPowState() + 1
                                    {
                                        Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 25.0f - i * 50.0f, 0.0f), rotZ_180);
                                    }
                                    //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 25.0f, 0.0f), Quaternion.identity);
                                    //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, -25.0f, 0.0f), Quaternion.identity);
                                    cooltime = ScrBullet.GetCooltime();//ScrBulletA.GetCooltime();
                                }
                                break;

                            case Bullet.POW_STATE.STATE_2:
                                if (cooltime <= 0.0f)
                                {
                                    for (int i = 0; i <= (int)ScrBullet.GetPowState() + 1; i++)
                                    {
                                        Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f + ((i % 2) * 50.0f), 50.0f - i * 50.0f, 0.0f), rotZ_180);
                                    }
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;

                            case Bullet.POW_STATE.STATE_3:
                                if (cooltime <= 0.0f)
                                {
                                    for (int i = 0; i < (int)ScrBullet.GetPowState(); i++)
                                    {
                                        Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(50.0f, 25.0f - (i * 50.0f), 0.0f), rotZ_180);
                                        Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 75.0f - (i * 150.0f), 0.0f), rotZ_180);
                                    }
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;
                        }
                        break;

                    case BULLET_TYPE.TYPE_B:
                        switch (ScrBullet.GetPowState())
                        {
                            case Bullet.POW_STATE.STATE_1:
                                if (cooltime <= 0.0f)
                                {
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, rotZ_180);
                                    Instantiate(bulletPrefab[(int)type], barrelback.transform.position, Quaternion.identity);
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;

                            case Bullet.POW_STATE.STATE_2:
                                if (cooltime <= 0.0f)
                                {
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, rotZ_180);
                                    for (int i = 0; i < (int)ScrBullet.GetPowState() + 1; i++)
                                    {
                                        Instantiate(bulletPrefab[(int)type], barrelback.transform.position + new Vector3(0.0f, 25.0f - i * 50.0f, 0.0f), Quaternion.identity);
                                    }
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;

                            case Bullet.POW_STATE.STATE_3:
                                if (cooltime <= 0.0f)
                                {
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, rotZ_180);
                                    for (int i = 0; i < (int)ScrBullet.GetPowState() + 1; i++)
                                    {
                                        Instantiate(bulletPrefab[(int)type], barrelback.transform.position + new Vector3(0.0f - ((i % 2) * 50.0f), 50.0f - i * 50.0f, 0.0f), Quaternion.identity);
                                    }
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;
                        }
                        break;

                    case BULLET_TYPE.TYPE_C:
                        Quaternion angle1, angle2, angle3;
                        angle1 = Quaternion.AngleAxis(210.0f, new Vector3(0.0f, 0.0f, 1.0f));
                        angle2 = Quaternion.AngleAxis(150.0f, new Vector3(0.0f, 0.0f, 1.0f));
                        switch (ScrBullet.GetPowState())
                        {
                            case Bullet.POW_STATE.STATE_1:
                                if (cooltime <= 0.0f)
                                {
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 50.0f, 0.0f), angle1);
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, -50.0f, 0.0f), angle2);
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;

                            case Bullet.POW_STATE.STATE_2:
                                if (cooltime <= 0.0f)
                                {
                                    angle3 = Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 50.0f, 0.0f), angle1);
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, angle3);
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, -50.0f, 0.0f), angle2);
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;

                            case Bullet.POW_STATE.STATE_3:
                                if (cooltime <= 0.0f)
                                {
                                    angle3 = Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 50.0f, 0.0f), angle1);
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, angle3);
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, -50.0f, 0.0f), angle2);
                                    cooltime = ScrBullet.GetCooltime();
                                }

                                //威力UP
                                break;
                        }
                        break;
                }

            }
        }
    }

    public int GetNowHP()
    {
        return now_hp;
    }

    public int GetRemaining()
    {
        return remaining;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("item"))
        {
            Destroy(other.gameObject);
            Debug.Log("Item Get!!");

            switch(type)
            {
                case BULLET_TYPE.TYPE_A:
                    ScrBullet.LevelUp();
                    break;

                case BULLET_TYPE.TYPE_B:
                    ScrBullet.LevelUp();
                    break;

                case BULLET_TYPE.TYPE_C:
                    ScrBullet.LevelUp();
                    break;
            }
        }
    }
}
