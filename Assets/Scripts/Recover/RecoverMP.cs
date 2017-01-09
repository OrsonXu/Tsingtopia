using UnityEngine;
using System.Collections;

public class RecoverMP : Recover {
    /// <summary>
    /// Recover the health value when entering the trigger
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            //PlayerMagic playerMagic = other.GetComponent<PlayerMagic>();
            //playerMagic.ChangeMagic(RecoverValue);
            MessageManager.TriggerEvent("PlayerChangeMagic", RecoverValue);
            DestorySelf();
        }
    }
}
