using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerInfo : MonoBehaviour
{
    private GameObject player;
    private Stats stats;
    private float hp = 0;
    private float maxhp = 0;
    private float exp = 0;
    private float maxExp = 0;
    private float lv = 0;
    private Slider hpSlider;
    private Slider expSlider;
    public Text hpText;
    public Text expText;
    public Text missionText;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<Stats>();
        hpSlider = transform.GetComponentsInChildren<Slider>()[0];
        expSlider = transform.GetComponentsInChildren<Slider>()[1];
    }

    void FixedUpdate()
    {
        bool statsChange = false;

        if (hp != stats.heath || maxhp != stats.maxHeath || exp != stats.exp || lv != stats.lv || maxExp != stats.maxExp)
        {
            statsChange = true;
        }

        if (statsChange == true)
        {
            hp = stats.heath;
            maxhp = stats.maxHeath;
            exp = stats.exp;
            maxExp = stats.maxExp;
            lv = stats.lv;

            hpSlider.value = hp / maxhp;
            expSlider.value = exp / maxExp;
            hpText.text = $"HEATH | {hp} / {maxhp}";
            expText.text = $"LEVEL {lv} | {exp} / {maxExp}";
        }
    }

    public void UpdateMissionText(string text) {
        missionText.text = text;
    }
}
