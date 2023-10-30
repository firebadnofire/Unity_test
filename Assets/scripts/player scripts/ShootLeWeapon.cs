using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Import the UnityEngine.UI namespace

public class ShootLeWeapon : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip reloadSound;

    public int maxMagazineSize = 30;
    private int currentBullets;
    private bool isReloading = false;

    public float reloadTime = 3f;

    public Text ammoText;  // Reference to the Text component

    void Start()
    {
        currentBullets = maxMagazineSize;
        UpdateAmmoText();  // Update the ammo text on start
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetButtonDown("Fire1") && currentBullets > 0)
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        UpdateAmmoText();  // Update the ammo text every frame
    }

    void UpdateAmmoText()
    {
        if (currentBullets == 0)
        {
            ammoText.text = "RELOAD WITH R";
        }
        else if (currentBullets <= maxMagazineSize * 0.3f)
        {
            ammoText.text = "Near empty";
        }
        else
        {
            ammoText.text = currentBullets + "/" + maxMagazineSize;  // Display current bullets and max magazine size
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        audioSource.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(reloadTime);
        currentBullets = maxMagazineSize;
        isReloading = false;
    }

    void Shoot()
    {
        currentBullets--;
        muzzleFlash.Play();
        audioSource.PlayOneShot(fireSound);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Shootable shootable = hit.transform.GetComponent<Shootable>();
            if (shootable != null)
            {
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
