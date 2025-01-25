using UnityEngine;

public class SpongeTool : MonoBehaviour
{
    [SerializeField]
    private GameObject dirtspotpos;

    private Transform spongepos;
    public float distance;
    public float margin = 0.5f; // Fixed spelling
    public Renderer dirtmaterial;
    public float fadeSpeed = 3f;

    private bool isFading = false; // To track if fading is in progress

    void Start()
    {
        spongepos = transform; // Assign the transform of the current GameObject

        if (dirtmaterial == null && dirtspotpos != null)
        {
            dirtmaterial = dirtspotpos.GetComponent<Renderer>();
        }
    }

    void Update() // Fixed method name
    {
        Cleaning();
    }

    public void Cleaning()
    {
        if (dirtspotpos == null || dirtmaterial == null)
        {
            return; // Exit if no dirtspot or material is assigned
        }

        distance = Vector3.Distance(spongepos.position, dirtspotpos.transform.position);

        if (distance < margin && !isFading)
        {
            StartCoroutine(FadeAndDestroy());
        }
    }

    private System.Collections.IEnumerator FadeAndDestroy()
    {
        isFading = true;

        Color color = dirtmaterial.material.color;

        while (color.a > 0f)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            color.a = Mathf.Clamp01(color.a);
            dirtmaterial.material.color = color;
            yield return null; // Wait for the next frame
        }

        Destroy(dirtspotpos);
        isFading = false;
    }
}
