using UnityEngine;
using System.Collections;

public class PlayerController : Character
{
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    PlayerHealth playerHealth;
    StateMachine<PlayerController> sm_player;
    Animator anim;

    public void PlayerPlusInit()
    {
        sm_player = new StateMachine<PlayerController>(this);
        sm_player.SetCurrentState(State_Player_Idle.Instantiate());

        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.Speed = MoveSpeed;

        playerShooting = GetComponentInChildren<PlayerShooting>();

        playerHealth = GetComponent<PlayerHealth>();
        playerHealth.MaxHealth = playerHealth.CurrentHealth = Health;
        playerHealth.enabled = true;

        anim = GetComponent<Animator>();
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
        if (!playerShooting.enabled)
            playerShooting.enabled = true;
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

}
