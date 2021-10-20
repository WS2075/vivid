using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    //BGM
    public enum BGM_TYPE
    {
        BGM_TITLE,
        BGM_GAME,
        BGM_BOSS,
        BGM_SCORERANK,
        BGM_CREDIT,
        BGM_OVER,
        BGM_CLEAR
    }

    //SE
    public enum SE_TYPE
    {
        SE_OK,
        SE_CURSOR,
        SE_SHOOT,
        SE_BULLET_SWITCH,
        SE_ITEM,
        SE_POWERUP,
        SE_RECOVERY,
        SE_ONEUP,
        SE_DAMAGE,
        SE_BREAK
    }

    //CrossFadeTime
    public const float CrossFadeTime = 1.0f;

    //volume(1.0 ~ 0.0)
    public float BGM_Volume;
    public float SE_Volume;
    public bool Mute = false;

    //AudioClip
    public AudioClip[] BGM_Clips;
    public AudioClip[] SE_Clips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
