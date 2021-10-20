using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Slider slider;
    private Player scrPlayer;

    private int maxhp;
    private int currenthp;

    // Start is called before the first frame update
    void Start()
    {
        scrPlayer = GetComponent<Player>();

        maxhp = scrPlayer.max_hp;
        currenthp = scrPlayer.GetNowHP();

        slider.value = currenthp;
    }

    // Update is called once per frame
    void Update()
    {
        currenthp = scrPlayer.GetNowHP();
        slider.value = currenthp;
    }
}
