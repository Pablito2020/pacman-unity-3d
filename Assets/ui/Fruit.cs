using UnityEngine;

namespace ui
{
    public class Fruit : MonoBehaviour
    {
        public delegate void FruitEaten();

        private void OnTriggerEnter(Collider other)
        {
            OnFruitEaten?.Invoke();
            Destroy(gameObject);
        }

        public static event FruitEaten OnFruitEaten;
    }
}