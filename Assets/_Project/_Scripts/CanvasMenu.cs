using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasMenu : MonoBehaviour
{
    public GameObject Botaosair;

    private void Start()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        Botaosair.SetActive(true);
    #endif
    }


    public void GameStart()
    {
        StartCoroutine(MudarCena("Game"));
    }

    IEnumerator MudarCena(string name)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(name);
    }

    public void Sair()
    {
        StartCoroutine(SairDoJogo());
    }

    IEnumerator SairDoJogo()
    {
        yield return new WaitForSecondsRealtime(0.02f); 
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
