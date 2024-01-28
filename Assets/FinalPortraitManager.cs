using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPortraitManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private SpriteRenderer dadSPR;
    [SerializeField] private SpriteRenderer momSPR;
    [SerializeField] private SpriteRenderer girlSPR;
    [SerializeField] private SpriteRenderer boySPR;
    [SerializeField] private SpriteRenderer loloSPR;
    [SerializeField] private SpriteRenderer babySPR;

    [SerializeField] private Sprite altdad;
    [SerializeField] private Sprite altmom;
    [SerializeField] private Sprite altgirl;
    [SerializeField] private Sprite altboy;
    [SerializeField] private Sprite altlolo;
    [SerializeField] private Sprite altbaby;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.win1)
            babySPR.sprite = altbaby;
        if (!gameManager.win2)
            loloSPR.sprite = altlolo;
        if (!gameManager.win3)
            girlSPR.sprite = altgirl;
        if (!gameManager.win4)
            boySPR.sprite = altboy;
        if (!gameManager.win5)
            momSPR.sprite = altmom;
        if (!gameManager.win6)
            dadSPR.sprite = altdad;
    }
}
