using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowardsPlayer : StateMachineBehaviour
{
    private Transform player;
    private Rigidbody2D rgBody;
    [SerializeField] private float speed = 3;
    [SerializeField] private float attRange = 2.5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rgBody = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<Enemy>().vulnerable)
        {
            Vector2 target = new Vector2(player.position.x, rgBody.position.y);
            Vector2 uvec = new Vector2((player.position.x - rgBody.position.x), (player.position.y - rgBody.position.y)).normalized;
            rgBody.velocity = new Vector2(uvec.x * speed, rgBody.velocity.y);
        }
        if (Vector2.Distance(player.position, rgBody.position) < attRange + 0.1 && (Vector2.Distance(player.position, rgBody.position) > attRange - 0.1))
        {
            animator.SetTrigger("JumpAttack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
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
