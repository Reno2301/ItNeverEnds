using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private Rigidbody2D playerRb;

    public float pullDistance;
    public float deathDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the rigidbody to view it as a rotating black hole
        rb.rotation += 8;


        //when the distance of the player to the black hole is closer than
        //the distance from where the black hole is working (pullDistance),
        //the player will be drawn closer to the black hole with a steering force.
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance < pullDistance)
        {
            Vector2 desiredVelocity = transform.position - player.transform.position;
            desiredVelocity.Normalize();
            desiredVelocity *= 0.5f;

            Vector2 steeringForce = desiredVelocity - playerRb.velocity;
            steeringForce.Normalize();
            steeringForce *= 0.01f;

            playerRb.velocity += steeringForce;
        }

        //when the distance between the black hole and the player is closer than
        //the distance in which the player dies (deathDistance), activate the method
        //in the PlayerController in which the player gets hit by a black hole.
        if(distance < deathDistance)
        {
            player.GetComponent<PlayerController>().GetHitByBlackHole();
        }
    }
}
