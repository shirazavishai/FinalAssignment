using UnityEngine;

public class ShotManager : MonoBehaviour
{
    [SerializeField] protected float timeBetweenAttacks;
    protected bool alreadyAttacked;
    [SerializeField] protected GameObject projectileDefense;
    [SerializeField] protected GameObject projectileAttack;


    // Update is called once per frame 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(SceneManager.IsAttack ? projectileAttack : projectileDefense, transform.position + Vector3.forward * 2, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 150, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
