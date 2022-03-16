using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{
    private AudioSource AudioSourceScript;
    public static AudioSource instancia;
    public PlayerController Jogador;

    public void Awake()
    {
        AudioSourceScript = GetComponent<AudioSource>();
        instancia = AudioSourceScript;
    }

    void Update()
    {
        // if (Jogador.statusPlayer.health < 0 )
        // {
        //     AudioSourceScript.enabled = false;
        // }
    }
}
