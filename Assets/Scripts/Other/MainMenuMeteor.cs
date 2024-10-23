using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMeteor : MonoBehaviour
{
    private MainMenuSpawnMeteor spawner;
    private Rigidbody2D rb;
    private TrailRenderer tr;

    public int speed;
    public float speedOffset;
    public int minScale, maxScale;
    public int scale;

    public float trailWidthFactor;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("MainMenuSpawner").GetComponent<MainMenuSpawnMeteor>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();

        scale = (int)Random.Range(minScale, maxScale + 1);
        transform.localScale = new Vector3(scale, scale, scale);

        CheckScale();
    }

    private void Update()
    {
        DestroyMeteor();
    }

    private void DestroyMeteor()
    {
        if (transform.position.x > spawner.targetPos.x + 30 ||
            transform.position.y < spawner.targetPos.y - 30)
        {
            spawner.meteorCount--;
            Destroy(gameObject);
        }
    }

    public void CheckScale()
    {
        tr.startWidth = scale * trailWidthFactor;

        if (scale <= minScale)
        {
            rb.velocity = new Vector2(speed - speedOffset, -speed + speedOffset);
            tr.time = 0.8f;
        }

        else if (scale <= (minScale + maxScale) / 2)
        {
            rb.velocity = new Vector2(speed, -speed);
            tr.time = 1.2f;
        }

        else if (scale <= maxScale)
        {
            rb.velocity = new Vector2(speed + speedOffset, -speed - speedOffset);
            tr.time = 1.6f;
        }
    }
}
