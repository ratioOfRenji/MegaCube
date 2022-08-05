using UnityEngine;
using Zenject;

public class CubeCollision : MonoBehaviour
{
    Cube _cube;
    AudioSource _audioSource;

    [Inject]
    private FX _FX;
    [Inject]
    private CubeSpawner _cubeSpawner;
    private void Awake()
    {
        _cube = GetComponent<Cube>();
        _audioSource = FindObjectOfType<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Cube otheCube = collision.gameObject.GetComponent<Cube>();

        // check if contacted with other cube
        if (otheCube != null && _cube.CubeID > otheCube.CubeID)
        {
            
            var cubesHaveSameNumber = _cube.CubeNumber == otheCube.CubeNumber;
            if (cubesHaveSameNumber)
            {
                Vector3 contactPoint = collision.contacts[0].point;
                
                var cubeNumberLesserThanMax = otheCube.CubeNumber < _cubeSpawner.maxCubeNumber;
                if (cubeNumberLesserThanMax)
                {
                    // spawn a new ube as a reslt
                    Cube newCube = _cubeSpawner.Spawn(_cube.CubeNumber * 2, contactPoint + Vector3.up * 1.6f);
                    _audioSource.Play();
                    // push the new cube up and forward: 
                    float pushForce = 2.5f;
                    newCube.CubeRgidbody.AddForce(new Vector3(0, .3f, 1f) * pushForce, ForceMode.Impulse);

                    // add some torque
                    float randomValue = Random.Range(-20f, 20f);
                    Vector3 randomDirection = Vector3.one * randomValue;
                    newCube.CubeRgidbody.AddTorque(randomDirection);
                }
                // the explosion should affect surrounded cubes too:
                Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
                float explosionForce = 400f;
                float explosionRadius = 1.5f;
                foreach (Collider coll in surroundedCubes)
                {
                    if (coll.attachedRigidbody != null)
                    {
                        coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
                    }
                }


                _FX.PlayCubeExplosionFX(contactPoint, _cube.CubeColor);


                //Destroy the two cubes:
                _cubeSpawner.DestroyCube(_cube);
                _cubeSpawner.DestroyCube(otheCube);
            }
        }
    }
}
