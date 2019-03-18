using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUI : MonoBehaviour
{
    private RectTransform rtransform;
    private Vector3 difScale;
    private float diff = 5;
    // Start is called before the first frame update
    void Start()
    {
        difScale = new Vector3(diff, diff, diff);
        
        rtransform = GetComponent<RectTransform>();
        print(rtransform.transform.position);
        print(rtransform.transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Ability1"))
        {
            print("Scale");
            print(difScale);
            rtransform.transform.position += difScale;
            print(rtransform.transform.localScale);
        }
    }
}
