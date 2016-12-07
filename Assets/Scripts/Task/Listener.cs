using UnityEngine;
using System.Collections;

public class Listener : MonoBehaviour {
    private TaskManager tskmnger;
    private TaskNode tsknd;

    // get task manager
    void Awake()
    {
        tskmnger = TaskManager.Instance();
    }
    // set the bounded tasknode of listener
    void setTaskNode(TaskNode tn)
    {
        tsknd = tn;
    }
    // try to update the state of a tasknode
    void updateTaskNode()
    {
        if (tsknd)
            tskmnger.tryUpdate(tsknd);       
    }
    
}

public class EventListener : Listener
{

}

public class KillsListener : Listener
{

}

public class RangeListener : Listener
{

}

public class TalkNPCListener : Listener
{

}