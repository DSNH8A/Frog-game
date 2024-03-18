using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private Sprite road;
    [SerializeField]
    private Sprite grassImage;

    [SerializeField]
    private GameObject[] cars = new GameObject[3];

    [SerializeField]
    private GameObject container;

    private Player player;
    private BoxCollider2D collider;
    private Rigidbody2D rb;

    private float lifetime;
    private float maxLifeTime = 20;

    private FosoObject fosoObject;
    private Camera mainCamera;

    private void OnEnable()
    {
        fosoObject = GameObject.Find("FosoObject").GetComponent<FosoObject>();
        lifetime = 0;
        mainCamera = FindObjectOfType<Camera>();    
    }

    void Start()
    {
        player = FindObjectOfType<Player>();    
        container = GameObject.Find("Container");

        this.gameObject.layer = 6;

        Map map = new Map();
        SpriteRenderer sprite = map.Image(road, new Vector3(0, 0, 0), this.transform);
        sprite.transform.localScale = new Vector2(2f, 1f);
        sprite.sortingOrder = 1;

        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.offset = new Vector2();
        collider.isTrigger = true;
        collider.size = new Vector2(30f, 6f);

        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        Grass grass = new Grass();
        SpriteRenderer grassSprite = grass.GrassImage(grassImage, new Vector3(0, 0, 0), this.transform);
        grassSprite.transform.localScale = new Vector2(2f, 1f);
    }

    private void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > maxLifeTime)
        {
            if (player.transform.position.y >= this.transform.position.y -3 
                && player.transform.position.y < this.transform.position.y +3)
            {
                this.lifetime = 0;

                for (int i = 0; i < fosoObject.roads.Count; i++)
                {
                    if (fosoObject.roads[i].transform.position.y > this.transform.position.y)
                    {
                        fosoObject.roads[i].lifetime = 0;
                    }
                }
            }

            else
            {
                Medence.Instance.ReturnToPool(this);
            }
        }

        if (player.transform.position.y > this.transform.position.y + 12)
        {
            StopCoroutines();
            Medence.Instance.ReturnToPool(this);
            fosoObject.CreateRoad();
        }
        
        if (player.transform.position.y < fosoObject.roads[0].transform.position.y - 3)
        {
            this.lifetime = 0;
        }
    }

    public void AssignSpawner()
    {
        StartCoroutine(fosoObject.CreateCars(transform.position.x -13f, transform.position.y + 0.8f, this));
        StartCoroutine(fosoObject.CreateCars(transform.position.x -13f, transform.position.y -2.2f, this));
        StartCoroutine(fosoObject.CreateCars(transform.position.x + 13f, transform.position.y +2.2f, this));
        StartCoroutine(fosoObject.CreateCars(transform.position.x + 13f, transform.position.y -0.8f, this));
    }

    private void StopCoroutines()
    {
        StopCoroutine(fosoObject.CreateCars(transform.position.x - 13f, transform.position.y + 0.8f, this));
        StopCoroutine(fosoObject.CreateCars(transform.position.x - 13f, transform.position.y - 2.2f, this));
        StopCoroutine(fosoObject.CreateCars(transform.position.x + 13f, transform.position.y + 2.2f, this));
        StopCoroutine(fosoObject.CreateCars(transform.position.x + 13f, transform.position.y - 0.8f, this));
    }
}