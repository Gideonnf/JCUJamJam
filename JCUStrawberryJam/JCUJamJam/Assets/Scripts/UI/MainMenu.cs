using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    bool playOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayLoop("bgm");
    }

    private void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if(!playOnce)
        {
            AudioManager.instance.PlayLoop("bgm");
            playOnce = true;
        }
            
  
    }

    public void StartGame()
    {
        SceneManager.LoadScene("mysampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
