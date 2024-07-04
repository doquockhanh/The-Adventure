using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    private Slider hpSlider;
    private Text hpText;

    void Awake()
    {
        hpSlider = transform.GetComponent<Slider>();
        hpText = transform.Find("hpTxt").GetComponent<Text>();
    }

    // Hàm để cập nhật thanh máu
    public void UpdateHealth(float hp, float maxHp)
    {
        if (hpSlider != null)
            hpSlider.value = hp / maxHp;
        if (hpText != null)
            hpText.text = $"{hp}";
    }
}
