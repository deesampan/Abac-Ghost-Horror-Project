using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class player_raycast : MonoBehaviour
{
    public Camera camera;
    player_stat player_stat_script;
    public Image cursorImage;
    public Sprite[] cursor_sprites;
    public GameObject warning_text;
    public AudioSource pickup_sound;
    void Start()
    {
        player_stat_script = GetComponent<player_stat>();
        warning_text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2f))
        {
            if (hit.transform.tag == "Item")
            {

                cursorImage.sprite = cursor_sprites[1];

                if (Input.GetMouseButtonDown(0))
                {
                    pickup_sound.Play();

                    Destroy(hit.transform.gameObject);

                    player_stat_script.add_collection(1);
                    StartCoroutine(wait_for_aim_next_item());
                }
            }
        }

        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (hit.transform.tag == "ceremony")
            {
                cursorImage.sprite = cursor_sprites[1];

                if (Input.GetMouseButtonDown(0))
                {
                    if (player_stat_script.collection < 8)
                    {
                        warning_text.SetActive(true);

                        StartCoroutine(wait_for_warning());
                    }
                    else
                    {
                        Debug.Log("Game Over");
                        GetComponent<player_stat>().win_lose_choose(true);
                    }
                }
            }
        }


        // cursorImage.sprite = cursor_sprites[0];
    }
    IEnumerator wait_for_warning()
    {
        yield return new WaitForSeconds(2f);
        warning_text.SetActive(false);
    }
    IEnumerator wait_for_aim_next_item()
    {
        yield return new WaitForSeconds(1f);
        player_stat_script.aim_item_hint();
    }

    void FixedUpdate()
    {
        cursorImage.sprite = cursor_sprites[0];


        
    }
}
