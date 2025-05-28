using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class TapToLoadScene : MonoBehaviour
{
    public string nextSceneName; // Assign the scene name in the Inspector

    public void OnMouseDown()
    {
        TransitionToNextScene();
    }

    private void TransitionToNextScene()
    {
        var xrManagerSettings = XRGeneralSettings.Instance.Manager;

        if (xrManagerSettings != null)
        {
            // Deinitialize XR loader
            xrManagerSettings.DeinitializeLoader();

            // Load the new scene
            SceneManager.LoadScene(nextSceneName);

            // Reinitialize XR loader in the new scene
            xrManagerSettings.InitializeLoaderSync();

            if (xrManagerSettings.activeLoader != null)
            {
                xrManagerSettings.StartSubsystems();
            }
            else
            {
                Debug.LogError("Failed to initialize XR Loader in the new scene.");
            }
        }
        else
        {
            Debug.LogError("XRGeneralSettings.Instance.Manager is null.");
        }
    }
}
