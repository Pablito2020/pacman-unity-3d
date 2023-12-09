using System.Collections.Generic;
using UnityEngine;

namespace ui
{
    public class Fruit : MonoBehaviour
    {
        
        private AudioSource audioSource;
        public delegate void FruitEaten();

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            OnFruitEaten?.Invoke();
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

        public static event FruitEaten OnFruitEaten;
    }
}