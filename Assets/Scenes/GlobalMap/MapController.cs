using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float movSpeed;
    [SerializeField] public float scaleSpeed;

    private Vector3 difScale;
    private RectTransform rectT;
    void Start()
    {
        rectT = GetComponent<RectTransform>();
        difScale = new Vector3(0, 0, 0);
    }
    void ScaleControll()
    {
        float _mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (_mouseWheel != 0)
        {
            Vector3 temp = rectT.localScale;
            if (_mouseWheel > 0)
            {
                temp.x *= scaleSpeed;
                temp.y *= scaleSpeed;
            }
            else
            {
                temp.x *= 2 - scaleSpeed;
                temp.y *= 2 - scaleSpeed;
            }
            rectT.localScale = temp;
        }
    }

    void MovingController()
    {
        if (Input.GetButton("UpMap"))
        {
            difScale.y = -movSpeed * Time.deltaTime;
            rectT.transform.position += difScale;
            difScale.y = 0;
        }
        if (Input.GetButton("DownMap"))
        {
            difScale.y = movSpeed * Time.deltaTime;
            rectT.transform.position += difScale;
            difScale.y = 0;
        }
        if (Input.GetButton("LeftMap"))
        {
            difScale.x = movSpeed * Time.deltaTime;
            rectT.transform.position += difScale;
            difScale.x = 0;
        }
        if (Input.GetButton("RightMap"))
        {
            difScale.x = -movSpeed * Time.deltaTime;
            rectT.transform.position += difScale;
            difScale.x = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        ScaleControll();
        MovingController();
    }
}
