using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    [Header("References")]
    public GameObject bullet;
    public GameObject crystal;
    private SpriteRenderer sr;
    private CircleCollider2D cc;
    [SerializeField] private SimpleFlash flashEffect;

    [Header("Planets")]
    public int planetNr;
    public Sprite[] planets;

    public int lives = 5;

    private Vector3 randomOffset;
    private int crystalCount;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();

        planetNr = (int)Random.Range(0, 14);
        SetSprite();

        crystalCount = (int)Random.Range(1, 5);
    }

    public void SetSprite()
    {
        string spriteName = sr.sprite.name;
        spriteName = spriteName.Replace("planet", "");
        int spriteNr = int.Parse(spriteName);

        sr.sprite = planets[planetNr];
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
            for (int i = 0; i < crystalCount; i++)
            {
                randomOffset = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
                source.PlayOneShot(clip);

                Instantiate(crystal, this.transform.position + randomOffset, Quaternion.identity);
            }

            sr.enabled = false;
            cc.enabled = false;

            source.PlayOneShot(clip);

            yield return new WaitForSeconds(1);

            Destroy(gameObject);
        }
    }
}
