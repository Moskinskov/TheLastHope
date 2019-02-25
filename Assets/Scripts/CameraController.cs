using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] [Range(1, 5)] private float cameraSpeed = 1;
    [SerializeField] [Range(5, 10)] private float zRange = 5; 
    [SerializeField] [Range(10, 20)] private float xRange = 10;

    public void CameraUpdate()
    {
        Vector3 cameraPos = Camera.main.transform.position;

        if (Input.mousePosition.x > Camera.main.pixelWidth * 0.8f && cameraPos.x < xRange)
        {
            cameraPos.x = Mathf.Lerp(cameraPos.x, cameraPos.x+4,Time.deltaTime * cameraSpeed);
        }
        else if (Input.mousePosition.x < Camera.main.pixelWidth * 0.2f && cameraPos.x > -xRange)
        {
            cameraPos.x = Mathf.Lerp(cameraPos.x, cameraPos.x - 4, Time.deltaTime * cameraSpeed);
        }

        if (Input.mousePosition.y > Camera.main.pixelHeight * 0.1f && cameraPos.z < zRange)
        {
            cameraPos.z = Mathf.Lerp(cameraPos.z, cameraPos.z, Time.deltaTime * cameraSpeed);
        }
        else if (Input.mousePosition.y < Camera.main.pixelHeight * 0.1f && cameraPos.z > -zRange)
        {
            cameraPos.z = Mathf.Lerp(cameraPos.z, cameraPos.z - 4, Time.deltaTime * cameraSpeed);
        }

        Camera.main.transform.position = cameraPos;
    }
    

    // Debug
    void Update()
    {
        CameraUpdate();
    }

}
