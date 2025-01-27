using UnityEngine;

public class HoldPosition : MonoBehaviour
{
    public GameObject holding; // The position where the object will be held (e.g., VR hand)
    public GameObject bubble; // The bubble surrounding the item

    private bool isHeld = false; // To track if the item is currently held

    // Method to "grab" the object
    public void Hold()
    {
        float distance = Vector3.Distance(this.transform.position, holding.transform.position);
        Debug.Log("Distance to holding point: " + distance);

        if (!isHeld && distance < 10.0f) // Adjust distance as needed
        {
            Debug.Log("Grabbing object...");
            this.transform.position = holding.transform.position;
            //this.transform.SetParent(holding.transform); // Make it a child of the holding position
            isHeld = true; // Mark it as held
        }
        else
        {
            Debug.Log("Cannot grab: Either already held or out of range.");

        }
    }

    // Method to destroy the bubble
    public void DestroyBubble()
    {
        if (bubble != null)
        {
            Debug.Log("Destroying bubble...");
            Destroy(bubble);
            isHeld = false; // Reset the held state
           // this.transform.SetParent(null); // Detach from holding position
        }
        else
        {
            Debug.Log("Bubble is already destroyed or not assigned.");
        }
    }
   
}
