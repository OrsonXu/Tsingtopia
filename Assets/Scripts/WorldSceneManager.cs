using UnityEngine;
using System.Collections;

public class WorldSceneManager : MonoBehaviour {

    GameObject player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (PlayerPrefs.HasKey("x"))
        {
            float x = PlayerPrefs.GetFloat("x");
            float z = PlayerPrefs.GetFloat("z");
            player.transform.position = new Vector3(x - 1, 0f, z - 1);
        }
    }

    
}
