using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public HealthScript p;

    public LayerMask whatIsGround, whatIsPlayer;

    public float damage = 10f;
    public float health = 50f;

    //patrola
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //napad
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //stanja
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //boja za stanja
    public Material red, green, yellow;

    private void Awake()
    {
        player = GameObject.Find("FPS Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    
        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

        GetComponent<MeshRenderer>().material = green;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);

        GetComponent<MeshRenderer>().material = yellow;
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            
            rb.AddForce(transform.forward * 24f, ForceMode.Impulse);
            rb.AddForce(transform.up * 4f, ForceMode.Impulse);

            Invoke(nameof(ShootEvent), timeBetweenAttacks - 1);

            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

            foreach (var item in allObjects)
            {
                if (item.name == "Sphere(Clone)")
                {
                    Destroy(item, 2);
                }
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

        GetComponent<MeshRenderer>().material = red;
    }

    public void ShootEvent ()
    {
        //float newHealth = p.Health - damage;
        //p.Health = newHealth;
        p.SetDamage(damage);
        //Debug.Log(newHealth);
        //GameController.Instance.SetDamage(damage);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        health = health - damage;

        if (health <= 0f)
        {
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
