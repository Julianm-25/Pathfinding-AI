using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    #region Variables
    //the AI character
    public GameObject Enemy;
    //the player
    public GameObject playerObject;
    [Header("Movement/Distance Variables")]
    //distance required for the AI to chase player
    public float ChasePlayerDistance;
    //speed of the AI
    public float Speed = 5f;
    //minimum distance required for the AI to register a waypoint as patrolled
    public float MinDistanceToWaypoint;
    //the waypoint index
    [Header("Waypoint Variables")]
    public GameObject[] Waypoint;
    //current targeted waypoint
    private int CurrentWaypoint = 0;
    //health of AI
    [Header("Health and Attacking Variables")]
    public int health = 5;
    //health of player/target
    public int playerHealth = 10;
    //time between attacks
    public float attackCooldown = 300f;
    //minimum attacking distance
    public float attackDistance = 1f;
    #endregion
    void Update()
    {
        //if the AI is within its minimum player chase distance, has more than 25% health:
        if (Vector2.Distance(playerObject.transform.position, Enemy.transform.position) < ChasePlayerDistance && health > 1)
        {
            //the player position becomes the chase target
            Chase(playerObject.transform.position);
            //if the player is within attacking distance and the attack is not on cooldown:
            if(Vector2.Distance(playerObject.transform.position, Enemy.transform.position) < attackDistance && attackCooldown == 300)
            {
                //reduce the players health by one
                playerHealth -= 1;
                //put the attack on cooldown
                attackCooldown = 0;
            }
        }
        //if the AI health is less than 25%:
        else if (health < 2)
        {
            //run away from the player
            Flee(playerObject.transform.position);
        }
        //if none of the above is happening:
        else
        {
            //move to current active waypoint in index
            Patrol();
        }
        //if the attack is on cooldown:
        if(attackCooldown < 300)
        {
            //begin timing down the cooldown
            attackCooldown += 1;
        }
    }
    #region Movement
    private void Patrol()
    {
        //set distance to the distance between the AI and current targeted waypoint in the index
        float distance = Vector2.Distance(Enemy.transform.position, Waypoint[CurrentWaypoint].transform.position);
        //if target destination was reached:
        if (distance < MinDistanceToWaypoint)
        {
            //iterate through the waypoint index
            CurrentWaypoint++;
        }

        //if we reached the last waypoint, start again
        if (CurrentWaypoint >= Waypoint.Length)
        {
            //return to the beginning of the index
            CurrentWaypoint = 0;
        }

        //move towards the current targeted index
        Chase(Waypoint[CurrentWaypoint].transform.position);

    }
    private void Chase(Vector2 targetPosition)
    {
        //chase the player
        Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position, targetPosition, Speed * Time.deltaTime);
    }
    private void Flee(Vector2 targetPosition)
    {
        //run away from player
        Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position, targetPosition, -Speed * Time.deltaTime);
    }
    #endregion
}