using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public abstract class GenericPooling<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    private T prefab;
    [SerializeField]
    private int counter = 0;

    [SerializeField]
    private T[] cars = new T[3];

    public static GenericPooling<T> Instance { get; private set; }
    private Queue<T> objects1 = new Queue<T>();
    private Queue<T> objects2 = new Queue<T>();

    private void Awake()
    {
        Instance = this;
    }

    public T Get()
    {
        if (objects1.Count == 0)
        {
            AddObjects(1);
        }
        return objects1.Dequeue();

    }

    public T GetCars()
    {
        if (objects2.Count == 0)
        {
            AddCars(1);
        }
        return objects2.Dequeue();
    }

    private void AddObjects(int count)
    {
        var newObject = Instantiate(prefab);
        newObject.gameObject.SetActive(false);
        objects1.Enqueue(newObject);
    }

    private void AddCars(int count)
    {
        int index = UnityEngine.Random.Range(0, cars.Length); 
        var newObject = Instantiate(cars[index]);  
        newObject.gameObject.SetActive(false);
        objects2.Enqueue(newObject);
    }

    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        try
        {
            objects1.Enqueue(objectToReturn);
            counter++;
        }
        finally 
        {
            objects2.Enqueue(objectToReturn);
            counter++;
        }
    }
}