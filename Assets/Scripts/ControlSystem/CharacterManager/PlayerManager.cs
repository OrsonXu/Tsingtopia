using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerManager : CharacterManager
{
    public bool InWorld { get; set; }
    public int Magic = 100;
    public int SkillMagic = 1;
    public int HealthRecoverRatePerMin = 10;
    public int MagicRecoverRatePerMin = 20;

    PlayerMovement _playerMovement;
    PlayerShooting _playerShooting;
    PlayerHealth _playerHealth;
    PlayerMagic _playerMagic;
    StateMachine<PlayerManager> _sm_player;
    PlayerKillCounter _playerKillCounter;
    SphereCollider _sphereCollider;
    Animator _anim;
    int _enemyListSize;

    public void Awake()
    {
        MessageManager.StartListening("PlayerInit", PlayerPlusInit);
    }
    /// <summary>
    /// Init of the player
    /// </summary>
    /// <param name="inWorld"></param>
    public void PlayerPlusInit(bool inWorld)
    {

        InWorld = inWorld;
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.initialSpeed = MoveSpeed;
        _playerMovement.enabled = false;

        _sm_player = new StateMachine<PlayerManager>(this);
        _sm_player.SetCurrentState(State_Player_Idle.Instantiate());

        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.enabled = true;
        _anim = GetComponent<Animator>();

        if (InWorld)
        {
            _sphereCollider.enabled = false;
            Debug.Log("Inworld.");
            return;
        }

        _playerHealth = GetComponent<PlayerHealth>();
        _playerHealth.MaxHealth = _playerHealth.CurrentHealth = Health;
        _playerHealth.RecoverRate = HealthRecoverRatePerMin;
        _playerHealth.enabled = true;

        _playerMagic = GetComponent<PlayerMagic>();
        _playerMagic.MaxMagic = _playerMagic.CurrentMagic = Magic;
        _playerMagic.RecoverRate = MagicRecoverRatePerMin;
        _playerMagic.enabled = true;

        _playerShooting = GetComponentInChildren<PlayerShooting>();
        _playerShooting.bulletMagicValue = SkillMagic;
        //_playerShooting.enabled = false;
        //_playerShooting.enabled = true;

        _enemyListSize = GameObject.FindGameObjectWithTag("InstanceManager").GetComponent<EnemySpawnManager>().enemies.Length;
        _playerKillCounter = GetComponent<PlayerKillCounter>();
        _playerKillCounter.Length = _enemyListSize;
        _playerKillCounter.enabled = true;
    }
    
    public void Update()
    {
        // Update the player FSM
        this.FSMUpdate();
    }
    /// <summary>
    /// Finite state machine update (looply)
    /// </summary>
    public void FSMUpdate()
    {
        _sm_player.SMUpdate();
    }
    /// <summary>
    /// idle function during the idle state
    /// </summary>
    public override void Idle()
    {
        _anim.SetBool("IsWalking", false);
    }
    /// <summary>
    /// move function during the move state
    /// </summary>
    public override void Move()
    {
        _anim.SetBool("IsWalking", true);
        if (!_playerMovement.enabled)
            _playerMovement.enabled = true;
    }
    /// <summary>
    /// Useskill function during the useskill state
    /// </summary>
    public void UseSkill()
    {
        if (_playerMagic.CanUseSkill(SkillMagic))
        {
            if (!_playerShooting.enabled)
                _playerShooting.enabled = true;
        }
        else
        {
            if (_playerShooting.enabled)
                _playerShooting.enabled = false;
        }
    }
    /// <summary>
    /// Die function when the player is in dead state
    /// </summary>
    public void Die()
    {
        MessageManager.TriggerEvent("PlayerDie");
        _anim.SetTrigger("Die");
        _playerShooting.DisableEffects();
        _playerMovement.enabled = false;
        _playerShooting.enabled = false;
    }

    /// <summary>
    /// Change some item. (Temporary unavailable)
    /// </summary>
    /// <returns></returns>
    public int ItemChange()
    {
        return 0;
    }
    /// <summary>
    /// Return the FSM of the player
    /// </summary>
    /// <returns></returns>
    public StateMachine<PlayerManager> GetFSM()
    {
        return _sm_player;
    }
    /// <summary>
    /// Flag for whether the player is dead
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        if (InWorld) return false;
        return _playerHealth.IsDead();
    }
    /// <summary>
    /// Recover the player health and magic to max.
    /// </summary>
    public void RecoverAll()
    {
        _playerHealth.CurrentHealth = _playerHealth.MaxHealth;
        _playerMagic.CurrentMagic = _playerMagic.MaxMagic;
    }

    public void AddCount(int EnemyID)
    {
        _playerKillCounter.AddCount(EnemyID);
    }
    /// <summary>
    /// When player is near any enemy
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyManager>().playerInRange = true;
        }
    }
    /// <summary>
    /// When player leaves some enemy
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyManager>().playerInRange = false;
        }
    }

}
