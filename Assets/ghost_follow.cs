using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class ghost_follow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;

    public float enemySpeed;
    public float enemySpeed_upgrade;
    public Transform respawn_point_ghost;
    public GameObject jumpscare_box;
    public AudioSource screaming_sound;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemy.speed = enemySpeed;

        jumpscare_box = GameObject.FindGameObjectWithTag("jumpscare");
    }

    // Update is called once per frame
    void Update()
    {
        check_distance_player();
        // float distance = range;
        // float dist = Vector3.Distance(enemy.transform.position, player.position);

        // if (dist <= distance)
        // {
        //     enemy.SetDestination(player.position);
        // }

        enemy.SetDestination(player.position);

    }
    public void ghost_upgrade()
    {
        enemy.speed = enemySpeed_upgrade;
    }
    public void ghost_change_position()
    {
        Debug.Log(jumpscare_box);
        screaming_sound.Play();
        StartCoroutine(wait_for_jumpscare());
        transform.position = new Vector3(-115.629997f, 2.43000007f, -9.55000019f);
    }

    public void check_distance_player()
    {
        float dis = Vector3.Distance(transform.position, player.position);
        if (dis < 1.9f)
        {
            player.GetComponent<player_hit>().hitting();
            ghost_change_position();
        }
    }

    IEnumerator wait_for_jumpscare()
    {
        jumpscare_box.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        jumpscare_box.transform.GetChild(0).gameObject.SetActive(false);
    }

}