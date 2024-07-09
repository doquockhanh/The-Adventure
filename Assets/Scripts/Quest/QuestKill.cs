using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class QuestKill : Quest
{
    public int EnemiesToKill { get; set; }
    public int EnemiesKilled { get; set; }
    public event System.Action<QuestKill> OnKilled;

    public QuestKill(string name, int enemiesToKill)
    {
        QuestName = name;
        EnemiesToKill = enemiesToKill;
    }

    public void EnemyKilled()
    {
        EnemiesKilled++;
        CheckCompletion();
        OnKilled?.Invoke(this);
    }

    public override void CheckCompletion()
    {
        if (EnemiesKilled >= EnemiesToKill)
        {
            CompleteQuest();
        }
    }
}