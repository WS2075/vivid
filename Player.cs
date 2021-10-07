using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private enum BULLET_TYPE
    {
        TYPE_A,
        TYPE_B,
        TYPE_C,
        TYPE_MAX
    };

    public int hp;
    public float speed;
    private float cooltime;
    private Rigidbody2D rd;

    public GameObject[] bulletPrefab;
    private BULLET_TYPE type = BULLET_TYPE.TYPE_A;
    private GameObject barrelforward;
    private GameObject barrelback;

    private Bullet_typeA ScrBulletA;
    private Bullet_typeB ScrBulletB;
    private Bullet_typeC ScrBulletC;


    // Start is called before the first frame update
    void Start()
    {
        ScrBulletA = bulletPrefab[(int)BULLET_TYPE.TYPE_A].GetComponent<Bullet_typeA>();
        ScrBulletB = bulletPrefab[(int)BULLET_TYPE.TYPE_B].GetComponent<Bullet_typeB>();
        ScrBulletC = bulletPrefab[(int)BULLET_TYPE.TYPE_C].GetComponent<Bullet_typeC>();

        ScrBulletA.pow = Bullet_typeA.POW_STATE.STATE_1;
        ScrBulletB.pow = Bullet_typeB.POW_STATE.STATE_1;
        ScrBulletC.pow = Bullet_typeC.POW_STATE.STATE_1;

        cooltime = ScrBulletA.GetCooltime();
        rd = GetComponent<Rigidbody2D>();

        barrelforward = transform.Find("barrel_forward").gameObject;
        barrelback = transform.Find("barrel_back").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        cooltime -= 1.0f;


        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
        }
        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
        }
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }


        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            type = BULLET_TYPE.TYPE_A;
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            type = BULLET_TYPE.TYPE_B;
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            type = BULLET_TYPE.TYPE_C;
        }


        switch (type)
        {
            case BULLET_TYPE.TYPE_A:
                if (Keyboard.current.spaceKey.isPressed)
                {
                    switch(ScrBulletA.GetPowState())
                    {
                        case Bullet_typeA.POW_STATE.STATE_1:
                            Debug.Log(ScrBulletA.GetPowState());
                            if (cooltime <= 0.0f)
                            {
                                for(int i = 0; i <= (int)ScrBulletA.GetPowState() + 1;i++)
                                {
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 25.0f - i * 50.0f, 0.0f), Quaternion.identity);
                                }
                                //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 25.0f, 0.0f), Quaternion.identity);
                                //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, -25.0f, 0.0f), Quaternion.identity);
                                cooltime = ScrBulletA.GetCooltime();
                            }
                            break;

                        case Bullet_typeA.POW_STATE.STATE_2:
                            if (cooltime <= 0.0f)
                            {
                                for (int i = 0; i <= (int)ScrBulletA.GetPowState() + 1; i++)
                                {
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f + ((i % 2) * 50.0f), 50.0f - i * 50.0f, 0.0f), Quaternion.identity);
                                }
                                cooltime = ScrBulletA.GetCooltime();
                            }
                            break;

                        case Bullet_typeA.POW_STATE.STATE_3:
                            if (cooltime <= 0.0f)
                            {
                                for (int i = 0; i < (int)ScrBulletA.GetPowState(); i++)
                                {
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(50.0f, 25.0f - (i * 50.0f), 0.0f), Quaternion.identity);
                                    Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 75.0f - (i * 150.0f), 0.0f), Quaternion.identity);
                                }
                                cooltime = ScrBulletA.GetCooltime();
                            }
                            break;
                    }
                }
                break;

            case BULLET_TYPE.TYPE_B:
                if (Keyboard.current.spaceKey.isPressed)
                {
                    switch (ScrBulletB.GetPowState())
                    {
                        case Bullet_typeB.POW_STATE.STATE_1:
                            if (cooltime <= 0.0f)
                            {
                                Quaternion rot = Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
                                Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, rot);
                                Instantiate(bulletPrefab[(int)type], barrelback.transform.position, Quaternion.identity);
                                cooltime = ScrBulletB.GetCooltime();
                            }
                            break;

                        case Bullet_typeB.POW_STATE.STATE_2:
                            if (cooltime <= 0.0f)
                            {
                                Quaternion rot = Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
                                Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, rot);
                                for(int i = 0; i < (int)ScrBulletB.GetPowState() + 1; i++)
                                {
                                    Instantiate(bulletPrefab[(int)type], barrelback.transform.position + new Vector3(0.0f, 25.0f - i * 50.0f, 0.0f), Quaternion.identity);
                                }
                                cooltime = ScrBulletB.GetCooltime();
                            }
                            break;

                        case Bullet_typeB.POW_STATE.STATE_3:
                            if (cooltime <= 0.0f)
                            {
                                Quaternion rot = Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
                                Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, rot);
                                for (int i = 0; i < (int)ScrBulletB.GetPowState() + 1; i++)
                                {
                                    Instantiate(bulletPrefab[(int)type], barrelback.transform.position + new Vector3(0.0f - ((i % 2) * 50.0f), 50.0f - i * 50.0f, 0.0f), Quaternion.identity);
                                }
                                cooltime = ScrBulletB.GetCooltime();
                            }
                            break;
                    }
                    
                }
                break;

            case BULLET_TYPE.TYPE_C:
                if (Keyboard.current.spaceKey.isPressed)
                {
                    Quaternion angle1, angle2, angle3;
                    angle1 = Quaternion.AngleAxis(210.0f, new Vector3(0.0f, 0.0f, 1.0f));
                    angle2 = Quaternion.AngleAxis(150.0f, new Vector3(0.0f, 0.0f, 1.0f));
                    switch (ScrBulletC.GetPowState())
                    {
                        case Bullet_typeC.POW_STATE.STATE_1:
                            if (cooltime <= 0.0f)
                            {
                                Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 50.0f, 0.0f), angle1);
                                Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, -50.0f, 0.0f), angle2);
                                cooltime = ScrBulletC.GetCooltime();
                            }
                            break;

                        case Bullet_typeC.POW_STATE.STATE_2:
                            if (cooltime <= 0.0f)
                            {
                                angle3 = Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
                                Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 50.0f, 0.0f), angle1);
                                Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, angle3);
                                Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, -50.0f, 0.0f), angle2);
                                cooltime = ScrBulletC.GetCooltime();
                            }
                            break;

                        case Bullet_typeC.POW_STATE.STATE_3:
                            if (cooltime <= 0.0f)
                            {
                                angle3 = Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
                                Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 50.0f, 0.0f), angle1);
                                Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, angle3);
                                Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, -50.0f, 0.0f), angle2);
                                cooltime = ScrBulletC.GetCooltime();
                            }

                            //ˆÐ—ÍUP
                            break;
                    }
                }
                break;
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
                    ScrBulletA.LevelUp();
                    //ScrBulletA.pow++;
                    break;

                case BULLET_TYPE.TYPE_B:
                    ScrBulletB.LevelUp();
                    break;

                case BULLET_TYPE.TYPE_C:
                    ScrBulletC.LevelUp();
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
