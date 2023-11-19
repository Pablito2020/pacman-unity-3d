using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceScript : MonoBehaviour
{
    Animator _animator;
    private Rigidbody _rb;
    private const float RotationSpeed = 3.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Walk();
    }

    void Rotate()
    {
        _rb.angularVelocity = getAngularRotation();
    }


    private Vector3 getAngularRotation()
    {
        if (IsRotatingLeft()) return new Vector3(0.0f, -RotationSpeed, 0.0f);
        if (IsRotatingRight()) return new Vector3(0.0f, RotationSpeed, 0.0f);
        return Vector3.zero;
    }
    
    private static bool IsRotatingLeft()
    {
        return Input.GetKey("a");
    }
    
    private static bool IsRotatingRight()
    {
        return Input.GetKey("d");
    }


    private void Walk()
    {
        if (IsGoingForward())
            _rb.velocity = 5.0f * transform.forward;
        _animator.SetBool("isWalking", IsGoingForward());
    }

    private static bool IsGoingForward()
    {
        return Input.GetKey("w");
    }
}
