using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour
{
    void Start()
    {
        //take particle system
        var particleSystem = GetComponent<ParticleSystem>();
        //destroy it after time (duration of particle system)
        Destroy(gameObject, particleSystem.main.duration);
    }
}
