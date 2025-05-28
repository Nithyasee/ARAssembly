using UnityEngine;
using UnityEngine.UI;

public class FanMovementController : MonoBehaviour
{
    public Slider xSlider; // Reference to the X position slider
    public Slider ySlider; // Reference to the Y position slider
    public Slider zSlider; // Reference to the Z position slider

    private GameObject selectedFan;

    private void Start()
    {
        xSlider.onValueChanged.AddListener(OnXSliderChanged);
        ySlider.onValueChanged.AddListener(OnYSliderChanged);
        zSlider.onValueChanged.AddListener(OnZSliderChanged);
    }

    private void Update()
    {
        // Check for touch or mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("fan"))
                {
                    selectedFan = hit.collider.gameObject;
                }
            }
        }
    }

    private void OnXSliderChanged(float value)
    {
        if (selectedFan != null)
        {
            Vector3 position = selectedFan.transform.position;
            position.x = value;
            selectedFan.transform.position = position;
        }
    }

    private void OnYSliderChanged(float value)
    {
        if (selectedFan != null)
        {
            Vector3 position = selectedFan.transform.position;
            position.y = value;
            selectedFan.transform.position = position;
        }
    }

    private void OnZSliderChanged(float value)
    {
        if (selectedFan != null)
        {
            Vector3 position = selectedFan.transform.position;
            position.z = value;
            selectedFan.transform.position = position;
        }
    }
}
