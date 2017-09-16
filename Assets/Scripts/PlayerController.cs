using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5f;
    private float lookSensitivity = 3f;

    private PlayerDriver driver;
    private PlayerRaycast pRaycast;
    // Use this for initialization
    void Start()
    {
        driver = GetComponent<PlayerDriver>();
        pRaycast = GetComponent<PlayerRaycast>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pRaycast.lockCursor)
        {
            if(Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            driver.Move(Vector3.zero);
            driver.Rotate(Vector3.zero);
            driver.RotateCamera(0f);
            return;
        }
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 horiMove = transform.right * xMove;
        Vector3 vertMove = transform.forward * zMove;

        Vector3 fVelocity = (horiMove + vertMove) * speed;
        driver.Move(fVelocity);

        //spins the actual character
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;
        driver.Rotate(rotation);

        //just moves the camera up or down
        float xRot = Input.GetAxisRaw("Mouse Y");
        float camRotationX = xRot * lookSensitivity;
        driver.RotateCamera(camRotationX);
    }
}
