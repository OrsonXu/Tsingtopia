using UnityEngine;
using System.Collections;

public class RecoverMP : Recover {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMagic playerMagic = other.GetComponent<PlayerMagic>();
            playerMagic.ChangeMagic(RecoverValue);
            DestorySelf();
        }
    }
}
