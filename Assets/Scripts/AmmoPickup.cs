using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int restorePercentage = 50; // Adjust this value as needed

    [SerializeField]
    private GameObject targetGameObject; // Serialized field for the target GameObject

    [SerializeField]
    private AudioClip restoreSound; // Serialized field for the restore sound

    private void OnTriggerEnter(Collider other)
    {
        if (targetGameObject != null)
        {
            Shoot gunShooter = targetGameObject.GetComponent<Shoot>();
            if (gunShooter != null)
            {
                int maxAmmo = gunShooter.maxResAmmo;
                int maxReserveAmmo = gunShooter.maxResAmmo;

                int ammoToRestore = (int)(maxReserveAmmo * (restorePercentage / 100f));

                gunShooter.reserveAmmo += ammoToRestore;

                // Make sure reserve ammo doesn't exceed the max
                if (gunShooter.reserveAmmo > maxAmmo)
                {
                    gunShooter.reserveAmmo = maxAmmo;
                }

                // Play the restore sound
                if (restoreSound != null)
                {
                    AudioSource.PlayClipAtPoint(restoreSound, transform.position);
                }

                // Deactivate the trigger object
                gameObject.SetActive(false);

                // Call the function to reactivate the trigger object after a delay
                Invoke("ReactivateAmmoPickup", 10f);
            }
        }
    }

    private void ReactivateAmmoPickup()
    {
        // Reactivate the trigger object after 10 seconds
        gameObject.SetActive(true);
    }
}
