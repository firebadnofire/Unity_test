using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadZone : MonoBehaviour
{
    public string sceneToLoad; // The name of the scene to load (make sure it's added to the build settings)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // You can change the tag to match your player's tag
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
