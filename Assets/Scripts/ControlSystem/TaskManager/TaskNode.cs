using UnityEngine;
using System.Collections;


public class TaskNode:MonoBehaviour
{

    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public TaskNode() { }

    public virtual void initData(int ID, string name, string description)
    {
        this.ID = ID;
        this.Name = name;
        this.Description = description;
    }
}
