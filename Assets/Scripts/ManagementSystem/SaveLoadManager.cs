using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using TheLastHope.Helpers;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Management
{
    public static class SaveLoadManager
    {
        public static ObjectDictionary objectsDictionary { get; set; }



        public static bool Load(out GameObject playerTrain, out Player player)
        {
            string dir = Application.persistentDataPath + "/saves";

			if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

			DirectoryInfo dir_i = new DirectoryInfo(dir);
            FileInfo[] info = dir_i.GetFiles("*.save");
            foreach (FileInfo f in info)
            {
                Debug.Log(f);
            }

            if (info.Length >= 1)
            {

                BinaryFormatter bf = new BinaryFormatter();
                var createdir = Directory.CreateDirectory(Application.persistentDataPath + "/saves");
                FileStream stream = new FileStream(dir + "/player" + (info.Length) + ".save", FileMode.Open);
                stream.Position = 0;
                SerializedPlayer sp = bf.Deserialize(stream) as SerializedPlayer;
                stream.Close();
                playerTrain = sp.CreatePlayerTrain(objectsDictionary);
                player = sp.player;
                Debug.Log($"Player credit = {player.Credit}");
                return playerTrain != null;
            }
            else
            {
                Debug.Log("LOAD FROM RESOURCES. SAVE IS NOT FOUND.");
                string path = @"PlayerTrain";
                playerTrain =
                    GameObject.Instantiate(Resources.Load(path, typeof(GameObject)), new Vector3(-4.945748f, 0.3745093f, -0.979275f),
                                            Quaternion.LookRotation(Vector3.forward, Vector3.up)) as GameObject;
                player = new Player(2000);
                return playerTrain != null;
            }
        }

        public static void SavePlayer(GameObject playerTrain, Player player)
        {
            SerializedPlayer sp = new SerializedPlayer(playerTrain, player);
            BinaryFormatter bf = new BinaryFormatter();
            var createdir = Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            string dir = Application.persistentDataPath + "/saves";

            DirectoryInfo dir_i = new DirectoryInfo(dir);
            FileInfo[] info = dir_i.GetFiles("*.save");
            foreach (FileInfo f in info)
            {
                Debug.Log(f);
            }

            FileStream stream = new FileStream(dir + "/player" + (info.Length + 1) + ".save", FileMode.Create);
            stream.Position = 0;
            bf.Serialize(stream, sp);
            stream.Close();
        }

        

    }

    [Serializable]
    public class SerializedPlayer
    {
        [SerializeField]
        public SerializableVector3 ParentTransform { get; set; }
        [SerializeField]
        List<string> cars = new List<string>();
        [SerializeField]
        public List<string> locos = new List<string>();
        [SerializeField]
        public List<SerializableVector3> CarsTransform = new List<SerializableVector3>();
        [SerializeField]
        public Player player;

        public SerializedPlayer()
        {

        }

        public SerializedPlayer(GameObject playerTrain, Player player)
        {
            this.player = player;
            ParentTransform = playerTrain.transform.position;
            Debug.Log("CHC" + playerTrain.transform.childCount);
            locos.Add("Loco.1");
            CarsTransform.Add(playerTrain.transform.GetChild(0).position);
            for (var i = 1; i < playerTrain.transform.childCount; i++)
            {
                cars.Add($"Cars.{i}");
                CarsTransform.Add(playerTrain.transform.GetChild(i).position);
            }


        }

        public GameObject CreatePlayerTrain(ObjectDictionary objectsDictionary)
        {
            GameObject parent = new GameObject("PlayerTrain");
            parent.AddComponent<TrainManager>();
            parent.transform.position = ParentTransform;
            GameObject loco;
            objectsDictionary.ObjectsDictionary.TryGetValue(locos[0] ,out  loco);
            loco = GameObject.Instantiate( loco, CarsTransform.ToArray()[0],
                                            Quaternion.LookRotation(Vector3.right, Vector3.up)) as GameObject;
            loco.transform.SetParent(parent.transform);

            var carNumber = 1; //Because loco is 0
            SerializableVector3[] carsTransform = CarsTransform.ToArray();
            GameObject[] carsIstances = new GameObject[CarsTransform.Count];
            carsIstances[0] = loco;
            foreach (var car in cars)
            {
                GameObject carObject;
                objectsDictionary.ObjectsDictionary.TryGetValue(cars[0], out carObject);
                carObject = GameObject.Instantiate(carObject, carsTransform[carNumber],
                                                Quaternion.LookRotation(Vector3.right, Vector3.up)) as GameObject;
                carObject.transform.SetParent(parent.transform);
                carsIstances[carNumber] = carObject;

                carNumber++;
            }
            for(var i=1;i< carsIstances.Length;i++)
            {
                carsIstances[i].GetComponent<Carriage>().Connector = carsIstances[i - 1].transform.Find("Connector").gameObject;
            }
            return parent;
        }
    }
}

