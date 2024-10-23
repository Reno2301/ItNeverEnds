using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    private PlayerController pc;
    private CircleCollider2D cc;
    private SpriteRenderer sr;
    public float duration;
    public float timer;
    public bool moveFaster;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cc = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveFaster)
        {
            timer += Time.deltaTime;

            pc.moveSpeed = pc.startSpeed * 1.5f;

            if (timer > duration)
            {
                pc.moveSpeed = pc.startSpeed;
                moveFaster = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pc.pickUpParticle.Play();
            moveFaster = true;

            cc.enabled = false;
            sr.enabled = false;
        }
    }
}
