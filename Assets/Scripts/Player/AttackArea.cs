using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public AudioClip Sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            {
            Destroy(collision.gameObject);
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.SpawnEnemies();
            }
        }
        Stats PlayerStats = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Stats>();
        Stats EnemyStats = collision.GetComponent<Stats>();
        if(EnemyStats != null && PlayerStats !=null )
        {
            EnemyStats.TakeDamage(PlayerStats.damage);
           
        }
        
        
        PlayShootSoundPlayer();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Stats PlayerStats = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Stats>();
        Stats EnemyStats = collision.collider.GetComponent<Stats>();
        if (EnemyStats != null && PlayerStats != null)
        {
            EnemyStats.TakeDamage(PlayerStats.damage);

        }
        
    }
    private void PlayShootSoundPlayer()
    {

        GameObject tempGO = new GameObject("TempAudio");
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = Sound;
        audioSource.Play();


        Destroy(tempGO, Sound.length);
    }
    

    
}
