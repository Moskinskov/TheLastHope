using System;
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
    public void SaveMapFile(PointController[] map, string prevLvlName, string nextLvlName)
    {
        XmlNode userNode;
        XmlAttribute attribute;
        XmlNode city;
        XmlElement element;
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode temp;

        XmlNode rootNode = xmlDoc.CreateElement("MapSetting");
        xmlDoc.AppendChild(rootNode);

        //Записываем название уровня предыдущего и следующего
        temp = xmlDoc.CreateElement("Lvl");
        element = xmlDoc.CreateElement("prevLvlName");
        element.SetAttribute("value", prevLvlName);
        temp.AppendChild(element);
        element = xmlDoc.CreateElement("nextLvlName");
        element.SetAttribute("value", nextLvlName);
        temp.AppendChild(element);
        rootNode.AppendChild(temp);

        temp = xmlDoc.CreateElement("Map");
        rootNode.AppendChild(temp);

        foreach (PointController p in map)
        {
            city = xmlDoc.CreateElement("City");
            element = xmlDoc.CreateElement("Name");
            element.SetAttribute("value", p.name);
            city.AppendChild(element);

            element = xmlDoc.CreateElement("IsOpen");
            element.SetAttribute("value", p.IsOpenPoint.ToString());
            city.AppendChild(element);

            element = xmlDoc.CreateElement("IsKey");
            element.SetAttribute("value", p.IsKeyPoint.ToString());
            city.AppendChild(element);

            element = xmlDoc.CreateElement("IsStart");
            element.SetAttribute("value", p.IsStartPoint.ToString());
            city.AppendChild(element);

            element = xmlDoc.CreateElement("IsBlock");
            element.SetAttribute("value", p.IsBlockPoint.ToString());
            city.AppendChild(element);

            temp.AppendChild(city);
        }

        xmlDoc.Save(Application.dataPath + "/" + fileName1 + ".xml");
    }

    public void LoadMapFile(PointController[] map)
    {
        XmlTextReader reader = new XmlTextReader(Application.dataPath + "/" + fileName1 + ".xml");
        int index = 0;
        while (reader.Read())
        {
            if (reader.IsStartElement("Name"))
            {
                //print(reader.GetAttribute("value"));
            }
            if (reader.IsStartElement("IsOpen"))
            {
                map[index].IsOpenPoint = Convert.ToBoolean(reader.GetAttribute("value"));
            }
            if (reader.IsStartElement("IsKey"))
            {
                map[index].IsKeyPoint = Convert.ToBoolean(reader.GetAttribute("value"));
            }
            if (reader.IsStartElement("IsStart"))
            {
                map[index].IsStartPoint = Convert.ToBoolean(reader.GetAttribute("value"));
            }
            if (reader.IsStartElement("IsBlock"))
            {
                map[index].IsBlockPoint = Convert.ToBoolean(reader.GetAttribute("value"));
                index += 1;
            }
        }
        reader.Close();
    }

    public string LoadNextLvlName()
    {
        string name = "";
        XmlTextReader reader = new XmlTextReader(Application.dataPath + "/" + fileName1 + ".xml");
        while (reader.Read())
        {
            if (reader.IsStartElement("nextLvlName"))
            {
                name = reader.GetAttribute("value");
            }
        }
        reader.Close();
        return name;
    }

    public string LoadPrevLvlName()
    {
        string name = "";
        XmlTextReader reader = new XmlTextReader(Application.dataPath + "/" + fileName1 + ".xml");
        while (reader.Read())
        {
            if (reader.IsStartElement("prevLvlName"))
            {
                name = reader.GetAttribute("value");
            }
        }
        reader.Close();
        return name;
    }

    public void SaveNextLvlName(string name)
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(Application.dataPath + "/" + fileName1 + ".xml");
        foreach (XmlElement p in xDoc.GetElementsByTagName("nextLvlName"))
        {
            p.SetAttribute("value", name);
        }
        xDoc.Save(Application.dataPath + "/" + fileName1 + ".xml");
    }

    public void SavePrevLvlName(string name)
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(Application.dataPath + "/" + fileName1 + ".xml");
        foreach (XmlElement p in xDoc.GetElementsByTagName("prevLvlName"))
        {
            p.SetAttribute("value", name);
        }
        xDoc.Save(Application.dataPath + "/" + fileName1 + ".xml");
    }
    #endregion

        #region DontWork code
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

    #endregion
}
