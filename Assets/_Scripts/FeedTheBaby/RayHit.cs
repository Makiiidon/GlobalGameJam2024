using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            //isHold = false;
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
        if (manager.IsValidIngredient()) {
            Debug.Log("Accepted Ingredient");
        }

        else
        {
            //Reset Position
            StartCoroutine(LerpReturn(m_HoldObject.transform.position, 0.2f));
        }
        manager.ResetTrigger();
    }
        
       
        

    IEnumerator LerpReturn(Vector3 startPos, float time)
    {

        //float t = 0;
        

        //while (t < 1) {
        //    t += Time.deltaTime * 1.0f;
        //    if (t > 1) { t = 1; }
        //    Vector3 lerpVal = Vector3.Lerp(startPos, m_PrevPosition, t);
        //    m_HoldObject.transform.position = lerpVal;
        //}

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the lerp factor between 0 and 1
            float lerpFactor = elapsedTime / time;

            // Use Lerp to interpolate between the start and target positions
            m_HoldObject.transform.position = Vector3.Lerp(startPos, m_PrevPosition, lerpFactor);

            yield return null;
        }

        // Ensure the final position is exactly the target position
        m_HoldObject.transform.position = m_PrevPosition;

        m_PrevPosition = Vector3.zero;
        m_HoldObject = null;

        yield return null;
        isHold = false;
    }


}
