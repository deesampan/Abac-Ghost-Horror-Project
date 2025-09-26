using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class time_counting : MonoBehaviour
{
    public TextMeshProUGUI time_text;
    public string time_c;
    public float time_f;
    public bool can_count = true;
    // Start is called before the first frame update
    void Start()
    {
        if (Time.fixedTime > 0)
        {
            time_f = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time_counting_func();

    }
    void time_counting_func()
    {
        if (!can_count) return;

        time_f += Time.deltaTime;

        time_text.text = "Time : " + time_f.ToString("F1");

        time_c = time_f.ToString("F1");
    }
    public void stop_counting()
    {
        can_count = false;
    }
}
