using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _cubeExplosionFX;

    ParticleSystem.MainModule cubeExplosionFXMainModule;
    
   

    
    private void Start()
    {
        cubeExplosionFXMainModule = _cubeExplosionFX.main;
    }
    public void PlayCubeExplosionFX(Vector3 positon, Color color)
    {
        cubeExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient(color);
        _cubeExplosionFX.transform.position = positon;
        _cubeExplosionFX.Play();
    }
}
