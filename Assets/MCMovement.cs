using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MCMovement : MonoBehaviour
{
    Camera cammy;
    UnityEngine.AI.NavMeshAgent agent;
    public LayerMask ground;

    private void Start()
    {
        cammy = Camera.main;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
    }
}
