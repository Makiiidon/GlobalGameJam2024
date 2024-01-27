using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static ConsumeLabels;

public class FoodGameManager : MonoBehaviour
{
    

    [SerializeField] private bool isValidInputIngredient = false;
    private GameObject referenceFood = null;
    private bool isOccupiedSpace = true;
    private int taskAssigned = 0; //1 for types 1 and 2 for ingredient ...
    private int valAssigned = 0; // Refer to Itemcode Enum

    private bool myTask1 = false;
    public bool MyTask1 { get { return myTask1; } }

    private bool myTask2 = false;
    public bool MyTask2 { get { return myTask2; } }

    public int waterVal = 0;
    public int milkVal = 0;



    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsValidIngredient()
    {
        return isValidInputIngredient;
    }

    
    public void ResetTrigger()
    {
        isOccupiedSpace = true;
        isValidInputIngredient = false;

        if (taskAssigned == 1)
        {
            Debug.LogError("Ingredient 1");
            valAssigned = valAssigned - 4;
            myTask1 = true;

            //Wait for the animation to end
            referenceFood.SetActive(false);
        }

        if (taskAssigned == 2)
        {
            Debug.LogError("Ingredient 2");
            //valAssigned = valAssigned;
            myTask2 = true;


            //Wait for the animation to end
            referenceFood.SetActive(false);
        }

        valAssigned = 0;
        taskAssigned = 0;
        referenceFood = null;
        IsCompleted();
    }

    private bool IsCompleted()
    {
        if (myTask1 && myTask2) {
            Debug.Log("Done Mixing");
            return true;
        }

        return false;
    }

    private void ProcessResults()
    {
        //Win Condition
        if (waterVal == 1 && milkVal == 1)
        {
            //Send the information and proceed to next minigame
            return;
        }

        //Lose
        else
        {
            if (milkVal == 2 && waterVal == 1)
            {
                //Gigachad
                return;
            }

            else
            {
                //Default Lose
                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Consumable") && isOccupiedSpace)
        {
        
            if (!collision.gameObject.GetComponent<ConsumeLabels>())
            {
                Debug.LogError("Missing Component");
            }

            if(!myTask1 && collision.gameObject.GetComponent<ConsumeLabels>().ItemType == INGREDIENTTYPES.MILK)
            {
              
                isValidInputIngredient = true;
                valAssigned = (int)collision.gameObject.GetComponent<ConsumeLabels>().ItemCode;
                taskAssigned = 1;
                referenceFood = collision.gameObject;
            }

            if (!myTask2 && collision.gameObject.GetComponent<ConsumeLabels>().ItemType == INGREDIENTTYPES.WATER)
            {
              
                isValidInputIngredient = true;
                valAssigned = (int)collision.gameObject.GetComponent<ConsumeLabels>().ItemCode;
                taskAssigned = 2;
                referenceFood = collision.gameObject;
            }
           
            isOccupiedSpace=false;

        }

    }
}
