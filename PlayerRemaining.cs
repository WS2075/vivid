using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerRemaining : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    private Player scrPlayer;
    private int remaining;
    private string str;
    // Start is called before the first frame update
    void Start()
    {
        scrPlayer = GetComponent<Player>();
        remaining = scrPlayer.GetRemaining();
        str = remaining.ToString("00");
        text.text = str;
    }

    // Update is called once per frame
    void Update()
    {
        if(remaining != scrPlayer.GetRemaining())
        {
            remaining = scrPlayer.GetRemaining();
            str = remaining.ToString("00");
            text.text = str;
        }
    }
}
