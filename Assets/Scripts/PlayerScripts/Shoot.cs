using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public Rigidbody projectile;
    public Transform firePoint; // Add a reference to the origin point of the projectile
    public float speed = 20;

    public int maxAmmo = 10;
    private int currentAmmo;
    public int maxResAmmo = 30;
    public int reserveAmmo;

    public float reloadTime = 2.0f;
    private bool isReloading = false;

    // Use this for initialization
    void Start()
    {
        currentAmmo = maxAmmo;
        reserveAmmo = maxResAmmo; // Initialize reserve ammo to the maximum value
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0 && !isReloading)
        {
            FireProjectile();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    void FireProjectile()
    {
        Rigidbody instantiatedProjectile = Instantiate(projectile,
                                                       firePoint.position, // Use the firePoint's position
                                                       transform.rotation) as Rigidbody;

        instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        currentAmmo--;

        // Log ammo counts
        Debug.Log("Current Ammo: " + currentAmmo);
        Debug.Log("Reserve Ammo: " + reserveAmmo);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        int neededAmmo = maxAmmo - currentAmmo;

        if (reserveAmmo >= neededAmmo)
        {
            currentAmmo = maxAmmo;
            reserveAmmo -= neededAmmo;
        }
        else
        {
            currentAmmo += reserveAmmo;
            reserveAmmo = 0;
        }

        // Ensure that reserve ammo doesn't exceed the maximum
        if (reserveAmmo > maxResAmmo)
        {
            reserveAmmo = maxResAmmo;
        }

        isReloading = false;
        // Log ammo counts
        Debug.Log("Current Ammo: " + currentAmmo);
        Debug.Log("Reserve Ammo: " + reserveAmmo);
    }
}