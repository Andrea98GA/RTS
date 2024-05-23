using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerAttack : MonoBehaviour
{
    public LayerMask targetLayer; // Specify the layer where the target objects reside
    public float attackDistance = 3f; // Set the attack distance

    private NavMeshAgent agent;
    private GameObject currentTarget;
    private bool isAttacking;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance; // Set stopping distance to attack distance
    }

    private void Update()
    {
        if (!isAttacking)
        {
            FindTarget();
        }
        else
        {
            if (currentTarget != null)
            {
                // Move towards the target
                agent.SetDestination(currentTarget.transform.position);
                
                // Check if the target is within attack distance
                float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
                if (distance <= attackDistance)
                {
                    Attack(currentTarget);
                }
            }
            else
            {
                // If target is lost, stop attacking
                isAttacking = false;
            }
        }
    }

    private void FindTarget()
    {
        // Find all objects in the target layer
        Collider[] targetColliders = Physics.OverlapSphere(transform.position, attackDistance, targetLayer);

        // If there are target objects, choose the closest one as the current target
        if (targetColliders.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            GameObject closestTarget = null;
            foreach (Collider targetCollider in targetColliders)
            {
                float distance = Vector3.Distance(transform.position, targetCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = targetCollider.gameObject;
                }
            }
            currentTarget = closestTarget;
            isAttacking = true;
        }
    }

    private void Attack(GameObject target)
    {
        // Implement your attack logic here
        Debug.Log("Attacking: " + target.name);
        // You can damage the target, destroy it, or perform any other action
    }

    // Visualize the attack distance in the Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
