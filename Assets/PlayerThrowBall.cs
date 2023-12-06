using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowBall : MonoBehaviour
{

    public GameObject ball;
    public Transform cam;
    private float ballForce = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            var newBall = Instantiate(ball);
            newBall.SetActive(true);
            var newRb = newBall.GetComponent<Rigidbody>();
            // TODO: This 3.78 is a hack about where is the camera position
            ball.transform.position = cam.position + new Vector3(0, 0, 3.78f);
            newRb.AddForce(cam.position + cam.forward * ballForce);
        }
    }
}
