using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, ICanTakeDamage
{
    #region Public Properties
    [Header("FX")]
    public GameObject hurtEffect;
    [HideInInspector]
    public bool isDead = false;
    [Header("MaxHealth")]
    public float maxHealth = 1000;
    public float nextTime = 0;
    public float rateTime = 2.5f;
    public int damageToGive = 50;
    public Vector2 force;
    public Transform skillTarget;
    public Transform Teleport1;
    public Transform Teleport2;
    #endregion
    #region Private properties
    private float currentHealth;
    private int isDeadId;
    private int isAttackId;
    private int isIdleId;
    private int isSkillId;
    private int isSkillOutId;
    private Animator anim;
    private Player player;
    private EnemyAI enemyAI;
    private float nextSkillTime = 5f;
    private float skillRateTime = 5f;
    private float nextTeleportTime = 7f;
    private float teleportRateTime = 3f;
    #endregion

    void Start()
    {
        currentHealth = maxHealth;
        isDeadId = Animator.StringToHash("isDead");
        isAttackId = Animator.StringToHash("isAttack");
        isIdleId = Animator.StringToHash("isIdle");
        isSkillId = Animator.StringToHash("isSkill");
        isSkillOutId = Animator.StringToHash("isSkillOut");
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindObjectOfType<Player>();

        enemyAI = GetComponent<EnemyAI>();
    }

    void Update()
    {
        // Check if it's time to cast a skill
        if (Time.time >= nextSkillTime && !isDead && player != null && !player.isDead)
        {
            StartCoroutine(CastSkill());
            nextSkillTime = Time.time + skillRateTime; // Set the next skill cast time
        }
        if (Time.time >= nextTeleportTime && !isDead && player != null && !player.isDead)
        {
            StartCoroutine(CastTeleport());
            nextTeleportTime = Time.time + teleportRateTime; // Set the next skill cast time
        }
    }

    public void TakeDamage(int damage, Vector2 force, GameObject instigator)
    {
        if (isDead) return;
        currentHealth -= damage;
        if (hurtEffect != null)
        {
            Instantiate(hurtEffect, instigator.transform.position, Quaternion.identity);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            anim.SetTrigger(isDeadId);

            Destroy(gameObject, 1.9f);
        }
        else
        {
            anim.SetTrigger("isDamaged");
        }
        Debug.Log("Enemy took damage");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.isDead) return;
        if (player == null) return;
        if (collision.CompareTag("Player"))
        {
            if (Time.time > nextTime)
            {
                nextTime = Time.time + rateTime;
                
                anim.SetTrigger(isAttackId);
                StartCoroutine(DelayedDamageToPlayer());
            }
        }
    }

    IEnumerator CastSkill()
    {
        anim.SetTrigger(isSkillId);
        yield return new WaitForSeconds(0f);
        Animator skillTargetAnimator = skillTarget.GetComponent<Animator>();
        if (skillTargetAnimator != null)
        {
            skillTargetAnimator.SetTrigger("isSkillOut");
        }
    }
    IEnumerator CastTeleport()
    {
        anim.SetTrigger("isTele"); 
        yield return new WaitForSeconds(0f);

        Transform teleportTarget = (Random.Range(0, 2) == 0) ? Teleport1 : Teleport2;

        transform.position = teleportTarget.position;

        Animator teleportAnimator = teleportTarget.GetComponent<Animator>();
        if (teleportAnimator != null)
        {
            teleportAnimator.SetTrigger("isTele");
        }
    }

    IEnumerator DelayedDamageToPlayer()
    {
        yield return new WaitForSeconds(0.7f); // Wait

        
        if (player != null && !player.isDead)
        {
            player.TakeDamage(damageToGive, force, gameObject);
        }
    }
}
