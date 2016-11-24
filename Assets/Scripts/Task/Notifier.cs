using UnityEngine;
using System.Collections;


public class Notifier : MonoBehaviour
{

    GameObject player;
    ItemBag itemBag;
    TaskTable taskTable;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        itemBag = player.GetComponent<ItemBag>();
        taskTable = player.GetComponent<TaskTable>();
    }

    // Add a new item into the itemBag

    public void notifyItemAdd(int ID, string name, Texture2D icon, string description)
    {
        itemBag.AddNewItem(ID, name, icon, description);
    }

    // Remove the item with regrad to its ID

    public void notifyItemRemove(int ID)
    {
        itemBag.RemoveItem(ID);
    }

    // Remove the item with regrad to its name

    // <param name="name"></param>
    public void notifyItemRemove(string name)
    {
        itemBag.RemoveItem(name);
    }

    // Add a new task into the task table


    public void notifyTaskAdd(int ID, string name, string description)
    {
        taskTable.addNewTask(ID, name, description);
    }

    // Remove the task with regrad to its ID


    public void notifyTaskRemove(int ID)
    {
        taskTable.removeTask(ID);
    }

    // Remove the task with regrad to its name

    public void notifyTaskRemove(string name)
    {
        taskTable.removeTask(name);
    }
}
