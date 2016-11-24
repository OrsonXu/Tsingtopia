using UnityEngine;
using System.Collections;

namespace Tasks
{
    public class Task
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Task() { }

        public virtual void initData(int ID, string name, string description)
        {
            this.ID = ID;
            this.Name = name;
            this.Description = description;
        }
    }
}