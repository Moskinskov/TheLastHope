using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Hangar
{
    public class Container : MonoBehaviour
    {
        public List<GameObject> hardwares;                        //list of hardwares in carriage
        public List<Transform> weaponPositions;                   //available weapon positions
        public int squareTypeCount;                               //qouatity of positions with type TypePosition.square
        void Start()
        {
            hardwares = new List<GameObject>();
            weaponPositions = new List<Transform>();
            for (int i = 0; i < squareTypeCount; i++)                        
            {
                hardwares.Add(null);
                weaponPositions.Add(transform.GetChild(i));
            }
        }

        public void AddNewHardware(GameObject hardware, int index)
        {
            hardwares[index] = Instantiate(hardware);
            hardwares[index].transform.position = weaponPositions[index].position;
            hardwares[index].transform.SetParent(weaponPositions[index]);
        }
        public void RemoveHardware(int index)
        {
            print("Remove");
            Destroy(hardwares[index]);
            hardwares[index] = null;
        }
    }
}