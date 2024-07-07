using UnityEngine;

public class Stats : MonoBehaviour
{
    public float heath = 0;
    public float maxHeath = 0;
    public float damage = 0;
    public float exp = 0;
    public float lv = 0;

    public float maxExp = 0;
    public float lvDmg = 0;
    public float lvHeath = 0;
    public float lvExp = 0;
    public event System.Action<Stats> OnLevelUp;
    public event System.Action<Stats> OnDeath;
    public event System.Action<Stats> OnTakeDamage;

    private HpBarController hpBarController;

    void Start()
    {
        hpBarController = transform.GetComponentInChildren<HpBarController>();
        UpdateHpBar();
    }

    public void TakeDamage(float damage)
    {
        heath -= damage;

        if (OnTakeDamage != null)
        {
            OnTakeDamage?.Invoke(this);
        }
        UpdateHpBar();
        CheckDie();
    }

    private void CheckDie()
    {
        if (heath <= 0)
        {
            if (OnDeath != null)
            {
                OnDeath?.Invoke(this);
            }
            Debug.Log(gameObject.name + "die");
        }
    }

    public void SetExp(float exp)
    {
        this.exp += exp;
        if (this.exp >= maxExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        exp = 0;
        lv += 1;
        maxExp += lvExp * lv;
        heath += lvHeath * lv;
        maxHeath += lvHeath * lv;
        damage += lvDmg * lv;

        if (OnLevelUp != null)
        {
            OnLevelUp?.Invoke(this);
        }

        UpdateHpBar();
    }

    private void UpdateHpBar()
    {
        if (hpBarController != null)
        {
            hpBarController.UpdateHealth(heath, maxHeath);
        }
        else
        {
            Debug.Log("Hpbar is null");
        }
    }
}
