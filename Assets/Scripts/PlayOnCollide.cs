using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayOnCollide : MonoBehaviour
{
    private AudioSource source;
    public float MinImpact = 1f;

    public float MinPitch = 0.5f, MaxPitch = 1f;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!source.isPlaying && collision.relativeVelocity.sqrMagnitude > Mathf.Pow(MinImpact, 2))
        {
            source.pitch = Random.Range(MinPitch, MaxPitch);
            source.Play();
        }
    }
}
