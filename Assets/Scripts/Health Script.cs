using UnityEngine;
using TMPro; // Required for TextMeshPro

public class HealthScript : MonoBehaviour
{
    public float Health = 100f;
    public float damageAmount = 10f;
    public BallMovement movementScript;
    public Collider2D killBox;
    public EnemyScript enemyScript;

    [Header("UI Settings")]
    public TMP_Text airText; // Drag your TextMeshPro UI object here in the Inspector

    void Start()
    {
        // Set the initial text when the game starts
        UpdateAirUI();
    }

    void Update()
    {
        if (Health <= 0)
        {
            Dead();
        }
    }

    // Now accepts the exact damage and references directly from the enemy
    public void TakeDamage(float incomingDamage, EnemyScript attacker, Collider2D triggerBox)
    {
        damageAmount = incomingDamage;
        enemyScript = attacker;
        killBox = triggerBox;
        Health -= damageAmount;

        // Clamp health so it doesn't display negative numbers (e.g., -10)
        Health = Mathf.Max(Health, 0);

        // Update the UI right after taking damage
        UpdateAirUI();
    }

    void UpdateAirUI()
    {
        if (airText != null)
        {
            // Mathf.CeilToInt rounds decimals up to a clean whole number
            airText.text = "Air Left: " + Mathf.CeilToInt(Health);
        }
    }

    void Dead()
    {
        movementScript.enabled = false;
    }
}
