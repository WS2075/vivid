using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    enum STATE
    {
        INVASION,
        FORMATION,
        BATTLE,
        WITHDRAW
    }

    public int hp;
    public float speed;
    private float cooltime;
    public GameObject barrel;
    public GameObject bullePrefab;
    private enemy_bullet scrEnemybullet;

    public Transform startMarker;
    public Transform endMarker;
    public float battletime;
    private float distance;
    private float elapsedTime;

    private STATE state;
    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(startMarker.position, endMarker.position);
        state = STATE.INVASION;

        scrEnemybullet = bullePrefab.GetComponent<enemy_bullet>();
        cooltime = scrEnemybullet.GetCooltime();
    }

    // Update is called once per frame
    void Update()
    {
        cooltime -= Time.deltaTime;

        Debug.Log(state);
        elapsedTime += Time.deltaTime;
        battletime -= Time.deltaTime;
        float now_Location = (elapsedTime * speed) / distance;

        transform.position = Vector3.Slerp(startMarker.position, endMarker.position, now_Location);

        if(battletime <= 0.0f)
        {
            state = STATE.WITHDRAW;
        }

        if(cooltime <= 0.0f)
        {
            Instantiate(bullePrefab, barrel.transform.position, Quaternion.identity);
            cooltime = scrEnemybullet.GetCooltime();
        }
        //if(now_Location >= 1.0f)
        //{
        //    SetStartEnd(endMarker, startMarker);
        //}
    }

    public void SetStartEnd(Transform start_pos, Transform end_pos)
    {
        startMarker = start_pos;
        endMarker = end_pos;

        elapsedTime = 0.0f;
        distance = Vector3.Distance(startMarker.position, endMarker.position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            Debug.Log("hit!!");
        }
    }
}
