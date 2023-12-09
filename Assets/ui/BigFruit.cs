using System.Collections.Generic;
using UnityEngine;

namespace ui
{
    public class BigFruit : MonoBehaviour
    {
        public delegate void BigFruitEaten();
        
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            OnBigFruitEaten?.Invoke();
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

        public static event BigFruitEaten OnBigFruitEaten;
    }
}