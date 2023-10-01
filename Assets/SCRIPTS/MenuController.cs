using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    [SerializeField] GameObject MenuInicio;
    [SerializeField] GameObject Credits;
    [SerializeField] GameObject SelectDificult;

    public void Play()
    {
        MenuInicio.SetActive(false);
        SelectDificult.SetActive(true);
    }

    public void ReadCredits()
    {
        MenuInicio.SetActive(false);
        Credits.SetActive(true);
    }

    public void Back()
    {

        MenuInicio.SetActive(true);
        Credits.SetActive(false);
        SelectDificult.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
