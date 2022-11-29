using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_CoolDown 
{
    private int  countdown;
    private GunPoint playerGun;
    private HUD_Controller hudController;

    public HUD_CoolDown()
    {
        countdown = 8;
        hudController = GameObject.Find("HUD").GetComponent<HUD_Controller>();
        playerGun = hudController.playerGun;
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
            playerGun.availableSeeds++;
            hudController.SetPlayerSeeds();
        }
    }
}
