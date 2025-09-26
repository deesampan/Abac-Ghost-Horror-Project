using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uimanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void change_to_game_scene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
