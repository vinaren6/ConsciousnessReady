using UnityEngine;
using System.Collections;

public class ParticlesFakeVelocity : MonoBehaviour
{

    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    private Vector3 velocity = new Vector3(0, 0, 0.01f);

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        InvokeRepeating("fakeVelocity", 0, 0.5f);
    }

    private void fakeVelocity()
    {

        velocity = -velocity;

        particles = new ParticleSystem.Particle[particleSystem.particleCount + 1];
        int amountOfParticles = particleSystem.GetParticles(particles);
        int i = 0;

        while (i < amountOfParticles)
        {
            particles[i].velocity = velocity;
            i++;
        }

        particleSystem.SetParticles(particles, amountOfParticles);
    }
}