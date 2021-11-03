using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 2.0f;
    [SerializeField]
    private float _jumpHeight = 1.0f;
    [SerializeField]
    private float _gravityValue = -9.81f;
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    [SerializeField]
    private bool _groundedPlayer;
    private Camera _mainCamera = null;
    [SerializeField]
    private float _cameraSensitivity = 2.0f;

    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        CameraMovement();
        CalculateMovement();
    }

    void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, mouseX * _cameraSensitivity);
        if (_mainCamera != null)
        {
            _mainCamera.transform.Rotate(Vector3.right, -mouseY * _cameraSensitivity, Space.Self); 
            Vector3 cameraRotation = _mainCamera.transform.localEulerAngles;
            cameraRotation.x = Mathf.Clamp(cameraRotation.x, 5f, 28f);
            _mainCamera.transform.localRotation = Quaternion.Euler(cameraRotation);
        }
    }

    void CalculateMovement()
    {
        _groundedPlayer = _controller.isGrounded;

        //if (_groundedPlayer && _playerVelocity.y < 0)
        //{
        //    _playerVelocity.y = 0f;
        //}

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move);
        _controller.Move(move * Time.deltaTime * _playerSpeed);

        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }

        if (_groundedPlayer == false)
        {
            _playerVelocity.y += _gravityValue * Time.deltaTime;
        }
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}
