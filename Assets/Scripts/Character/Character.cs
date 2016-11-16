using UnityEngine;
using System.Collections;

abstract public class Character : MonoBehaviour {

    /// <summary>
    /// The status set of any character
    /// </summary>
    public enum status { Idle, Move, Attack, Dead };

    /// <summary>
    /// The health value of the character
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// The speed of movement of the character
    /// </summary>
    public int MoveSpeed { get; set; }

    /// <summary>
    /// The list of items that the character has
    /// </summary>
    protected ArrayList items;

    /// <summary>
    /// The current status of the character
    /// </summary>
    public status CurrentStatus { get; set; }

    ///// <summary>
    ///// The animator of the character in Unity
    ///// </summary>
    //protected Animator anim;

    ///// <summary>
    ///// The RigidBody of the character in Unity
    ///// </summary>
    //protected Rigidbody rb;

    /// <summary>
    /// The initilization of the character
    /// </summary>
    /// <param name="healthValue"> The initial max health value of the character </param>
    public void Initialization(int healthValue, int speed, Vector3 pos, Vector3 rot)
    {
        Health = healthValue;
        MoveSpeed = speed;
        CurrentStatus = status.Idle;
        //anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();
        //rb.MovePosition(pos);
        //rb.MoveRotation(Quaternion.Euler(rot));
    }

    /// <summary>
    /// The move function of the character
    /// </summary>
    abstract public void Move();

    /// <summary>
    /// Change item function of the character
    /// </summary>
    /// <returns> The index of the item list </returns>
    abstract public int ItemChange();

    abstract public void UseSkill();

    /// <summary>
    /// The health change of the character
    /// </summary>
    /// <returns> The remaining health value of the character </returns>
    public int HealthChange(int value)
    {
        Health += value;
        if (Health <= 0)
        {
            CurrentStatus = status.Dead;
        }
        return Health;
    }

    abstract public void SwitchFSM();
}
