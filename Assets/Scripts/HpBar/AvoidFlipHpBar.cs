using UnityEngine;

public class AvoidFlip : MonoBehaviour
{
    private Transform thisHolder;
    public bool currentSide = true;

    void Awake()
    {
        thisHolder = transform.parent;;
    }

    void Update()
    {
        if (thisHolder == null) return;
        if (currentSide != thisHolder.localScale.x > 0)
        {
            currentSide = thisHolder.localScale.x > 0;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
        }
    }
}
