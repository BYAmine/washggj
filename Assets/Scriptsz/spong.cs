using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeTool : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> dirtSpots; // List of dirt spots
   public GameObject Particle;
    [SerializeField] private AudioClip sound;
  
    public float washingDuration = 10f;
     public Quaternion spawnRotation;
    public bool isWashing = false;

    public Transform spongePosition;
    public float margin = 0.5f; // Distance threshold for cleaning
    public float fadeSpeed = 3f;

    private List<Renderer> dirtMaterials = new List<Renderer>(); // Store renderers of dirt spots
    private List<bool> isFadingList = new List<bool>(); // Track fading state for each dirt spot

    void Start()
    {
        spongePosition = transform; // Assign the transform of the current GameObject

        // Populate materials and fading states
        foreach (GameObject dirtSpot in dirtSpots)
        {
            if (dirtSpot != null)
            {
                Renderer renderer = dirtSpot.GetComponent<Renderer>();
                if (renderer != null)
                {
                    dirtMaterials.Add(renderer);
                    isFadingList.Add(false); // Initialize fading state for this spot
                }
            }
        }
     
        
    }

    void Update()
    {
        Cleaning();
    }

    public void Cleaning()
{
    // Loop through all dirt spots
    for (int i = 0; i < dirtSpots.Count; i++)
    {
        // Skip if the spot or material is missing
        if (dirtSpots[i] == null || dirtMaterials[i] == null)
        {
            continue;
        }

        // Calculate the distance between the sponge and the dirt spot
        float distance = Vector3.Distance(spongePosition.position, dirtSpots[i].transform.position);
        Debug.Log(distance);

        // If within the margin and not already fading, start fading and play the particle effect
        if (distance < margin && !isFadingList[i])
        {
            StartCoroutine(FadeAndDestroy(i)); // Pass the index to handle the specific spot
            Particle.SetActive(true); // Activate particle effect
           
        }
    }
}

    private IEnumerator FadeAndDestroy(int index)
    {
        isFadingList[index] = true;

        Renderer dirtMaterial = dirtMaterials[index];
        Color color = dirtMaterial.material.color;

        // Fade out the material
        while (color.a > 0f)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            color.a = Mathf.Clamp01(color.a);
            dirtMaterial.material.color = color;
            yield return null; // Wait for the next frame
        }

        Destroy(dirtSpots[index],5f); // Remove the dirt spot
        dirtSpots[index] = null; // Set the reference to null
        isFadingList[index] = false;
    }
}
