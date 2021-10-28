using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
//using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
    //playable
    public GameObject playableObj;
    private PlayableDirector director;

    // Start is called before the first frame update
    void Start()
    {
        director = playableObj.GetComponent<PlayableDirector>();
        foreach (Transform childTransform in gameObject.transform)
        {
            if (childTransform.tag == "enemy")
            {
                foreach (var binding in director.playableAsset.outputs)
                {
                    if (binding.streamName == "Enemy Playable Track")
                    {
                        if (director.GetGenericBinding(binding.sourceObject) == null)
                        {
                            director.SetGenericBinding(binding.sourceObject, childTransform.gameObject);
                            break;
                        }
                    }
                }
            }

            if (childTransform.tag == "boss")
            {
                foreach (var binding in director.playableAsset.outputs)
                {
                    if (binding.streamName == "Loop Track")
                    {
                        if (director.GetGenericBinding(binding.sourceObject) == null)
                        {
                            director.SetGenericBinding(binding.sourceObject, childTransform.gameObject);
                            break;
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
