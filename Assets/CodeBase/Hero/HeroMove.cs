using System;
using CodeBase.Infrastructure;
using CodeBase.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float movementSpeed;

        private InputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = Game.inputService;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0f;
                movementVector.Normalize();
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            characterController.Move(movementSpeed * movementVector * Time.deltaTime);
        }
    }
}