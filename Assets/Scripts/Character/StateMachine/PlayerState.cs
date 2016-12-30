using UnityEngine;
using System.Collections;
public class State_Player_Idle : State<PlayerManager>
{
    public static State_Player_Idle instance;

    public static State_Player_Idle Instantiate()
    {
        if (instance == null)
        {
            instance = new State_Player_Idle();
        }

        return instance;
    }

    public override void Enter(PlayerManager obj)
    {
        // Nothing is done here
    }

    public override void Execute(PlayerManager obj)
    {
        // If the player dies
        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Player_Die.Instantiate());
        }
        // The loop function during the idle
        obj.Idle();
        // If there is any input, change to the move idle
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            obj.GetFSM().ChangeState(State_Player_Move.Instantiate());
        }
        // If the player is in the worldscene, it cannnot shoot.
        if (!obj.InWorld)
        {
            if (Input.GetButton("Fire1"))
            {
                obj.GetFSM().ChangeState(State_Player_UseSkill.Instantiate());
            }
        }
    }

    public override void Exit(PlayerManager obj)
    {
        // Nothing is done here
    }
}

public class State_Player_Move : State<PlayerManager>
{
    public static State_Player_Move instance;

    public static State_Player_Move Instantiate()
    {
        if (instance == null)
        {
            instance = new State_Player_Move();
        }

        return instance;
    }

    public override void Enter(PlayerManager obj)
    {

    }

    public override void Execute(PlayerManager obj)
    {
        // If the player dies
        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Player_Die.Instantiate());
        }
        // The loop function during the move state
        obj.Move();
        // If there is no input detected, change to idle state 
        if (Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Vertical") == 0)
        {
            obj.GetFSM().ChangeState(State_Player_Idle.Instantiate());
        }
        // Cannot shoot if in the World Scene
        if (!obj.InWorld)
        {
            if (Input.GetButton("Fire1"))
            {
                obj.GetFSM().ChangeState(State_Player_UseSkill.Instantiate());
            }
        }
    }

    public override void Exit(PlayerManager obj)
    {
        // Nothing is done here
    }
}

public class State_Player_UseSkill : State<PlayerManager>
{
    public static State_Player_UseSkill instance;

    public static State_Player_UseSkill Instantiate()
    {
        if (instance == null)
        {
            instance = new State_Player_UseSkill();
        }

        return instance;
    }

    public override void Enter(PlayerManager obj)
    {

    }

    public override void Execute(PlayerManager obj)
    {
        // If the player die
        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Player_Die.Instantiate());
        }
        // Loop function during useskill state
        obj.UseSkill();
        // If there is any move input, to move state
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            obj.GetFSM().ChangeState(State_Player_Move.Instantiate());
        }
        // Else to idle state
        else
        {
            obj.GetFSM().ChangeState(State_Player_Idle.Instantiate());
        }

        
    }

    public override void Exit(PlayerManager obj)
    {
        // Nothing is done here
    }
}

public class State_Player_Die : State<PlayerManager>
{
    public static State_Player_Die instance;

    public static State_Player_Die Instantiate()
    {
        if (instance == null)
        {
            instance = new State_Player_Die();
        }

        return instance;
    }

    public override void Enter(PlayerManager obj)
    {
        obj.Die();
    }

    public override void Execute(PlayerManager obj)
    {
        // Nothing is done here
    }

    public override void Exit(PlayerManager obj)
    {
        // Nothing is done here
    }
}
