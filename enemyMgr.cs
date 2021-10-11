using System.Collections;
using System.Collections.Generic;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class enemyMgr : MonoBehaviour
{
    public GameObject obj;
    public GameObject enemyPrefab;
    private enemy scrEnemy;
    private GameObject group;
    private float delay;

    private float width;
    private float width_space;

    private float height;
    private float height_space;

    private int cnt;

    // Start is called before the first frame update
    void Start()
    {
        scrEnemy = enemyPrefab.GetComponent<enemy>();

        width = (float)Screen.width / 2;
        width_space = (float)Screen.width / 12;
        height = (float)Screen.height / 2;
        height_space = (float)Screen.height / 7;

        for(int x = 0; x < 13; x++)
        {
            for(int y = 0; y < 8; y++)
            {
                var parent = this.transform;
                Instantiate(obj, new Vector3(width - (x * width_space), height - (y * height_space), 0.0f), Quaternion.identity, parent);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(scrEnemy.GetState());
        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            group = new GameObject("group");
            cnt = 0;
            SpawnEnemy(5, 7, 48);
        }

        //if(scrEnemy.GetState() == enemy.STATE.FORMATION)
        //{
        //    int i = 0;
        //    foreach(var child in group.transform)
        //    {
        //        scrEnemy = group.transform.GetChild(i).GetComponent<enemy>();
        //        scrEnemy.SetStartEnd(transform.GetChild(7), transform.GetChild(50 - i));
        //        i++;
        //    }
        //}
    }

    void SpawnEnemy(int enemy_num, int start_num, int end_num)
    {
        Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity, group.transform);

        scrEnemy = group.transform.GetChild(cnt).GetComponent<enemy>();
        scrEnemy.SetInvasion();
        scrEnemy.SetStartEnd(transform.GetChild(start_num), transform.GetChild(end_num));
        cnt++;

        if(cnt < enemy_num )
        {
            Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ => SpawnEnemy(enemy_num - 1,start_num,end_num));
        }
        
    }

    public void NextPoint()
    {
        scrEnemy.SetStartEnd(transform.GetChild(48), transform.GetChild(22));
    }
}
 