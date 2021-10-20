using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// A behaviour that is attached to a playable
[Serializable]
public class enemyPlayableBehaviour : PlayableBehaviour
{
    public GameObject charaObj;
    public int StartPosNum;
    public int EndPosNum;
    public int FormationPosNum;
    public enemy.MOVE_TYPE move_type;
    private enemy ScrEnemy;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (playerData != null)
        {
            if (charaObj == null)
            {
                charaObj = playerData as GameObject;
                Debug.Log(charaObj);
            }
        }
    }

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
        if (charaObj != null)
        {
            ScrEnemy = charaObj.GetComponent<enemy>();
            ScrEnemy.startPosNum = StartPosNum;
            ScrEnemy.endPosNum = EndPosNum;
            ScrEnemy.formationPosNum = FormationPosNum;
            ScrEnemy.move_type = move_type;
            Debug.Log(EndPosNum);
            Debug.Log(move_type);
        }
    }
}
