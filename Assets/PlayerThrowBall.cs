using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowBall : MonoBehaviour
{
    public GameObject ball;
    public Transform cam;
    private readonly float ballForce = 600f;
    private Animator _animator;
    private readonly List<GameObject> balls = new();
    private bool isThrowing;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        GameUI.onGameFinished += () =>
        {
            foreach (var ball in balls) Destroy(ball);
            balls.Clear();
        };
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("f") && !_animator.GetBool("isThrowing"))
        {
            StartCoroutine(throwAnimation());
            StartCoroutine(throwBall());
        }
    }


    private IEnumerator<WaitForSeconds> throwAnimation()
    {
        _animator.SetBool("isThrowing", true);
        yield return new WaitForSeconds(GetThrowingAnimClipSeconds());
        _animator.SetBool("isThrowing", false);
    }

    private IEnumerator<WaitForSeconds> throwBall()
    {
        if (isThrowing) yield break;
        isThrowing = true;
        var newBall = Instantiate(ball);
        balls.Add(newBall);
        var newRb = newBall.GetComponent<Rigidbody>();
        var sec = GetThrowingAnimClipSeconds();
        yield return new WaitForSeconds(sec / 2);
        newBall.transform.position = cam.position + cam.forward * 5;
        newBall.SetActive(true);
        newRb.useGravity = true;
        newRb.AddForce(Vector3.up + cam.forward * ballForce);
        isThrowing = false;
    }

    public float GetThrowingAnimClipSeconds()
    {
        var clips = _animator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
            switch (clip.name)
            {
                case "Throw":
                    return clip.length;
            }

        throw new Exception("Clip not found");
    }
}