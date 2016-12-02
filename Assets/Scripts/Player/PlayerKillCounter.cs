using UnityEngine;
using System.Collections;

public class PlayerKillCounter : MonoBehaviour {
    public int length { get; set; }
    private int[] CountList;

	// Use this for initialization
	void Start () {
        CountList = new int[length];
        for (int i = 0; i < CountList.Length; i++)
        {
            CountList[i] = 0;
        }
	}

    public void AddCount(int EnemyID)
    {
        CountList[EnemyID]++;
    }
}
