using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float moveX;
    float moveY;
    float moveZ;
    Vector3 cameraPos;

    private void Update()
    {
        CameraInput();
    }

    void CameraInput()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            moveX = -0.05f;
            cameraPos.x = moveX;
            if (Camera.main.transform.position.x <= -14)
            {
                cameraPos.x = -14;
            }
            else
            {
                transform.Translate(cameraPos.x, 0, 0, Space.World);
            }
        }

        if (Input.GetKey(KeyCode.C))
        {
            moveX = +0.05f;
            cameraPos.x = moveX;
            if (Camera.main.transform.position.x >= 14)
            {
                cameraPos.x = 14;
            }
            else
            {
                transform.Translate(cameraPos.x, 0, 0, Space.World);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveZ = +0.05f;
            cameraPos.z = moveZ;
            if (Camera.main.transform.position.z >= 14)
            {
                cameraPos.z = 14;
            }
            else
            {
                transform.Translate(0, 0, cameraPos.z, Space.World);
            }
        }

        if (Input.GetKey(KeyCode.X))
        {
            moveZ = -0.05f;
            cameraPos.z = moveZ;
            if (Camera.main.transform.position.z <= -14)
            {
                cameraPos.z = -14;
            }
            else
            {
                transform.Translate(0, 0, cameraPos.z, Space.World);
            }
        }
    }
}
