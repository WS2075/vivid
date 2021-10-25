using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class enemyPlayableAsset : PlayableAsset
{
    // Factory method that generates a playable based on this asset
    private enemyPlayableBehaviour behaviour = new enemyPlayableBehaviour();
    //動かすオブジェクトを設定
    public int StartPosNum;
    public int EndPosNum;
    public int FormationPosNum;
    public enemy.MOVE_TYPE move_type;
    public enemy.BULLET_TYPE bullet_type;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        behaviour.StartPosNum = StartPosNum;
        behaviour.EndPosNum = EndPosNum;
        behaviour.FormationPosNum = FormationPosNum;
        behaviour.move_type = move_type;
        behaviour.bullet_type = bullet_type;

        var playable = ScriptPlayable<enemyPlayableBehaviour>.Create(graph,behaviour);
        return playable;
    }
}
