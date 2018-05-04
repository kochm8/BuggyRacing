using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMenuBehaviour : MonoBehaviour {

    public void OnButtonClickMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
