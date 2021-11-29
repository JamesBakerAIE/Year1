using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player States & State Machine")]
        private StateMachine movementStateMachine = null;
        [HideInInspector] public WalkingState walkingState = null;
        [HideInInspector] public SprintingState sprintingState = null;
        [HideInInspector] public CrouchingState crouchingState = null;
        [HideInInspector] public HidingState hidingState = null;
        [HideInInspector] public PuzzleState puzzleState = null;

        [Header("Player Movement")]
        public CharacterController controller = null;
        public float walkSpeed = 0f;
        public float sprintSpeed = 0f;
        public float crouchSpeed = 0f;
        public float maxSprintTime = 0f;
        [HideInInspector] public float currentSprintTime = 0f;
        public float gravityMultiplier = 0f;
        public float cameraCrouchDistance = 0.5f;
        public float crouchRaycastHeight = 0.85f;
        public LayerMask ignoreLayerMask = 0;

        [HideInInspector] public Vector3 velocity = Vector3.zero;
        [HideInInspector] public float playerCrouchColliderHeight = 0f;
        [HideInInspector] public Vector3 playerColliderNormalSize = Vector3.zero;
        public Animator playerAnimator = null;
        public Animator leftArmAnimator = null;

        [Header("Player Looking")]
        public Transform playerCamera = null;
        public int mouseSensitivity = 0;
        [HideInInspector] public float cameraPitch;

        [Header("Player Hiding")]
        public float interactRange = 0.0f;
        public float walkToLockerTime = 0.0f;
        public float exitLockerTime = 0.0f;
        public LayerMask hideSpotLayerMask = 0;
        public float lockerExitDistance = 0;
        public float lockerHeightOffset = 0;


        [Header("Player Puzzles")]
        public float pickupDistance = 0;
        public float pickupRadius = 0;
        
        public float puzzleInteractRange = 0;

        public float keycardInsertTime = 0;

        public LayerMask keycardLayerMask = 0;
        public LayerMask keycardHolderLayerMask = 0;
        public LayerMask computerLayerMask = 0;

        public int keycardCount = 0;



        [HideInInspector] public RaycastHit result;

        // VARIABLES FOR FPS COUNTER
        private int frameCount;
        private float elapsedTime;
        private double frameRate;





        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            // Creating state machines and the states themselves
            movementStateMachine = new StateMachine();

            walkingState = new WalkingState(this, movementStateMachine);
            sprintingState = new SprintingState(this, movementStateMachine);
            crouchingState = new CrouchingState(this, movementStateMachine);
            hidingState = new HidingState(this, movementStateMachine);
            puzzleState = new PuzzleState(this, movementStateMachine);

            movementStateMachine.Initialize(walkingState);
        }


        private void OnGUI()
        {

        }

        private void Update()
        {
            movementStateMachine.CurrentState.HandleInput();

            movementStateMachine.CurrentState.LogicUpdate();

            currentSprintTime = Mathf.Clamp(currentSprintTime, 0, maxSprintTime);
        }

        private void LateUpdate()
        {
            movementStateMachine.CurrentState.LateLogicUpdate();
        }

        private void FixedUpdate()
        {
            movementStateMachine.CurrentState.PhysicsUpdate();
        }

        // Check if player is below an object (under a table, inside a vent, etc.)
        public bool CheckCollisionOverlap(Vector3 point)
        {
            Ray ray = new Ray(point, Vector3.up);

            if(Physics.Raycast(ray, crouchRaycastHeight, ~ignoreLayerMask))
            {
                return true;
            }

            return false;
        }
    }
}
