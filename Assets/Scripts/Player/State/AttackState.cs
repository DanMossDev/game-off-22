using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState
{
    float initTime;
    bool playedSound;
    public override void EnterState(PlayerController context) 
    {
        SFXController.Instance.PlaySFX(context.attackSound);
        context.animator.ResetTrigger("Attack");
        context.animator.SetTrigger("Attack");
        context.canAttack = false;
        playedSound = false;
        
        InitAttack(context);
    }

    public override void OnDive(PlayerController context, bool isPressed)
    {}
    public override void OnAttack(PlayerController context)
    {}

    void InitAttack(PlayerController context)
    {
        initTime = Time.time;
        context.isInvincible = true;
        context.rigidBody.useGravity = false;
        context.rigidBody.velocity *= 0.05f;
    }
    public override void UpdateState(PlayerController context) 
    {
        Rotate(context);
        if (Time.time - initTime < 0.25f) return;
        if (Time.time - initTime >= 3) {
            context.ChangeState(context.baseState);
            return;
        }

        if (context.target == null) {
            Vector3 diveBurst = new Vector3(context.horizontalInput, 0, context.verticalInput).normalized;

            context.rigidBody.AddForce(diveBurst * 40, ForceMode.Impulse);
            context.ChangeState(context.baseState);
            return;
        }
        if (!playedSound) {
            SFXController.Instance.PlaySFX(context.diveSound);
            playedSound = true;
        }

        context.rigidBody.velocity = (context.target.transform.position - context.transform.position) * 10;
    }
    public override void LeaveState(PlayerController context) 
    {
        context.target = null;
        context.rigidBody.useGravity = true;
        context.EndInvincibility();
    }

    public override void OnCollision(PlayerController context, Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.GetComponent<HPManager>().TakeDamage();
            context.rigidBody.AddForce((other.contacts[0].normal + Vector3.up * 3).normalized * context.hitBounce / 1.5f, ForceMode.VelocityChange);
            context.canAttack = true;
            context.ChangeState(context.baseState);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            other.gameObject.GetComponent<BossTarget>().OnHit();
            context.rigidBody.AddForce((other.contacts[0].normal + Vector3.up * 2).normalized * context.hitBounce, ForceMode.VelocityChange);
            context.canAttack = true;
            context.ChangeState(context.baseState);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Boss")) context.TakeDamage(other);
    }

    void Rotate(PlayerController context)
    {
        Quaternion toRotation;
        if (context.target != null) toRotation = Quaternion.LookRotation(new Vector3(context.target.transform.position.x - context.transform.position.x, 0, context.target.transform.position.z - context.transform.position.z), Vector3.up);
        else if (context.horizontalInput == 0 && context.verticalInput == 0) return;
        else toRotation = Quaternion.LookRotation(new Vector3(context.horizontalInput, 0, context.verticalInput), Vector3.up);
        context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, toRotation, context.rotationSpeed * 4 * Time.deltaTime);
    }
}
