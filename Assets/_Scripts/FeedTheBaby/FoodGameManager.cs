using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class FoodGameManager : MonoBehaviour
{


    [SerializeField] private bool isValidInputIngredient = false;
    private bool isOccupiedSpace = true;
    //[SerializeField] private List<string> List 

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Consumable") && isOccupiedSpace)
        {
            Debug.LogWarning("Find Same Tag");
            isValidInputIngredient = true;
            isOccupiedSpace=false;
        }
    }

   
}
