using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player States & State Machine")]
        private StateMachine movementStateMachine = null;
        [HideInInspector] public WalkingState walkingState = null;
        [HideInInspector] public SprintingState sprintingState = null;
        [HideInInspector] public CrouchingState crouchingState = null;

        [Header("Player Movement")]
        public CharacterController controller = null;
        public float walkSpeed = 0f;
        public float sprintSpeed = 0f;
        public float gravityMultiplier = 0f;
        [SerializeField] private Animator playerAnimator = null;
        [HideInInspector] public Vector3 velocity = Vector3.zero;

        [Header("Player Looking")]
        public Transform playerCamera = null;
        public int mouseSensitivity = 0;
        [HideInInspector] public float cameraPitch;

        [Header("Player Noise")]
        [SerializeField] private SphereCollider noiseCollider = null;
        [SerializeField] private float noiseRadius = 0f;
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked; 
            movementStateMachine = new StateMachine();

            walkingState = new WalkingState(this, movementStateMachine);
            sprintingState = new SprintingState(this, movementStateMachine);
           // crouchingState = new CrouchingState(this, movementStateMachine);

            movementStateMachine.Initialize(walkingState);
        }

        private void Update()
        {
            movementStateMachine.CurrentState.HandleInput();

            movementStateMachine.CurrentState.LogicUpdate();
        }

        private void LateUpdate()
        {
            movementStateMachine.CurrentState.LateLogicUpdate();
        }

        private void FixedUpdate()
        {
            movementStateMachine.CurrentState.PhysicsUpdate();
        }
    }
}
