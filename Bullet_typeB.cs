using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_typeB : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        pow = POW_STATE.STATE_1;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = transform.eulerAngles.z * (Mathf.PI / 180.0f);
        Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);

        transform.position -= dir * speed * Time.deltaTime;
        //transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;

        if (transform.position.x > 960 || transform.position.x < -960)
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
        if (pow == POW_STATE.STATE_MAX)
        {
            pow = POW_STATE.STATE_3;
        }
    }
}
