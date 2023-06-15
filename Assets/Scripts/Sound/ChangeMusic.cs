using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public int indexMusic;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("ChangeMusic") != null)
        {
            AudioManager.instance.ChangeMusic(indexMusic);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
