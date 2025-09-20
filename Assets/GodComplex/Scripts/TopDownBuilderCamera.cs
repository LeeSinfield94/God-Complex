using UnityEngine;

namespace Unity.Template.CompetitiveActionMultiplayer
{
    public class TopDownBuilderCamera : MonoBehaviour
    {
        public float height = 10f;        // Height of the camera from the scene
        public float zoomSpeed = 2f;      // Zoom speed (scrolling speed)
        public float maxZoom = 20f;       // Maximum zoom distance
        public float minZoom = 5f;        // Minimum zoom distance

        private Vector2 movementInput;    // For WASD or Arrow keys input (for moving the camera)
        private float zoomInput;          // For mouse scroll wheel input
        private Camera cam;

        private BuilderController controls;  // Reference to the PlayerControls class

        void Awake()
        {
            // Set up player input actions
            controls = new BuilderController();

            // Movement input (WASD or Arrow Keys)
            controls.Move.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            controls.Move.Move.canceled += ctx => movementInput = Vector2.zero;

            // Zoom input (Mouse Scroll Wheel)
            controls.Zoom.Newaction.performed += ctx => zoomInput = ctx.ReadValue<float>();
            controls.Zoom.Newaction.canceled += ctx => zoomInput = 0f;
        }

        void OnEnable()
        {
            controls.Enable(); // Enable the input system actions
        }

        void OnDisable()
        {
            controls.Disable(); // Disable the input system actions
        }

        void Start()
        {
            cam = GetComponent<Camera>(); // Get the Camera component
            transform.position = new Vector3(transform.position.x, height, transform.position.z); // Set the initial height
            transform.rotation = Quaternion.Euler(90f, 0f, 0f); // Ensure camera is looking down
        }

        void Update()
        {
            MoveCamera();   // Move the camera based on WASD/Arrow keys
            ZoomCamera();   // Zoom the camera based on the mouse scroll wheel
        }

        // Move the camera based on WASD or Arrow keys
        void MoveCamera()
        {
            // Calculate movement along the X and Z axes
            Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y) * Time.deltaTime * 5f;
            transform.position += movement; // Apply movement
        }

        // Zoom the camera in/out based on the mouse scroll wheel
        void ZoomCamera()
        {
            // Zoom the camera by modifying the height
            height -= zoomInput * zoomSpeed;
            height = Mathf.Clamp(height, minZoom, maxZoom); // Clamp height to max/min zoom limits

            // Apply the new height to the camera position
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }
    }
}
