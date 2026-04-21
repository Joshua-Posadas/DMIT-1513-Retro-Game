using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float comfortDistance = 6f;

    [Header("Detection")]
    public float detectionDistance = 10f;
    public float detectionAngle = 90f;
    public float wallBuffer = 0.5f;
    public float loseSightDistance = 15f;
    public LayerMask wallMask;

    [Header("Combat")]
    public float shootCooldown = 0.8f;
    public float damage = 10f;

    [Header("Patrol")]
    public float patrolRadiusMin = 2f;
    public float patrolRadiusMax = 5f;

    private float shootTimer = 0f;
    private Enemy enemy;

    private bool chasing = false;

    private Vector3 originPosition;
    private Vector3 patrolTarget;
    private bool goingOut = true;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        originPosition = transform.position;
        PickNewPatrolTarget();
    }

    private void Update()
    {
        if (enemy == null || enemy.IsDead())
            return;

        shootTimer -= Time.deltaTime;

        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if (chasing && distToPlayer > loseSightDistance)
        {
            chasing = false;
            goingOut = true;
        }

        if (!chasing && PlayerDetected())
        {
            chasing = true;
        }

        if (chasing)
        {
            ChasePlayer();
            TryShoot();
        }
        else
        {
            Patrol();
        }
    }

    private bool PlayerDetected()
    {
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0;

        float dist = toPlayer.magnitude;

        if (dist > detectionDistance)
            return false;

        float angle = Vector3.Angle(transform.forward, toPlayer);
        if (angle > detectionAngle * 0.5f)
            return false;

        Vector3 origin = transform.position + Vector3.up * 1.5f;
        Vector3 dir = toPlayer.normalized;

        if (Physics.Raycast(origin, dir, dist, wallMask))
            return false;

        return true;
    }

    private void Patrol()
    {
        Vector3 target = goingOut ? patrolTarget : originPosition;
        Vector3 toTarget = target - transform.position;
        toTarget.y = 0;

        if (toTarget.sqrMagnitude < 0.1f)
        {
            if (goingOut)
            {
                goingOut = false;
            }
            else
            {
                goingOut = true;
                PickNewPatrolTarget();
            }
            return;
        }

        if (toTarget.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(toTarget);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                Time.deltaTime * 2f
            );
        }

        Vector3 move = toTarget.normalized * moveSpeed * Time.deltaTime;
        TryMove(move);
    }

    private void PickNewPatrolTarget()
    {
        Vector2 rand = Random.insideUnitCircle.normalized;
        float radius = Random.Range(patrolRadiusMin, patrolRadiusMax);

        Vector3 offset = new Vector3(rand.x, 0f, rand.y) * radius;
        patrolTarget = originPosition + offset;
    }

    private void ChasePlayer()
    {
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0;

        float dist = toPlayer.magnitude;

        if (dist > comfortDistance)
        {
            Vector3 move = transform.forward * moveSpeed * Time.deltaTime;
            TryMove(move);
        }
    }

    private void TryMove(Vector3 move)
    {
        if (!Blocked(transform.position, move))
        {
            transform.position += move;
            return;
        }

        Vector3 left = -transform.right * moveSpeed * Time.deltaTime;
        if (!Blocked(transform.position, left))
        {
            transform.position += left;
            return;
        }

        Vector3 right = transform.right * moveSpeed * Time.deltaTime;
        if (!Blocked(transform.position, right))
        {
            transform.position += right;
            return;
        }
    }

    private bool Blocked(Vector3 pos, Vector3 move)
    {
        Vector3 origin = pos + Vector3.up * 1f;

        float checkDistance = move.magnitude + wallBuffer;

        return Physics.Raycast(origin, move.normalized, checkDistance, wallMask);
    }

    private void TryShoot()
    {
        if (shootTimer > 0f)
            return;

        Vector3 origin = transform.position + Vector3.up * 1.5f;
        Vector3 dir = (player.position - origin).normalized;

        if (Physics.Raycast(origin, dir, 50f, wallMask))
            return;

        if (Physics.Raycast(origin, dir, out RaycastHit hit, 50f))
        {
            IDamagable dmg = hit.collider.GetComponent<IDamagable>();
            if (dmg != null)
                dmg.TakeDamage(damage);
        }

        shootTimer = shootCooldown;
    }
}
