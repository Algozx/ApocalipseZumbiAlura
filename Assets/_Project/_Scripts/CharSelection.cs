using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelection : MonoBehaviour
{
    public GameObject[] PlayerSkins;
    private int Index;


    private void Start()
    {
        Index = PlayerPrefs.GetInt("PlayerSelected");

        PlayerSkins = new GameObject[transform.childCount];

        //Preenchendo os arrays
        for (int i = 0; i < transform.childCount; i++)
            PlayerSkins[i] = transform.GetChild(i).gameObject;

        //Desligando a renderização dos modelos.
        foreach (GameObject go in PlayerSkins)
            go.SetActive(false);


        //Ligando o primeiro Index
        if (PlayerSkins[Index])
        {
            PlayerSkins[Index].SetActive(true);
            PlayerSkins[0].SetActive(true);
        }
    }

    public void Voltar()
    {
        //Desativar o modelo atual
        PlayerSkins[Index].SetActive(false);

        Index--;
        if (Index < 0)
            Index = PlayerSkins.Length - 1;

        //Ativar o novo modelo
        PlayerSkins[Index].SetActive(true);
    }

    public void Proximo()
    {
        //Desativar o modelo atual
        PlayerSkins[Index].SetActive(false);

        Index++;
        if (Index == PlayerSkins.Length)
            Index = 0;

        //Ativar o novo modelo
        PlayerSkins[Index].SetActive(true);
    }

    public void Confirmar()
    {
        PlayerPrefs.SetInt("PlayerSelected", Index);
    }

    
}
