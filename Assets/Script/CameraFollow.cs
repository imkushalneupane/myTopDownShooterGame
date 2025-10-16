using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    [SerializeField]
    Vector3 _offset = new Vector3(0,0,-10);
    [SerializeField]
    private float _cameraSmoothSpeed = 17f;

   
   

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos= _player.position + _offset; //targeted position of camera

        transform.position = Vector3.Lerp(transform.position, cameraPos ,_cameraSmoothSpeed * Time.deltaTime);
        
    }
}
