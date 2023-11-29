using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroFight : StateMachineBehaviour
{
    private Transform backPoint;
    private Vector2 target;
    private Rigidbody2D rgBody;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        backPoint = GameObject.Find("backpoint").transform;
        target = new Vector2(backPoint.position.x, backPoint.position.y);
        rgBody = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 newPos = Vector2.MoveTowards(rgBody.position, target, 5 * Time.fixedDeltaTime);
        rgBody.MovePosition(newPos);
        if (Vector2.Distance(target, rgBody.position) <= 0.1)
        {
            animator.SetTrigger("JumpAttack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
