using UnityEngine;
using System.Collections;

public class CarBehavior : MonoBehaviour
{
    private Transform point1;
    private Transform point2;
    private float moveSpeed;
    private float rotationSpeed;
    private CarSystem carSystem;
    private bool reachedPoint1 = false;
    private bool rotating = false;

    public void Initialize(Transform point1, Transform point2, float moveSpeed, float rotationSpeed, CarSystem carSystem)
    {
        this.point1 = point1;
        this.point2 = point2;
        this.moveSpeed = moveSpeed;
        this.rotationSpeed = rotationSpeed;
        this.carSystem = carSystem;
    }

    void Update()
    {
        // If the required points are not assigned, exit to prevent errors
        if (point1 == null || point2 == null)
        {
            Debug.LogError("Waypoints are not assigned. Please ensure 'point1' and 'point2' are set properly.");
            return;
        }

        if (rotating) return;

        if (!reachedPoint1)
        {
            MoveToPoint(point1);

            if (Vector3.Distance(transform.position, point1.position) < 0.1f)
            {
                reachedPoint1 = true;
                StartCoroutine(SmoothRotate(90));
            }
        }
        else
        {
            MoveToPoint(point2);

            if (Vector3.Distance(transform.position, point2.position) < 0.1f)
            {
                carSystem.OnCarDestroyed(gameObject); // Notify the system to handle car destruction
            }
        }
    }

    void MoveToPoint(Transform targetPoint)
    {
        if (targetPoint == null)
        {
            Debug.LogError("Target point is null!");
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
    }

    IEnumerator SmoothRotate(float angle)
    {
        rotating = true;
        float rotated = 0f;

        while (rotated < Mathf.Abs(angle))
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, step, 0);
            rotated += step;
            yield return null;
        }

        rotating = false;
    }
}
