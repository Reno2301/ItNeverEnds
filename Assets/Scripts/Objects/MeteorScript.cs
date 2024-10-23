using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public GameObject bullet;
    [SerializeField] private SimpleFlash flashEffect;
    [SerializeField] private ParticleSystem particle;

    Vector3 playerPos;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip clip;

    [Header("Scale")]
    public int smallMeteorScale = 1;
    public int mediumMeteorScale = 2;
    public int bigMeteorScale = 3;

    [Header("Speed")]
    public float smallMeteorSpeed;
    public float mediumMeteorSpeed;
    public float bigMeteorSpeed;

    [Header("Lives")]
    public int smallMeteorLives;
    public int mediumMeteorLives;
    public int bigMeteorLives;

    [Header("TrailWidth")]
    public float trailWidthFactor = 0.9f;

    [Header("Meteors")]
    public int meteorNr;
    public Sprite[] meteors;
    public float meteorDeathRadius;

    [Header("Powerups")]
    public int dropFactor;

    [Header("HealUp")]
    public GameObject healthUp;
    public int healthUpChance;

    [Header("ShootFast")]
    public GameObject shootFast;
    public int shootFastChance;

    [Header("MoveFast")]
    public GameObject moveFast;
    public int moveFastChance;

    private TrailRenderer tr;
    private SpriteRenderer sr;
    private CircleCollider2D cc;

    private int lives;

    private float scale;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tr = GetComponent<TrailRenderer>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();

        playerPos = player.transform.position;

        meteorNr = (int)Random.Range(0, 2);

        scale = (int)Random.Range(smallMeteorScale, bigMeteorScale + 1);
        transform.localScale = new Vector3(scale, scale, scale);
        CheckScale();
        CheckMeteorTrailWidth();
        SetSprite();
    }

    private void Update()
    {
        playerPos = player.transform.position;

        float distance = Vector3.Distance(playerPos, transform.position);

        if (distance > meteorDeathRadius)
        {
            Destroy(gameObject);
        }
    }

    public void SetSprite()
    {
        string spriteName = sr.sprite.name;
        spriteName = spriteName.Replace("Meteorite", "");
        int spriteNr = int.Parse(spriteName);

        sr.sprite = meteors[meteorNr];
    }

    public void MeteorDirection(float meteorSpeed)
    {
        Vector3 direction = playerPos - transform.position;
        rb.velocity = new Vector2(direction.x + Random.Range(-5, 5), direction.y + Random.Range(-5, 5)).normalized * meteorSpeed;
    }

    public void CheckMeteorLives(int lives)
    {
        this.lives = lives;
    }

    public void CheckMeteorTrailWidth()
    {
        tr.startWidth = scale * trailWidthFactor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            lives--;
            flashEffect.Flash();
            Destroy(collision.gameObject);
        }

        if (lives <= 0)
        {
            int drop = Random.Range(1, (healthUpChance + shootFastChance + moveFastChance)* dropFactor + 1);
            if (healthUpChance > drop)
            {
                Instantiate(healthUp, transform.position, Quaternion.identity);
            }
            else if (shootFastChance + healthUpChance > drop)
            {
                Instantiate(shootFast, transform.position, Quaternion.identity);
            }
            else if (shootFastChance + healthUpChance + moveFastChance > drop)
            {
                Instantiate(moveFast, transform.position, Quaternion.identity);
            }

            StartCoroutine(Break());
        }
    }

    private IEnumerator Break()
    {
        source.PlayOneShot(clip);

        particle.Play();

        sr.enabled = false;
        tr.enabled = false;
        cc.enabled = false;

        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(gameObject);
    }

    public void CheckScale()
    {
        if (scale <= smallMeteorScale)
        {
            MeteorDirection(smallMeteorSpeed);
            CheckMeteorLives(smallMeteorLives);
            tr.time = 1;
        }
        else if (scale <= mediumMeteorScale)
        {
            MeteorDirection(mediumMeteorSpeed);
            CheckMeteorLives(mediumMeteorLives);
            tr.time = 3;
        }

        else if (scale <= bigMeteorScale)
        {
            MeteorDirection(bigMeteorSpeed);
            CheckMeteorLives(bigMeteorLives);
            tr.time = 5;
        }
    }
}
