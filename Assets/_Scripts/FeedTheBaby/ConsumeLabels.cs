using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class ConsumeLabels : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void OnEnable()
    {
        //StartCoroutine(LerpRotate(50, 2));
    }

    public enum INGREDIENTTYPES
    {
        WATER = 1,
        MILK = 2,
        NONE = 0
    }

    public enum ITEMCODE
    {
        CLEANWATER = 1,
        PURIFIEDWATER = 2,
        NONPURIFIEDWATER = 3,
        PUREWATER = 4,
        BABYMILK = 5,
        PROTEINMILK = 6,
        SOJUMILK = 7,
        WHITEPAINTMILK = 8,
        NONE = 0
    }

    [SerializeField] private INGREDIENTTYPES itemType = INGREDIENTTYPES.NONE ;
    [SerializeField] private ITEMCODE itemCode = ITEMCODE.NONE;

    public INGREDIENTTYPES ItemType { get { return itemType; } }
    public ITEMCODE ItemCode { get { return itemCode; } }


    public void SetItemType(INGREDIENTTYPES types)
    {
        itemType = types;
    }

    public void SetItemCode(ITEMCODE code)
    {
        itemCode = code;
    }

    public void TriggerAnim()
    {
        StartCoroutine(LerpRotate(45, 0.8f));
    }

    IEnumerator LerpRotate(float angle, float time)
    {
        float elapsedTime = 0;
        Vector3 intialRot = transform.rotation.eulerAngles;
        while (elapsedTime < time/2.0f)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the lerp factor between 0 and 1
            float lerpFactor = elapsedTime / time;

            // Use Lerp to interpolate between the start and target positions
            Vector3 Rot = new Vector3(intialRot.x, intialRot.y, Mathf.Lerp(0, angle, lerpFactor));
            transform.eulerAngles =  new Vector3(Rot.x, Rot.y, Rot.z);

            //transform.rotation.SetEulerRotation(Rot);

            yield return null;
        }

        transform.eulerAngles = new Vector3(intialRot.x, intialRot.y, angle);

        elapsedTime = 0;
        while (elapsedTime < time / 2.0f)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the lerp factor between 0 and 1
            float lerpFactor = elapsedTime / time;

            // Use Lerp to interpolate between the start and target positions
            Vector3 Rot = new Vector3(intialRot.x, intialRot.y, Mathf.Lerp(angle, 0, lerpFactor));;
            //transform.rotation = Quaternion.Euler(Rot);
            transform.eulerAngles = new Vector3(Rot.x, Rot.y, Rot.z);

            //transform.rotation.SetEulerRotation(Rot);

            yield return null;
        }

        transform.eulerAngles = new Vector3(intialRot.x, intialRot.y, 0);
        this.gameObject.SetActive(false);
    }
}
