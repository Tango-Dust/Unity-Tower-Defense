using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{

    public GameObject goal;
    public NavMeshAgent agent;
    public bool seekAndDestroy;

    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Destination");
        agent.destination = goal.gameObject.transform.position;
        this.agent.acceleration = 100;
	}
	
	// Update is called once per frame
	void Update ()
    {
        seekAndDestroy = GetComponent<EnemyController>().seekAndDestroy;
        if (seekAndDestroy)
        {
            agent.isStopped = true;
        }
	}
}
