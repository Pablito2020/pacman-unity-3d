using UnityEngine;

namespace ui
{
    public class BigFruit : MonoBehaviour
    {
        public delegate void BigFruitEaten();

        private void OnTriggerEnter(Collider other)
        {
            OnBigFruitEaten?.Invoke();
            Destroy(gameObject);
        }

        public static event BigFruitEaten OnBigFruitEaten;
    }
}