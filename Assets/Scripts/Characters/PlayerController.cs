using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour


{
    private NavMeshAgent agent;
    private Animator anim;

    private GameObject attackTarget;
    private float lastattackTime;
    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        Mousemanager.instance.OnMouseClicked += MoveToTarget;
        Mousemanager.instance.OnEnemyClicked += EventAttack;
    }
    
    void Update()
    {
        SwitchAnimation();
        lastattackTime -= Time.deltaTime;
    }
    public void SwitchAnimation()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void MoveToTarget(Vector3 target)
    {
        StopAllCoroutines();
        agent.isStopped = false;
        agent.destination = target;
    }

    public void EventAttack(GameObject target)
    {
        if(target != null)
        {
            attackTarget = target;
            StartCoroutine(MoveToAttackTarget());

        }
    }

    IEnumerator MoveToAttackTarget()
    {
        agent.isStopped = false;
        transform.LookAt(attackTarget.transform);

        while(Vector3.Distance(transform.position, attackTarget.transform.position) > 1)
        {
            agent.destination = attackTarget.transform.position;
            yield return null;
        }

        agent.isStopped = true;

        if (lastattackTime  < 0)
        {
            anim.SetTrigger("Attack");

            lastattackTime = 0.5f;
        }

    }




}
