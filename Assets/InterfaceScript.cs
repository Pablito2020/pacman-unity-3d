using UnityEngine;

public class InterfaceScript : MonoBehaviour
{
    private const float RotationSpeed = 50.0f;
    private const float WalkSpeed = 5.0f;

    private Animator _animator;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_animator.GetBool("isThrowing")) return;
        Rotate();
        Walk();
    }

    private void Rotate()
    {
        transform.Rotate(getAngularRotation());
        _animator.SetBool("isRotatingRight", Input.GetKey("d"));
        _animator.SetBool("isRotatingLeft", Input.GetKey("a"));
    }


    private Vector3 getAngularRotation()
    {
        var x = Input.GetAxis("Horizontal");
        return new Vector3(0, x * Time.deltaTime * RotationSpeed, 0);
    }

    private void Walk()
    {
        var y = Input.GetAxis("Vertical");
        transform.Translate(0, 0, y * Time.deltaTime * WalkSpeed);
        _animator.SetBool("isWalking", IsGoingForward());
        _animator.SetBool("isGoingBack", IsGoingBackwards());
    }

    private static bool IsGoingForward()
    {
        return Input.GetKey("w");
    }
    
    private static bool IsGoingBackwards()
    {
        return Input.GetKey("s");
    }
}