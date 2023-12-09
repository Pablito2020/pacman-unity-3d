using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.name.Contains("Ball")) return;
        StartCoroutine(PlayAndDestroy());
    }
    
    
        IEnumerator<WaitForSeconds> PlayAndDestroy()
        { 
            var waitValue = (audioSource.clip.length/2);
            audioSource.Play();
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(waitValue);
            Destroy(gameObject);
        } 
}