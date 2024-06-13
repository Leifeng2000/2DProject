using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("LeiFeng/PlayerAttack")]

public class PlayerAttack : MonoBehaviour
{
    public float radiusAttack = 0.5f;
    public Transform pointAttack;
    public float attackRate = 0.2f;
    public float nextAttack = 0;
    public LayerMask enemyLayer;
    public float timerDelay = 0.2f;
    public int damageToGive = 50;
    public Vector2 force;
    private Animator anim;
    private int isAttackAnimationId;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        isAttackAnimationId = Animator.StringToHash("isAttack");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger(isAttackAnimationId);
            GetKeyR();
        }
    }
    private bool GetKeyR()
    {
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            StartCoroutine(Attack(timerDelay));
            return true;
        }
        else
            return false;
    }
    IEnumerator Attack(float delay)
    {
        yield return new WaitForSeconds(delay);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(pointAttack.position, radiusAttack, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            var damageable = enemy.GetComponent<ICanTakeDamage>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageToGive, force, gameObject);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (pointAttack != null)
        {
            Gizmos.DrawWireSphere(pointAttack.position, radiusAttack);
        }
    }
}
