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
        this.FSMUpdate();
    }
    /// <summary>
    /// Finite state machine update (looply)
    /// </summary>
    public void FSMUpdate()
    {
        _sm_player.SMUpdate();
    }

    public override void Idle()
    {
        _anim.SetBool("IsWalking", false);
    }

    public override void Move()
    {
        _anim.SetBool("IsWalking", true);
        if (!_playerMovement.enabled)
            _playerMovement.enabled = true;
    }

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

    public void Die()
    {
        MessageManager.TriggerEvent("PlayerDie");
        _anim.SetTrigger("Die");
        _playerShooting.DisableEffects();
        _playerMovement.enabled = false;
        _playerShooting.enabled = false;
    }


    public int ItemChange()
    {
        return 0;
    }

    public StateMachine<PlayerManager> GetFSM()
    {
        return _sm_player;
    }

    public bool IsDead()
    {
        if (InWorld) return false;
        return _playerHealth.IsDead();
    }

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
