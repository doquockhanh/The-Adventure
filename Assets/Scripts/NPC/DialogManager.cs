using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum DialogType
{
    dialogSkip = 1,
    dialogInput = 2,
    dialogRequest = 3
}
public class DialogManager : MonoBehaviour
{
    private Text dialogText;
    private Button acceptButton;
    private Button declineButton;
    private DialogType type = DialogType.dialogRequest;

    private Queue<string> sentences;
    private UnityAction acceptAction;
    private UnityAction declineAction;
    private Canvas canvas;
    private GameObject dialogRequest;
    private GameObject currentDialog;
    private float delay = 2f;
    private GameObject btnGroup;

    void Start()
    {
        sentences = new Queue<string>();
        dialogRequest = Resources.Load<GameObject>("DialogRequest");
    }

    public void StartDialog(Canvas canvas, List<string> dialog, UnityAction onAccept, UnityAction onDecline, float delay)
    {
        this.canvas = canvas;
        this.delay = delay;
        sentences.Clear();

        foreach (string sentence in dialog)
        {
            sentences.Enqueue(sentence);
        }

        acceptAction = onAccept;
        declineAction = onDecline;

        StartCoroutine(DisplayNextSentence());
    }

    IEnumerator DisplayNextSentence()
    {
        switch (type)
        {
            case DialogType.dialogRequest:
                {
                    Transform textSpawnPoint = canvas.transform.Find("dialogAppearPoint");
                    Vector2 pos = textSpawnPoint == null ? canvas.transform.position : textSpawnPoint.transform.position;
                    currentDialog = Instantiate(dialogRequest, pos, Quaternion.identity);
                    currentDialog.transform.SetParent(canvas.transform);
                    currentDialog.transform.localScale = new Vector3(1f, 1f, 0);
                    acceptButton = currentDialog.GetComponentsInChildren<Button>()[0];
                    declineButton = currentDialog.GetComponentsInChildren<Button>()[1];
                    acceptButton.onClick.AddListener(AcceptDialog);
                    declineButton.onClick.AddListener(DeclineDialog);
                    dialogText = currentDialog.GetComponentInChildren<Text>();

                    btnGroup = GameObject.Find("ButtonGroup");
                    btnGroup.SetActive(false);
                    break;
                }
        }

        while (sentences.Count >= 1)
        {
            string sentence = sentences.Dequeue();
            dialogText.text = sentence;
            yield return new WaitForSeconds(delay);
        }

        Enddialog();
        StopAllCoroutines();
    }

    void Enddialog()
    {
        btnGroup.SetActive(true);
    }

    public void AcceptDialog()
    {
        Destroy(currentDialog);
        acceptAction?.Invoke();
    }

    public void DeclineDialog()
    {
        Destroy(currentDialog);
        declineAction?.Invoke();
    }
}
