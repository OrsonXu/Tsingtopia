//*****
// Depricated

//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using System;


//public class TaskTable : MonoBehaviour
//{

//    public List<Task> TaskList;

//    public void addNewTask(int ID, string name, string description)
//    {
//        Task task = new Task();
//        task.initData(ID, name, description);
//        TaskList.Add(task);
//    }

//    public void removeTask(int ID)
//    {
//        for (int i = 0; i < TaskList.Count; i++)
//        {
//            if (TaskList[i].ID == ID)
//            {
//                TaskList.Remove(TaskList[i]);
//                break;
//            }
//        }
//    }

//    public void removeTask(string name)
//    {
//        for (int i = 0; i < TaskList.Count; i++)
//        {
//            if (TaskList[i].Name == name)
//            {
//                TaskList.Remove(TaskList[i]);
//                break;
//            }
//        }
//    }

//    public void save(string version)
//    {
//        string filePath = Application.persistentDataPath + "\\" + version + "\\TaskTableData.dat";
//        BinaryFormatter bf = new BinaryFormatter();
//        FileStream file = File.Create(filePath);

//        TaskTableData data = new TaskTableData();
//        data.TaskList = this.TaskList;

//        bf.Serialize(file, data);
//        file.Close();
//    }

//    public void load(string version)
//    {
//        string filePath = Application.persistentDataPath + "\\" + version + "\\TaskTableData.dat";
//        if (File.Exists(filePath))
//        {
//            BinaryFormatter bf = new BinaryFormatter();
//            FileStream file = File.Open(filePath, FileMode.Open);
//            TaskTableData data = (TaskTableData)bf.Deserialize(file);
//            file.Close();

//            this.TaskList = data.TaskList;
//        }
//    }
//}

//[Serializable]
//class TaskTableData
//{
//    public List<Task> TaskList;
//}