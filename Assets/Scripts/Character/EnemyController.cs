using UnityEngine;
using System.Collections;

public class EnemyController : Character {

    public bool playerInRange { get; set; }
    public float searchingTurnSpeed = 120f;
    public float sightRange = 20f;
    public float ContinueAttackRange = 20f;

    public Transform[] MovePoints;
    int movePointIndex;
    public float alertThreshold = 2f;

    MeshRenderer meshRenderer;
    int enemyID;
    float alertTimer = 0f;
    EnemyMovement enemyMovement;
    EnemyHealth enemyHealth;
    EnemyAttack enemyAttack;
    StateMachine<EnemyController> sm_enemy;
    Animator anim;

    GameObject player;
    PlayerController playerController;

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

        player =  GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        meshRenderer = transform.FindChild("Cube").GetComponent<MeshRenderer>();

        //MovePoints = new Transform[GameObject.Find("Movepoints").transform.childCount];
        //for (int i = 0; i < MovePoints.Length ; ++i)
        //{
        //    MovePoints[i] = GameObject.Find("Movepoints").transform.FindChild("Movepoint" + (i + 1).ToString());
        //}
        movePointIndex = 0;
    }

    public override void Idle()
    {
        meshRenderer.material.color = Color.green;
        anim.SetBool("IsWalking", false);
        if (enemyMovement.enabled)
        {
            enemyMovement.enabled = false;
        }
        if (enemyAttack.enabled)
        {
            enemyAttack.enabled = false;
        }
    }

    public void Alert()
    {
        meshRenderer.material.color = Color.yellow;
        transform.Rotate(0, searchingTurnSpeed * Time.deltaTime, 0);
        alertTimer += Time.deltaTime;
        if (enemyMovement.enabled)
        {
            enemyMovement.enabled = false;
        }
        if (enemyAttack.enabled)
        {
            enemyAttack.enabled = false;
        }
    }


    public override void Move()
    {
        meshRenderer.material.color = Color.green;
        alertTimer = 0;
        anim.SetBool("IsWalking", true);
        if (!enemyMovement.enabled)
        {
            enemyMovement.enabled = true;
            //movePointIndex = (movePointIndex + 1) % MovePoints.Length;
            enemyMovement.SetDestination(MovePoints[movePointIndex].position);
        }
        else
        {
            if (enemyMovement.CloseEnough())
            {
                movePointIndex = (movePointIndex + 1) % MovePoints.Length;
                enemyMovement.SetDestination(MovePoints[movePointIndex].position);
            }
        }
        if (enemyAttack.enabled)
        {
            enemyAttack.enabled = false;
        }
    }

    public void UseSkill()
    {
        meshRenderer.material.color = Color.red;
        alertTimer = 0;
        enemyMovement.enabled = true;
        enemyMovement.SetDestination(player.transform.position);
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

    /// <summary>
    /// If the player is dead, then cannot move
    /// </summary>
    /// <returns></returns>
    public bool CanMove()
    {
        return !(playerController.IsDead());
    }

    public bool CanAlerted()
    {
        return playerInRange;
    }

    public bool CanContinueAlerted()
    {
        return (alertTimer < alertThreshold);
    }

    public bool CanAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, sightRange) && hit.collider.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanContinueAttack()
    {
        float dist = Mathf.Sqrt((transform.position - player.transform.position).sqrMagnitude);
        if (dist < ContinueAttackRange)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    public void Update()
    {
        sm_enemy.SMUpdate();
    }
    public StateMachine<EnemyController> GetFSM()
    {
        return sm_enemy;
    }

}
