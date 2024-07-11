using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoving : MonoBehaviour
{
    private Animator anim;
    public float speed = 2f;
    private Rigidbody2D rb;
    public float minX;
    public float maxX;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Moverandom());

    }
     IEnumerator Moverandom()
    {
        while(true)
        {
            Vector2 randomPos = SetRandomTargetPosition();
            anim.SetBool("Ismoving", true);
            while(Vector2.Distance(transform.position, randomPos) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, randomPos, speed * Time.deltaTime);
                FlipTowardsTarget(randomPos);
                yield return null;
            }
            anim.SetBool("Ismoving", false);
            yield return new WaitForSeconds(3f);
        }
    }
    Vector2 SetRandomTargetPosition()
    {
        float randomX = Random.Range(minX, maxX);
        return new Vector2(randomX, transform.position.y);
    }

    public void FlipTowardsTarget(Vector2 target)
    {
        if (target.x < transform.position.x && transform.localScale.x > 0
            || target.x > transform.position.x && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

}
