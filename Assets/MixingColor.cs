using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingColor : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
    float elapsedTime = 0;

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float time = 3.0f;
        

        Color colorRed = Color.red;
        Color colorGreen = Color.green;

        SpriteRenderer spriteRender = animator.gameObject.GetComponent<SpriteRenderer>();
        FoodGameManager manager = animator.gameObject.GetComponent<FoodGameManager>();


        elapsedTime += Time.deltaTime;

        if (elapsedTime + 1.5f > time && elapsedTime < 3.0f)
        {
            int randNum = Random.RandomRange(-2, 2);
            if (randNum > 0)
            {
                //Get the Color Commponent of the sprite
                spriteRender.color = colorRed;
            }

            else
            {
                spriteRender.color = colorGreen;
            }
        }

        else if(elapsedTime > time)
        {
            animator.SetInteger("Total Ingredients", 0);
        }

        //if (elapsedTime > time)
        //{
        //    manager.ProcessResults();

        //    //Change the value of the param.
        //    animator.SetInteger("Total Ingredients", 0);

        //    //Process the new value color
        //    if (manager.RetrieveOutcome() == 1)
        //    {
        //        spriteRender.color = colorGreen;
        //        Debug.Log("Good Bottle");
        //    }

        //    else
        //    {
        //        spriteRender.color = colorRed;
        //        Debug.Log("BAd Bottle");
        //    }

        //    elapsedTime = 0;

        //}
       

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Color colorRed = Color.red;
        Color colorGreen = Color.green;

        SpriteRenderer spriteRender = animator.gameObject.GetComponent<SpriteRenderer>();
        FoodGameManager manager = animator.gameObject.GetComponent<FoodGameManager>();

       
        manager.ProcessResults();

        

        //Process the new value color
        if (manager.RetrieveOutcome() == 1)
        {
            spriteRender.color = colorGreen;
            Debug.Log("Good Bottle");
        }

        else
        {
            spriteRender.color = colorRed;
            Debug.Log("BAd Bottle");
        }

        elapsedTime = 0;

       
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
