using UnityEngine;

public class BubbleSound : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 [SerializeField] private AudioClip sound;
 public void SoundBubble(){
     SoundManager.instance.PlaySound(sound);
 }
}
