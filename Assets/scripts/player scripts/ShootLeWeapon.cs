using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLeWeapon : MonoBehaviour
{
    public float damage = 10f; // Damage dealt by the weapon
    public float range = 100f; // Range of the weapon
    public Camera fpsCam; // Reference to the first-person camera
    public ParticleSystem muzzleFlash; // Reference to the muzzle flash effect
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip fireSound; // Reference to the audio clip

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();  // Play the muzzle flash effect

        // Play the fire sound
        audioSource.PlayOneShot(fireSound);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // Get the Shootable script component on the hit object, if it has one
            Shootable shootable = hit.transform.GetComponent<Shootable>();
            if (shootable != null)
            {
                // Call OnShot on the Shootable script, passing the damage amount
                shootable.OnShot(damage);
            }
        }
    }
}

public class DamageHandler : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
