using UnityEngine;
using System.Collections;

public class PlayerKillCounter : MonoBehaviour {
    public int Length { get; set; }
    private int[] _countList;
    private bool _finished;
    /// <summary>
    /// Override, register a message event
    /// </summary>
    void OnEnable()
    {
        MessageManager.StartListening("EnemyDieWithID", AddCount);
    }
    /// <summary>
    /// Override, unregister a message event
    /// </summary>
    void OnDisable()
    {
        MessageManager.StopListening("EnemyDieWithID", AddCount);
    }
	
	void Start () {
        // Initiate the count list
        _countList = new int[Length];
        _finished = false;
        for (int i = 0; i < _countList.Length; i++)
        {
            _countList[i] = 0;
        }
	}
    /// <summary>
    /// Add the count of the corresonding enemy index
    /// </summary>
    /// <param name="EnemyID">enemy ID</param>
    public void AddCount(int EnemyID)
    {
        _countList[EnemyID]++;
    }
    /// <summary>
    /// Check whether the counter is fullfilled
    /// </summary>
    /// <returns></returns>
    public bool checkFinish()
    {
        foreach (int i in _countList)
        {
            if (i <= 1)
                return false;
        }
        return true;
    }

    public void Update()
    {
        if (!_finished && checkFinish())
        {
            //Debug.Log("Clear!!!!");
            // Trigger a event when the player achieve the requirement
            MessageManager.TriggerEvent("PlayerFinish");
            _finished = true;
        }
    }
}
