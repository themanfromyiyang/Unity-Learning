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
        var target = attackTarget;
        if (target == null)
        {
            yield break;
        }

        var targetTransform = target.transform;
        agent.isStopped = false;
        transform.LookAt(targetTransform);
        // TODO:修改攻击范围参数

        while(target != null && (transform.position - targetTransform.position).sqrMagnitude > 1)
        {
            agent.destination = targetTransform.position;
            yield return null;
        }

        if (target == null)
        {
            agent.isStopped = true;
            yield break;
        }

        agent.isStopped = true;

        if (lastattackTime  < 0)
        {
            anim.SetTrigger("Attack");

            lastattackTime = 0.5f;
        }

    }




}
