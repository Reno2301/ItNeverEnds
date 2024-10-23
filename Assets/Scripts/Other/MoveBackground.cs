using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    private float lengthX;
    private float lengthY;

    private float startPosX;
    private float startPosY;

    private GameObject cam;
    [SerializeField] private float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        startPosX = transform.position.x;
        startPosY = transform.position.y;

        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        float tempX = (cam.transform.position.x * (1 - parallaxEffect));
        float tempY = (cam.transform.position.y * (1 - parallaxEffect));

        float distanceX = (cam.transform.position.x * parallaxEffect);
        float distanceY = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);

        if (tempX > startPosX + lengthX)
        {
            startPosX += lengthX;
        } 
        else if (tempX < startPosX - lengthX)
        {
            startPosX -= lengthX;
        }       
        
        if (tempY > startPosY + lengthY)
        {
            startPosY += lengthY;
        } 
        else if (tempY < startPosY - lengthY)
        {
            startPosY -= lengthY;
        }
    }
}
