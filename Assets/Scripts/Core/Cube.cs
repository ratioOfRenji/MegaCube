using UnityEngine;
using TMPro;

public class Cube : MonoBehaviour
{
    static int staticID = 0;
    [SerializeField] private TMP_Text[] numbersText;

    [HideInInspector] public int CubeID;
    [HideInInspector] public Color CubeColor;
    [HideInInspector] public int CubeNumber;
    [HideInInspector] public Rigidbody CubeRgidbody;
    [HideInInspector] public bool IsMainCube;

    private MeshRenderer _cubeMeshRenderer;

    private void Awake()
    {
        CubeID = staticID++;
        _cubeMeshRenderer = GetComponent<MeshRenderer>();
        CubeRgidbody = GetComponent<Rigidbody>();
    }
    public void SetColor(Color color)
    {
        CubeColor = color;
        _cubeMeshRenderer.material.color = color;
    }

    public void SetNumber( int number)
    {
        CubeNumber = number;
        for (int i = 0; i < 6; i++)
        {
            numbersText[i].text = number.ToString();
        }
    }
    
}
