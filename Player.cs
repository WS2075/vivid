using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using QFSW.MOP2;

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
    private bool bulletC_active = false;
    private bool isFire = false;
    private bool isBlink = false;
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

    //[SerializeField]
    //private Transform BulletCase;

    //ObjectPool
    [SerializeField]
    private ObjectPool BulletPoolA;
    [SerializeField]
    private ObjectPool BulletPoolB;
    [SerializeField]
    private ObjectPool BulletPoolC;

    private Animator anime;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();

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

        if(move.y > 0.0f)
        {
            anime.SetBool("Up", true);
            Debug.Log(move.y);
        }
        else if(move.y < 0.0f)
        {
            anime.SetBool("Down", true);
        }
        else
        {
            anime.SetBool("Up", false);
            anime.SetBool("Down", false);
        }
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

        if(!bulletC_active)
        {
            if(type == BULLET_TYPE.TYPE_C)
            {
                type++;
            }
        }

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
                                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_SHOOT);
                                    for (int i = 0; i <= (int)ScrBullet.GetPowState() + 1; i++)//ScrBulletA.GetPowState() + 1
                                    {
                                        //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 25.0f - i * 50.0f, 0.0f), rotZ_180, BulletCase);
                                        CreateBullet(barrelforward.transform.position + new Vector3(0.0f, 25.0f - i * 50.0f, 0.0f), rotZ_180);
                                    }
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;

                            case Bullet.POW_STATE.STATE_2:
                                if (cooltime <= 0.0f)
                                {
                                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_SHOOT);
                                    for (int i = 0; i <= (int)ScrBullet.GetPowState() + 1; i++)
                                    {
                                        //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f + ((i % 2) * 50.0f), 50.0f - i * 50.0f, 0.0f), rotZ_180, BulletCase);
                                        CreateBullet(barrelforward.transform.position + new Vector3(0.0f + ((i % 2) * 50.0f), 50.0f - i * 50.0f, 0.0f), rotZ_180);
                                    }
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;

                            case Bullet.POW_STATE.STATE_3:
                                if (cooltime <= 0.0f)
                                {
                                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_SHOOT);
                                    for (int i = 0; i < (int)ScrBullet.GetPowState(); i++)
                                    {
                                        //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(50.0f, 25.0f - (i * 50.0f), 0.0f), rotZ_180, BulletCase);
                                        //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 75.0f - (i * 150.0f), 0.0f), rotZ_180, BulletCase);
                                        CreateBullet(barrelforward.transform.position + new Vector3(50.0f, 25.0f - (i * 50.0f), 0.0f), rotZ_180);
                                        CreateBullet(barrelforward.transform.position + new Vector3(0.0f, 75.0f - (i * 150.0f), 0.0f), rotZ_180);
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
                                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_SHOOT);
                                    //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, rotZ_180, BulletCase);
                                    //Instantiate(bulletPrefab[(int)type], barrelback.transform.position, Quaternion.identity, BulletCase);
                                    CreateBullet(barrelforward.transform.position, rotZ_180);
                                    CreateBullet(barrelback.transform.position, Quaternion.identity);
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;

                            case Bullet.POW_STATE.STATE_2:
                                if (cooltime <= 0.0f)
                                {
                                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_SHOOT);
                                    //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, rotZ_180, BulletCase);
                                    CreateBullet(barrelforward.transform.position, rotZ_180);
                                    for (int i = 0; i < (int)ScrBullet.GetPowState() + 1; i++)
                                    {
                                        //Instantiate(bulletPrefab[(int)type], barrelback.transform.position + new Vector3(0.0f, 25.0f - i * 50.0f, 0.0f), Quaternion.identity, BulletCase);
                                        CreateBullet(barrelback.transform.position + new Vector3(0.0f, 25.0f - i * 50.0f, 0.0f), Quaternion.identity);
                                    }
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;

                            case Bullet.POW_STATE.STATE_3:
                                if (cooltime <= 0.0f)
                                {
                                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_SHOOT);
                                    //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, rotZ_180, BulletCase);
                                    CreateBullet(barrelforward.transform.position, rotZ_180);
                                    for (int i = 0; i < (int)ScrBullet.GetPowState() + 1; i++)
                                    {
                                        //Instantiate(bulletPrefab[(int)type], barrelback.transform.position + new Vector3(0.0f - ((i % 2) * 50.0f), 50.0f - i * 50.0f, 0.0f), Quaternion.identity, BulletCase);
                                        CreateBullet(barrelback.transform.position + new Vector3(0.0f - ((i % 2) * 50.0f), 50.0f - i * 50.0f, 0.0f), Quaternion.identity);
                                    }
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;
                        }
                        break;

                    case BULLET_TYPE.TYPE_C:
                        Quaternion angle1, angle2;
                        angle1 = Quaternion.AngleAxis(210.0f, new Vector3(0.0f, 0.0f, 1.0f));
                        angle2 = Quaternion.AngleAxis(150.0f, new Vector3(0.0f, 0.0f, 1.0f));
                        switch (ScrBullet.GetPowState())
                        {
                            case Bullet.POW_STATE.STATE_1:
                                if (cooltime <= 0.0f)
                                {
                                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_SHOOT);
                                    //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 50.0f, 0.0f), angle1, BulletCase);
                                    //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, -50.0f, 0.0f), angle2, BulletCase);
                                    CreateBullet(barrelforward.transform.position + new Vector3(0.0f, 50.0f, 0.0f), angle1);
                                    CreateBullet(barrelforward.transform.position + new Vector3(0.0f, -50.0f, 0.0f), angle2);
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;

                            case Bullet.POW_STATE.STATE_2:
                                if (cooltime <= 0.0f)
                                {
                                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_SHOOT);
                                    //angle3 = Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
                                    //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 50.0f, 0.0f), angle1, BulletCase);
                                    //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, angle3, BulletCase);
                                    //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, -50.0f, 0.0f), angle2, BulletCase);
                                    CreateBullet(barrelforward.transform.position + new Vector3(0.0f, 50.0f, 0.0f), angle1);
                                    CreateBullet(barrelforward.transform.position, rotZ_180);
                                    CreateBullet(barrelforward.transform.position + new Vector3(0.0f, -50.0f, 0.0f), angle2);
                                    cooltime = ScrBullet.GetCooltime();
                                }
                                break;

                            case Bullet.POW_STATE.STATE_3:
                                if (cooltime <= 0.0f)
                                {
                                    SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_SHOOT);
                                    //angle3 = Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
                                    //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, 50.0f, 0.0f), angle1, BulletCase);
                                    //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position, angle3, BulletCase);
                                    //Instantiate(bulletPrefab[(int)type], barrelforward.transform.position + new Vector3(0.0f, -50.0f, 0.0f), angle2, BulletCase);
                                    CreateBullet(barrelforward.transform.position + new Vector3(0.0f, 50.0f, 0.0f), angle1);
                                    CreateBullet(barrelforward.transform.position, rotZ_180);
                                    CreateBullet(barrelforward.transform.position + new Vector3(0.0f, -50.0f, 0.0f), angle2);
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

    public void ReSpawn()
    {
        anime.SetBool("Respawn", true);
        now_hp = max_hp;
        remaining = remaining - 1;

        Invoke("SetBlink", 2);
    }

    private void SetBlink()
    {
        isBlink = false;
        anime.SetBool("Respawn", false);
    }

    private void CreateBullet(Vector3 position, Quaternion angle)
    {
        switch(type)
        {
            case BULLET_TYPE.TYPE_A:
                var newBulletA = BulletPoolA.GetObject(position);
                newBulletA.transform.rotation = angle;
                break;
            case BULLET_TYPE.TYPE_B:
                var newBulletB = BulletPoolB.GetObject(position);
                newBulletB.transform.rotation = angle;
                break;
            case BULLET_TYPE.TYPE_C:
                var newBulletC = BulletPoolC.GetObject(position);
                newBulletC.transform.rotation = angle;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //item
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

        if(!isBlink)
        {
            //enemy_bullet
            if (other.gameObject.CompareTag("enemy_bullet"))
            {
                SoundManager.instance.PlaySE(SoundManager.SE_TYPE.SE_DAMAGE);
                Destroy(other.gameObject);
                now_hp -= other.gameObject.GetComponent<enemy_bullet>().damage;

                if (now_hp <= 0)
                {
                    isBlink = true;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
