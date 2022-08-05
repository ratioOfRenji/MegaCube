using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPosX;
    [Space]
    //[SerializeField] private MyTouchSlider touchSlider;
    [Inject]
    private CubeSpawner _spawner;
    [Inject]
    private TouchSlider _slider;
    private Cube _mainCube;

    private bool isPointerDown;
    private bool canMove;
    private Vector3 cubePos;

    private void Start()
    {
        SpawnCube();
        canMove = true;

        // listen to slider events
        _slider.OnPointerDownEvent += OnPointerDown;
        _slider.OnPointerDragEvent += OnPointerDrag;
        _slider.OnPointerUpEvent += OnPointerUp;
    }

    private void Update()
    {
        if (isPointerDown&& canMove)
        {
            _mainCube.transform.position = Vector3.Lerp(_mainCube.transform.position, cubePos, moveSpeed * Time.deltaTime);
        }
    }

    private void OnPointerDown()
    {
        isPointerDown = true;
    }
    private void OnPointerDrag(float xMovement)
    {
        if(isPointerDown)
        {
            cubePos = _mainCube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX;
        }
    }
    private void OnPointerUp()
    {
        if(isPointerDown && canMove)
        {
            isPointerDown = false;
            canMove = false;
            // push the cube
            _mainCube.CubeRgidbody.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);

            Invoke("SpawnNewCube", 0.3f);

        }
    }
    private void SpawnNewCube()
    {
        _mainCube.IsMainCube = false;
        canMove = true;
        SpawnCube();
    }
    private void SpawnCube()
    {
        _mainCube = _spawner.SpawnRandom();
        _mainCube.IsMainCube = true;

        // reset  cubePos variable
        cubePos = _mainCube.transform.position;
    }
    private void OnDestroy()
    {
        // remove listeners
        _slider.OnPointerDownEvent -= OnPointerDown;
        _slider.OnPointerDragEvent -= OnPointerDrag;
        _slider.OnPointerUpEvent -= OnPointerUp;
    }
}
