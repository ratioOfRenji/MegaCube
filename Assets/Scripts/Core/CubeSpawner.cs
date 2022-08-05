using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CubeSpawner : MonoBehaviour
{
   
    Queue<Cube> _cubesQueue = new Queue<Cube>();
    [SerializeField] private int cubesQueueCapacity = 20;
    [SerializeField] private bool autoQueueGrow = true;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;
    [SerializeField] private SceneContext _contex;

    [HideInInspector] public int maxCubeNumber; // 4096 2^12

    private int maxPower = 12;

    private Vector3 defaultSpawnPosition;
    private void Awake()
    {        defaultSpawnPosition =  transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);
        InitializeCubesQueue();


    }
    private void InitializeCubesQueue()
    {
        for (int i = 0; i < cubesQueueCapacity; i++)
        {
            AddCubeToQueue();
        }
    }

    private void AddCubeToQueue()
    {
      Cube cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity, transform).GetComponent<Cube>();
        cube.gameObject.SetActive(false);
        cube.IsMainCube = false;
        _contex.Container.Inject(cube.GetComponent<CubeCollision>());
        _cubesQueue.Enqueue(cube);
    }
    public Cube Spawn(int number, Vector3 position)
    {
        if (_cubesQueue.Count == 0)
        {
            if (autoQueueGrow)
            {
                cubesQueueCapacity++;
                AddCubeToQueue();
            } else
            {
                Debug.LogError("[Cubes Queue] : no more cubes available in th pool");
                return null;
            }
        }
        Cube cube = _cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(number);
        cube.SetColor(GetColor(number));
        cube.gameObject.SetActive(true);

        return cube;
    }
    public Cube SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(), defaultSpawnPosition);
    }
    public void DestroyCube(Cube cube)
    {
        cube.CubeRgidbody.velocity = Vector3.zero;
        cube.CubeRgidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.IsMainCube = false;
        cube.gameObject.SetActive(false);
        _cubesQueue.Enqueue(cube);
    }
    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));
    }
    private Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log (number) / Mathf.Log(2)) - 1];
    }
}
