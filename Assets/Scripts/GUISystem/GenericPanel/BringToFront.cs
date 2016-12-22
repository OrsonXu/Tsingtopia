using UnityEngine;
using System.Collections;

public class BringToFront : MonoBehaviour
{
    //Set as the front when this dialogue pop up
    void OnEnable()
    {
        transform.SetAsLastSibling();
    }
}