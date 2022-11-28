using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_CoolDown : MonoBehaviour
{
    private int  countdown;
    private PlayerController playerController;
    private HUD_Controller hudController;

    public HUD_CoolDown()
    {
        this.countdown = 8;
        this.playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        this.hudController = GameObject.Find("HUD").GetComponent<HUD_Controller>();
    }

    public IEnumerator StartCountdown(TextMeshProUGUI countdowntxt)
    {
        if (countdown > 0)
        {
            countdowntxt.gameObject.SetActive(true);
            countdowntxt.text = $"{countdown}";
            yield return new WaitForSeconds(1);
            countdown--;
            yield return StartCountdown(countdowntxt);
        }
        else
        {
            countdowntxt.gameObject.SetActive(false);
            countdown = 8;
            playerController.semillas++;
            hudController.SetPlayerSeeds();
        }
    }
}
