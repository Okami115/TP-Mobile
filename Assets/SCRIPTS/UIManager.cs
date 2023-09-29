using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] mobileIU;

    void Start()
    {

#if UNITY_ANDROID
        for (int i = 0; i < mobileIU.Length; i++) { mobileIU[i].SetActive(true); }
#else
        for (int i = 0; i < mobileIU.Length; i++) { mobileIU[i].SetActive(false); }
#endif
        
    }

}
