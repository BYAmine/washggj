using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWashManager : MonoBehaviour
{
    [System.Serializable]
    public class Car
    {
        //public string carName;
        public GameObject carObject;
    }

    public List<Car> carList;
    public Transform washingSpot;
    public Transform paintingSpot;
    public float carWashTime = 30f;

    private int currentCarIndex = 0;
    private float timer = 0f;
    private bool isWashing = false;

    void Start()
    {
        if (carList.Count > 0)
        {
            SpawnCar();
        }
        else
        {
            Debug.LogWarning("Car list is empty!");
        }
    }

    void Update()
    {
        if (!isWashing || carList.Count == 0) return;

        timer += Time.deltaTime;
        Debug.Log(timer);

        if (timer > carWashTime)
        {
            Debug.Log("You ran out of time!");
            EndCarWash(false); 
        }
    }

    void SpawnCar()
    {
        if (currentCarIndex >= carList.Count)
        {
            Debug.Log("All cars have been washed.");
            return;
        }

        Car currentCar = carList[currentCarIndex];
        currentCar.carObject.SetActive(true);

       
        StartCoroutine(MoveCarToSpot(currentCar.carObject, washingSpot.position, () =>
        {
            Animator animator = currentCar.carObject.GetComponent<Animator>();
            if (animator != null)
            {
                Debug.Log("Arriving Animation");
                animator.SetTrigger("Arrive");
            }

            // Reset the timer and start washing
            ResetTimer();
            isWashing = true;

            Debug.Log("Started washing the car.");
        }));
    }

    public void EndCarWash(bool success)
    {
        isWashing = false;

        Car currentCar = carList[currentCarIndex];
        StartCoroutine(MoveCar(currentCar.carObject, paintingSpot.position, () =>
        {
            
            currentCar.carObject.SetActive(false);

            // Move to the next car
            currentCarIndex++;
            if (currentCarIndex < carList.Count)
            {
                SpawnCar();
            }
            else
            {
                Debug.Log("All cars processed.");
            }
        }));

        ResetTimer(); 
    }

    IEnumerator MoveCar(GameObject car, Vector3 targetPosition, System.Action onComplete)
    {
        float duration = 2f; 
        Vector3 startPosition = car.transform.position;
        float elapsedTime = 0f;

        Animator animator = car.GetComponent<Animator>();
        if (animator != null)
        {
            Debug.Log("Exit Animation");
            animator.SetTrigger("Exit"); 
        }

        while (elapsedTime < duration)
        {
            car.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        car.transform.position = targetPosition;

        onComplete?.Invoke();
    }

    IEnumerator MoveCarToSpot(GameObject car, Vector3 targetPosition, System.Action onComplete)
    {
        float duration = 2f; 
        Vector3 startPosition = car.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            car.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        car.transform.position = targetPosition;

        onComplete?.Invoke();
    }

    private void ResetTimer()
    {
        timer = 0f;
        Debug.Log("Timer reset.");
    }
}
