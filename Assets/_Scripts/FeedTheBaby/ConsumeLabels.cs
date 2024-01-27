using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeLabels : MonoBehaviour
{
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

    private void OnEnable()
    {

    }

    public void SetItemType(INGREDIENTTYPES types)
    {
        itemType = types;
    }

    public void SetItemCode(ITEMCODE code)
    {
        itemCode = code;
    }
}
