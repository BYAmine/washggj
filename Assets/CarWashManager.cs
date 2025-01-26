using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarWashManager : MonoBehaviour
{
    [System.Serializable]
    public class Car
    {
        public GameObject carObject; // Reference to the car GameObject
    }

    public List<Car> carList; // List of cars
    public Transform washingSpot; // Position for washing
    public List<Transform> waypoints; // Waypoints for the path
    public float carWashTime = 5f; // Time before car exits

    private int currentCarIndex = 0; // Current car being processed
    private bool isWashing = false;
    private float washEndTime;

    void Start()
    {
        if (carList.Count > 0)
        {
            SpawnCar(); // Start with the first car
        }
        else
        {
            Debug.LogWarning("Car list is empty!");
        }
    }

    void Update()
    {
        // Check if the washing time is over for the current car
        if (isWashing && Time.time >= washEndTime)
        {
            isWashing = false;
            MoveCarThroughWaypoints(carList[currentCarIndex].carObject); // Start moving the current car through waypoints
        }
    }

    void SpawnCar()
    {
        if (currentCarIndex >= carList.Count)
        {
            Debug.Log("All cars have been processed!");
            return;
        }

        Car currentCar = carList[currentCarIndex];
        currentCar.carObject.SetActive(true);

        // Move the car to the washing spot
        if (currentCarIndex == 0)
        {
            MoveCarAlongPath(currentCar.carObject, washingSpot.position, () =>
            {
                Debug.Log("Car arrived at washing spot.");
                isWashing = true;
                washEndTime = Time.time + carWashTime; // Set the wash timer
            });
        }
        else
        {
            // Move the car to the last car's previous position
            Car previousCar = carList[currentCarIndex - 1];
            MoveCarAlongPath(currentCar.carObject, previousCar.carObject.transform.position, () =>
            {
                // Once the car reaches its spot, send it to the washing spot
                MoveCarAlongPath(currentCar.carObject, washingSpot.position, () =>
                {
                    Debug.Log("Car arrived at washing spot.");
                    isWashing = true;
                    washEndTime = Time.time + carWashTime; // Set the wash timer
                });
            });
        }
    }

    void MoveCarThroughWaypoints(GameObject car)
    {
        // Start moving the car through the waypoints
        MoveCarAlongPath(car, waypoints[0].position, () =>
        {
            Debug.Log("Car started moving along waypoints.");
            StartCoroutine(FollowWaypoints(car, waypoints));
        });
    }

    void MoveCarAlongPath(GameObject car, Vector3 targetPosition, System.Action onComplete)
    {
        NavMeshAgent agent = car.GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPosition); // Move car to the target position

        StartCoroutine(WaitUntilArrival(agent, onComplete));
    }

    IEnumerator WaitUntilArrival(NavMeshAgent agent, System.Action onComplete)
    {
        // Wait until the car reaches the destination
        while (agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }
        onComplete?.Invoke(); // Call the completion callback
    }

    IEnumerator FollowWaypoints(GameObject car, List<Transform> path)
    {
        NavMeshAgent agent = car.GetComponent<NavMeshAgent>();

        for (int i = 0; i < path.Count; i++)
        {
            agent.SetDestination(path[i].position); // Set the next waypoint as destination

            // Wait until the car reaches this waypoint before moving to the next
            yield return new WaitUntil(() => agent.remainingDistance <= agent.stoppingDistance);
        }

        // Once the car finishes the waypoints, destroy it and spawn the next car
        Destroy(car);
        Debug.Log("Car finished path and has been destroyed.");

        // Move to the next car if there are more cars
        currentCarIndex++;
        if (currentCarIndex < carList.Count)
        {
            SpawnCar(); // Spawn the next car
        }
        else
        {
            Debug.Log("All cars processed.");
        }
    }
}
