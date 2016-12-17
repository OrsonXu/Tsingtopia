using UnityEngine;
using System.Collections;

public class State_Enemy_Idle : State<EnemyController>
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
    public override void Enter(EnemyController obj)
    {
        // Nothing is done here
    }

    public override void Execute(EnemyController obj)
    {
        obj.Idle();

        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Enemy_Die.Instantiate());
        }
        if (obj.CanMove())
        {
            obj.GetFSM().ChangeState(State_Enemy_Move.Instantiate());
        }
    }

    public override void Exit(EnemyController obj)
    {
        // Nothing is done here
    }
}

public class State_Enemy_Move : State<EnemyController>
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

    public override void Enter(EnemyController obj)
    {
        // Nothing is done here
    }

    public override void Execute(EnemyController obj)
    {
        obj.Move();

        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Enemy_Die.Instantiate());
        }
        if (!obj.CanMove())
        {
            obj.GetFSM().ChangeState(State_Enemy_Idle.Instantiate());
        }
        if (obj.CanAlerted())
        {
            obj.GetFSM().ChangeState(State_Enemy_Alert.Instantiate());
        }
    }

    public override void Exit(EnemyController obj)
    {
        // Nothing is done here
    }
}

public class State_Enemy_UseSkill : State<EnemyController>
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

    public override void Enter(EnemyController obj)
    {
        count = 0;
        // Nothing is done here
    }
    public int count = 0;
    public override void Execute(EnemyController obj)
    {
        obj.UseSkill();
        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Enemy_Die.Instantiate());
        }
        if (!obj.CanMove())
        {
            obj.GetFSM().ChangeState(State_Enemy_Idle.Instantiate());
        }
        if (!obj.CanContinueAttack())
        {
            obj.GetFSM().ChangeState(State_Enemy_Alert.Instantiate());
        }
    }

    public override void Exit(EnemyController obj)
    {
        // Nothing is done here
    }
}

public class State_Enemy_Die : State<EnemyController>
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

    public override void Enter(EnemyController obj)
    {
        obj.Die();
    }

    public override void Execute(EnemyController obj)
    {
        // Nothing is done here
    }

    public override void Exit(EnemyController obj)
    {
        // Nothing is done here
    }
}

public class State_Enemy_Alert : State<EnemyController>
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

    public override void Enter(EnemyController obj)
    {
        
    }

    public override void Execute(EnemyController obj)
    {
        obj.Alert();
        if (obj.IsDead())
        {
            obj.GetFSM().ChangeState(State_Enemy_Die.Instantiate());
        }
        if (!obj.CanMove())
        {
            obj.GetFSM().ChangeState(State_Enemy_Idle.Instantiate());
        }
        if (obj.CanAttack())
        {
            obj.GetFSM().ChangeState(State_Enemy_UseSkill.Instantiate());
        }
        if (!obj.CanContinueAlerted())
        {
            obj.GetFSM().ChangeState(State_Enemy_Move.Instantiate());
        }
    }

    public override void Exit(EnemyController obj)
    {
        // Nothing is done here
    }
}