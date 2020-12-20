using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{

    [SerializeField]
    private float QuitDelay = 0f;

    public void Quit()
    {
        Application.Quit();
    }

    public void QuitWithDelay()
    {
        Invoke("Quit", QuitDelay);
    }


    //public void OnApplicationQuit()
    //{
    //    Application.Quit();
    //}

}
