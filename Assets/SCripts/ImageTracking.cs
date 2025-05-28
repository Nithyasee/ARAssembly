using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections; // For Coroutines

public class ImageTracking : MonoBehaviour
{
    public GameObject planePrefab; // 3D Plane Prefab to show when image is detected
    public GameObject rotatingModelPrefab1; // First rotating object to show above the plane
    public GameObject rotatingModelPrefab2; // Second rotating object to show above the plane
    public GameObject rotatingModelPrefab3; // Second rotating object to show above the plane
    private ARTrackedImageManager arTrackedImageManager;
    private GameObject planeInstance;

    

    void Start()
    {
        arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var addedImage in eventArgs.added)
        {
            if (addedImage.referenceImage.name == "MAIN") // Ensure it matches your target image
            {
                // Instantiate the plane immediately at the image's position
                Vector3 planePosition = addedImage.transform.position;
                planeInstance = Instantiate(planePrefab, planePosition, Quaternion.identity);
                

                // Start the Coroutine to spawn two models after a delay
                StartCoroutine(SpawnTwoModelsAfterDelay(2f, planePosition));
            }
        }
    }

    // Coroutine to wait for a specified delay (in seconds) and then spawn two models
    IEnumerator SpawnTwoModelsAfterDelay(float delay, Vector3 planePosition)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Instantiate the first rotating model above the plane after the delay
        Vector3 fanPosition1 = planePosition + new Vector3(0, 0.05f, 0); // Adjust height if needed
        Instantiate(rotatingModelPrefab1, fanPosition1, Quaternion.identity);

        // Instantiate the second rotating model beside the first one (adjust position as needed)
        Vector3 fanPosition2 = planePosition + new Vector3(0.4f, 0.05f, 0); // Slight offset for the second model
        Instantiate(rotatingModelPrefab2, fanPosition2, Quaternion.identity);

        // Instantiate the second rotating model beside the first one (adjust position as needed)
        Vector3 fanPosition3 = planePosition + new Vector3(-0.4f, 0.06f, 0); // Slight offset for the second model
        Instantiate(rotatingModelPrefab3, fanPosition3, Quaternion.identity);
    }
}
