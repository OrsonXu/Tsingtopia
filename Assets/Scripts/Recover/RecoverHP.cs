using UnityEngine;
using System.Collections;

public class RecoverHP : Recover {

    /// <summary>
    /// Recover the health value when entering the trigger
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            //PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            //playerHealth.ChangeHealth(RecoverValue);
            MessageManager.TriggerEvent("PlayerChangeHealth", RecoverValue);
            DestorySelf();
        }
    }

}
