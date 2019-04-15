using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ResourceLoadableObj : MonoBehaviour
{
    [SerializeField] GameObject thisObj;
    public GameObject ThisObj { get { return ThisObj; } set { thisObj = value; } }

}
