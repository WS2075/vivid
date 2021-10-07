using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletLogo : MonoBehaviour
{
    enum ACTIVE_STATE
    {
        ACTIVE,
        DEACTIVE,
        NONE,
        STATE_MAX
    }

    private ACTIVE_STATE state;

    [SerializeField]
    private Sprite[] Logo;

    void Update()
    {
        this.gameObject.GetComponent<Image>().sprite = Logo[(int)state];
    }

    void On()
    {
        state = ACTIVE_STATE.ACTIVE;
    }

    void Off()
    {
        state = ACTIVE_STATE.DEACTIVE;
    }

    void None()
    {
        state = ACTIVE_STATE.NONE;
    }
}
