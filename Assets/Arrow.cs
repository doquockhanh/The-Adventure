using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    private Stats stats;
    public float speedArrow;
    void Start()
    {
        if (TryGetComponent(out stats))
        {
            stats.OnDeath += OnDeath;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speedArrow * Time.deltaTime);
        Destroy(gameObject, 2.5f);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            Stats playerStats = collider.GetComponent<Stats>();
            playerStats.TakeDamage(stats.damage);
            Destroy(gameObject);
        }
    }
    public void OnDeath(Stats stats)
    {
        Destroy(gameObject.GetComponent<Stats>());
    }
}
