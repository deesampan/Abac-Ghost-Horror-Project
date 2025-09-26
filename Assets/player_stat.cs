using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class player_stat : MonoBehaviour
{
    public int collection = 0;
    public TextMeshProUGUI collect_text;
    public ghost_follow ghost_follow_script;

    public GameObject heart_board;
    public int player_heart = 3;
    public time_counting time_script;
    public GameObject result_window_show;

    public haunted_item[] item_list;
    public haunted_item current_aim_item;
    public GameObject ceremonial_platform;
    public TextMeshProUGUI item_distance_hint_text;

    public AudioSource heart_beat_audio;
    public GameObject warning_text;
    public GameObject ghost_prefab;
    public Transform respawn_point_for_ghost;
    void Start()
    {
        add_collection(0);
        aim_item_hint();
    }

    // Update is called once per frame
    void Update()
    {
        check_ghost_distance();

        if (collection < 8)
        {
            if (current_aim_item == null) return;

            float dis = Vector3.Distance(transform.position, current_aim_item.transform.position);
            item_distance_hint_text.text = "( Item Distance Hint : " + dis.ToString("F1") + " m )";
        }
        else
        {
            float dis = Vector3.Distance(transform.position, ceremonial_platform.transform.position);
            item_distance_hint_text.text = "( Ceremonial Platform : " + dis.ToString("F1") + " m )";
        }
    }
    public void win_lose_choose(bool _b)
    {
        Destroy(ghost_follow_script.gameObject);
        StartCoroutine(wait_for_change_scene(_b));
    }

    public void add_collection(int amount)
    {
        collection += amount;

        collect_text.text = "Collected : " + collection.ToString() + " / 8";

        if (collection == 4)
        {
            add_more_ghost();
        }

        if (collection >= 8)
        {
            collect_text.text = "Go to Ceremonial Platform";
            ghost_follow_script.ghost_upgrade();
        }
    }
    public void redude_player_heart()
    {
        player_heart -= 1;

        Destroy(heart_board.transform.GetChild(0).gameObject);
    }
    IEnumerator wait_for_change_scene(bool _b)
    {
        time_script.stop_counting();

        result_window_show.SetActive(true);
        result_window_show.GetComponent<Animation>().Play("fade");

        if (_b) result_window_show.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "You Win!";
        else result_window_show.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "You Lose!";


        MainManager.Instance.PlayTime = time_script.time_c;
        MainManager.Instance.PlayerName = MainManager.Instance.PlayerName;
        MainManager.Instance.Died = _b;
        MainManager.Instance.SaveDataToFile();

        yield return new WaitForSeconds(2);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    public void aim_item_hint()
    {
        for (int i = 0; i < item_list.Length; i++)
        {
            Debug.Log("checked " + item_list[i] != null);
            if (item_list[i] != null)
            {
                current_aim_item = item_list[i];
                return;
            }
        }
    }
    public void check_ghost_distance()
    {
        float dis = Vector3.Distance(transform.position, ghost_follow_script.transform.position);
        if (dis < 35)
        {
            if (!heart_beat_audio.isPlaying) heart_beat_audio.Play();
            heart_beat_audio.pitch = 1 + (10 - dis) / 10;
        }
        else
        {
            heart_beat_audio.Stop();
        }
    }
    public void add_more_ghost()
    {
        StartCoroutine(wait_for_warning_text_ghost());
        Instantiate(ghost_prefab, respawn_point_for_ghost.position, Quaternion.identity);
    }

    IEnumerator wait_for_warning_text_ghost()
    {
        warning_text.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        warning_text.SetActive(false);
    }
}
