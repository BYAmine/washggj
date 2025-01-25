using UnityEngine;

public class DirtManager : MonoBehaviour
{
    public GameObject dirtPrefab; // Assign your DirtSpot prefab here
    public GameObject car; // Assign your car GameObject
    public int maxDirtSpots = 10;

    void Start()
    {
        GenerateDirtSpots();
    }

    void GenerateDirtSpots()
    {
        for (int i = 0; i < maxDirtSpots; i++)
        {
            Vector3 randomPosition = GetRandomPointOnCar();
            GameObject dirtSpot = Instantiate(dirtPrefab, randomPosition, Quaternion.identity);
            dirtSpot.transform.SetParent(car.transform); // Parent to the car
        }
    }

    Vector3 GetRandomPointOnCar()
    {
        // Use the car's bounds to find random points
        Bounds carBounds = car.GetComponent<Renderer>().bounds;
        float x = Random.Range(carBounds.min.x, carBounds.max.x);
        float y = Random.Range(carBounds.min.y, carBounds.max.y);
        float z = Random.Range(carBounds.min.z, carBounds.max.z);
        return new Vector3(x, y, z);
    }
}
