using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public AudioClip Sound;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
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
        if (collision.collider.CompareTag("Enemy"))
        {
            if (EnemyStats != null && PlayerStats != null)
            {
                EnemyStats.TakeDamage(PlayerStats.damage);

            }
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
