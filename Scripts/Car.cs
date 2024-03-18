using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    public Test parent; 
    private SpriteRenderer renderer;
    private Vector2 pos;
    private int[] speedVariation = new int[] {1, 2, 3};
    private int variations;

    private float carCreateRate;
    public Vector2 direction;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        pos = transform.position;

        variations = speedVariation[Random.Range(0, 3)];
        carCreateRate = Random.Range(2f, 5f);
    }

    void Update()
    {
        Pooling();
        GiveThemSpeed(variations);
        GiveThemDirection();
    }

    private void GiveThemSpeed(int variations)
    {
        switch (variations)
        {
            case 1:
            {
               speed = 8;
            }
                break;
            case 2:
            {
               speed = 7;
            }
                break;
            case 3:
            {
                speed = 6;
            }
                break;
        }
    }

    private void GiveThemDirection()
    {
        if (direction == new Vector2(1, 0))
        {
            renderer.flipX = false;
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if (direction == new Vector2(-1, 0))
        {
            renderer.flipX = true;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void Pooling()
    {
        if (transform.position.x > 14f || transform.position.x < -14f || transform.parent.gameObject.active == false)
        {
            CarPool.Instance.ReturnToPool(this);
        }
    }
}