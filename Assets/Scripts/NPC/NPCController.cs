using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public DialogManager dialogManager;
    private Canvas canvas;
    private bool asked = false;
    private QuestManager questManager;
    private QuestKill quest1;
    private QuestKill quest2;
    private QuestKill quest3;
    private GameObject player;
    private int quest1ems = 0;
    private int quest2ems = 0;
    private int quest3ems = 0;
    private bool questAccepted = false;
    private List<string> dialog = new List<string>
    {
        "XIN CHAO NHA THAM HIEM ABC. TOI DA CHO BAN RAT LAU O DAY",
        "HAY GIUP TOI TIEU DIET QUAI VAT DE MANG LAI BINH YEN CHO VUNG DAT NAY",
        "BAN CO DONG Y THU THACH NAY KHONG?"
    };

    void Start()
    {
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
        questManager = GameObject.Find("QuestHolder").GetComponent<QuestManagerHolder>().questManager;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (questAccepted)
        {
            if (quest1.IsCompleted == false)
            {
                int checkq1ems = FindGameObjectsWithNamePart("Eagle");
                if (quest1ems > checkq1ems)
                {
                    quest1.EnemyKilled(quest1ems - checkq1ems);
                    quest1ems = checkq1ems;
                }
            }

            if (quest2.IsCompleted == false)
            {
                int checkq2ems = FindGameObjectsWithNamePart("Bunny");
                if (quest2ems > checkq2ems)
                {
                    quest2.EnemyKilled(quest2ems - checkq2ems);
                    quest2ems = checkq2ems;
                }
            }
            if (quest3.IsCompleted == false)
            {
                int checkq3ems = FindGameObjectsWithNamePart("Bat");
                if (quest3ems > checkq3ems)
                {
                    quest3.EnemyKilled(quest3ems - checkq3ems);
                    quest3ems = checkq3ems;
                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (asked == false && other.CompareTag("Player"))
        {
            asked = true;
            canvas.gameObject.transform.Find("StartQuestBtn").gameObject.SetActive(false);
            dialogManager.StartDialog(canvas, dialog, AcceptQuest, DeclineQuest, 2f);
        }
    }

    void AcceptQuest()
    {
        Debug.Log("Quest Accepted!");
        questAccepted = true;
        quest1 = new("Tieu diet 5 dai bang bien di", 5);
        quest1.OnKilled += OnKilled;
        quest1.experience = 50;
        quest1ems = FindGameObjectsWithNamePart("Eagle");
        questManager.AddQuest(quest1);
        quest2 = new("Tieu diet 5 tho bien di", 5);
        quest2.OnKilled += OnKilled;
        quest2.experience = 30;
        quest2ems = FindGameObjectsWithNamePart("Bunny");
         questManager.AddQuest(quest2);
        quest3 = new("Tieu diet 3 doi ien di", 3);
        quest3.OnKilled += OnKilled;
        quest3.experience = 30;
        quest3ems = FindGameObjectsWithNamePart("Bat");
         questManager.AddQuest(quest3);
        UpdateUi();
    }

    void DeclineQuest()
    {
        Debug.Log("Quest Declined!");
        asked = false;
        canvas.gameObject.transform.Find("StartQuestBtn").gameObject.SetActive(true);
    }

    private void OnKilled(QuestKill quest)
    {
        if (quest.IsCompleted)
        {
            player.GetComponent<Stats>().SetExp(quest.experience);
        }
        UpdateUi();
    }

    private void UpdateUi()
    {
        List<Quest> quests = questManager.getAllQuest();
        Debug.Log(quests.Count);
        string text = "";
        foreach (Quest _q in quests)
        {
            QuestKill qk = _q as QuestKill;
            Debug.Log(qk.IsCompleted);
            if (qk.IsCompleted == false)
                text += $"- {qk.QuestName} ({qk.EnemiesKilled}/{qk.EnemiesToKill})\n";
        }
        player.GetComponentInChildren<GetPlayerInfo>().UpdateMissionText(text);
    }

    public int FindGameObjectsWithNamePart(string namePart)
    {
        List<GameObject> foundObjects = new List<GameObject>();

        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains(namePart))
            {
                foundObjects.Add(obj);
            }
        }

        return foundObjects.Count;
    }
}