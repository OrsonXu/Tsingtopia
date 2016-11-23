using UnityEngine;
using System.Collections;

public class Task
{

	public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Task() { }

    public virtual void InitData(int ID, string name, string description)
    {
        this.ID = ID;
        this.Name = name;
        this.Description = description;
    }
}
