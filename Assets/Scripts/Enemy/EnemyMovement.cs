using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float areaRadius = 10.0f;
    [SerializeField] private float changeDirectionInterval = 3.0f;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private bool isMoving = true; // Flag to control movement

    private void Start()
    {
        startingPosition = transform.position;
        SetNewRandomTargetPosition();
        StartCoroutine(ChangeDirectionRoutine());
    }

    private void Update()
    {
        if (isMoving)
        {
            Move();
        }
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (isMoving)
        {
            yield return new WaitForSeconds(Random.Range(0,changeDirectionInterval));
            SetNewRandomTargetPosition();
        }
    }

    private void SetNewRandomTargetPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle * areaRadius;
        targetPosition = new Vector3(startingPosition.x + randomDirection.x, startingPosition.y, startingPosition.z + randomDirection.y);
    }

    private void Move()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotate towards the target position
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }

        // If reached the target position, set a new target position
        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
        {
            SetNewRandomTargetPosition();
        }
    }

    public void StopMovement()
    {
        isMoving = false;
        StopCoroutine(ChangeDirectionRoutine());
    }

    public void StartMovement()
    {
        isMoving = true;
        StartCoroutine(ChangeDirectionRoutine());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startingPosition, areaRadius);
    }
}
