using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{

    public Cube[] _cubesArray;

    public GameObject winUI;
    public void winGame()
    {
        _cubesArray = GetComponentsInChildren<Cube>();


        for (int i = 0; i < _cubesArray.Length; i++)
        {
            if (_cubesArray[i].CubeNumber == 4096)
            {
                Time.timeScale = 0f;
                winUI.SetActive(true);
            }

        }
    }

    private void Update()
    {
        winGame();
    }

}
