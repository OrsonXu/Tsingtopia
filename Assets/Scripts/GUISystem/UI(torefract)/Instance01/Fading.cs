using UnityEngine;

public class Fading : MonoBehaviour
{

    private void OnEnable()
    {
        MessageManager.StartListening("PlayerDie", InstanceOver);
    }


    private void InstanceOver()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("InstanceOver");
    }
}
