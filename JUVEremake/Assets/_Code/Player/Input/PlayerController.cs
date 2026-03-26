using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionsHolder inputActionsHolder;
    private GameInputActions _inputActions;
    private CharacterController _characterController;
    private SpriteRenderer _spriteRenderer;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3.0f;
    private Vector2 _movementDirection;
    private Vector2 _currentInput;

    private void OnDestroy()
    {
        _inputActions.Player.Interact.performed -= InteractPlayer;
    }

    private void Start()
    {
        Prepare();
    }

    private void Prepare()
    {
        _characterController = GetComponent<CharacterController>();
        _inputActions = inputActionsHolder._GameInputActions;
        _inputActions.Player.Interact.performed += InteractPlayer;
    }

    void Update()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        _currentInput = _inputActions.Player.Movement.ReadValue<Vector2>();

        float horizontal = _currentInput.x;
        float vertical = _currentInput.y;

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            vertical = 0;
            horizontal = Mathf.Sign(horizontal); 
        }
        else if (Mathf.Abs(vertical) > 0)
        {
            horizontal = 0;
            vertical = Mathf.Sign(vertical);
        }

        _movementDirection = new Vector2(horizontal, vertical);

        Vector3 move = new Vector3(_movementDirection.x, _movementDirection.y, 0);
        _characterController.Move(move * moveSpeed * Time.deltaTime);
    }
    private void InteractPlayer(InputAction.CallbackContext ctx)
    {
        Debug.Log("InteractPlayer");
    }
}
