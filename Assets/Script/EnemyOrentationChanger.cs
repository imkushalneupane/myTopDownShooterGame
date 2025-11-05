
using UnityEngine;

public class EnemyOrentationChanger : MonoBehaviour
{
    
     Transform _player;
    [SerializeField] float _flipSpeed = 5f;

    private Vector3 _orignalScale;
    private Vector3 _flippedScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        _player = playerObj.transform;

        //cache the orignal scale
        _orignalScale = transform.localScale;

        //creating flipped verisoin (mirrored z axis)
        _flippedScale = new Vector3 (-_orignalScale.x, _orignalScale.y ,  _orignalScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null)
            return;

        //check which side is player on
        bool playerOnLeft = _player.position.x < transform.position.x;

        //calculating a side to face towards
        Vector3 targetScale = playerOnLeft ? _flippedScale : _orignalScale;

        //smoothly interpolate between current and targated one
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, _flipSpeed * Time.deltaTime);
        
    }
}
