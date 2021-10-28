using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.MOP2;

public class Bullet : MonoBehaviour
{
    public enum POW_STATE
    {
        STATE_1,
        STATE_2,
        STATE_3,
        STATE_MAX
    };

    public int damege;
    public float speed;
    public float cooltime;
    public POW_STATE pow;

    private GameObject Mgr;
    private TimeMgr timeMgr;

    //ObjectPool
    [SerializeField]
    private ObjectPool BulletPool;

    // Start is called before the first frame update
    void Start()
    {
        pow = POW_STATE.STATE_1;

        Mgr = GameObject.Find("GameManager");
        timeMgr = Mgr.GetComponent<TimeMgr>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);

        transform.position -= dir * speed * Time.deltaTime * timeMgr.GetGameSpeed();
        //transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;

        if (transform.position.x > 960 || transform.position.x < -960 ||
            transform.position.y > 540 || transform.position.y < -540)
        {
            //Destroy(gameObject);
            BulletPool.Release(gameObject);
        }
    }

    public float GetCooltime()
    {
        return cooltime;
    }

    public POW_STATE GetPowState()
    {
        return pow;
    }

    public void LevelUp()
    {
        pow++;
        if (pow == POW_STATE.STATE_MAX)
        {
            pow = POW_STATE.STATE_3;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.activeInHierarchy)
        {
            if(other.gameObject.CompareTag("enemy"))
            {
                BulletPool.Release(gameObject);
            }
        }      
    }
}
