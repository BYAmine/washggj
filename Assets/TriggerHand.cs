using UnityEngine;

public class TriggerHand : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bubble")
        {
            Destroy(collision.gameObject);
        }
    }
}
