using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowBall : MonoBehaviour
{
    public GameObject ball;
    public Transform cam;
    private readonly float ballForce = 10f;
    private Animator _animator;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("f") && !_animator.GetBool("isThrowing"))
        {
            var newBall = Instantiate(ball);
            newBall.SetActive(true);
            var newRb = newBall.GetComponent<Rigidbody>();
            // TODO: This 3.78 is a hack about where is the camera position
            ball.transform.position = cam.position + new Vector3(0, 0, 3.78f);
            newRb.AddForce(cam.position + cam.forward * ballForce);
            StartCoroutine(setThrowingToFalse());
        }
    }
    
    
    private IEnumerator<WaitForSeconds> setThrowingToFalse()
    {
        _animator.SetBool("isThrowing", true);
        yield return new WaitForSeconds(GetThrowingAnimClipSeconds());
        _animator.SetBool("isThrowing", false);
    }
    
    
    public float GetThrowingAnimClipSeconds()
    {
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            switch(clip.name)
            {
                case "Throw":
                    return clip.length;
                default:
                    break;
            }
        }
        throw new Exception("Clip not found");
    } 
    
}