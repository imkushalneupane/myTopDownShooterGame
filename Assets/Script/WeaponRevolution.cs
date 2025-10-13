
using UnityEngine;

public class WeaponRevolution : MonoBehaviour
{
    [Header("weapon")]
    [SerializeField]
    private Transform _enemyAIPos;
    [SerializeField]
    private float _revolveSpeed = 250f;
    [SerializeField]
    private float _revolveRadius = 2f;
    
    private float _angle;



    private void Start()
    {
        _angle = Random.Range(0f, 360f); // start at a random point around the enemy
    }



    // Update is called once per frame
    void Update()
    {
        WeaponRev();
    }

    private void WeaponRev()
    {
        _angle += _revolveSpeed * Time.deltaTime;
        if (_angle >= 360) //increasing angle over time
            _angle -= 360;
         float rad = _angle * Mathf.Deg2Rad; //converting degrees to radians

        //computing position relative to moving point
        float X = Mathf.Cos(rad) * _revolveRadius;
        float Y = Mathf.Sin(rad) * _revolveRadius;
        Vector3 offset = new Vector3(X, Y, 0);

        transform.position = _enemyAIPos.position + offset;

        


    }
}
