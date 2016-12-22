using UnityEngine;
using System.Collections;

public class RecoverHP : Recover {


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.ChangeHealth(RecoverValue);
            DestorySelf();
        }
    }

}
