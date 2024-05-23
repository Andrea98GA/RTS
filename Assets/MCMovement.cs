using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MCMovement : MonoBehaviour
{
    Camera cammy;
    NavMeshAgent agent;
    public LayerMask ground;

    private void Start()
    {
        cammy = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cammy.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground)) 
            {
                agent.SetDestination(hit.point);
            }

        }

        if (agent.hasPath)
            {
                if (agent.remainingDistance < 0.1f && !agent.pathPending)
                {
                    Vector3 randomDirection = Random.insideUnitSphere * 5f;
                    randomDirection += transform.position;
                    NavMeshHit hit;
                    NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas);
                    Vector3 finalPosition = hit.position;
                    agent.SetDestination(finalPosition);
                }
            }

    }

}
