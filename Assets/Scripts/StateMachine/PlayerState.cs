using UnityEngine;
using System.Collections;
public class State_Player_Idle : State<PlayerController>
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

    public override void Enter(PlayerController obj)
    {
        // Nothing is done here
    }

    public override void Execute(PlayerController obj)
    {
        obj.Idle();

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            obj.GetFSM().ChangeState(State_Player_Move.Instantiate());
        }

        if (Input.GetButton("Fire1"))
        {
            obj.GetFSM().ChangeState(State_Player_UseSkill.Instantiate());
        }

        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Player_Die.Instantiate());
        }
    }

    public override void Exit(PlayerController obj)
    {
        // Nothing is done here
    }
}

public class State_Player_Move : State<PlayerController>
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

    public override void Enter(PlayerController obj)
    {

    }

    public override void Execute(PlayerController obj)
    {
        obj.Move();
        if (Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Vertical") == 0)
        {
            obj.GetFSM().ChangeState(State_Player_Idle.Instantiate());
        }

        if (Input.GetButton("Fire1"))
        {
            obj.GetFSM().ChangeState(State_Player_UseSkill.Instantiate());
        }

        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Player_Die.Instantiate());
        }
    }

    public override void Exit(PlayerController obj)
    {
        // Nothing is done here
    }
}

public class State_Player_UseSkill : State<PlayerController>
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

    public override void Enter(PlayerController obj)
    {

    }

    public override void Execute(PlayerController obj)
    {
        obj.UseSkill();

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            obj.GetFSM().ChangeState(State_Player_Move.Instantiate());
        }
        else
        {
            obj.GetFSM().ChangeState(State_Player_Idle.Instantiate());
        }

        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Player_Die.Instantiate());
        }
    }

    public override void Exit(PlayerController obj)
    {
        // Nothing is done here
    }
}

public class State_Player_Die : State<PlayerController>
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

    public override void Enter(PlayerController obj)
    {
        obj.Die();
    }

    public override void Execute(PlayerController obj)
    {
        // Nothing is done here
    }

    public override void Exit(PlayerController obj)
    {
        // Nothing is done here
    }
}
