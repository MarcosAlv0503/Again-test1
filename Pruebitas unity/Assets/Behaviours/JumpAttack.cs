using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : StateMachineBehaviour
{
    private Rigidbody2D rgBody;
    private Collider2D coll;
    private Transform player;
    private Enemy enemy;
    private bool falling = false;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float jumpForce = 10f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemy.attack = true;
        enemy.vulnerable = true;
        
        rgBody = animator.GetComponent<Rigidbody2D>();
        coll = animator.GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 target = new Vector2(player.position.x, player.position.y + 3);
        Vector2 uvec = new Vector2((target.x - rgBody.position.x), (target.y - rgBody.position.y)).normalized;
        enemy.flipFollowPlayer = false;
        if (enemy.isFlipped)
        {
            rgBody.transform.rotation = Quaternion.Euler(animator.transform.rotation.x, 180f, 40);
        }
        else
        {
            rgBody.transform.rotation = Quaternion.Euler(animator.transform.rotation.x, 0, 40);
        }
        rgBody.velocity = uvec * jumpForce;
        animator.SetBool("Grounded", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(rgBody.velocity.y < -0.01f)
        {
            falling = true;
        }
        if (falling)
        {
            if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .2f, ground))
            {
                falling = false;
                animator.SetBool("Grounded", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        rgBody.transform.rotation = Quaternion.Euler(animator.transform.rotation.x, animator.transform.rotation.y, 0);
        enemy.flipFollowPlayer = true;
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
