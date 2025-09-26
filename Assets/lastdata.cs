using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class lastdata : MonoBehaviour
{
    public TextMeshProUGUI player_name_text;
    public TextMeshProUGUI player_time_text;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        MainManager.Instance.LoadData();

        player_name_text.text = "Player Name : " + MainManager.Instance.PlayerName;
        player_time_text.text = "Time Played : " + MainManager.Instance.PlayTime;
    }
}
