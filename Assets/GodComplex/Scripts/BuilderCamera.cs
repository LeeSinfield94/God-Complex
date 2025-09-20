using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

namespace Unity.Template.CompetitiveActionMultiplayer
{
    public class BuilderCamera : MonoBehaviour
    {
        public float moveSpeed = 5f; // Camera movement speed
        public float lookSpeed = 2f; // Camera rotation speed
        public float zoomSpeed = 10f; // Zoom speed (mouse scroll)
        public float maxZoom = 50f;  // Maximum zoom
        public float minZoom = 5f;   // Minimum zoom
        public Key unlockCursorKey = Key.Escape;
        private BuilderController controls;
        private Vector2 moveInput;
        private Vector2 lookInput;
        private float scrollInput;

        private float yaw = 0f;   // Horizontal rotation (yaw)
        private float pitch = 0f; // Vertical rotation (pitch)

        private Camera cam;

        void Awake()
        {
            // Get input actions
            controls = new BuilderController();

            // Register input actions
            controls.Move.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            controls.Move.Move.canceled += ctx => moveInput = Vector2.zero; // Stop movement when released

            controls.Look.Newaction.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
            controls.Look.Newaction.canceled += ctx => lookInput = Vector2.zero; // Stop looking when released

            controls.UpDown.Newaction.performed += ctx => scrollInput = ctx.ReadValue<float>();
            controls.UpDown.Newaction.canceled += ctx => scrollInput = 0f; // Stop zooming when released
        }

        void OnEnable()
        {
            controls.Enable(); // Enable input actions
        }

        void OnDisable()
        {
            controls.Disable(); // Disable input actions
        }

        void Start()
        {
            cam = GetComponent<Camera>(); // Get the Camera component
            LockCursor(); // Lock the cursor at the start
        }

        void Update()
        {
            // Camera movement
            MoveCamera();

            // Camera rotation (look around)
            LookAround();

            // Camera zoom (scroll wheel)
            ZoomCamera(); 
            if (Keyboard.current[unlockCursorKey].wasPressedThisFrame)
            {
                UnlockCursor();
            }
        }

        void MoveCamera()
        {
            // Only apply movement when there's input
            if (moveInput != Vector2.zero)
            {
                float horizontal = moveInput.x * moveSpeed * Time.deltaTime;
                float vertical = moveInput.y * moveSpeed * Time.deltaTime;

                // Move camera based on input
                Vector3 move = transform.right * horizontal + transform.forward * vertical;
                transform.position += move;
            }
        }

        void LookAround()
        {
            // Only apply rotation when there's mouse input
            if (lookInput != Vector2.zero)
            {
                yaw += lookInput.x * lookSpeed;
                pitch -= lookInput.y * lookSpeed;

                // Limit the pitch to prevent camera flipping
                pitch = Mathf.Clamp(pitch, -90f, 90f);

                // Apply the rotation
                transform.eulerAngles = new Vector3(pitch, yaw, 0f);
            }
        }

        void ZoomCamera()
        {
            // Zoom in/out using the mouse scroll wheel
            if (scrollInput != 0f)
            {
                float zoomChange = scrollInput * zoomSpeed * Time.deltaTime;
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - zoomChange, minZoom, maxZoom);
            }
        }

        void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the center
            Cursor.visible = false;  // Hide the cursor
        }

        // Unlock the cursor (e.g., on pressing Escape)
        void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
            Cursor.visible = true;  // Make the cursor visible again
        }
    }
}
