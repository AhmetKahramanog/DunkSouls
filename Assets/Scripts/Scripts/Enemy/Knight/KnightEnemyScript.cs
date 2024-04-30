using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightEnemyScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float distance;
    [SerializeField] private float speed;
    [SerializeField] private float stopRange;
    private Animator animator;
    private float attackTimer;
    public bool IsAbleParry { get; set; }
    private bool canMove = true;
    private RaycastHit hit;
    [SerializeField] private float damage;
    private KnighHealth knightHealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
        knightHealth = GetComponent<KnighHealth>();
    }

    private void Update()
    {
        if (!knightHealth.IsDie)
        {
            FollowToPlayer();
            Parried("KnightAttack");
            if (Parry.IsKnightParried)
            {
                canMove = false;
            }
            else
            {
                canMove = true;
            }
        }
    }


    private void FollowToPlayer()
    {
        if (DistanceToPlayer() <= 5f && DistanceToPlayer() >= stopRange && canMove)
        {
            LookAtPlayer();
            transform.Translate(0f,0f,speed * Time.deltaTime);
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
            if (DistanceToPlayer() <= stopRange && canMove)
            {
                Attack();
            }
        }
    }


    private float DistanceToPlayer()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        return distance;
    }

    private void Attack()
    {
        if (Time.time - attackTimer > 3.5f)
        {
            animator.SetTrigger("Attack");
            attackTimer = Time.time;
            IsAbleParry = true;
            PlayerHealth.DoAbleParry = true;
        }
    }

    private void Parried(string animationName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag(animationName))
        {
            IsAbleParry = false;
        }
    }

    private void LookAtPlayer()
    {
        var playerLookAt = player.transform.position;
        playerLookAt.y = 0f;
        transform.LookAt(playerLookAt);
    }

    public void AttackDamage()
    {
        bool isHit = Physics.SphereCast(transform.position, 0.7f, transform.forward, out hit, 2f);
        if (isHit)
        {   
            if (hit.transform.TryGetComponent(out PlayerHealth playerHealth) && !Parry.IsKnightParried)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }


}
