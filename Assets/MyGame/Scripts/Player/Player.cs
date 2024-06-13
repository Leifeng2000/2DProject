using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerAttack;
[AddComponentMenu("LeiFeng/Player")]

public class Player : MonoBehaviour
{
    public float maxHealth = 1000;
    private float currentHealth = 0;
    public GameObject hurtEffect;
    [HideInInspector]
    public bool isDead = false;

    private Animator animator;

    public Slider healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage, Vector2 force, GameObject instigator)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            animator.SetTrigger("isDead");

            Destroy(gameObject, 1f);
        }
        else
        {
            animator.SetTrigger("isDamaged");
            if (healthBar != null)
            {
                healthBar.value = currentHealth;
            }
        }
        Debug.Log("Player took damage");
    }

}

