using UnityEngine;
using System.Collections;

public class State_Enemy_Idle : State<EnemyManager>
{
    public static State_Enemy_Idle instance;

    public static State_Enemy_Idle Instantiate()
    {
        if (instance == null)
        {
            instance = new State_Enemy_Idle();
        }
        return instance;
    }
    public override void Enter(EnemyManager obj)
    {
        // Nothing is done here
    }

    public override void Execute(EnemyManager obj)
    {
        // The loop function during the idle state of the enemy.
        obj.Idle();
        // If the ememy dies
        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Enemy_Die.Instantiate());
        }
        // If the player die
        if (obj.CanMove())
        {
            obj.GetFSM().ChangeState(State_Enemy_Move.Instantiate());
        }
    }

    public override void Exit(EnemyManager obj)
    {
        // Nothing is done here
    }
}

public class State_Enemy_Move : State<EnemyManager>
{
    public static State_Enemy_Move instance;

    public static State_Enemy_Move Instantiate()
    {
        if (instance == null)
        {
            instance = new State_Enemy_Move();
        }

        return instance;
    }

    public override void Enter(EnemyManager obj)
    {
        // Nothing is done here
    }

    public override void Execute(EnemyManager obj)
    {
        // Function during the move state
        obj.Move();
        // If the ememy dies
        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Enemy_Die.Instantiate());
        }
        // If the player die
        if (!obj.CanMove())
        {
            obj.GetFSM().ChangeState(State_Enemy_Idle.Instantiate());
        }
        // If the player is in the range
        if (obj.CanAlerted())
        {
            obj.GetFSM().ChangeState(State_Enemy_Alert.Instantiate());
        }
    }

    public override void Exit(EnemyManager obj)
    {
        // Nothing is done here
    }
}

public class State_Enemy_UseSkill : State<EnemyManager>
{
    public static State_Enemy_UseSkill instance;

    public static State_Enemy_UseSkill Instantiate()
    {
        if (instance == null)
        {
            instance = new State_Enemy_UseSkill();
        }
        return instance;
    }

    public override void Enter(EnemyManager obj)
    {
        // Nothing is done here
    }
    public override void Execute(EnemyManager obj)
    {
        // The function during the useskill loop
        obj.UseSkill();
        // If the enemy is dead
        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Enemy_Die.Instantiate());
        }
        // If the player die
        if (!obj.CanMove())
        {
            obj.GetFSM().ChangeState(State_Enemy_Idle.Instantiate());
        }
        // If the player is out of the range
        if (!obj.CanContinueAttack())
        {
            obj.GetFSM().ChangeState(State_Enemy_Alert.Instantiate());
        }
    }

    public override void Exit(EnemyManager obj)
    {
        // Nothing is done here
    }
}

public class State_Enemy_Die : State<EnemyManager>
{
    public static State_Enemy_Die instance;

    public static State_Enemy_Die Instantiate()
    {
        if (instance == null)
        {
            instance = new State_Enemy_Die();
        }

        return instance;
    }

    public override void Enter(EnemyManager obj)
    {
        // If the enemy die, this state is absorptive
        obj.Die();
    }

    public override void Execute(EnemyManager obj)
    {
        // Nothing is done here
    }

    public override void Exit(EnemyManager obj)
    {
        // Nothing is done here
    }
}

public class State_Enemy_Alert : State<EnemyManager>
{
    public static State_Enemy_Alert instance;

    public static State_Enemy_Alert Instantiate()
    {
        if (instance == null)
        {
            instance = new State_Enemy_Alert();
        }

        return instance;
    }

    public override void Enter(EnemyManager obj)
    {
        
    }

    public override void Execute(EnemyManager obj)
    {
        // The loop function during the alert state
        obj.Alert();
        // if the enemy die
        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Enemy_Die.Instantiate());
        }
        // If the player die
        if (!obj.CanMove())
        {
            obj.GetFSM().ChangeState(State_Enemy_Idle.Instantiate());
        }
        // If the player is in the range
        if (obj.CanAttack())
        {
            obj.GetFSM().ChangeState(State_Enemy_UseSkill.Instantiate());
        }
        // If the player is out of the range for a dwell time
        if (!obj.CanContinueAlerted())
        {
            obj.GetFSM().ChangeState(State_Enemy_Move.Instantiate());
        }
    }

    public override void Exit(EnemyManager obj)
    {
        // Nothing is done here
    }
}