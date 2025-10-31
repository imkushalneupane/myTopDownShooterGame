using UnityEngine;

public class JuggernutAi : MonoBehaviour
{
    public enum State { Patrol, Rocket, Chase, Search, Attack }

    [Header("References")]
    public Transform[] patrolPoints;
    public Transform player;
    public Transform eyePoint;
    public EnemyShooting enemyShooting;
     public RocketShooting rocketShooting;

    [Header("Movement")]
    public float rotationSpeed = 8f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float stopDistance = 1f;

    [Header("Detection")]


    public float detectionRadius = 8f;
    public float RocketRange = 14f;

    public float attackRange = 6f;
    public LayerMask obstacleMask;
    public LayerMask playerMask;

    [Header("Search Behaviour")]
    public float searchDuration = 4f;

    [Header("Debug")]
    public bool showGizmos = true;

    private Rigidbody2D rb;
    private int currentPatrolIndex = 0;
    private State state = State.Patrol;
    private Vector2 lastSeenPosition;
    private float searchTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Auto-find player if not assigned
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p) player = p.transform;
        }

        if (eyePoint == null) eyePoint = transform;

        GoToNextPatrolPoint();
    }

    void Update()
    {
        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Rocket:
                Rocket();
                break;

            case State.Chase:
                Chase();
                break;

            case State.Search:
                Search();
                break;

            case State.Attack:
                Attack();
                break;
        }

        // --- State Transitions ---
        if (CanSeePlayer())
        {
            lastSeenPosition = player.position;

            if (Vector2.Distance(transform.position, player.position) <= attackRange)
                ChangeState(State.Attack);
            else if (Vector2.Distance(transform.position, player.position) <= RocketRange)
                ChangeState(State.Rocket);
            else
                ChangeState(State.Chase);
        }
        else
        {
            if (state == State.Attack || state == State.Chase|| state == State.Rocket)
                ChangeState(State.Search);
        }
    }

    // =====================
    // STATE METHODS
    // =====================

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        MoveTowards(patrolPoints[currentPatrolIndex].position, patrolSpeed);

        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < stopDistance)
            GoToNextPatrolPoint();
    }
void Rocket()
{
    if (player == null || rocketShooting == null) return;

    // Face the player smoothly
    RotateTowards(player.position);

    // Move slowly toward player or hold position
    rb.linearVelocity = Vector2.zero; // Optional: Stop moving while firing
    // Or: MoveTowards(player.position, chaseSpeed * 0.5f);

    // Fire rockets with cooldown
    if (rocketShooting.CanShoot())
        rocketShooting.TryShootRocket();
}

    void Chase()
    {
        if (player == null) return;

        MoveTowards(lastSeenPosition, chaseSpeed);
        RotateTowards(lastSeenPosition);
    }

    void Search()
    {
        MoveTowards(lastSeenPosition, chaseSpeed);

        if (Vector2.Distance(transform.position, lastSeenPosition) < stopDistance)
        {
            searchTimer += Time.deltaTime;
            if (searchTimer >= searchDuration)
            {
                searchTimer = 0f;
                ChangeState(State.Patrol);
            }
        }
    }

    void Attack()
    {
        if (player == null) return;

        rb.linearVelocity = Vector2.zero; // Stop moving
        RotateTowards(player.position);

        if (enemyShooting != null)
            enemyShooting.Shoot(player);


        // If player moves out of range, chase again
        if (Vector2.Distance(transform.position, player.position) > attackRange)
            ChangeState(State.Chase);
    }

    // =====================
    // HELPER METHODS
    // =====================

    void MoveTowards(Vector2 target, float speed)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * speed;
        RotateTowards(target);
    }

    void RotateTowards(Vector2 target)
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float smoothAngle = Mathf.LerpAngle(rb.rotation, angle, rotationSpeed * Time.deltaTime);
        rb.rotation = smoothAngle;
    }



    bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector2 dirToPlayer = (player.position - eyePoint.position);
        float distance = dirToPlayer.magnitude;

        if (distance > detectionRadius)
            return false;

        Vector2 facingDir = transform.right;
        float angle = Vector2.Angle(facingDir, dirToPlayer);
        RaycastHit2D hit = Physics2D.Raycast(eyePoint.position, dirToPlayer.normalized, distance, playerMask | obstacleMask);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
            return true;

        return false;
    }


    void ChangeState(State newState)
    {
        if (state == newState) return;
        state = newState;
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        //for rocket 
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange + 4f);
        /*aba fov dekhauna
        Gizmos.color = Color.blue;
        Vector3 leftBoundary = Quaternion.Euler(0, 0, viewAngle * 0.5f) * transform.right;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, -viewAngle * 0.5f) * transform.right;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * detectionRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * detectionRadius);
        */
    }
}
