using System.Collections.Generic;
using UnityEngine;

public class WashParticleSystem : MonoBehaviour
{
    [Tooltip("List of soap particle systems to play randomly")]
    public List<ParticleSystem> soapParticles;

   
    public void PlayRandomSoapParticle()
    {
        if (soapParticles == null || soapParticles.Count == 0)
        {
            Debug.LogWarning("No soap particles assigned to the manager!");
            return;
        }

        int randomIndex = Random.Range(0, soapParticles.Count);
        ParticleSystem selectedParticle = soapParticles[randomIndex];

        if (selectedParticle != null)
        {
            selectedParticle.Play();
        }
        else
        {
            Debug.LogWarning($"Particle system at index {randomIndex} is null!");
        }
    }
}
