using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void PlaySinglePlayer()
    {
        SceneManager.LoadScene(2);
        FSM.instance.ChangeScene(GameManager.GameType.SinglePlayer);
    }

    public void PlayMultiPlayer()
    {
        SceneManager.LoadScene(1);
        FSM.instance.ChangeScene(GameManager.GameType.MultiPlayer);
    }

}
