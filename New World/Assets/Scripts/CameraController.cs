using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Rigidbody playerRigid;
    public GameObject player;
    private float cameraSensitivity = 5f;
    private float cameraRotationLimit = 45f;
    private float currentCameraRotation_x = 0f;

    [SerializeField] private GameObject camera;


    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = new Vector3(0f, 1f, -1f);
        camera.transform.position = player.transform.position + player.transform.rotation * distance;
        ChangeView();
    }

    void LateUpdate()
    {

    }

    void ChangeView()
    {
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity;

        currentCameraRotation_x -= mouseY;
        currentCameraRotation_x = Mathf.Clamp(currentCameraRotation_x, -cameraRotationLimit, cameraRotationLimit);

        
        // 회전
        camera.transform.localRotation = Quaternion.Euler(currentCameraRotation_x, 0f, 0f);
        camera.transform.parent.Rotate(Vector3.up * mouseX);
        
 
    }
}
