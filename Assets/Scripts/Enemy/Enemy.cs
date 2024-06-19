using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyAnimation), typeof(EnemyMovement), typeof(HealthBar))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private EnemyAnimation enemyAnimation;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private float deathDelay = 1.5f; // Delay before death
    [HideInInspector] public EnemySpawner enemySpawner;
    private Transform player;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        player = Player.Instance.transform;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float amount = 1f)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            StartCoroutine(DieSequence());
        }

        if (currentHealth > 0)
        {
            healthBar.EnableView(); // Show health bar on taking damage
            healthBar.ResetHideTimer(); // Reset the hide timer
            healthBar.SetHealth(currentHealth); // Update health bar value
        }
    }

    private IEnumerator DieSequence()
    {
        // Stop enemy movement
        enemyMovement.StopMovement();

        // Switch to floating animation
        enemyAnimation.SetAnimActive(AnimName.Floating);

        healthBar.DisabledView();

        float elapsedTime = 0f;

        while (elapsedTime < deathDelay)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * 2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Notify the EnemySpawner
        if (enemySpawner != null)
        {
            enemySpawner.RemoveEnemyFromList(this);
        }

        // Destroy the enemy game object
        Destroy(gameObject);
    }
}
