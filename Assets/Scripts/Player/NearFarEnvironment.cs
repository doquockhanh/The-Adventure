using System.Collections.Generic;
using UnityEngine;

public class NearFarEnvironment : MonoBehaviour
{
    public Transform player;
    public Transform[] backgrounds;
    public float[] parallaxScales;
    private Vector3 previousPlayerPosition;
    private List<Vector2> diffPos = new();
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        previousPlayerPosition = player.position;
        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector2 diff = new(backgrounds[i].position.x - player.position.x, backgrounds[i].position.y - player.position.y);
            diffPos.Add(diff);
        }
    }

    void FixedUpdate()
    {
        if (diffPos.Count == 0) {
            Debug.Log("nit");
            return;
        }
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallaX = (previousPlayerPosition.x - player.position.x) * parallaxScales[i];
            float parallaY = (previousPlayerPosition.y - player.position.y) * parallaxScales[i];
            diffPos[i] = new Vector2(diffPos[i].x + parallaX, diffPos[i].y + parallaY);
            float backgroundTargetPosX = player.position.x + diffPos[i].x;
            float backgroundTargetPosY = player.position.y + diffPos[i].y;
            backgrounds[i].position = new Vector3(backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);
        }
        previousPlayerPosition = player.position;
    }
}