using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SEManager : MonoBehaviour
{
    [SerializeField] private AudioSource audio;

    [SerializeField] private AudioClip Sound1;
    [SerializeField] private AudioClip Sound2;

    private bool isPlay;

    // Start is called before the first frame update
    void Start()
    {
        //audio.GetComponent<AudioSource>();
        isPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SE1()
    {
        audio.PlayOneShot(Sound1);
    }

    public void SE2()
    {
        audio.PlayOneShot(Sound2);
    }

    public bool SECheck()
    {
        StartCoroutine(Checking(audio, () => { isPlay = true; }));
        return isPlay;
    }

    private IEnumerator Checking(AudioSource source,UnityAction callback)
    {
        while(true)
        {
            yield return new WaitForFixedUpdate();
            if(!audio.isPlaying)
            {
                callback();
                break;
            }
        }
    }
}
