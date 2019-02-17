using System;
using System.Collections.Generic;
using TheLastHope.Data;
using UnityEngine;
namespace TheLastHope.Generators
{
    class RailGenerator : AGenerator
    {
        [SerializeField] private GameObject _rails;
        [SerializeField][Range(10,20)] private float railLength;
        [SerializeField] private float startTerrainCoordinate;    //if Terrain is located in x[-15; 15] startTerrainCoordinate will be -15
        [SerializeField] private int railsCount = 3;
        #region Debugging; start without GameManager
        //void Start()
        //{
        //    SceneData sceneData = new SceneData();
        //    Generate(sceneData);
        //}
        #endregion

        public override void Generate(SceneData sceneData)
        {
            //first rail
            _rails.GetComponentsInChildren<Transform>()[1].localScale = new Vector3(railLength, 0.5f, 0.5f);
            _rails.GetComponentsInChildren<Transform>()[1].localPosition = new Vector3(railLength / 2, 0, 1);
            //second rail
            _rails.GetComponentsInChildren<Transform>()[2].localScale = new Vector3(railLength, 0.5f, 0.5f);
            _rails.GetComponentsInChildren<Transform>()[2].localPosition = new Vector3(railLength / 2, 0, -1);

            if (sceneData.rails.Count == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    sceneData.rails.Add(Instantiate(
                                _rails,
                                new Vector3(startTerrainCoordinate+(i * railLength), 0, 0),
                                Quaternion.identity));
                }
            }
            else if(sceneData.rails.Count < railsCount)
            {
                sceneData.rails.Add(Instantiate(
                                _rails,
                                new Vector3(sceneData.rails[sceneData.rails.Count-1].transform.position.x + railLength, 0, 0),
                                Quaternion.identity));
            }
        }
    

        public override void Generate(GameObject obj, SceneData sceneData, List<Vector3> positions)
        {
            throw new NotImplementedException();
        }
    }
}
