using UnityEngine;

public class finalholdPos : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject holding; // The position where the object will be held (e.g., VR hand)
    public GameObject bubble; // The bubble surrounding the item

    private bool isHeld = false; // To track if the item is currently held
    // Method to "grab" the object
    public void Hold()
    {
        float distance = Vector3.Distance(this.transform.position, holding.transform.position);
        Debug.Log("Distance to holding point: " + distance);

        if (!isHeld && distance < 10.0f) 
        {
            Debug.Log("Grabbing object...");
            this.transform.position = holding.transform.position;
            //this.transform.SetParent(holding.transform); 
            isHeld = true; // Mark it as held
            Destroy(bubble);
            isHeld = false;
        }
        else
        {
            Debug.Log("hold"); 

        }
    }

}
