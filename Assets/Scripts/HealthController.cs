using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float maxHP = 100.0f;
    private float currentHP;

    public AudioClip hitSound; // Attach your hit sound clip in the Unity inspector
    private AudioSource audioSource;

    private void Start()
    {
        currentHP = maxHP;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
            }
        }
    }

    private void TakeDamage(float damage)
    {
        currentHP -= damage;

        // Print the current HP value every time it's hit
        Debug.Log("Current HP: " + currentHP);

        if (currentHP <= 0)
        {
            // Optionally, you can play a death sound here before destroying the object.
            // For example, audioSource.PlayOneShot(deathSound);
            Destroy(gameObject);
        }
        else
        {
            if (hitSound != null)
            {
                audioSource.PlayOneShot(hitSound); // Play the hit sound when a successful hit occurs
            }
        }
    }
}
