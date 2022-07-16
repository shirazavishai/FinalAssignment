using UnityEngine;
using UnityEngine.UI;
using System;

public class BaseSoldier : MonoBehaviour
{
    [SerializeField] protected GameObject projectile;
    [SerializeField] public LayerMask whatToAttack;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Slider slider;
    [SerializeField] protected float timeBetweenAttacks;
    [SerializeField] protected float sightRange, attackRange;
    [SerializeField] protected Transform _gunPosition;
    [SerializeField] protected float health;

    protected Collider[] enemyInSightRange, enemyInAttackRange;

    protected bool _isAttacking;

    public event Action DeathEvent;

    private bool ShouldAttack
    {
        get
        {
            return (enemyInAttackRange != null && enemyInAttackRange.Length > 0);
        }
    }

    private void Update()
    {
        enemyInSightRange = Physics.OverlapSphere(transform.position, sightRange, whatToAttack);
        enemyInAttackRange = Physics.OverlapSphere(transform.position, attackRange, whatToAttack);

        if (ShouldAttack)
        {
            Shoot();
        }

        Move();
    }

    private void Shoot()
    {
        if (_isAttacking) return;

        _isAttacking = true;

        _animator.SetTrigger("Shoot");

        var currentEnemy = enemyInAttackRange[0];
        transform.LookAt(currentEnemy.transform);

        var towardsVector = (currentEnemy.transform.position - _gunPosition.position).normalized;
        Rigidbody rb = Instantiate(projectile, _gunPosition.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(towardsVector * 300f, ForceMode.Impulse);

        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    private void ResetAttack()
    {
        _isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Defense") && other.CompareTag("AmoAttack") || gameObject.CompareTag("Attack") && other.CompareTag("AmoDefense"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        slider.value = health / 3;

        if (health <= 0) Invoke(nameof(DestroySelf), 0.5f);
    }

    private void DestroySelf()
    {
        if (DeathEvent != null)
        {
            DeathEvent.Invoke();
        }
        Destroy(gameObject);
    }

    protected virtual void Move() { }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

