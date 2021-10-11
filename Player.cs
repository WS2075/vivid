using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public enum BULLET_TYPE
    {
        TYPE_A,
        TYPE_B,
        TYPE_C,
        TYPE_MAX
    };

    public int hp;
    public float speed;
    private float cooltime;
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
    }

    // Update is called once per frame
    void Update()
    {
        cooltime -= Time.deltaTime;


        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime * timeMgr.GetGameSpeed();
        }
        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime * timeMgr.GetGameSpeed();
        }
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime * timeMgr.GetGameSpeed();
        }
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime * timeMgr.GetGameSpeed();
        }


        if (timeMgr.GetGameSpeed() > 0.1f)
        {

            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                type++;
                if (type == BULLET_TYPE.TYPE_MAX)
                {
                    type = BULLET_TYPE.TYPE_A;
                }
            }


            switch (type)
            {
                case BULLET_TYPE.TYPE_A:
                    ScrBullet = bulletPrefab[(int)BULLET_TYPE.TYPE_A].GetComponent<Bullet>();
                    if (Keyboard.current.jKey.isPressed || Keyboard.current.kKey.isPressed || Keyboard.current.lKey.isPressed)
                    {
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
                    }
                    break;

                case BULLET_TYPE.TYPE_B:
                    ScrBullet = bulletPrefab[(int)BULLET_TYPE.TYPE_B].GetComponent<Bullet>();
                    if (Keyboard.current.jKey.isPressed || Keyboard.current.kKey.isPressed || Keyboard.current.lKey.isPressed)
                    {
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

                    }
                    break;

                case BULLET_TYPE.TYPE_C:
                    ScrBullet = bulletPrefab[(int)BULLET_TYPE.TYPE_C].GetComponent<Bullet>();
                    if (Keyboard.current.jKey.isPressed || Keyboard.current.kKey.isPressed || Keyboard.current.lKey.isPressed)
                    {
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

                                //ˆÐ—ÍUP
                                break;
                        }
                    }
                    break;
            }

        }

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

    //void FixedUpdate()
    //{

    //    if (Keyboard.current.rightArrowKey.isPressed)
    //    {
    //        rd.velocity = new Vector2(speed, rd.velocity.y);
    //    }
    //    else if (Keyboard.current.leftArrowKey.isPressed)
    //    {
    //        rd.velocity = new Vector2(-speed, rd.velocity.y);
    //    }

    //    else
    //    {
    //        rd.velocity = Vector2.zero;
    //    }
    //}
}
