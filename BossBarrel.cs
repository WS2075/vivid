using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.MOP2;

public class BossBarrel : MonoBehaviour
{
    //public Transform BulletCase;

    [Header("Bullet Pool")]
    [SerializeField]
    private ObjectPool BulletPoolA;

    [SerializeField]
    private GameObject BulletPrefab;

    public float CoolTime;
    private float default_cooltime;

    // Start is called before the first frame update
    void Start()
    {
        default_cooltime = CoolTime;
    }

    // Update is called once per frame
    void Update()
    {
        CoolTime -= Time.deltaTime;

        if (CoolTime <= 0.0f)
        {
            //Instantiate(BulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
            CreateBullet(gameObject.transform.position, gameObject.transform.rotation);
            CoolTime = default_cooltime;
        }
    }

    private void CreateBullet(Vector3 position, Quaternion angle)
    {
        var newBulletA = BulletPoolA.GetObject(position);
        newBulletA.transform.rotation = angle;
    }
}
