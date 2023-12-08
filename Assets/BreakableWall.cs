using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.name.Contains("Ball")) return;
        Destroy(gameObject);
    }
}