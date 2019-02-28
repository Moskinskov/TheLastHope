using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Generators
{
    class PropsGenerator : AGenerator
    {
        private int startGenerationPosition;

        #region Debugging
        [SerializeField] private GameObject[] prop;
        [SerializeField] private int step = 1;
        [SerializeField] private Vector2 activeTerrainGeometry = new Vector2(15, 5); //Active spawn zone x[-15;15] z[-5;5]
        //void Start()
        //{
        //    SceneData sceneData = new SceneData();
        //    sceneData.rails.Add(new GameObject());
        //    sceneData.rails[0].transform.position = Vector3.zero;
        //    Generate(sceneData);
        //}
        #endregion

        public override void Generate(SceneData sceneData)
        {
            if( sceneData.targetPropsCount > sceneData.props.Count)
            {
                startGenerationPosition = (int)activeTerrainGeometry.x;

                var stepPos = startGenerationPosition;
                for (var i= sceneData.props.Count; i< sceneData.targetPropsCount;i++)
                {
                    float zCoordinate = Random.Range(-activeTerrainGeometry.y, activeTerrainGeometry.y);

                    //if props cross the rails; rails' width is [-5;5]
                    if (zCoordinate < sceneData.rails[sceneData.rails.Count - 1].transform.position.z + 5 &&
                        zCoordinate > sceneData.rails[sceneData.rails.Count - 1].transform.position.z)
                    {
                        zCoordinate += 5;
                    }
                    else if (zCoordinate > sceneData.rails[sceneData.rails.Count - 1].transform.position.z - 5 &&
                             zCoordinate < sceneData.rails[sceneData.rails.Count - 1].transform.position.z)
                    {
                        zCoordinate -= 5;
                    }

                    sceneData.props.Add(Instantiate(
                        prop[Random.Range(0, prop.Length)],
                        new Vector3(stepPos, 0, zCoordinate),
                        Quaternion.identity));
                    stepPos += step;
                }
            }
            
        }

        public override void Generate(GameObject gameObject, SceneData sceneData, List<Vector3> positions)
        {
            throw new System.NotImplementedException();
        }
    }
}
