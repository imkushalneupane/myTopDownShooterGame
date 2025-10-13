
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float _bulletLifeTime = 3f; //how long before bullet disappear
    //private float _timer;


    private void Start()
    {
        //_timer = 0f;
        Destroy(gameObject,_bulletLifeTime);
    }

    /*
    private void Update()
    {
        
    _timer += Time.deltaTime; //Increment to timer 

    if(_timer > _bulletLifeTime) //checks if timer is up
        {
            Destroy(gameObject); //destroyes the bullet
            _timer = 0f;
        }
    }

    */


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        //this is where you check if youer hitting enemy
        //damage enemy


    }

}
