﻿using UnityEngine;

public class RandomValue : StateMachineBehaviour
{
    public int maxValue;
    public string mirrorValue = "MirrorName";
    public string nameValue = "Name";

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var value = Random.Range(0, maxValue);

        while (value == animator.GetFloat(mirrorValue)) value = Random.Range(0, maxValue);

        animator.SetFloat(nameValue, value);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var value = Random.Range(0, maxValue);

        while (value == animator.GetFloat(mirrorValue)) value = Random.Range(0, maxValue);

        animator.SetFloat(nameValue, value);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}