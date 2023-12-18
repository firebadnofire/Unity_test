using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float maxHP = 100.0f;
    private float currentHP;
    public AudioClip hitSound; // Attach your hit sound clip in the Unity inspector
    private AudioSource audioSource;

    public int maxBullets = 30; // Editable in the Unity editor
    private int remainingBullets;
    public int maxReserveAmmo = 90; // Editable in the Unity editor
    private int reserveAmmo;
    private bool isReloading;

    private void Start()
    {
        currentHP = maxHP;
        audioSource = GetComponent<AudioSource>();
        remainingBullets = maxBullets; // Initialize the remaining bullets to the maximum.
        reserveAmmo = maxReserveAmmo; // Initialize the reserve ammo to the maximum.
        isReloading = false;
    }

    private void Update()
    {
        // Check if the player presses 'R' to reload when not already reloading and there are bullets left to reload.
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && remainingBullets < maxBullets && reserveAmmo > 0)
        {
            isReloading = true;
            // Add a delay for the reload animation or sound here if needed.
            Reload();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isReloading && other.CompareTag("bullet") && remainingBullets > 0)
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                remainingBullets--;

                // Print the current HP, remaining bullets, and reserve ammo every time a bullet hits.
                Debug.Log("Current HP: " + currentHP);
                Debug.Log("Remaining Bullets: " + remainingBullets);
                Debug.Log("Reserve Ammo: " + reserveAmmo);
            }
        }
    }

    private void TakeDamage(float damage)
    {
        currentHP -= damage;

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

    private void Reload()
    {
        // Calculate how many bullets to reload based on what's available in reserve ammo.
        int bulletsToReload = Mathf.Min(maxBullets - remainingBullets, reserveAmmo);

        remainingBullets += bulletsToReload;
        reserveAmmo -= bulletsToReload;

        isReloading = false;
    }
}
