using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    public int lives;
    [SerializeField] private SimpleFlash flashEffect;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip clip;

    private TrailRenderer tr;
    private SpriteRenderer sr;
    private CircleCollider2D cc;
    private EnemyShooting es;
    public SpriteRenderer rp;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        es = GetComponent<EnemyShooting>();

        lives = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            lives--;
            flashEffect.Flash();
            Destroy(collision.gameObject);
        }

        StartCoroutine(Dead());

    }

    private IEnumerator Dead()
    {
        if (lives <= 0)
        {
            sr.enabled = false;
            cc.enabled = false;
            es.enabled = false;
            rp.enabled = false;

            source.PlayOneShot(clip);

            yield return new WaitForSeconds(1);

            Destroy(gameObject);
        }
    }
}
