using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public GameObject GetSelectedGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        Physics.Raycast(ray, out raycastHit, Mathf.Infinity);
        if (raycastHit.collider != null)
        {
            return raycastHit.collider.gameObject;
        }
        else
        {
            return this.gameObject;
        }
    }
}
