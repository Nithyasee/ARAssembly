using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneTransition : MonoBehaviour
{
    public string nextSceneName; // Name of the next AR scene to load

    private void OnTriggerEnter(Collider other)
    {
        // If the object tapped or interacted with
        if (other.CompareTag("fan"))
        {
            // Transition to the next scene
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // Use the SceneManager to load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
