using System.Collections;
using UnityEngine;
using TMPro;

public class closeintro : MonoBehaviour
{
    public float closeDelay = 5f;
    public TMP_Text countdownText;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void OnMouseDown()
    {
        Debug.Log("dsadsa");
        close_menu();
    }

    void OnEnable()
    {
        StartCoroutine(CloseAfterDelay());
    }

    IEnumerator CloseAfterDelay()
    {
        float timeLeft = closeDelay;

        while (timeLeft > 0)
        {
            if (countdownText != null)
                countdownText.text = "เกมจะเริ่มต้นใน " + Mathf.Ceil(timeLeft).ToString() + "...";

            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        if (countdownText != null)
            countdownText.text = "เริ่มเกม...";

        close_menu();
    }
    public void close_menu()
    {
        gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
