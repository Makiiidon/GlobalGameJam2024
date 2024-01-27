using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private int currentHealth;
    [SerializeField] private int currentStamina;
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxStamina;
    [SerializeField] private float playerPunchDelay;
    [SerializeField] private float dodgeDelay;


    // Sprites
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite baseFistSprite;
    [SerializeField] private Sprite leftPunchSprite;
    [SerializeField] private Sprite rightPunchSprite;
    [SerializeField] private Sprite dodgeSprite;

    // Punch 
    [SerializeField] private bool isPunchingLeft = false;
    [SerializeField] private bool isPunchingRight = false;

    // Dodge
    [SerializeField] private bool isDodging = false;
    [SerializeField] private bool dodgedLeft = true;
    [SerializeField] private float dodgeTicks = 0.0f;
    [SerializeField] private float DODGE_REGENERATION_INTERVAL = 3.0f;
    [SerializeField] private bool canRegen = false;





    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();

       
        if(Input.GetMouseButtonDown(0) && !isPunchingRight && !isDodging) // Left Punch
        {
            isPunchingLeft = true;
            StartCoroutine(LeftPunch());
        }
        else if (Input.GetMouseButtonDown(1) && !isPunchingLeft && !isDodging) // Right Punch
        {
            isPunchingRight = true;
            StartCoroutine(RightPunch());
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isPunchingLeft && !isPunchingRight && currentStamina >= 1)
        {
            currentStamina -= 1;
            isDodging = true;
            dodgedLeft = true;
            StartCoroutine(LeftDodge());
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isPunchingLeft && !isPunchingRight && currentStamina >= 1)
        {
            currentStamina -= 1;
            isDodging = true;
            dodgedLeft = false;
            StartCoroutine(RightDodge());
        }

        // Stamina Regen
        if (currentStamina == 0)
        {
            canRegen = true;
        }


        if(canRegen) 
        {
            dodgeTicks += Time.deltaTime;
            if (dodgeTicks >= DODGE_REGENERATION_INTERVAL)
            {
                dodgeTicks = 0.0f;
                if (currentStamina < maxStamina)
                    currentStamina++;
                else
                    canRegen = false;
            }
        }
      

    }

    void UpdateText()
    {
        healthText.text = currentHealth.ToString();
        staminaText.text = currentStamina.ToString();   
    }

    IEnumerator LeftPunch()
    {
        spriteRenderer.sprite = leftPunchSprite;
        yield return new WaitForSeconds(playerPunchDelay);
        spriteRenderer.sprite = baseFistSprite;
        isPunchingLeft = false;
    }

    IEnumerator RightPunch()
    {
        spriteRenderer.sprite = rightPunchSprite;
        yield return new WaitForSeconds(playerPunchDelay);
        spriteRenderer.sprite = baseFistSprite;
        isPunchingRight = false;
    }

    IEnumerator LeftDodge()
    {
        spriteRenderer.sprite = dodgeSprite;
        yield return new WaitForSeconds(dodgeDelay);
        spriteRenderer.sprite = baseFistSprite;
        isDodging = false;
    }

    IEnumerator RightDodge()
    {
        spriteRenderer.flipX = true;
        spriteRenderer.sprite = dodgeSprite;
        yield return new WaitForSeconds(dodgeDelay);
        spriteRenderer.sprite = baseFistSprite;
        isDodging = false;
        spriteRenderer.flipX = false;
    }

    public void SubtractHealth()
    {
        currentHealth--;
        if (currentHealth <= 0) 
        {
            Debug.Log("YOU LOSE");
        }
    }

    public bool GetIsDodging()
    {
        return isDodging;
    }

    public bool GetIsDodgingLeft()
    {
        return dodgedLeft;
    }

}

