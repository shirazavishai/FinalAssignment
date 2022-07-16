using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AtackAI : BaseSoldier
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] public Transform _leader;
    private Vector3 _leaderOffset;
    public bool _isStaticLeader;
    private bool _alreadySetDestination;

    private void Start()
    {
        _alreadySetDestination = false;
        agent.updateRotation = false;
        _leaderOffset = new Vector3(Random.Range(-10, -10), 0, Random.Range(-10, 10));    
    }

    protected override void Move()
    {
        if (ShouldChaseEnemy)
        {
            Follow(enemyInSightRange[0].transform);
        }
        else
        {
            Follow(_leader);
        }
    }

    private void Follow(Transform toFollow)
    {
        if ((_leader.position - transform.position).magnitude < 15)
        {
            agent.Stop();
            _animator.SetBool("IsRunning", false);
        }
        else
        {
            if (agent.isStopped)
            {
                agent.Resume();
            }

            if (!_isStaticLeader)
            {
                _animator.SetBool("IsRunning", true);
                agent.SetDestination(toFollow.position + _leaderOffset);
            }
            else
            {
                if (!_alreadySetDestination)
                {
                    _alreadySetDestination = true;
                    _animator.SetBool("IsRunning", true);
                    agent.SetDestination(toFollow.position + _leaderOffset);
                }
            }
        }
    }

    protected bool ShouldChaseEnemy
    {
        get
        {
            return enemyInSightRange != null && enemyInSightRange.Length > 0 || (enemyInAttackRange == null && enemyInAttackRange.Length >= 0);
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    protected void ChaseEnemy()
    {
        if (enemyInSightRange != null && enemyInSightRange.Length > 0)
        {
            agent.SetDestination(enemyInSightRange[0].transform.position);
        }
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }
}