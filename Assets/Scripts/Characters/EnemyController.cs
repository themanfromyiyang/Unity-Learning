using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyStates{ GUARD, PATROL, CHASE, DEAD } 
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour

{
    public EnemyStates enemyStates;
    private NavMeshAgent agent;

    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
    }
 
}
