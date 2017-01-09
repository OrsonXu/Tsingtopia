using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class ItemBag : MonoBehaviour
{

    public List<Item> ItemList;
    /// <summary>
    /// Add a new item
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="name"></param>
    /// <param name="icon"></param>
    /// <param name="description"></param>
    public void AddNewItem(int ID, string name, Texture2D icon, string description)
    {
        Item item = new Item();
        item.InitData(ID, name, icon, description);
        ItemList.Add(item);
    }
    /// <summary>
    /// Remove item by ID
    /// </summary>
    /// <param name="ID">item ID</param>
    public void RemoveItem(int ID)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].ID == ID)
            {
                ItemList.Remove(ItemList[i]);
                break;
            }
        }
    }
    /// <summary>
    /// Remove the item by name
    /// </summary>
    /// <param name="name">item name</param>
    public void RemoveItem(string name)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].Name == name)
            {
                ItemList.Remove(ItemList[i]);
                break;
            }
        }
    }
    /// <summary>
    /// Save function
    /// </summary>
    /// <param name="version"></param>
    public void Save(string version)
    {
        string filePath = Application.persistentDataPath + "\\" + version + "\\ItemBagData.dat";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);

        ItemBagData data = new ItemBagData();
        data.ItemList = this.ItemList;

        bf.Serialize(file, data);
        file.Close();
    }
    /// <summary>
    /// Load in the data
    /// </summary>
    /// <param name="version"></param>
    public void Load(string version)
    {
        string filePath = Application.persistentDataPath + "\\" + version + "\\ItemBagData.dat";
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            ItemBagData data = (ItemBagData)bf.Deserialize(file);
            file.Close();

            this.ItemList = data.ItemList;
        }
    }
}

[Serializable]
class ItemBagData
{
    public List<Item> ItemList;
}