using UnityEngine;

public class NoDespwanDropItems : MonoBehaviour
{


    [Header("Floating Animation")]
    [SerializeField] float floatHeight = 0.2f;
    [SerializeField] float floatSpeed = 2.5f;

    private Vector3 startPos;
    private float randomOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // Store starting position and add random offset for variety
        startPos = transform.position;
        randomOffset = Random.Range(0f, Mathf.PI * 2f); // Random phase offset
    }

    void Update()
    {
        // Calculate floating motion using sine wave
        float newY = startPos.y + Mathf.Sin((Time.time * floatSpeed) + randomOffset) * floatHeight;

        // Apply the floating motion while keeping X and Z positions
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}