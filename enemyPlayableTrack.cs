using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(enemyPlayableAsset))]
[TrackBindingType(typeof(GameObject))]
public class enemyPlayableTrack : TrackAsset
{
    protected override Playable CreatePlayable(PlayableGraph graph, GameObject go, TimelineClip clip)
    {
        Playable playable = base.CreatePlayable(graph, go, clip);

        return playable;
    }
}
