using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TheLastHope.Management.AbstractLayer;
using UnityEngine;


public class TurretType
{
    public string tur1 = "tur1";
    public string tur2 = "tur2";
    public string tur3 = "tur3";
    public string tur4 = "tur4";
}

public class CarrigeType
{
    public string car1 = "car1";
    public string car2 = "car2";
    public string car3 = "car3";
    public string car4 = "car4";
}

public class Carrige : MonoBehaviour
{
    public string carName;
    public List<string> turrets;
}


public class Train : MonoBehaviour
{
    public List<string> train;
}










public class SLSystem : MonoBehaviour
{
    private string fileName1 = "MapSettings";
    private string fileName2 = "TrainSettings";



    #region SLMapFile
    public void SaveMapFile(string cityName)
    {
        XmlElement element;
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("Map");
        xmlDoc.AppendChild(rootNode);

        element = xmlDoc.CreateElement("City");
        element.SetAttribute("value", cityName);
        rootNode.AppendChild(element);
        xmlDoc.Save(Application.dataPath + "/" + fileName1 + ".xml");
    }

    public string LoadMapFile()
    {
        XmlTextReader reader = new XmlTextReader(Application.dataPath + "/" + fileName1 + ".xml");
        while (reader.Read())
        {
            if (reader.IsStartElement("City"))
            {
                return reader.GetAttribute("value");
            }
        }
        return "";
    }
    #endregion



    private void AddCarrige(XmlDocument xmlDoc, XmlNode train, int index, int count)
    {
        XmlNode car = xmlDoc.CreateElement("Carrige" + index, "Type" + index);
        
        for (int i = 0; i < count; i++)
        {
            XmlElement element;
            element = xmlDoc.CreateElement("Node" + i);
            element.SetAttribute("Slot" + i, "Turret" + i);
            car.AppendChild(element);
        }
        train.AppendChild(car);

        
    }

    private void SaveTrainFile()
    {
        XmlElement element;
        

        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("GameSettings");
        

        XmlNode Train = xmlDoc.CreateElement("Train");
        AddCarrige(xmlDoc, Train, 0, 4);
        AddCarrige(xmlDoc, Train, 1, 4);
        AddCarrige(xmlDoc, Train, 2, 6);



        rootNode.AppendChild(Train);
        xmlDoc.AppendChild(rootNode);
        xmlDoc.Save(Application.dataPath + "/" + fileName2 + ".xml");
    }






    void SaveXML()
    {
        XmlNode userNode;
        XmlAttribute attribute;
        XmlElement element;

        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("GameSettings");
        xmlDoc.AppendChild(rootNode);
        
        
        /*
        element = xmlDoc.CreateElement("key_bool");
        element.SetAttribute("value", saveBool.ToString());
        rootNode.AppendChild(element);

        element = xmlDoc.CreateElement("key_float");
        element.SetAttribute("value", saveFloat.ToString());
        rootNode.AppendChild(element);

        element = xmlDoc.CreateElement("key_int");
        element.SetAttribute("value", saveInt.ToString());
        rootNode.AppendChild(element);

        userNode = xmlDoc.CreateElement("KeysArray");
        for (int i = 0; i < saveArray.Length; i++)
        {
            element = xmlDoc.CreateElement("key");
            element.SetAttribute("value", saveArray[i].ToString());
            userNode.AppendChild(element);
        }
        rootNode.AppendChild(userNode);
        */
        userNode = xmlDoc.CreateElement("Info");
        attribute = xmlDoc.CreateAttribute("Unity");
        attribute.Value = Application.unityVersion;
        userNode.Attributes.Append(attribute);
        userNode.InnerText = "Company Name: " + Application.companyName + " :: Product Name: " + Application.productName;
        rootNode.AppendChild(userNode);

        xmlDoc.Save(Application.dataPath + "/" + fileName1 + ".xml");
    }

    void LoadXML()
    {
        try
        {
            List<string> tmp = new List<string>();
            XmlTextReader reader = new XmlTextReader(Application.dataPath + "/" + fileName1 + ".xml");
            while (reader.Read())
            {
                /*
                if (reader.IsStartElement("key_int"))
                {
                    int k;
                    if (int.TryParse(reader.GetAttribute("value"), out k)) loadInt = k;
                }
                if (reader.IsStartElement("key_float"))
                {
                    float k;
                    if (float.TryParse(reader.GetAttribute("value"), out k)) loadFloat = k;
                }
                if (reader.IsStartElement("key_bool"))
                {
                    bool k;
                    if (bool.TryParse(reader.GetAttribute("value"), out k)) loadBool = k;
                }
                if (reader.IsStartElement("KeysArray"))
                {
                    while (reader.ReadToFollowing("key"))
                    {
                        tmp.Add(reader.GetAttribute("value"));
                    }
                }
                */
            }
            /*
            loadArray = new string[tmp.Count];
            for (int i = 0; i < tmp.Count; i++)
            {
                loadArray[i] = tmp[i];
            }
            */
            reader.Close();
        }

        catch (System.Exception)
        {
            Debug.Log("Ошибка чтения файла!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
