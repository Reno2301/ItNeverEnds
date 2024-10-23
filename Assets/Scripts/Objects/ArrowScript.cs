using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public GameObject planet;
    private GameObject player;
    public Rigidbody2D rb;
    public Vector3 planetPos;
    public float arrowOffset;
    public float timer;
    private SpriteRenderer sr;
    private Color color;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        color = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        Vector3 rotation = transform.position - player.transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90);

        Vector3 position = planet.transform.position - player.transform.position;
        position = position.normalized * arrowOffset;
        transform.position = player.transform.position + position;

        if(timer > 2)
        {
            sr.color = new Color(0, 0, 0, 0);

            if (timer > 10)
            {
                sr.color = color;
                timer = 0;
            }
        }
    }
}
