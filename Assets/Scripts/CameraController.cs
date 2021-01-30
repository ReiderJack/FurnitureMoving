using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float movementSpeed = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;


    private Transform _thisTransform;

    private void Start()
    {

        _thisTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        _thisTransform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        
        if(Input.GetKey(KeyCode.RightArrow))
        {
            _thisTransform.Translate(new Vector3(movementSpeed * Time.deltaTime,0,0));
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            _thisTransform.Translate(new Vector3(-movementSpeed * Time.deltaTime,0,0));
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            _thisTransform.Translate(new Vector3(0, 0,-movementSpeed * Time.deltaTime));
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            _thisTransform.Translate(new Vector3(0,0,movementSpeed * Time.deltaTime));
        }
    }
}
