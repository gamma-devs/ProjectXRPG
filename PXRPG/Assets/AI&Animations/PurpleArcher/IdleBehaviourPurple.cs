using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  This is the idle AI for purple archer, this archer will just stand on his place and shoot the player when
 *  player is in range.
 */
public class IdleBehaviourPurple : StateMachineBehaviour
{
    PlayerController player;
    public float alertRadius;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 enemyPos = new Vector2(animator.transform.position.x, animator.transform.position.z);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        float distance = Vector2.Distance(enemyPos, playerPos);
        if(distance < alertRadius)
        {
            //Set charge to true.
            animator.SetBool("charge", true);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
