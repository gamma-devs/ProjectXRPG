using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBehaviour : StateMachineBehaviour
{
    public GameObject cs;
    GameObject lr;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lr = Instantiate(cs);//.GetComponent<LineRenderer>();
        lr.GetComponent<CurveScript>().draw(animator.GetComponent<SpriteRenderer>().transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lr.GetComponent<CurveScript>().updatePositions(animator.GetComponent<SpriteRenderer>().transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(lr);
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
