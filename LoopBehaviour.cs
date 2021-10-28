using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
// A behaviour that is attached to a playable
public class LoopBehaviour : PlayableBehaviour
{
    public GameObject boss;

    public PlayableDirector director { get; set; }

    public WaitTimeline waitTimeline { get; set; }

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if(waitTimeline.trigger == true)
        {
            waitTimeline.trigger = false;
            return;
        }

        Debug.Log(waitTimeline.trigger);
        director.time -= playable.GetDuration();
    }

    // Called each frame while the state is set to Play
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (playerData != null)
        {
            if (boss == null)
            {
                boss = playerData as GameObject;
                Debug.Log(boss);
            }
        }

        if (boss.activeInHierarchy == true)
        {
            return;
        }

        waitTimeline.trigger = true;
    }
}
