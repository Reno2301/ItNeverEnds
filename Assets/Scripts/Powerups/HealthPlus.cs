using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlus : MonoBehaviour
{
    private PlayerController pc;

    // Update is called once per frame
    void Update()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pc.pickUpParticle.Play();

            if (pc.lives <= 2)
            {
                pc.lives++;
            }

            Destroy(gameObject);
        }
    }
}
