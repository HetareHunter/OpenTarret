using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    ParticleSystem[] particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponents<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            foreach (var p in particles)
            {
                p.Play();
            }
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            foreach (var p in particles)
            {
                p.Stop();
            }
        }
    }
}
