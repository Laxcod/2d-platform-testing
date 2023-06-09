using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorSettings : MonoBehaviour
{
    public void Kembali()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
