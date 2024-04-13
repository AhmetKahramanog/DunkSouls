using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    private float currentHealth;
    [SerializeField] private Slider healthBar;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = health;
    }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth > 0 && !PlayerDash.isDashing)
        {
            if (!PlayerAttack.isAttack)
            {
                animator.SetTrigger("Hurt");
            }
            healthBar.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            healthBar.value = 0;
            animator.SetTrigger("Die");
            StartCoroutine(Death(1.6f));
        }
    }

    private IEnumerator Death(float delayDie)
    {
        yield return new WaitForSeconds(delayDie);
        Destroy(gameObject);
    }
}
