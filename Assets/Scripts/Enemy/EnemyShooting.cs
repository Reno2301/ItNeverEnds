using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("References")]
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;
    public Vector3 playerPos;
    public float shootTimer;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip clip;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;

        float distance = Vector2.Distance(transform.position, playerPos);

        Vector3 rotation = playerPos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
         
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (distance < 10)
        {
            timer += Time.deltaTime;

            if (timer > shootTimer)
            {
                timer = 0;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        source.PlayOneShot(clip);

        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
