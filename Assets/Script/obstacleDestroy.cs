using UnityEngine;

public class obstacleDestroy : MonoBehaviour
{

    [SerializeField] private GameObject destroyEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
