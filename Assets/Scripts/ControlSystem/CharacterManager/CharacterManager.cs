using UnityEngine;
using System.Collections;
/// <summary>
/// Basic class for CharacterManager
/// </summary>
abstract public class CharacterManager : BaseManager{

    /// <summary>
    /// The health value of the character
    /// </summary>
    public int Health;

    /// <summary>
    /// The speed of movement of the character
    /// </summary>
    public int MoveSpeed;

    /// <summary>
    /// The list of items that the character has
    /// </summary>
    protected ArrayList items;

    /// <summary>
    /// The initilization of the character
    /// </summary>
    /// <param name="healthValue"> The initial max health value of the character </param>
    public void Init(Vector3 pos, Vector3 rot)
    {
        transform.position = pos;
        transform.rotation = Quaternion.Euler(rot);
    }
    /// <summary>
    /// The idle function of the character
    /// </summary>
    abstract public void Idle();
    /// <summary>
    /// The move function of the character
    /// </summary>
    abstract public void Move();
}
