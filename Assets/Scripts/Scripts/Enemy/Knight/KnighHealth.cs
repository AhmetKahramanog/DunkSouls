using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnighHealth : Enemy
{
    [SerializeField] private float health;
    private float currentHealth;
    public Slider knightEnemyHealthBar;
    [SerializeField] private ParticleSystem bloodParticle;
    private Animator animator;
    public bool IsDie { get; set; } = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = health;
        bloodParticle.Stop();
    }

    public override void GetDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth > 0)
        {
            knightEnemyHealthBar.value = currentHealth;
            animator.SetTrigger("Hit");
            bloodParticle.Play();
        }
        else
        {
            knightEnemyHealthBar.value = 0;
            animator.SetTrigger("Die");
            StartCoroutine(Death(6f));
            IsDie = true;
        }
    }
}
