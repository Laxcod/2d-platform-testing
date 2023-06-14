using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    FadeInOut fadeInOut;

    // Start is called before the first frame update
    void Start()
    {
        fadeInOut = FindObjectOfType<FadeInOut>();

        fadeInOut.FadeOut();
    }
}
