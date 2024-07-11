using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public List<string> sentences;
    public UnityEngine.Events.UnityEvent onDialogEnd;
}