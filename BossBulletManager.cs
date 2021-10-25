using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BarrelObj;

    [SerializeField]
    private int ObjCount;
    [SerializeField]
    private float Radius;
    [SerializeField]
    private float CoolTime;

    private BossBarrel scrBossBarrel;
    private float repeat = 1.0f;
    private Quaternion angle;

    //1,2,3,4,1,3  1,2,3,4,1,3
    [SerializeField]
    private Transform[] MovePosition;
    private float distance;
    private float elapsedTime;
    public float move_speed;
    public int startpos;

    // Start is called before the first frame update
    void Start()
    {
        scrBossBarrel = BarrelObj.GetComponent<BossBarrel>();
        scrBossBarrel.CoolTime = CoolTime;

        float deg = 360.0f / ObjCount;

        //円状に配置
        var Cycle = 2.0f * Mathf.PI;

        for(int i = 0; i < ObjCount; i++)
        {
            var point = ((float)i / ObjCount) * Cycle;
            var repeatPoint = point * repeat;

            var x = Mathf.Cos(repeatPoint) * Radius;
            var y = Mathf.Sin(repeatPoint) * Radius;

            var position = new Vector3(x, y, 0.0f);

            angle = Quaternion.AngleAxis(deg * i - 180.0f, new Vector3(0.0f, 0.0f, 1.0f));

            Instantiate(BarrelObj, gameObject.transform.position + position, angle, transform);
        }

        distance = Vector3.Distance(MovePosition[startpos].localPosition, MovePosition[startpos + 1].localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, 30.0f * Time.deltaTime);

        elapsedTime += Time.deltaTime;
        float now_Location = (elapsedTime * move_speed) / distance;
        if(now_Location <= 1.0f)
        {
            if(startpos < MovePosition.Length - 1)
            {
                transform.localPosition = Vector3.Lerp(MovePosition[startpos].localPosition, MovePosition[startpos + 1].localPosition, now_Location);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(MovePosition[startpos].localPosition, MovePosition[0].localPosition, now_Location);
            }
        }
        else
        {
            if(startpos < MovePosition.Length - 1)
            {
                SetStartEnd(startpos + 1);
            }
            else
            {
                SetStartEnd(0);  
            }
        }
       
    }

    private void SetStartEnd(int startNum)
    {
        startpos = startNum;
        elapsedTime = 0.0f;
        if(startpos < MovePosition.Length - 1)
        {
            distance = Vector3.Distance(MovePosition[startpos].localPosition, MovePosition[startpos + 1].localPosition);
        }
        else
        {
            distance = Vector3.Distance(MovePosition[startpos].localPosition, MovePosition[0].localPosition);
        }
        
    }
}
