using UnityEngine;
using System.Collections;

public class PlayerController : Character
{
    //int floorMask;
    //int shootableMask;
    //int attackValue;

    //float timer;
    //float timeBetweenBullets = 0.15f;
    //float effectsDisplayTime = 0.2f;
    //int range = 100;
    //Ray shootRay;
    //RaycastHit shootHit;
    //ParticleSystem gunParticles;
    //LineRenderer gunLine;
    //AudioSource gunAudio;
    //Light gunLight;

    //public void PlayerPlusInit()
    //{
    //    floorMask = LayerMask.GetMask("Floor");
    //    shootableMask = LayerMask.GetMask("Shootable");
    //    attackValue = 50;
    //    gunParticles = GetComponentInChildren<ParticleSystem>();
    //    gunLine = GetComponentInChildren<LineRenderer>();
    //    gunAudio = GetComponentInChildren<AudioSource>();
    //    gunLight = GetComponentInChildren<Light>();
    //}

    //public override void Move()
    //{
    //    float h = Input.GetAxis("Horizontal");
    //    float v = Input.GetAxis("Vertical");
    //    if (h == 0 && v == 0)
    //    {
    //        CurrentStatus = status.Idle;
    //        anim.SetBool("IsWalking", false);
    //    }
    //    else
    //    {
    //        CurrentStatus = status.Move;
    //        anim.SetBool("IsWalking", true);
    //        Vector3 movement = new Vector3(h, 0f, v);
    //        movement = movement.normalized * MoveSpeed * Time.deltaTime;
    //        rb.MovePosition(transform.position + movement);
    //    }

    //    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    RaycastHit floorHit;
    //    if (Physics.Raycast(camRay, out floorHit, 1000f, floorMask))
    //    {
    //        Vector3 playerToMouse = floorHit.point - transform.position;
    //        playerToMouse.y = 0f;
    //        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
    //        rb.MoveRotation(newRotation);
    //    }

    //}

    //public override int ItemChange()
    //{
    //    return 0;
    //}

    //public override void SwitchFSM()
    //{
    //    switch (CurrentStatus)
    //    {
    //        case status.Idle:
    //        case status.Move:
    //        case status.Attack:
    //            Move();
    //            UseSkill();
    //            break;
    //        case status.Dead:
    //        default:
    //            break;
    //    }
    //}

    //public override void UseSkill()
    //{
    //    if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
    //    {
    //        CurrentStatus = status.Attack;
    //        timer = 0f;

    //        gunAudio.Play();

    //        gunLight.enabled = true;

    //        gunParticles.Stop();
    //        gunParticles.Play();

    //        gunLine.enabled = true;
    //        gunLine.SetPosition(0, transform.position + new Vector3(0.4f, 0.1f, 0.7f));

    //        shootRay.origin = transform.position + new Vector3(0.4f,0.1f,0.7f);
    //        shootRay.direction = transform.forward;

    //        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
    //        {
    //            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
    //            if (enemyHealth != null)
    //            {
    //                enemyHealth.TakeDamage(attackValue, shootHit.point);
    //            }
    //            gunLine.SetPosition(1, shootHit.point);
    //        }
    //        else
    //        {
    //            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
    //        }
    //    }
    //}

    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    if (timer >= timeBetweenBullets * effectsDisplayTime)
    //    {
    //        DisableEffects();
    //    }
    //}

    //public void DisableEffects()
    //{
    //    gunLine.enabled = false;
    //    gunLight.enabled = false;
    //}

    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    PlayerHealth playerHealth;

    public void PlayerPlusInit()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        playerHealth.enabled = true;
        playerShooting = GetComponentInChildren<PlayerShooting>();
        playerHealth.startingHealth = playerHealth.currentHealth = Health;
        playerMovement.speed = MoveSpeed;
    }


    public override void Move()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            CurrentStatus = status.Idle;
        }
        else
        {
            CurrentStatus = status.Move;
        }   
    }

    public override void UseSkill()
    {
        CurrentStatus = status.Attack;
    }

    public override void SwitchFSM()
    {
        switch (CurrentStatus)
        {
            case status.Idle:
            case status.Attack:
                Move();
                playerShooting.enabled = true;
                playerMovement.enabled = true;
                break;
            case status.Move:
                Move();
                //playerShooting.enabled = false;
                break;
            case status.Dead:
                playerMovement.enabled = false;
                playerShooting.enabled = false;
                break;
            default:
                break;
        }
    }
    public override int ItemChange()
    {
        return 0;
    }

}
