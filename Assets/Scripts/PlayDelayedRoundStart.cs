using UnityEngine;
using System.Collections;
using System;

public class PlayDelayedRoundStart : MonoBehaviour, IRoundStatusTarget 
{
    public AudioClip Clip;
    public float Delay;

    public void RoundEnd()
    {
    }

    public void RoundStart()
    {
        GetComponent<AudioSource>().clip = Clip;
        GetComponent<AudioSource>().PlayDelayed(Delay);
    }

}
