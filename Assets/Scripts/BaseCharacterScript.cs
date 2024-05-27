using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterScript : MonoBehaviour
{
    protected float health;
    protected StatusEffects currentStatEffect;

    protected float shieldHealth = 0;

    private float shieldBreakEffectSize = 2f;
    private float shieldBreakDamage = 0;
    private StatusEffects shieldBreakStatusEffect = StatusEffects.NONE;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if (shieldHealth > 0)
        {
            float remainingDamage = damage - shieldHealth;

            if (remainingDamage > 0)
            {
                shieldHealth = 0;
                TakeDamage(remainingDamage);
                ProcessShieldEffect();
            }
            else
            {
                shieldHealth -= damage;
                if (shieldHealth <= 0)
                {
                    ProcessShieldEffect();
                }
            }
        }
        else
        {
            health -= damage;
        }
    }

    public void AddShield(float shieldAmount, float shieldBreakDamage = 0, StatusEffects shieldBrokenStatusEffect = StatusEffects.NONE) 
    {
        shieldHealth += shieldAmount;
        shieldBreakEffectSize += shieldBreakDamage;
        this.shieldBreakStatusEffect = shieldBrokenStatusEffect;
    }

    public void SetShieldBreakSize(float shieldBreakEffectSize)
    {
        this.shieldBreakEffectSize = shieldBreakEffectSize;
    }

    private void ApplyStatusEffect(StatusEffects status)
    {
        currentStatEffect = status;
    }

    private void BreakShield()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, shieldBreakEffectSize);

        foreach (var hitCollider in hitColliders)
        {
            BaseCharacterScript health = hitCollider.GetComponent<BaseCharacterScript>();
            if (health != null)
            {
                health.TakeDamage(shieldBreakDamage);
                health.ApplyStatusEffect(shieldBreakStatusEffect);
            }
        }
    }

    private void ProcessShieldEffect()
    {
        if (shieldBreakDamage > 0 || shieldBreakStatusEffect != StatusEffects.NONE)
        {
            BreakShield();
        }

        // Does nothing when there is no shield break damage/status
        return;
    }
}

public enum StatusEffects
{
    NONE,
    FREEZE,
}
