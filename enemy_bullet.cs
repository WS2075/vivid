using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet : MonoBehaviour
{

    public float speed;

    private GameObject Mgr;
    private TimeMgr timeMgr;
    // Start is called before the first frame update
    void Start()
    {
        Mgr = GameObject.Find("GameManager");
        timeMgr = Mgr.GetComponent<TimeMgr>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);

        transform.position -= dir * speed * Time.deltaTime * timeMgr.GetGameSpeed();

        if (transform.position.x > 960 || transform.position.x < -960 ||
            transform.position.y > 540 || transform.position.y < -540)
        {
            Destroy(gameObject);
        }
    }
}
