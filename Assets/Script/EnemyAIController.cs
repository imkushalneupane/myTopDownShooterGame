using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    [Header("emenyAI")]
    [SerializeField]
    private GameObject _player; //player refrence
    [SerializeField]
    private float _followSpeed = 2f;


    private void Start()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }


    // Update is called once per frame
    void Update()
    {
        //movetowars player
        transform.position = Vector2.MoveTowards(
            transform.position,
            _player.transform.position ,
            _followSpeed* Time.deltaTime
           );
        

        
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _followSpeed = 0f;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
        {
            _followSpeed = 2f;
        }
    }

}
