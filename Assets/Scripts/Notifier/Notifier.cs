using UnityEngine;
using System.Collections;

public class Notifier : MonoBehaviour
{

    GameObject player;
    ItemBag itemBag;
    TaskTable taskTable;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Pl\ayer");
        itemBag = player.GetComponent<ItemBag>();
        taskTable = player.GetComponent<TaskTable>();
    }
    /// <summary>
    /// Add a new item into the itemBag
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="name"></param>
    /// <param name="icon"></param>
    /// <param name="description"></param>
    public void notify_item_add(int ID, string name, Texture2D icon, string description)
    {
        itemBag.AddNewItem(ID, name, icon, description);
    }
    /// <summary>
    /// Remove the item with regrad to its ID
    /// </summary>
    /// <param name="ID"></param>
    public void notify_item_remove(int ID)
    {
        itemBag.RemoveItem(ID);
    }
    /// <summary>
    /// Remove the item with regrad to its name
    /// </summary>
    /// <param name="name"></param>
    public void notify_item_remove(string name)
    {
        itemBag.RemoveItem(name);
    }
    /// <summary>
    /// Add a new task into the task table
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    public void notify_task_add(int ID, string name, string description)
    {
        taskTable.AddNewTask(ID, name, description);
    }
    /// <summary>
    /// Remove the task with regrad to its ID
    /// </summary>
    /// <param name="ID"></param>
    public void notify_task_remove(int ID)
    {
        taskTable.RemoveTask(ID);
    }
    /// <summary>
    /// Remove the task with regrad to its name
    /// </summary>
    /// <param name="name"></param>
    public void notify_task_remove(string name)
    {
        taskTable.RemoveTask(name);
    }
}
