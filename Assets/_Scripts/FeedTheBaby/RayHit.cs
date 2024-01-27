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

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GetComponent<Camera>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isHold)
        {
            Debug.Log("Trigger Left Click");
            RegisterHit();
            isHold  = true;
        }

        if (Input.GetMouseButton(0) && isHold && m_HoldObject != null)
        {
            //Hold Function
            Debug.Log("Hold Left Click");
            HoldObject();
        }

        if (Input.GetMouseButtonUp(0) && isHold)
        {
            //Drop Function
            Debug.Log("Release Left Click");
            ObjectRelease();
            isHold = false;
        }
    }

    private void RegisterHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            m_HoldObject = hit.collider.gameObject;
            m_PrevPosition = hit.collider.gameObject.transform.position;
            //Debug.Log(hit.collider.gameObject.name);
            //Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
            
        }
    }

    private void HoldObject()
    {
        Vector3 camPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_HoldObject.transform.position = new Vector3(camPosition.x, camPosition.y, m_HoldObject.transform.position.z);
    }

    private void ObjectRelease()
    {
        m_HoldObject.transform.position = new Vector3(m_PrevPosition.x, m_PrevPosition.y, m_PrevPosition.z);

        //Check if its inside the boundaries
        if (m_HoldObject.GetComponent<Collider2D>().bounds.Intersects
            (m_MixingObject.GetComponent<Collider2D>().bounds))
        {
            Debug.Log("Bounds intersecting");
        }

        //Check if its task are completed


        //Remove Hold Position

       

        m_PrevPosition = Vector3.zero;
        m_HoldObject = null;
    }
}
