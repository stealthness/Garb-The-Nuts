using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource MunchSource;
  

    public void Munch()
    {
        if (MunchSource != null)
        {
            MunchSource.Play();
        }
        else
        {
            MunchSource = GetComponent<AudioSource>();
            MunchSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {

            Debug.Log("<1>");
            Munch();
        }
    }
}
