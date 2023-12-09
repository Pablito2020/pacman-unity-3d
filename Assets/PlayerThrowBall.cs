using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowBall : MonoBehaviour
{
    public GameObject ball;
    public Transform cam;
    private readonly float ballForce = 600f;
    private readonly List<GameObject> balls = new();
    private Animator _animator;

    private AudioSource audioSource;
    private bool isThrowing;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        GameUI.onGameFinished += cleanBalls;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_animator.GetBool("isThrowing"))
        {
            StartCoroutine(throwAnimation());
            StartCoroutine(throwBall());
        }

        if (Input.GetKeyDown(KeyCode.Q)) cleanBalls();
    }

    public void cleanBalls()
    {
        foreach (var ball in balls) Destroy(ball);
        balls.Clear();
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
        var newRb = newBall.GetComponent<Rigidbody>();
        var sec = GetThrowingAnimClipSeconds();
        yield return new WaitForSeconds(sec / 2);
        audioSource.Play();
        newBall.transform.position = cam.position + cam.forward * 5;
        newBall.SetActive(true);
        newRb.useGravity = true;
        newRb.AddForce(Vector3.up + cam.forward * ballForce);
        balls.Add(newBall);
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