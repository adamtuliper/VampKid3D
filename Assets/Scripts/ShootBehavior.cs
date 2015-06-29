using UnityEngine;
using System.Collections;
using System.Net;

public class ShootBehavior : StateMachineBehaviour
{

    //Have we already shot - need to wait before we can reset.
    private bool _shot;
    public Rigidbody Projectile;
    public int Speed;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Reset variable
        _shot = false;
    }
  

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Don't overlap shot animations
        if (stateInfo.normalizedTime > .4f && !_shot)
        {
            _shot = true;
            var fireLocation = GameObject.FindGameObjectWithTag("FireLocation").transform;
            var projectile = Instantiate(Projectile, fireLocation.position, Quaternion.identity) as Rigidbody;

            projectile.velocity = animator.gameObject.transform.TransformDirection(Vector3.forward*Speed);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
