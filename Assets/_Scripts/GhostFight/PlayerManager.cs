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
    [SerializeField] private EnemyManager enemyManager;

    // Cooldown
    [SerializeField] private bool isCooldown = false;
    private float cooldownTicks = 0.0f;
    [SerializeField] private float cooldownDuration;

    // Sprites
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite baseFistSprite;
    [SerializeField] private Sprite leftPunchSprite;
    [SerializeField] private Sprite rightPunchSprite;
    [SerializeField] private Sprite dodgeSprite;

    // Punch 
    [SerializeField] private bool isPunchingLeft = true;
    [SerializeField] private bool didPunch = false;

    // Dodge
    [SerializeField] private bool isDodging = false;
    [SerializeField] private bool dodgedLeft = true;
    [SerializeField] private float dodgeTicks = 0.0f;
    [SerializeField] private float DODGE_REGENERATION_INTERVAL = 3.0f;
    [SerializeField] private bool canRegen = false;

    // Game States
    [SerializeField] private PlayerUIManager playerUIManager;
    [SerializeField] private GhostUIManager ghostUIManager;

    [SerializeField] private bool canBeDamaged = true;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerUIManager.GetIsInstructionsFinished())
        {
            UpdateText();

            if (!CheckIfDead())
            {
                if (isCooldown)
                {
                    cooldownTicks += Time.deltaTime;
                    if (cooldownTicks > cooldownDuration)
                    {
                        cooldownTicks = 0.0f;
                        isCooldown = false;
                    }
                }

                if (Input.GetMouseButtonDown(0) && !isDodging && !isCooldown) // Left Punch
                {
                    didPunch = true;
                    if (isPunchingLeft)
                    {
                        StartCoroutine(LeftPunch());
                    }
                    else if (!isPunchingLeft)
                    {
                        StartCoroutine(RightPunch());
                    }
                    isCooldown = true;
                }
                else if (Input.GetKeyDown(KeyCode.A) && !didPunch && currentStamina >= 1)
                {
                    currentStamina -= 1;
                    isDodging = true;
                    dodgedLeft = true;
                    StartCoroutine(LeftDodge());
                }
                else if (Input.GetKeyDown(KeyCode.D) && !didPunch && currentStamina >= 1)
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


                if (canRegen)
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
            else
            {
                StartCoroutine(playerUIManager.TriggerFadeIn());
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
        enemyManager.SetHitSprite();
        yield return new WaitForSeconds(playerPunchDelay);
        enemyManager.SetBaseSprite();
        spriteRenderer.sprite = baseFistSprite;
        isPunchingLeft = !isPunchingLeft;
        didPunch = false;
    }

    IEnumerator RightPunch()
    {
        spriteRenderer.sprite = rightPunchSprite;
        enemyManager.SetFlippedHitSprite();
        yield return new WaitForSeconds(playerPunchDelay);
        enemyManager.SetBaseSprite();
        spriteRenderer.sprite = baseFistSprite;
        isPunchingLeft = !isPunchingLeft;
        didPunch = false;
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

    public bool CheckIfDead()
    {
        if (currentHealth <= 0)
            return true;
        else
            return false;  
    }

    public void SubtractHealth()
    {
        if(canBeDamaged)
        {
            currentHealth--;
            if (currentHealth <= 0 && !ghostUIManager.IsGhostDead())
            {
                Debug.Log("YOU LOSE");
                GameManager.Instance.SetWinState(6, false);
            }
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

    public bool GetIsPlayerPunching()
    {
        if (didPunch)
            return true;

        else
            return false;
    }

    public void SetCanBeDamaged(bool flag)
    {
        canBeDamaged = flag;
    }
}

