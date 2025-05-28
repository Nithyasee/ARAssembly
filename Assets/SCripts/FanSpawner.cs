using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FanSpawner : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject fanPrefab; // Prefab of the fan model

    private GameObject spawnedFan;

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            // Spawn the fan prefab at the tracked image's position
            if (trackedImage.referenceImage.name == "FAN") // Replace with your image name
            {
                if (spawnedFan == null)
                {
                    spawnedFan = Instantiate(fanPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                }
            }
        }
    }
}
