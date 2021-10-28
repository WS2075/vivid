using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTimeline : MonoBehaviour
{
    private bool isTrigger;

    public bool trigger
    {
        get
        {
#if UNITY_EDITOR
            if(UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode == false)
            return true;
#endif
            return isTrigger;
        }

        set
        {
            isTrigger = value;
        }
    }
}
