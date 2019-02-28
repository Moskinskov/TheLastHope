using System.Collections.Generic;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;
using UnityEngine;

namespace TheLastHope.Generators
{
    class StaticGenerator : AGenerator
    {
        private int startGenerationPosition;

        [SerializeField] private GameObject[] prop;
        [SerializeField] private int step = 1;
        [SerializeField] private Vector2 activeTerrainGeometry = new Vector2(15, 5); //Active spawn zone x[-15;15] z[-5;5]
        [SerializeField] private float railsWidth = 2;
        #region Debugging
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
            if (sceneData.Props.Count != 0)
            {
                startGenerationPosition = (int) activeTerrainGeometry.x - 1;
            }
            else
            {
                startGenerationPosition = (int)-activeTerrainGeometry.x;
            }

            for (int i = startGenerationPosition; i < activeTerrainGeometry.x; i+=step)
            {
                if (sceneData.Props.Count < sceneData.TargetPropsCount)
                {
                    float zCoordinate = Random.Range(-activeTerrainGeometry.y, activeTerrainGeometry.y);
                    
                    if (zCoordinate < sceneData.Rails[sceneData.Rails.Count - 1].transform.position.z + railsWidth &&
                        zCoordinate > sceneData.Rails[sceneData.Rails.Count - 1].transform.position.z)
                    {
                        zCoordinate += railsWidth;
                    }
                    else if (zCoordinate > sceneData.Rails[sceneData.Rails.Count - 1].transform.position.z - railsWidth &&
                             zCoordinate < sceneData.Rails[sceneData.Rails.Count - 1].transform.position.z)
                    {
                        zCoordinate -= railsWidth;
                    }

                    sceneData.Props.Add(Instantiate(
                        prop[Random.Range(0, prop.Length)],
                        new Vector3(i, 0, zCoordinate),
                        Quaternion.identity));
                }
                else
                {
                    break;
                }
            }
        }

        public override void Generate(GameObject obj, SceneData sceneData, List<Vector3> positions)
        {
            throw new System.NotImplementedException();
        }
    }
}
