using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    private GameObject player;
    private PlayerController pc;
    private ScoreHandler sh;
    private SpriteRenderer sr;
    private int points;

    public int crystalPoint0 = 5;
    public int crystalPoint1 = 10;
    public int crystalPoint2 = 15;
    public int crystalPoint3 = 20;
    public int crystalPoint4 = 25;
    public int crystalPoint5 = 30;

    public int crystalNr;
    public Sprite[] crystals;

    // Start is called before the first frame update
    void Start()
    {
        sh = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreHandler>();
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        sr = GetComponent<SpriteRenderer>();

        crystalNr = Random.Range(0, 6);
        SetSprite();
    }

    public void SetSprite()
    {
        string spriteName = sr.sprite.name;
        spriteName = spriteName.Replace("crystal", "");
        int spriteNr = int.Parse(spriteName);

        sr.sprite = crystals[crystalNr];
    }

    public void SetScore()
    {
        switch (sr.sprite.name)
        {
            case "crystal0":
                sh.countCrystal0++;
                points = crystalPoint0;
                break;
            case "crystal1":
                sh.countCrystal1++;
                points = crystalPoint1;
                break;
            case "crystal2":
                sh.countCrystal2++;
                points = crystalPoint2;
                break;
            case "crystal3":
                sh.countCrystal3++;
                points = crystalPoint3;
                break;
            case "crystal4":
                sh.countCrystal4++;
                points = crystalPoint4;
                break;
            case "crystal5":
                sh.countCrystal5++;
                points = crystalPoint5;
                break;
        }

        sh.score += points;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SetScore();
            Destroy(gameObject);
        }
    }
}
