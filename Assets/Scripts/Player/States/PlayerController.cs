﻿using System.Collections;
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

        [Header("Player Movement")]
        public CharacterController controller = null;
        public float walkSpeed = 0f;
        public float sprintSpeed = 0f;
        public float crouchSpeed = 0f;
        public float gravityMultiplier = 0f;
        public float cameraCrouchDistance = 0.5f;
        public float crouchRaycastHeight = 0.85f;
        public LayerMask ignoreLayerMask = 0;

        [HideInInspector] public Vector3 velocity = Vector3.zero;
        [HideInInspector] public float playerCrouchColliderHeight = 0f;
        [HideInInspector] public Vector3 playerColliderNormalSize = Vector3.zero;
        [SerializeField] private Animator playerAnimator = null;

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

        [Header("Player Puzzles")]

        public CapsuleCollider pickupCollider = null;
        public float pickupColliderHeight = 0;
        public float pickupColliderRadius = 0;

        public LayerMask keycardLayerMask = 0;

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

            movementStateMachine.Initialize(walkingState);

            QualitySettings.vSyncCount = 0;

            pickupColliderHeight = pickupCollider.height;
            pickupColliderRadius = pickupCollider.radius;
        }


        private void OnGUI()
        {
            // FPS COUNTER
            GUI.Label(new Rect(50, 50, 50, 50), frameRate.ToString());
        }

        private void Update()
        {
            movementStateMachine.CurrentState.HandleInput();

            movementStateMachine.CurrentState.LogicUpdate();

            //FPS COUNTER
            frameCount++;
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 0.5f)
            {
                frameRate = System.Math.Round(frameCount / elapsedTime, 1, System.MidpointRounding.AwayFromZero);
                frameCount = 0;
                elapsedTime = 0;
            }
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
