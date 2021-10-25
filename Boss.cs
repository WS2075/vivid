using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Status")]
    public int MaxHP;
    private int NowHP;

    //[Header("Bullet")]
    ////private GameObject BulletManager;
    ////private BossBulletManager scrBulletMgr;
    //[SerializeField]
    //private int ObjCount;
    //[SerializeField]
    //private float Radius;
    //[SerializeField]
    //public float Cooltime;


    // Start is called before the first frame update
    void Start()
    {
        NowHP = MaxHP;

        //scrBulletMgr = BulletManager.GetComponent<BossBulletManager>();
        //scrBulletMgr.SetParameter(ObjCount, Radius, Cooltime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetNowHP()
    {
        return NowHP;
    }

    public void Damage(int value)
    {
        NowHP -= value;
    }
}
