using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffects
{
    NONE,
    FREEZE,
}

public class BaseCharacterScript : MonoBehaviour
{
    [SerializeField] protected float health;
    protected StatusEffects currentStatEffect;

    protected float shieldHealth = 0;

    private float shieldBreakEffectSize = 2f;
    private float shieldBreakDamage = 0;
    private StatusEffects shieldBreakStatusEffect = StatusEffects.NONE;

    public float ShieldBreakEffectSize
    {
        set
        {
            shieldBreakEffectSize = value;
        }
    }
    public StatusEffects ShieldBreakStatusEffect
    {
        set
        {
            shieldBreakStatusEffect = value;
        }
    }

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

    private void ProcessShieldEffect()
    {
        if (shieldBreakDamage > 0 || shieldBreakStatusEffect != StatusEffects.NONE)
        {
            BreakShield();
        }

        // Does nothing when there is no shield break damage/status
        return;
    }

    public void AddShield(float shieldAmount, float shieldBreakDamage = 0, StatusEffects shieldBreakStatusEffect = StatusEffects.NONE) 
    {
        shieldHealth += shieldAmount;
        if (currentStatEffect == StatusEffects.NONE)
        {
            this.shieldBreakStatusEffect = shieldBreakStatusEffect;
        }
        if (this.shieldBreakDamage < shieldBreakDamage)
        {
            this.shieldBreakDamage = shieldBreakDamage;
        }
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
                health.ShieldBreakStatusEffect = shieldBreakStatusEffect;
            }
        }

        shieldBreakDamage = 0;
    }
}
