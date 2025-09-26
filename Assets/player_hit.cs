using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player_hit : MonoBehaviour
{
    public GameObject ghost;
    public Transform respawn_point;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // check_ghost_hit_player();
    }
    void check_ghost_hit_player()
    {
        if (ghost == null) return;

        if (Vector3.Distance(ghost.transform.position, transform.position) < 1.9)
        {
            ghost.GetComponent<ghost_follow>().ghost_change_position();

            GetComponent<player_stat>().redude_player_heart();

            if (GetComponent<player_stat>().player_heart == 0)
            {
                GetComponent<player_stat>().win_lose_choose(false);

                return;
            }

            transform.position = respawn_point.position;
        }
    }

    public void hitting()
    {
        // ghost.GetComponent<ghost_follow>().ghost_change_position();

        GetComponent<player_stat>().redude_player_heart();

        if (GetComponent<player_stat>().player_heart == 0)
        {
            GetComponent<player_stat>().win_lose_choose(false);

            return;
        }

        transform.position = respawn_point.position;
    }
}