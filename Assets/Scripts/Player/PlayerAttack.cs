using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackRadius = 3.0f;
    [SerializeField] private int maxAttacks = 2;
    [SerializeField] private GameObject circleIndicator;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    public void HandleAttack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius);
        enemiesInRange.Clear();

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                enemiesInRange.Add(hit.gameObject);
            }
        }

        circleIndicator.SetActive(enemiesInRange.Count > 0);

        if (enemiesInRange.Count > maxAttacks)
        {
            enemiesInRange.RemoveRange(maxAttacks, enemiesInRange.Count - maxAttacks);
        }

        foreach (GameObject enemy in enemiesInRange)
        {
            // Deal damage to enemies here
            enemy.GetComponent<Enemy>().TakeDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}