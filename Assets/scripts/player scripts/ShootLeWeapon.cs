using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text lowAmmoText;  // Reference to the Text component for low ammo
    public Text noAmmoText;  // Reference to the Text component for no ammo

    public int lowAmmoThreshold = 10;  // Define a threshold for low ammo

    void Start()
    {
        currentBullets = maxMagazineSize;
        UpdateAmmoText();
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

        UpdateAmmoText();
    }

    void UpdateAmmoText()
    {
        if (currentBullets == 0)
        {
            ammoText.text = "";
            noAmmoText.text = "RELOAD WITH R";
        }
        else if (currentBullets <= lowAmmoThreshold)
        {
            ammoText.text = "Low Ammo";
            lowAmmoText.text = currentBullets + "/" + maxMagazineSize;
            noAmmoText.text = "";
        }
        else
        {
            ammoText.text = currentBullets + "/" + maxMagazineSize;
            lowAmmoText.text = "";
            noAmmoText.text = "";
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
