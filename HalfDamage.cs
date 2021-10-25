using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDamage : MonoBehaviour
{
    [SerializeField]
    private GameObject BossObj;
    private Boss scrBoss;

    // Start is called before the first frame update
    void Start()
    {
        scrBoss = BossObj.GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("bullet"))
        {
            scrBoss.Damage(other.gameObject.GetComponent<Bullet>().damege / 2);
        }
    }
}
