using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLeWeapon : MonoBehaviour
{
    public float damage = 10f; // Damage dealt by the weapon
    public float range = 100f; // Range of the weapon
    public Camera fpsCam; // Reference to the first-person camera
    public ParticleSystem muzzleFlash; // Reference to the muzzle flash effect

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play(); // Play the muzzle flash effect

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // Check if the hit object has a damage handler and apply damage
            DamageHandler damageHandler = hit.transform.GetComponent<DamageHandler>();
            if (damageHandler != null)
            {
                damageHandler.TakeDamage(damage);
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
