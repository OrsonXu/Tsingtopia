using UnityEngine;
using System.Collections;

public class EnemyController : Character {

    public bool playerInRange { get; set; }
    public float searchingTurnSpeed = 120f;
    public float sightRange = 20f;
    public float ContinueAttackRange = 20f;

    public Transform[] MovePoints;
    private int movePointIndex;
    public float alertThreshold = 2f;

    private MeshRenderer _meshRenderer;
    private int _enemyID;
    private float _alertTimer = 0f;
    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;
    private EnemyAttack _enemyAttack;
    private StateMachine<EnemyController> _sm_enemy;
    private Animator _anim;
    private bool _playerDead;

    private GameObject _player;

    private void OnEnable()
    {
        MessageManager.StartListening("PlayerDie", PlayerDie);
    }

    private void OnDisable()
    {
        MessageManager.StopListening("PlayerDie", PlayerDie);
    }

    public void EnemyPlusInit(int enemyID)
    {
        this._enemyID = enemyID;
        _sm_enemy = new StateMachine<EnemyController>(this);
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

        movePointIndex = 0;
        _playerDead = false;
    }
    public void Update()
    {
        _sm_enemy.SMUpdate();
    }

    public override void Idle()
    {
        _meshRenderer.material.color = Color.green;
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

    public void Alert()
    {
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


    public override void Move()
    {
        _meshRenderer.material.color = Color.green;
        _alertTimer = 0;
        _anim.SetBool("IsWalking", true);
        if (!_enemyMovement.enabled)
        {
            _enemyMovement.enabled = true;
            //movePointIndex = (movePointIndex + 1) % MovePoints.Length;
            _enemyMovement.SetDestination(MovePoints[movePointIndex].position);
        }
        else
        {
            if (_enemyMovement.CloseEnough())
            {
                movePointIndex = (movePointIndex + 1) % MovePoints.Length;
                _enemyMovement.SetDestination(MovePoints[movePointIndex].position);
            }
        }
        if (_enemyAttack.enabled)
        {
            _enemyAttack.enabled = false;
        }
    }

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

    public void Die()
    {
        _anim.SetTrigger("Die");
        _enemyMovement.enabled = false;
        _enemyAttack.enabled = false;
        MessageManager.TriggerEvent("EnemyDieWithID", _enemyID);
    }

    private void PlayerDie()
    {
        _playerDead = true;
    }

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

    public bool CanAlerted()
    {
        return playerInRange;
    }

    public bool CanContinueAlerted()
    {
        return (_alertTimer < alertThreshold);
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


    public StateMachine<EnemyController> GetFSM()
    {
        return _sm_enemy;
    }

}
