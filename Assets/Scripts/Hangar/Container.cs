/// Limerence Games
/// The Last Hope
/// Curator: Danny Kotov
/// Commented

using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Hangar
{
    /// <summary>
    /// Container of a carriage
    /// </summary>
    public class Container : MonoBehaviour
    {
        /// <summary>
        /// List of all hardware on the carriage
        /// </summary>
        public List<GameObject> hardwares;
        /// <summary>
        /// Positions where Hardware could be installed
        /// </summary>
        public List<Transform> hardwarePositions;
        /// <summary>
        /// How many positions of type 'square' on carriage
        /// </summary>
        public int squareTypeCount;

        private void Start()
        {
            hardwares = new List<GameObject>();
            hardwarePositions = new List<Transform>();
            for (int i = 0; i < squareTypeCount; i++)
            {
                hardwares.Add(null);
                hardwarePositions.Add(transform.GetChild(i));
            }
        }

        public void AddNewHardware(GameObject hardware, int index)
        {
            hardwares[index] = Instantiate(hardware);
            hardwares[index].transform.position = hardwarePositions[index].position;
            hardwares[index].transform.SetParent(hardwarePositions[index]);
        }
        public void RemoveHardware(int index)
        {
            Destroy(hardwares[index]);
            hardwares[index] = null;
        }
    }
}