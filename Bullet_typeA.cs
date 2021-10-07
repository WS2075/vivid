using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_typeA : MonoBehaviour
{
    public enum POW_STATE
    {
        STATE_1,
        STATE_2,
        STATE_3,
        STATE_MAX
    };

    public float speed;
    public float cooltime;
    public POW_STATE pow;
    //public int bulletLevel;

    // Start is called before the first frame update
    void Start()
    {
        //bulletLevel = 1;
        pow = POW_STATE.STATE_1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;

        if(transform.position.x > 960 || transform.position.x < -960)
        {
            Destroy(gameObject);
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
        if(pow == POW_STATE.STATE_MAX)
        {
            pow = POW_STATE.STATE_3;
        }
    }
    //public int GetBulletLevel()
    //{
    //    Debug.Log(bulletLevel);
    //    return bulletLevel;
    //}
}
