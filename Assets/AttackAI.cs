using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerAttackAI : MonoBehaviour
{
    public LayerMask targetLayer; // Specify the layer where the target objects reside
    public float detectionRange = 10f; // Set the detection range
    public float attackDistance = 3f; // Set the attack distance

    private NavMeshAgent agent;
    private GameObject currentTarget;
    private bool isFollowing;
    private bool isAttacking;
    public float timer;
    public float attackcd = 5f;
    public float health = 50f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance; // Set stopping distance to attack distance
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!isFollowing)
        {
            FindTarget();
        }
        else
        {
            if (currentTarget != null)
            {
                // Move towards the target
                agent.SetDestination(currentTarget.transform.position);

                // Check if within attack distance
                float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
                if (!isAttacking && distance <= attackDistance)
                {
                    isAttacking = true;
                }
            }
            else
            {
                // If target is lost, stop attacking
                isAttacking = false;
                isFollowing = false;
            }
        }
        if (isAttacking)
        {
            if (timer >= attackcd){
                Debug.Log(timer);
                Attack(currentTarget);
            }
            
        }
    }

    private void FindTarget()
    {
        // Find all objects in the target layer within the detection range
        Collider[] targetColliders = Physics.OverlapSphere(transform.position, detectionRange, targetLayer);

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
            isFollowing = true;
        }
    }

    private void Attack(GameObject target)
    {
        Debug.Log("Attacking: " + target.name);
        if(target.GetComponent<PlayerAttackAI>()){
            target.GetComponent<PlayerAttackAI>().health -= 5;
            if(target.GetComponent<PlayerAttackAI>().health<= 0){
                target.GetComponent<PlayerAttackAI>().Death();
            }
        }
        if(target.GetComponent<RTSBase>()){
            target.GetComponent<RTSBase>().TakeDamage(5);
            if(target.GetComponent<RTSBase>().baseHealth <= 0){
                target.GetComponent<RTSBase>().DestroyBase();
            }
        }
        timer = 0f;
        isAttacking = false;
    }

    private void Death(){
        Destroy(gameObject);
    }

    // Visualize the detection range and attack distance in the Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
