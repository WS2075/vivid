using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    private Slider slider;
    private Boss scrBoss;

    private int MaxHP;
    private int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        scrBoss = GetComponent<Boss>();

        MaxHP = scrBoss.MaxHP;
        currentHP = scrBoss.GetNowHP();

        slider = GameObject.FindWithTag("boss_hp").GetComponent<Slider>();

        slider.maxValue = MaxHP;
        slider.value = currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = scrBoss.GetNowHP();
        slider.value = currentHP;
    }
}
