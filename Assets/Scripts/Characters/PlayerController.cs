using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        Mousemanager.instance.OnMouseClicked += MoveToTarget;
    }
    public void MoveToTarget(Vector3 target)
    {
        agent.destination = target;
    }
    void Update()
    {
            SwitchAnimation();
    }
    public void SwitchAnimation()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

}
