using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    enum State
    {
        Walk,
        Sprint,
        Crouch,
        Hide,
        Puzzle,
    }
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Movement")]
        public CharacterController controller = null;
        [SerializeField] private Animator playerAnimator = null;

        [HideInInspector] public Vector3 move = Vector3.zero;
        [SerializeField] private float walkSpeed = 0f; 



        private State state;

        [Header("Player Looking")]
        [SerializeField] private Transform playerCamera = null;
        [SerializeField] private int mouseSensitivity = 0;
        private float cameraPitch = 0f;


        [Header("Player Misc.")]
        [SerializeField] private SphereCollider noiseCollider = null;


        private void Start()
        {
            state = State.Walk;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            CheckState();
        }

        public void CheckState()
        {
            switch(state)
            {
                case State.Walk:
                    Movement(walkSpeed);
                    CameraLook();
                    break;
            }
        }

        private void Movement(float _speed)
        {
            Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            inputDir.Normalize();

            Vector3 velocity = (transform.forward * inputDir.y + transform.right * inputDir.x) * walkSpeed;

            controller.Move(velocity * Time.deltaTime);
        }
        private void CameraLook()
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            cameraPitch -= mouseDelta.y * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

            playerCamera.localEulerAngles = Vector3.right * cameraPitch;

            transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);
        }
  

    }

}
