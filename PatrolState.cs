using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public FSM machine;
    private float timer = 0;

    private int currentWayPoint = 0;

    public void Enter(FSM machine)
    {
        this.machine = machine;
        this.machine.animator.SetBool("isIdle", false);
        this.machine.animator.SetBool("isWalking", true);
    }

    public void Execute()
    {
        machine.health -= 5 * Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= 10)
        {
            machine.ChangeState(new IdleState());
        }

        if (machine.health < 10)
        {
            machine.ChangeState(new GoToBenchState());
        }
        Patrol();
    }

    public void Exit()
    {
        
    }

    private void Patrol()
    {
        machine.agent.SetDestination(machine.wayPoints[currentWayPoint].position);
        if(machine.agent.remainingDistance<= machine.agent.stoppingDistance)
        {
            currentWayPoint = Random.Range(0, machine.wayPoints.Length);
        }
    }
}
