using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;

    void Update()
    {
        LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        if (player != null)
        {
            if (transform.position.x > player.position.x && !isFlipped)
            {
                Flip();
            }
            else if (transform.position.x < player.position.x && isFlipped)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 flipped = transform.localScale;
        flipped.x *= -1;
        transform.localScale = flipped;
        if (transform.GetComponent<SpriteRenderer>() != null)
        {
            transform.Rotate(0f, 180f, 0f);
        }
        else
        {
            transform.Rotate(180f, 0f, 0f);
        }
    }
}
