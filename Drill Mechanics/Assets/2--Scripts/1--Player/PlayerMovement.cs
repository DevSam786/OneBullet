using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]PlayerControls playerControls;
    public Animator playerAnim;
    public float moveSpeed;
    Vector2 moveInput;
    Vector2 aim;
    Vector3 moveValues;
    bool isMovementPressed;
    bool isAimPressed;

    public SliderBarSystem slider;
    int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] bool isGamepad;
    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float gamepadRotatingSmoothing = 1000f;


    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Controls.Move.started += onMovementAction;
        playerControls.Controls.Move.performed += onMovementAction;
        playerControls.Controls.Move.canceled += onMovementAction;
        playerControls.Controls.Aim.started += OnAimAction;
        playerControls.Controls.Aim.performed += OnAimAction;
        playerControls.Controls.Aim.canceled += OnAimAction;
        currentHealth = maxHealth;
        slider.SetMaxHealth(maxHealth);
    }
    #region Inputs
    void onMovementAction(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        moveValues.x = moveInput.x;
        moveValues.z = moveInput.y;
        isMovementPressed = moveInput.x != 0 || moveInput.y != 0;
    }
    void OnAimAction(InputAction.CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
        isAimPressed = aim.x != 0 || aim.y != 0;
    }
    #endregion
    void Update()
    {
        if(isAimPressed)
        {
            HandleAim();
        }
        if (isMovementPressed)
        {
            HandleMove();     
        }
        HandleAnimation();
    }
    void HandleAim()
    {
        if(isGamepad)
        {
            if(Mathf.Abs(aim.x) > controllerDeadzone || Mathf.Abs(aim.y) > controllerDeadzone)
            {
                Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
                if(playerDirection.sqrMagnitude > 0)
                {
                    Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, gamepadRotatingSmoothing  * Time.deltaTime);
                }
            }            
        }
        else
        {
            Ray ray =  Camera.main.ScreenPointToRay(aim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;
            if(groundPlane.Raycast(ray,out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
            }
        }       
    }
    void LookAt(Vector3 point)
    {
        Vector3 heightCorrectedPoint = new Vector3(point.x,transform.position.y,point.z);
        transform.LookAt(heightCorrectedPoint);
    }
    
    void HandleMove()
    {
        if(Mathf.Abs(moveValues.x) > 0.4f || Mathf.Abs(moveValues.z) > 0.4f)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var skewedInput = matrix.MultiplyPoint3x4(moveValues).normalized;
            transform.position += skewedInput * moveSpeed * Time.deltaTime;
        }       
    }
    void HandleAnimation()
    {
        if(moveValues.sqrMagnitude > 0)
        {
            playerAnim.SetBool("IsWalking", true);
        }
        else
        {
            playerAnim.SetBool("IsWalking", false);
        }

    }

    private void OnEnable()
    {
        playerControls.Controls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Controls.Disable();
    }
    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        slider.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
