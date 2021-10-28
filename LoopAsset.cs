using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class LoopAsset : PlayableAsset, ITimelineClipAsset
{

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var playable = ScriptPlayable<LoopBehaviour>.Create(graph);
        LoopBehaviour behaviour = playable.GetBehaviour();
        behaviour.director = go.GetComponent<PlayableDirector>();
        behaviour.waitTimeline = go.GetComponent<WaitTimeline>();
        return playable;
    }
}
