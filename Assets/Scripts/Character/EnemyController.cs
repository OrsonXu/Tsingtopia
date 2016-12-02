using UnityEngine;
using System.Collections;

public class EnemyController : Character {

    int enemyID;
    EnemyMovement enemyMovement;
    EnemyHealth enemyHealth;
    EnemyAttack enemyAttack;
    StateMachine<EnemyController> sm_enemy;
    Animator anim;
    PlayerController playerController;

    //public void Awake()
    //{
    //    EnemyPlusInit();
    //}

    public void EnemyPlusInit(int enemyID)
    {
        this.enemyID = enemyID;
        sm_enemy = new StateMachine<EnemyController>(this);
        sm_enemy.SetCurrentState(State_Enemy_Move.Instantiate());

        enemyMovement = GetComponent<EnemyMovement>();
        enemyMovement.Speed = MoveSpeed;

        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.MaxHealth = enemyHealth.CurrentHealth = Health;
        enemyHealth.enabled = true;

        enemyAttack = GetComponent<EnemyAttack>();

        anim = GetComponent<Animator>();

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    public override void Idle()
    {
        anim.SetBool("IsWalking", false);
        if (enemyMovement.enabled)
        {
            enemyMovement.enabled = false;
        }
    }

    public void Update()
    {
        sm_enemy.SMUpdate();
    }

    public override void Move()
    {
        UseSkill();
        anim.SetBool("IsWalking", true);
        if (!enemyMovement.enabled)
        {
            enemyMovement.enabled = true;
        }
    }

    public void UseSkill()
    {
        if (!enemyAttack.enabled)
        {
            enemyAttack.enabled = true;
        }
    }

    public void Die()
    {
        anim.SetTrigger("Die");
        enemyMovement.enabled = false;
        enemyAttack.enabled = false;
        playerController.AddCount(enemyID);
    }

    public bool IsDead()
    {
        return enemyHealth.IsDead();
    }

    public StateMachine<EnemyController> GetFSM()
    {
        return sm_enemy;
    }

    public bool CanMove()
    {
        return !(playerController.IsDead());
    }

}
