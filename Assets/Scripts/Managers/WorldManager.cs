using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {

    GameObject player;
    PlayerController playerController;

    void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //playerController = player.GetComponent<PlayerController>();
        //playerController.Init(new Vector3(0f, 0f, 0f), new Vector3(0f, 90f, 0f));
        //playerController.InWorld = true;
        //playerController.PlayerPlusInit();

        MessageManager.TriggerEvent("PlayerInit", true);

        //PlayerPrefs.DeleteAll();
        //if (PlayerPrefs.HasKey("x"))
        //{
        //    float x = PlayerPrefs.GetFloat("x");
        //    float z = PlayerPrefs.GetFloat("z");
        //    player.transform.position = new Vector3(x - 1, 0f, z - 1);
        //}
    }

    
}
