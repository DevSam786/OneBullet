using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Variables")]
    public float moveSpeed;
    public Transform orienation;
    public float groundDrag;
    public LayerMask whatIsGround;
    public float playerHeight;
    public Transform rayStartingPoint;

    [Header("Slider")]
    public float maxSlideTime;
    public float slideForce;
    float timerSlide;
    
    bool sliding;
    [Header("Input")]
    public KeyCode slideControl = KeyCode.LeftControl;
    //Attributes

    bool isGrounded;

    Vector3 moveInput;
    Rigidbody rb;
    Vector3 moveDir;
    Vector3 skewedInput;
    Vector2 aim;
    // Start is called before the first frame update
    void Start()
    {        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        timerSlide = maxSlideTime;        
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        Aim();
        GroundCheck();
        if (Input.GetKeyDown(slideControl))
        {
            StartSliding();
        }
       
    }
    private void FixedUpdate()
    {
        Move();
        if (sliding == true)
        {
            SlidingMovement();
        }
    }
    void GatherInput()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
       
    }
    void GroundCheck()
    {
        isGrounded = Physics.Raycast(rayStartingPoint.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
   
    void Move()
    {
        moveDir = moveInput.x * Vector3.right + moveInput.z * Vector3.forward;
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        skewedInput = matrix.MultiplyPoint3x4(moveDir).normalized;
        rb.AddForce(skewedInput.normalized * moveSpeed * 10f, ForceMode.Force);
    } 
    #region Aiming
    void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if(groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            LookAt(point);                
        }
    }
    void LookAt(Vector3 point)
    {
        Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);        
        transform.LookAt(heightCorrectedPoint);
    }
    #endregion
    #region Sliding
    void StartSliding()
    {
        sliding = true;
    }
    void SlidingMovement()
    {
        rb.AddForce(skewedInput.normalized * slideForce, ForceMode.Force);
        timerSlide -= Time.deltaTime;
        if(timerSlide <= 0)
        {
            StopSliding();
            timerSlide = maxSlideTime;
        }
    }
    void StopSliding()
    {
        sliding = false;
    }
    #endregion
}
