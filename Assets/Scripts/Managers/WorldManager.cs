using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {

    GameObject player;
    PlayerManager playerController;

    void Awake()
    { 
        
        //playerController = player.GetComponent<PlayerManager>();
        //playerController.Init(new Vector3(0f, 0f, 0f), new Vector3(0f, 90f, 0f));
        //playerController.InWorld = true;
        //playerController.PlayerPlusInit();

        //MessageManager.TriggerEvent("PlayerInit", true);
        

        //PlayerPrefs.DeleteAll();
        //if (PlayerPrefs.HasKey("x"))
        //{
        //    float x = PlayerPrefs.GetFloat("x");
        //    float z = PlayerPrefs.GetFloat("z");
        //    player.transform.position = new Vector3(x - 1, 0f, z - 1);
        //}
    }

    void Start()
    {
        GenericDialogueManager.Instance().SetDisactive();
        MessageManager.TriggerEvent("PlayerInit", true);
        //PlayerPrefs.DeleteKey("x");
        //PlayerPrefs.DeleteKey("z");
        if (PlayerPrefs.HasKey("x"))
        {
            float x = PlayerPrefs.GetFloat("x");
            float z = PlayerPrefs.GetFloat("z");
            Debug.Log("X : " + x.ToString() + "Z : " + z.ToString() + "Using the playerPref value");
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(x + 10f, 0, z);
        }
        else
        {
            TaskManager.activeListAdd(1);
            TaskManager.TriggerTask("Task1Trigger");
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("x");
        PlayerPrefs.DeleteKey("z");
    }

    
}
