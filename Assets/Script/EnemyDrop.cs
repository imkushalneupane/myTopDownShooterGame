
using UnityEngine;


public class EnemyDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject healthPotion;
    [SerializeField] 
    private GameObject pistolBullet;
    [SerializeField]
    private GameObject shotgunBullet;

   


    private void OnEnable()
    {
        EnemyHealth.OnEnemyDead += Drop;
    }

    private void Drop(Transform deadTransform)
    {

        // Check if this is the enemy that died
        if (deadTransform != transform) return;

        float randomValue   = Random.Range(0f, 100f); //getting random percentage change

        if (randomValue <= 20f)  //20% chance for heal portion
        {
            Instantiate(healthPotion, GetRandomizedPostion(deadTransform.position), deadTransform.transform.rotation);
            Debug.Log("Dropped HealPotion ");
        }
        else if(randomValue <= 45f)  //25% chance for pistol bullet
        {
            Instantiate(pistolBullet , GetRandomizedPostion(deadTransform.position), deadTransform.transform.rotation);
            Debug.Log ("Dropped pistol bullet");
        }
        else if(randomValue <= 60f)  //15% chance for shotgun bullet
        {
            Instantiate(shotgunBullet,deadTransform.transform.position, deadTransform.transform.rotation);
            Debug.Log("Dropped ShotGun bullet");
        }
    }

    private Vector3 GetRandomizedPostion(Vector3 basePostion)
    {
        float randomX = Random.Range(-.5f,.5f);
        float randomY = Random.Range(-.5f, .5f);
        return basePostion + new Vector3(randomX , randomY ,0);
    }

 

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDead -= Drop;
    }


}
