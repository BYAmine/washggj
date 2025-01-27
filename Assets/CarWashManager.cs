using UnityEngine;
using System.Collections.Generic;

public class CarSystem : MonoBehaviour
{
    public List<GameObject> cars; // List of car prefabs
    public Transform startPoint; // Starting position for cars
    public Transform point1; // First waypoint
    public Transform point2; // Second waypoint
    public float moveSpeed = 5f; // Speed of the cars
    public float rotationSpeed = 90f; // Degrees per second
    public float timerInterval = 5f; // Time before the next car starts

    private Queue<GameObject> carQueue; // Queue to manage cars
    private float timer; // Timer to control car starts

    void Start()
    {
        carQueue = new Queue<GameObject>(cars); // Initialize the queue with the cars list
        SpawnNextCar(); // Spawn the first car
    }

    void Update()
    {
        // Countdown the timer
        timer -= Time.deltaTime;

        // If the timer reaches zero and there's another car in the queue
        if (timer <= 0 && carQueue.Count > 0)
        {
            SpawnNextCar();
        }
    }

    void SpawnNextCar()
    {
        // Reset the timer
        timer = timerInterval;

        // Get the next car from the queue
        GameObject car = carQueue.Dequeue();
        GameObject newCar = Instantiate(car, startPoint.position, Quaternion.identity);

        // Attach the CarBehavior script and set the waypoints
        CarBehavior carBehavior = newCar.AddComponent<CarBehavior>();
        carBehavior.Initialize(point1, point2, moveSpeed, rotationSpeed, this);
    }

    public void OnCarDestroyed(GameObject car)
    {
        Destroy(car);

        // If there are cars in the queue, move the next one to the start position
        if (carQueue.Count > 0)
        {
            SpawnNextCar();
        }
    }
}
