using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashForce;

    private bool canDashing = true;
    internal static bool isDashing = false;
    public static bool DashAttack { get; set; } = false;

    [SerializeField] private float dashTimer = 0.4f;
    [SerializeField] private float dashCooldown = 1f;

    private Animator animator;
    private Rigidbody playerRB;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDashing)
        {
            StartCoroutine(Dash());
        }
        DashAttackDelay();
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        animator.SetTrigger("Dash");
        canDashing = false;
        yield return new WaitForSeconds(0.3f);
        float elapsedTime = 0f;
        while (elapsedTime < dashTimer)
        {
            playerRB.AddForce(transform.forward * dashForce * Time.deltaTime, ForceMode.VelocityChange);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Dashing(0.17f));
        yield return new WaitForSeconds(dashCooldown);
        canDashing = true;
    }

    private IEnumerator Dashing(float time)
    {
        yield return new WaitForSeconds(time);
        isDashing = false;
    }

    public void DashAttackDelay()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Roll"))
        {
            DashAttack = true;
            StartCoroutine(ResetDashAttackDelay());
        }
    }

    private IEnumerator ResetDashAttackDelay()
    {
        yield return new WaitForSeconds(0.3f);
        DashAttack = false;
    }

}
