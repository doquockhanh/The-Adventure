using UnityEngine;

public class AvoidFlip : MonoBehaviour
{
    private Transform baseCanvas;
    public bool currentSide = true;

    void Awake()
    {
        baseCanvas = transform.Find("BaseCanvas");
    }

    void Update()
    {
        if (baseCanvas == null) return;
        if (currentSide != transform.localScale.x > 0)
        {
            currentSide = transform.localScale.x > 0;
            baseCanvas.localScale = new Vector3(-baseCanvas.localScale.x, baseCanvas.localScale.y, 0);
        }
    }
}
