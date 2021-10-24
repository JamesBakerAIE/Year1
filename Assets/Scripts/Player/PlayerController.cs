using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Movement")]
        public CharacterController controller = null;
        [SerializeField] private Animator playerAnimator = null;

        [HideInInspector] public Vector3 move = Vector3.zero;
        [SerializeField] private float walkSpeed = 0f;


        [Header("Player Looking")]
        [SerializeField] private Transform playerCamera = null;
        [SerializeField] private int mouseSensitivity = 0;
        private float cameraPitch = 0f;


        [Header("Player Misc.")]
        [SerializeField] private SphereCollider noiseCollider = null;
        [SerializeField] private float noiseRadius = 0f;

        private void Start()
        {

        }

        private void Update()
        {

        }
    }
}
