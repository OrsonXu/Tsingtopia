using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeSceneTest : MonoBehaviour {

    GameObject player;
    void OnTriggerEnter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.LoadScene("Instance01");
        PlayerPrefs.SetFloat("x", player.transform.position.x);
        PlayerPrefs.SetFloat("z", player.transform.position.z);
        SceneManager.LoadScene("Instance01");
    }

}
