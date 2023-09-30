using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaurds : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f; // Enemy's movement speed

    [Header("chase and attack")]
    public float chaseDistance = 10f; // Distance at which the enemy starts chasing
    public float attackDistance = 2f; // Distance at which the enemy attacks

    [Header("patrolling")]
    public Transform[] patrolPoints; // Array of patrol points for the enemy to move between
    public float patrolSpeed = 2f; // Patrol speed
    private int currentPatrolPointIndex = 0;
    private bool isPatrolling = true;

    private Animator animator;

    private void Start()
    {
        //animator = GetComponent<Animator>();

        // Start patrolling if patrol points are defined
        if (patrolPoints.Length > 0)
        {
            StartCoroutine(PatrolRoutine());
        }
    }

    private void Update()
    {
        float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x);

        if (distanceToPlayer <= chaseDistance)
        {
            isPatrolling = false;

            if (distanceToPlayer <= attackDistance)
            {
                AttackPlayer();
            }

            // Move towards the player only on the X-axis
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else if (!isPatrolling) // Resume patrolling when the player is out of range
        {
            isPatrolling = true;
        }
    }

    private void AttackPlayer()
    {
        // when guard touch the player 
        
    }

    private IEnumerator PatrolRoutine()
    {
        while (true) // Continuously patrol
        {
            if (patrolPoints.Length > 0)
            {
                Vector3 targetPosition = new Vector3(patrolPoints[currentPatrolPointIndex].position.x, transform.position.y, transform.position.z);
                Vector3 moveDirection = (targetPosition - transform.position).normalized;

                // Move towards the current patrol point
                transform.position += moveDirection * patrolSpeed * Time.deltaTime;

                // Check if the enemy has reached the current patrol point on the X-axis
                if (Mathf.Approximately(transform.position.x, targetPosition.x))
                {
                    // Switch to the next patrol point or reset to the first point
                    currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
                }
            }

            yield return null;
        }
    }
}