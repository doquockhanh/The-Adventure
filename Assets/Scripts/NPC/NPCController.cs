using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public DialogManager dialogManager;
    private Canvas canvas;
    private bool asked = false;
    private List<string> dialog = new List<string>
    {
        "XIN CHAO NHA THAM HIEM ABC. TOI DA CHO BAN RAT LAU O DAY",
        "HAY GIUP TOI TIEU DIET QUAI VAT DE MANG LAI BINH YEN CHO VUNG DAT NAY",
        "BAN CO DONG Y THU THACH NAY KHONG?"
    };

    void Start()
    {
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (asked == false && other.CompareTag("Player"))
        {
            asked = true;
            Destroy(canvas.gameObject.transform.Find("StartQuestBtn").gameObject);
            dialogManager.StartDialog(canvas, dialog, AcceptQuest, DeclineQuest, 2f);
        }
    }

    void AcceptQuest()
    {
        Debug.Log("Quest Accepted!");
        // Thực hiện các hành động khác khi chấp nhận
    }

    void DeclineQuest()
    {
        Debug.Log("Quest Declined!");
        // Thực hiện các hành động khác khi từ chối
    }

}