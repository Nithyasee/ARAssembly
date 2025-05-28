using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class FanAssemblyManager : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;  // AR Image Manager
    public GameObject fanAssemblyPrefab;  // Prefab holding all fan parts
    public Canvas arCanvas;  // AR Canvas with instructions
    public TextMeshProUGUI instructionText;  // UI Text for instructions
    public Button nextButton;  // "Next" Button

    private GameObject fanAssemblyInstance;  // Instance of fan assembly
    private int currentStep = 0;  // Current assembly step
    private Animator[] animators;  // Array of Animator components
    private string[] instructions = new string[]
    {
        "Step 1: Place the Back Guard Rim.",
        "Step 2: Then attach the Motor to it.",
        "Step 3: After that attach the Plastic Rim.",
        "Step 4: Finally fix the Front Guard Rim and screw it."
    };

    void Start()
    {
        // Ensure the Canvas and Button are inactive initially
        arCanvas.gameObject.SetActive(false);
        nextButton.onClick.AddListener(NextStep);
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            if (trackedImage.referenceImage.name == "FAN")
            {
                // Instantiate fan assembly and position it
                fanAssemblyInstance = Instantiate(fanAssemblyPrefab, trackedImage.transform.position, trackedImage.transform.rotation);

                // Get all Animator components
                animators = fanAssemblyInstance.GetComponentsInChildren<Animator>(true);

                // Activate the Canvas and display the first instruction
                arCanvas.gameObject.SetActive(true);
                instructionText.text = instructions[currentStep];
            }
        }
    }

    public void NextStep()
    {
        if (currentStep < animators.Length)
        {
            // Activate and play the next animation
            animators[currentStep].gameObject.SetActive(true);
            animators[currentStep].SetTrigger("Play");

            // Update instructions for the next step
            instructionText.text = instructions[currentStep];

            // Increment the step counter
            currentStep++;

            // Disable the button if it's the last step
            if (currentStep == instructions.Length)
            {
                nextButton.interactable = false;
            }
        }
    }
}
