using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using TMPro;
using static ConsumeLabels;

public class FoodGameManager : MonoBehaviour
{
    //UI
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;

    //Dialogue
    [SerializeField] private AudioClip dialogue_1;
    [SerializeField] private AudioClip dialogue_2;

    //WIN Condition
    [SerializeField] private AudioClip win_1;
    [SerializeField] private AudioClip lose_1;




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

    private int gameOutcome = 0;

    //Time Interval
    float cooldown = 3;
    float elapsedTime = 0;
    bool isNotFinished = true;



    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown < 0 && isNotFinished)
        {
            cooldown = Random.Range(2, 4);
            int choice = Random.Range(1, 5);
            if(choice < 3) {
                AudioManager.Instance.PlayVox(dialogue_2);
            }
           else
            {
                AudioManager.Instance.PlayVox(dialogue_1);
            }

        }
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
            
            milkVal = valAssigned - 4;
            myTask1 = true;

            referenceFood.GetComponent<ConsumeLabels>().TriggerAnim();
            //Wait for the animation to end\
            
            UpdateWallUI();


        }

        if (taskAssigned == 2)
        {
           
            waterVal = valAssigned;
            myTask2 = true;
            referenceFood.GetComponent<ConsumeLabels>().TriggerAnim();
            //Wait for the animation to end
            UpdateWallUI();


        }

        valAssigned = 0;
        taskAssigned = 0;
        referenceFood = null;
        IsCompleted();
    }

    private void UpdateWallUI()
    {
        string BaseText = "Milk Bottle Ingredients<br>";
        string Line1;
        string Line2;
        if (MyTask1)       {
            Line1 = "- Milk (Done) <br>" ;
        }

        else {
            Line1 = "- Milk<br>";
        }

        if (MyTask2)
        {
            Line2 = "- Water (Done) <br>";
        }

        else
        {
            Line2 = "- Water<br>";
        }

        m_TextMeshProUGUI.text = BaseText + Line1 + Line2;
    }

    private bool IsCompleted()
    {
        if (myTask1 && myTask2) {
            //Debug.Log("Done Mixing");
            GetComponent<Animator>().SetInteger("Total Ingredients", 2);
            isNotFinished = false;
            return true;
        }

        return false;
    }

    public void ProcessResults()
    {
        //Win Condition
        if (waterVal == 1 && milkVal == 1)
        {
            //Send the information and proceed to next minigame
            gameOutcome = 1;
            GameManager.Instance.SetWinState(1, true);
            AudioManager.Instance.PlayVox(win_1);
            PlayWin();
            return;
        }

        //Lose
        else
        {
            if (milkVal == 2 && waterVal == 1)
            {
                //Gigachad
                gameOutcome = 2;
                GameManager.Instance.SetWinState(1, false);

               
            }

            else
            {
                //Default Lose
                gameOutcome = 3;
                GameManager.Instance.SetWinState(1, false);
                
            }

            PlayLose();
            return;
        }
    }

    public int RetrieveOutcome()
    {
        return gameOutcome;
    }

    public void PlayWin()
    {
        AudioManager.Instance.PlayVox(win_1);
    }

    public void PlayLose()
    {
        AudioManager.Instance.PlayVox(lose_1);
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
