using UnityEngine;
using System.Collections;

public class RecoverMP : Recover {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            PlayerMagic playerMagic = other.GetComponent<PlayerMagic>();
            playerMagic.ChangeMagic(RecoverValue);
            DestorySelf();
        }
    }
}
