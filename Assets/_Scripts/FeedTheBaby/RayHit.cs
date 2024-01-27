using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHit : MonoBehaviour
{
    private Camera m_Camera;

    //Hold Game Object Information
    private bool isHold = false;
    private GameObject m_HoldObject;
    private Vector3 m_PrevPosition;
   

    //Reference Dropping Off
    [SerializeField] private GameObject m_MixingObject;
    


    private void OnEnable()
    {
        m_Camera = GetComponent<Camera>();

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isHold)
        {
            
            RegisterHit();
            isHold = true;
        }

        if (Input.GetMouseButton(0) && isHold && m_HoldObject != null)
        {
            //Hold Function
          
            HoldObject();
        }

        if (Input.GetMouseButtonUp(0) && isHold)
        {
            //Drop Function
           
            ObjectRelease();
            isHold = false;
        }
    }

    private void RegisterHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name == "Food Bottle")
                return;

            m_HoldObject = hit.collider.gameObject;
            m_PrevPosition = hit.collider.gameObject.transform.position;
            

        }
    }

    private void HoldObject()
    {
        Vector3 camPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_HoldObject.transform.position = new Vector3(camPosition.x, camPosition.y, m_HoldObject.transform.position.z);
    }

    private void ObjectRelease()
    {
        
     
        //Check if its task are completed
        FoodGameManager manager = m_MixingObject.GetComponent<FoodGameManager>();
        //if (manager.IsValidIngredient())
        //{
        //    Debug.Log("Accepted Ingredient");
        //}
        manager.ResetTrigger();
       
        //Remove Hold Position

        if (m_HoldObject != null)
            m_HoldObject.transform.position = new Vector3(m_PrevPosition.x, m_PrevPosition.y, m_PrevPosition.z);


        m_PrevPosition = Vector3.zero;
        m_HoldObject = null;
    }


}
