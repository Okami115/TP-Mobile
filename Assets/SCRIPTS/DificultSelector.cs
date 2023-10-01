using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DificultSelector : MonoBehaviour
{
    [SerializeField] GameObject[] Cones;
    [SerializeField] GameObject[] Boxes;
    [SerializeField] GameObject Taxis;

    [SerializeField] GameObject[] DirLights;

    [SerializeField] Material Easy;
    [SerializeField] Material Normal;
    [SerializeField] Material Hard;

    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        Time.timeScale = 0.0f;
    }

    public void SetEasy()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        gameManager.Timmer = 65;

        for (int i = 0; i < Cones.Length; i++) 
        {
            Cones[i].gameObject.SetActive(false);
        }

        for(int i = 0;i < Boxes.Length;i++) 
        {
            Boxes[i].gameObject.SetActive(false);
        }

        Taxis.gameObject.SetActive(false);

        RenderSettings.skybox = Easy;
    }

    public void SetNormal()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        gameManager.Timmer = 50;

        Taxis.gameObject.SetActive(false);  
        RenderSettings.skybox = Normal;

        for (int i = 0; i < DirLights.Length / 2; i++)
        {
            DirLights[i].gameObject.SetActive(false);
        }
    }

    public void SetHard()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        gameManager.Timmer = 45;
        RenderSettings.skybox = Hard;

        for (int i = 0; i < DirLights.Length; i++)
        {
            DirLights[i].gameObject.SetActive(false);
        }
    }
}
