using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EnemyStates
{
    Patrol,
    Attack
}

public class MonsterEnemyMovement : Enemy
{
    private EnemyStates currentState;
    [SerializeField] private float patrolSpeed;
    private readonly float patrolTimer = 4f;
    private bool isForward = true;
    private float timer = 0f;
    [SerializeField] private Transform player;
    [SerializeField] private float runSpeed = 5f;
    public readonly float maxDistance = 1f;
    private float distance;
    private Animator animator;
    private float currentSpeed;
    [SerializeField] private float damage;
    [SerializeField] private Transform hitBox;
    private float attackTimer = 4f;
    [SerializeField] private float health;
    private float currentHealth;
    public Slider enemyHealthBar;
    public bool isDie { get; set; } = false;

    private bool stopAllFunc = false;

    [SerializeField] private GameObject bloodEffectParticle;

    public static GameObject bloodEffect { get; set; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentState = EnemyStates.Patrol;
        currentSpeed = patrolSpeed;
        animator.SetBool("isWalk", true);
        currentHealth = health;
        enemyHealthBar.value = currentHealth;
    }
    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if (distance < 5f && !stopAllFunc)
        {
            Vector3 targetRotation = player.transform.position;
            targetRotation.y = transform.position.y;
            transform.LookAt(targetRotation);
            currentState = EnemyStates.Attack;
        }
        else
        {
            currentState = EnemyStates.Patrol;
        }
        if (distance <= 1.8f && !stopAllFunc)
        {
            animator.SetBool("isWalk", false);
            attackTimer += Time.deltaTime;
            if (attackTimer >= 6f)
            {
                animator.SetTrigger("Attack");
                attackTimer = 0f;
                StartCoroutine(AttackDamage());
            }
        }

        timer += Time.deltaTime;
        switch (currentState)
        {
            case EnemyStates.Patrol:
                if (!stopAllFunc)
                {
                    Patrol();
                }
                break;
            case EnemyStates.Attack:
                if (!stopAllFunc)
                {
                    Attack();
                }
                break;
        }
    }

    private void Attack()
    {
        currentSpeed = runSpeed;
        if (distance >= 3f)
        {
            animator.SetBool("isWalk", true);
        }
        if (distance >= 1.2f)
        {
            transform.Translate(0f, 0f, currentSpeed * Time.deltaTime);
        }
    }

    public override void GetDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth > 0)
        {
            animator.SetTrigger("Hit");
            enemyHealthBar.value = currentHealth;
            bloodEffect = Instantiate(bloodEffectParticle, transform.position, Quaternion.identity);
        }
        if (currentHealth <= 0)
        {
            isDie = true;
            stopAllFunc = true;
            enemyHealthBar.value = 0;
            animator.SetTrigger("Die");
            StartCoroutine(Death(3f));
        }
    }

    public void Patrol()
    {
        //currentSpeed = patrolSpeed;
        //transform.Translate(0f, 0f, currentSpeed * Time.deltaTime);

        //if (timer > patrolTimer)
        //{
        //    isForward = !isForward;
        //    timer = 0f;
        //}

        //float yRotation = isForward ? 90f : -90f;
        //transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        currentSpeed = patrolSpeed;
        transform.Translate(0f, 0f, currentSpeed * Time.deltaTime);
        if (timer > patrolTimer)
        {
            isForward = !isForward;
            float yRotation = transform.localRotation.eulerAngles.y;
            yRotation += 180f;
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            timer = 0;
        }

    }

    private IEnumerator AttackDamage()
    {
        //RaycastHit hit;
        //bool isHit = Physics.CapsuleCast(transform.position, hitBox.transform.position, 0.2f, hitBox.transform.forward, out hit, 1f);
        //if (isHit)
        //{
        //    if (hit.transform.TryGetComponent(out PlayerHealth playerHealth))
        //    {
        //        playerHealth?.TakeDamage(damage);
        //        attackTimer = 0f;
        //    }
        //}
        yield return new WaitForSeconds(1f);
        RaycastHit hit;
        bool isHit = Physics.Raycast(hitBox.transform.position, hitBox.transform.forward, out hit, 3f);
        if (isHit)
        {
            if (hit.transform.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(damage);
                //attackTimer = 0f;
            }
        }
    }

}
