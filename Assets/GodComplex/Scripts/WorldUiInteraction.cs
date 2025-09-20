using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class WorldUIInteraction : MonoBehaviour
{
    public Camera playerCamera;  // Camera from which to cast the ray (main camera usually)
    public EventSystem eventSystem;  // Reference to Unity's event system
    public GraphicRaycaster graphicRaycaster;  // Raycaster for UI interaction

    void Update()
    {
        // Detect if the left mouse button is clicked
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Perform the raycast to detect UI interactions
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue()); // Cast ray from mouse position

            // Perform raycast to check if the mouse is pointing at a UI element
            if (Physics.Raycast(ray, out hit))
            {
                // Now, use GraphicRaycaster to check if UI is clicked
                PointerEventData pointerData = new PointerEventData(eventSystem)
                {
                    position = Mouse.current.position.ReadValue()
                };

                // List of Raycast Results
                var results = new System.Collections.Generic.List<RaycastResult>();
                graphicRaycaster.Raycast(pointerData, results);

                // Check if we hit a UI element
                if (results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        GameObject clickedObject = result.gameObject;

                        // If the clicked object is a button, trigger its onClick event
                        Button button = clickedObject.GetComponent<Button>();
                        if (button != null)
                        {
                            button.onClick.Invoke();  // Simulate button click
                        }
                    }
                }
            }
        }
    }
}
