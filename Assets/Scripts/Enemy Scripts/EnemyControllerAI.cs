using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerAI : BaseEnemy
{
    public static EnemyControllerAI instance;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //patroling
    public float walkSpeed;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //chasing
    float runSpeed = 7;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //temporary code
    bool activateShip = false;

    private void Awake()
    {
        walkSpeed = m_Speed;
        agent = GetComponent<NavMeshAgent>();
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.PlayerExist())
        {
            player.GetComponent<Transform>().position = Player.instance.transform.position;
        }
        else
        {
            Patroling();
        }

        //check if player in sight or attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //if not in both sight and attack range
        if (!playerInSightRange && !playerInAttackRange)
        {
            //enemy patrols
            Debug.Log("Patrol State");
            UI.instance.enemyState.text = "Enemy State: Patrol";
            Patroling();
        }
        //if in sight but not in attack range
        if (playerInSightRange && !playerInAttackRange)
        {
            //enemy chase player
            Debug.Log("Chase Player State");
            UI.instance.enemyState.text = "Enemy State: Chase";
            ChasePlayer();
        }
        //if both in sight and attack range
        if (playerInSightRange && playerInAttackRange)
        {
            //attack player
            Debug.Log("Attack Player State");
            UI.instance.enemyState.text = "Enemy State: Attack";
            AttackPlayer();
        }

        //temporary code
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (activateShip == false)
            {
                activateShip = true;
                GameObject.Find("Map").GetComponent<MoveShip>().enabled = true;
            }
            else if (activateShip == true)
            {
                activateShip = false;
                GameObject.Find("Map").GetComponent<MoveShip>().enabled = false;
            }
        }
    }

    private void Patroling() 
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet) 
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //check if its out of map
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.speed = runSpeed;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //stop enemy from moving
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //attack code here
            Debug.Log("Attack player");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmos()
    {
        //attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        //sight range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
