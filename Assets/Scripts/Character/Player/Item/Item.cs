using UnityEngine;
using System.Collections;
/// <summary>
/// Basic class of item
/// </summary>
public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public Texture2D Icon { get; set; }
    public string Description { get; set; }

    public Item() { }

    public virtual void InitData(int ID, string name, Texture2D icon, string description)
    {
        this.ID = ID;
        this.Name = name;
        this.Icon = icon;
        this.Description = description;
    }
}
