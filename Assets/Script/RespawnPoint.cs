using UnityEngine;
using System.Collections;
public class RespawnPoint : MonoBehaviour
{
    private Vector3 currentCheckpoint = new Vector3(-71, -48, 0);

    public Transform spawnPoint;
    private float respawnDelay = 3f;
    private int i;
  
    void Start()
    {

     transform.position = currentCheckpoint;

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            currentCheckpoint = transform.position;
        }
    }

     IEnumerator RespawnAtCheckpoint()
{
    yield return new WaitForSeconds(respawnDelay);
    transform.position = currentCheckpoint;
}


}
