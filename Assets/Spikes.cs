using UnityEngine;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class TriggerLoop : MonoBehaviour
{
    private bool isTriggering = false;
   
    private Stats stats;

    private Coroutine triggerCoroutine;

    private void Start()
    {
        if (TryGetComponent(out stats))
        {
            stats.OnDeath += OnDeath;
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
      if(!isTriggering && collider.CompareTag("Player"))
        {
           triggerCoroutine = StartCoroutine(TriggerLoopCoroutine(collider));
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && triggerCoroutine != null)
        {
            StopCoroutine(triggerCoroutine);
            isTriggering = false;
        }
    }
    private IEnumerator TriggerLoopCoroutine(Collider2D collider)
    {
        if (!isTriggering)
        {
            isTriggering = true;
            Stats playerStats = collider.GetComponent<Stats>();

            while (collider.CompareTag("Player") && playerStats != null && stats != null)
            {
                playerStats.TakeDamage(stats.damage);
                yield return new WaitForSeconds(0.5f);
            }

            isTriggering = false;
        }
    }
    
    public void OnDeath(Stats stats)
    {
        Destroy(gameObject.GetComponent<Stats>());
    }

}