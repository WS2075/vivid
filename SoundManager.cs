using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //BGM
    public enum BGM_TYPE
    {
        BGM_TITLE,
        BGM_GAME,
        BGM_BOSS,
        BGM_SCORERANK,
        BGM_CREDIT,
        BGM_OVER,
        BGM_CLEAR,
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
        SE_BREAK,
        SE_MAX
    }

    //CrossFade
    public const float CrossFadeTime = 1.0f;
    private bool isCrossFade;

    //volume(1.0 ~ 0.0)
    public float BGM_Volume;
    public float SE_Volume;
    public bool Mute = false;

    //AudioClip
    public AudioClip[] BGM_Clips;
    public AudioClip[] SE_Clips;

    //AudioSource
    private AudioSource[] BGM_Sources = new AudioSource[2];
    private AudioSource[] SE_Sources = new AudioSource[(int)SE_TYPE.SE_MAX];

    private int currentBgmIndex;

    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        //BGM Add AudioSource
        BGM_Sources[0] = gameObject.AddComponent<AudioSource>();
        BGM_Sources[1] = gameObject.AddComponent<AudioSource>();

        //SE Add AudioSource
        for (int i = 0; i < SE_Sources.Length; i++)
        {
            SE_Sources[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCrossFade)
        {
            //BGM Volume
            BGM_Sources[0].volume = BGM_Volume;
            BGM_Sources[1].volume = BGM_Volume;

            //SE Volume
            for (int i = 0; i < SE_Sources.Length; i++)
            {
                SE_Sources[i].volume = SE_Volume;
            }
        }
    }

    //PlayBGM
    public void PlayBGM(BGM_TYPE bgm_type, bool loopFlg = true)
    {
        int index = (int)bgm_type;
        currentBgmIndex = index;

        if(BGM_Sources[0].clip != null && BGM_Sources[0].clip == BGM_Clips[index])
        {
            return;
        }
        else if(BGM_Sources[1].clip != null && BGM_Sources[1].clip == BGM_Clips[index])
        {
            return;
        }

        if(BGM_Sources[0].clip == null && BGM_Sources[1].clip == null)
        {
            BGM_Sources[0].loop = loopFlg;
            BGM_Sources[0].clip = BGM_Clips[index];
            BGM_Sources[0].Play();
        }
        else
        {
            //CrossFade Play
            StartCoroutine(CrossFadeChangeBGM(index, loopFlg));
        }
    }

    private IEnumerator CrossFadeChangeBGM(int index, bool loopFlg)
    {
        isCrossFade = true;

        if(BGM_Sources[0].clip != null)
        {
            BGM_Sources[1].volume = 0;
            BGM_Sources[1].clip = BGM_Clips[index];
            BGM_Sources[1].loop = loopFlg;
            BGM_Sources[1].Play();

            BGM_Sources[0].DOFade(0, CrossFadeTime).SetEase(Ease.Linear);
            yield return new WaitForSeconds(CrossFadeTime);
            BGM_Sources[0].Stop();
            BGM_Sources[0].clip = null;
        }
        else
        {
            BGM_Sources[0].volume = 0;
            BGM_Sources[0].clip = BGM_Clips[index];
            BGM_Sources[0].loop = loopFlg;
            BGM_Sources[0].Play();

            BGM_Sources[1].DOFade(0, CrossFadeTime).SetEase(Ease.Linear);
            yield return new WaitForSeconds(CrossFadeTime);
            BGM_Sources[1].Stop();
            BGM_Sources[1].clip = null;
        }

        isCrossFade = false;
    }


    //All BGM Stop
    public void StopBGM()
    {
        BGM_Sources[0].Stop();
        BGM_Sources[1].Stop();

        BGM_Sources[0].clip = null;
        BGM_Sources[1].clip = null;
    }

    //BGM Stop
    public void MuteBGM()
    {
        BGM_Sources[0].Stop();
        BGM_Sources[1].Stop();
    }

    //BGM Resume
    public void ResumeBGM()
    {
        BGM_Sources[0].Play();
        BGM_Sources[1].Play();
    }


    //Play SE
    public void PlaySE(SE_TYPE seType)
    {
        int SE_index = (int)seType;
        if(SE_index < 0 || SE_Clips.Length <= SE_index)
        {
            return;
        }

        foreach(AudioSource source in SE_Sources)
        {
            if(source.isPlaying == false)
            {
                source.clip = SE_Clips[SE_index];
                source.Play();
                return;
            }
        }
    }

    //Stop SE
    public void StopSE()
    {
        foreach (AudioSource source in SE_Sources)
        {
            source.Stop();
            source.clip = null;
        }
    }
}
