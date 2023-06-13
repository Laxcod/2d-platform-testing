using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public int nextScene;
    public void OpenScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
