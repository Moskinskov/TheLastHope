using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Management
{
    public class ObjectDictionary : MonoBehaviour
    {
        [SerializeField] List<GameObject> enemiesList = new List<GameObject>();
        [SerializeField] List<GameObject> groundList = new List<GameObject>();
        [SerializeField] List<GameObject> staticObjsList = new List<GameObject>();
        public Dictionary<string, GameObject> ObjectsDictionary = new Dictionary<string, GameObject>();

        private void Awake()
        {

        }
        // Start is called before the first frame update
        void Start()
        {
            SetupDictionary("E.", enemiesList, ObjectsDictionary);
            SetupDictionary("G.", groundList, ObjectsDictionary);
            SetupDictionary("S.", staticObjsList, ObjectsDictionary);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetupDictionary(string prefix, List<GameObject> list, Dictionary<string,GameObject> dict)
        {
            var i = 0;
            dict.Add(prefix + i, null);
            foreach (var obj in list)
            {
                i++;
                dict.Add(prefix + i, obj);
            }
        }
    }
}

