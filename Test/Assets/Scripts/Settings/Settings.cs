using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject necstLoc;
    [SerializeField] int enNeed = 3;
    public static int enKill = 0;
    void Update()
    {
        if(enKill==enNeed)
        {
            necstLoc.SetActive(true);
        }
        Exit();
    }
    public static void RelodScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
