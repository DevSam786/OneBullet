using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    Vector3 moveInput;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.AddForce(moveInput * moveSpeed * 10f, ForceMode.Force);
    }
    void GatherInput()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
    }
}
