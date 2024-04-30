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
    public static bool DoAbleParry { get; set; } = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = health;
    }


    public void TakeDamage(float amount)
    {
        DoAbleParry = false;
        currentHealth -= amount;
        if (currentHealth > 0 && !PlayerDash.isDashing)
        {
            if (!PlayerAttack.isAttack && !Parry.IsParryAnimation)
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
