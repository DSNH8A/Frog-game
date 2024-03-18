using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Player player;
    private float playerY;

    void Start() 
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (player != null)
        {
            if (player.transform.position.y > playerY)
            {
                playerY = player.transform.position.y;    
            }
            transform.position = new Vector3 (transform.position.x, playerY, -30f);
        }
    }
}