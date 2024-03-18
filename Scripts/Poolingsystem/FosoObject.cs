using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FosoObject : MonoBehaviour
{
    public List<Test> roads = new List<Test>();
    private float roadCreateRate = 5f;
    private float roadCreateTime = 0;

    private int time = 0;
    private int counter = 0;
    private bool mehet = false;

    private Transform test;

    private void Start()
    {
        CreateRoad();
        CreateRoad();
        CreateRoad();

    }

    void Update()
    {
        roadCreateTime += Time.deltaTime;
        if (roads.Count > 10)
        {
            roadCreateTime = 0;
            roadCreateRate = 0;
        }

        if (roadCreateTime > roadCreateRate)
        {
            roadCreateTime = 0;
            CreateRoad();
        }
    }

    public void CreateRoad()
    {
        var road = Medence.Instance.Get();

        if (road != null)
        {
            roads.Add(road);
            road.transform.rotation = transform.rotation;
            road.transform.position = new Vector2(transform.position.x, transform.position.y + counter);
            test = road.transform;
            road.gameObject.SetActive(true);
            counter += 6;
            mehet = true;
            road.AssignSpawner();
        }
    }

    public IEnumerator CreateCars(float posOnRoadX, float posOnRoadY , Test currentTest)
    {
        while (true)
        {
            var car = CarPool.Instance.GetCars();

            if (car != null)
            {
                car.parent = currentTest;             
                car.transform.parent = currentTest.transform;
                car.transform.rotation = transform.rotation;
                car.transform.position = new Vector2(posOnRoadX, posOnRoadY);

                if (car.transform.position.x >= 0)
                {
                    car.direction = Vector2.left;
                }

                if(car.transform.position.x <= 0)
                {
                    car.direction = Vector2.right;
                }

                car.gameObject.SetActive(true);

                int randomTime = Random.Range(2, 5);
                yield return new WaitForSeconds(Random.Range(randomTime, 7));
            }
        }
    }
}