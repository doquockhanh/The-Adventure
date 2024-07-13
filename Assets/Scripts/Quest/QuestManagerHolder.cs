using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManagerHolder : MonoBehaviour
{
    public QuestManager questManager;
    void Awake()
    {
        questManager = new();
    }
}
