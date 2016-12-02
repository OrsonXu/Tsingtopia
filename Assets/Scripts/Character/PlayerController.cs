using UnityEngine;
using System.Collections;

public class PlayerController : Character
{
    public bool InWorld { get; set; }
    public int Magic = 100;
    public int SkillMagic = 1;
    public int HealthRecoverRatePerMin = 10;
    public int MagicRecoverRatePerMin = 20;

    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    PlayerHealth playerHealth;
    PlayerMagic playerMagic;
    StateMachine<PlayerController> sm_player;
    PlayerKillCounter playerKillCounter;
    Animator anim;

    int enemyListSize;

    public void PlayerPlusInit()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.Speed = MoveSpeed;
        playerMovement.enabled = false;

        sm_player = new StateMachine<PlayerController>(this);
        sm_player.SetCurrentState(State_Player_Idle.Instantiate());

        anim = GetComponent<Animator>();

        if (InWorld)
        {
            return;
        }

        playerShooting = GetComponentInChildren<PlayerShooting>();
        playerShooting.bulletMagicValue = SkillMagic;
        //playerShooting.enabled = true;

        playerHealth = GetComponent<PlayerHealth>();
        playerHealth.MaxHealth = playerHealth.CurrentHealth = Health;
        playerHealth.RecoverRate = HealthRecoverRatePerMin;
        //playerHealth.enabled = true;

        playerMagic = GetComponent<PlayerMagic>();
        playerMagic.MaxMagic = playerMagic.CurrentMagic = Magic;
        playerMagic.RecoverRate = MagicRecoverRatePerMin;
        //playerMagic.enabled = true;

        enemyListSize = GameObject.FindGameObjectWithTag("InstanceManager").GetComponent<EnemyManager>().enemies.Length;

        playerKillCounter = GetComponent<PlayerKillCounter>();
        playerKillCounter.length = enemyListSize;
        //playerKillCounter.enabled = true;
    }

    public override void Idle()
    {
        anim.SetBool("IsWalking", false);
    }

    public override void Move()
    {
        anim.SetBool("IsWalking", true);
        if (!playerMovement.enabled)
            playerMovement.enabled = true;
    }

    public void UseSkill()
    {
        if (playerMagic.CanUseSkill(SkillMagic))
        {
            if (!playerShooting.enabled)
                playerShooting.enabled = true;
        }
        else
        {
            if (playerShooting.enabled)
                playerShooting.enabled = false;
        }
    }

    public void Die()
    {
        anim.SetTrigger("Die");
        playerShooting.DisableEffects();
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void FSMUpdate()
    {
        sm_player.SMUpdate();
    }

    public void Update()
    {
        //if (!InWorld)
        this.FSMUpdate();
    }

    public int ItemChange()
    {
        return 0;
    }

    public StateMachine<PlayerController> GetFSM()
    {
        return sm_player;
    }

    public bool IsDead()
    {
        return playerHealth.IsDead();
    }

    public void RecoverAll()
    {
        playerHealth.CurrentHealth = playerHealth.MaxHealth;
    }

    public void AddCount(int EnemyID)
    {
        playerKillCounter.AddCount(EnemyID);
    }

}
