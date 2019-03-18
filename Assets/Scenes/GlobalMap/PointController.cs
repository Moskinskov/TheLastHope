using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    [SerializeField] public GameObject Arrow = null;
    [SerializeField] public List<GameObject> Neighboring;
    [SerializeField] public List<float> Distance;
    [SerializeField] public string name;
    [SerializeField] private bool curActive = false;
    [SerializeField] private bool canActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
