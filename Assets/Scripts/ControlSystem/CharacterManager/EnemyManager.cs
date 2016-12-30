using UnityEngine;
using System.Collections;

public class EnemyManager : CharacterManager {

    public bool playerInRange { get; set; }
    public float searchingTurnSpeed = 120f;
    public float sightRange = 20f;
    public float ContinueAttackRange = 20f;

    public Transform[] MovePoints;
    private int _movePointIndex;
    public float alertThreshold = 2f;

    private MeshRenderer _meshRenderer;
    private int _enemyID;
    private float _alertTimer = 0f;
    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;
    private EnemyAttack _enemyAttack;
    private StateMachine<EnemyManager> _sm_enemy;
    private Animator _anim;
    private bool _playerDead;

    private GameObject _player;
    /// <summary>
    /// Override, register a message event
    /// </summary>
    private void OnEnable()
    {
        MessageManager.StartListening("PlayerDie", PlayerDie);
    }
    /// <summary>
    /// Override, unregister a message event
    /// </summary>
    private void OnDisable()
    {
        MessageManager.StopListening("PlayerDie", PlayerDie);
    }
    /// <summary>
    /// Init the enemy
    /// </summary>
    /// <param name="enemyID"></param>
    public void EnemyPlusInit(int enemyID)
    {
        this._enemyID = enemyID;
        _sm_enemy = new StateMachine<EnemyManager>(this);
        _sm_enemy.SetCurrentState(State_Enemy_Move.Instantiate());

        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyMovement.Speed = MoveSpeed;

        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyHealth.MaxHealth = _enemyHealth.CurrentHealth = Health;
        _enemyHealth.enabled = true;

        _enemyAttack = GetComponent<EnemyAttack>();

        _anim = GetComponent<Animator>();

        _player =  GameObject.FindGameObjectWithTag("Player");

        _meshRenderer = transform.FindChild("Cube").GetComponent<MeshRenderer>();

        _movePointIndex = 0;
        _playerDead = false;
    }
    public void Update()
    {
        // Update the FSM state
        _sm_enemy.SMUpdate();
    }
    /// <summary>
    /// Idle loop function during idle state
    /// </summary>
    public override void Idle()
    {
        // Update the cube color
        _meshRenderer.material.color = Color.green;
        // Trigger the animation
        _anim.SetBool("IsWalking", false);
        if (_enemyMovement.enabled)
        {
            _enemyMovement.enabled = false;
        }
        if (_enemyAttack.enabled)
        {
            _enemyAttack.enabled = false;
        }
    }
    /// <summary>
    /// Alert loop function during the alert state
    /// </summary>
    public void Alert()
    {
        //Update cube color
        _meshRenderer.material.color = Color.yellow;
        transform.Rotate(0, searchingTurnSpeed * Time.deltaTime, 0);
        _alertTimer += Time.deltaTime;
        if (_enemyMovement.enabled)
        {
            _enemyMovement.enabled = false;
        }
        if (_enemyAttack.enabled)
        {
            _enemyAttack.enabled = false;
        }
    }

    /// <summary>
    /// Loop function during the move state
    /// </summary>
    public override void Move()
    {
        // Update the cube color
        _meshRenderer.material.color = Color.green;
        _alertTimer = 0;
        _anim.SetBool("IsWalking", true);
        if (!_enemyMovement.enabled)
        {
            _enemyMovement.enabled = true;
            //movePointIndex = (movePointIndex + 1) % MovePoints.Length;
            _enemyMovement.SetDestination(MovePoints[_movePointIndex].position);
        }
        else
        {
            if (_enemyMovement.CloseEnough())
            {
                _movePointIndex = (_movePointIndex + 1) % MovePoints.Length;
                _enemyMovement.SetDestination(MovePoints[_movePointIndex].position);
            }
        }
        if (_enemyAttack.enabled)
        {
            _enemyAttack.enabled = false;
        }
    }
    /// <summary>
    /// Useskill loop funciotn during the useskill function
    /// </summary>
    public void UseSkill()
    {
        _meshRenderer.material.color = Color.red;
        _alertTimer = 0;
        _enemyMovement.enabled = true;
        _enemyMovement.SetDestination(_player.transform.position);
        if (!_enemyAttack.enabled)
        {
            _enemyAttack.enabled = true;
        }
    }
    /// <summary>
    /// Funtion when the enemy die
    /// </summary>
    public void Die()
    {
        _anim.SetTrigger("Die");
        _enemyMovement.enabled = false;
        _enemyAttack.enabled = false;
        MessageManager.TriggerEvent("EnemyDieWithID", _enemyID);
    }
    /// <summary>
    /// Function when the player dies, 
    /// </summary>
    private void PlayerDie()
    {
        _playerDead = true;
    }
    /// <summary>
    /// Public flag for whether the enemy is dead
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return _enemyHealth.IsDead();
    }

    /// <summary>
    /// If the player is dead, then cannot move
    /// </summary>
    /// <returns></returns>
    public bool CanMove()
    {
        //return !(playerController.IsDead());
        return !_playerDead;
    }
    /// <summary>
    /// Flag for whether the player is in the alert range
    /// </summary>
    /// <returns></returns>
    public bool CanAlerted()
    {
        return playerInRange;
    }
    /// <summary>
    /// Flag for whether the enemy can still stay in the alert state
    /// </summary>
    /// <returns></returns>
    public bool CanContinueAlerted()
    {
        return (_alertTimer < alertThreshold);
    }
    /// <summary>
    /// Flag for whether the enemy can attack the player.
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Flag for whether the enemy can continue to attack the player
    /// </summary>
    /// <returns></returns>
    public bool CanContinueAttack()
    {
        // The player is in the attack range
        float dist = Mathf.Sqrt((transform.position - _player.transform.position).sqrMagnitude);
        if (dist < ContinueAttackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Return the FSM of the enemy
    /// </summary>
    /// <returns></returns>
    public StateMachine<EnemyManager> GetFSM()
    {
        return _sm_enemy;
    }

}
