using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public GameObject waffle;
    private static PlayerController waffleController;

    private static bool hasSuperSpeedBeenShown = false;
    private static bool hasSuperJumpBeenShown = false;
    private static bool hasSuperMiniBeenShown = false;

    public static GameObject superSpeedPowerup;
    public static GameObject superJumpPowerup;
    public static GameObject superMiniPowerup;

    void Start()
    {
        waffleController = waffle.GetComponent<PlayerController>();

        GameObject mouse = GameObject.Find("Mouse");
        superSpeedPowerup = mouse.transform.Find("Super Speed Powerup").gameObject;

        GameObject frenchToast = GameObject.Find("French Toast");
        superJumpPowerup = frenchToast.transform.Find("Super Jump Powerup").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // SUPER SPEED
    public static void showSuperSpeedPowerup()
    {
        if (!hasSuperSpeedBeenShown)
        {
            superSpeedPowerup.SetActive(true);
        }
        hasSuperSpeedBeenShown = true;
    }

    public static void receiveSuperSpeedPowerup()
    {
        waffleController.setRunSpeed(50);
    }


    // SUPER JUMP
    public static void showSuperJumpPowerup()
    {
        if (!hasSuperJumpBeenShown)
        {
            superJumpPowerup.SetActive(true);
        }
        hasSuperJumpBeenShown = true;
    }

    public static void receiveSuperJumpPowerup()
    {
        // Draw power-up and handle toggling
    }

    // SUPER MINI
    public static void showSuperMiniPowerup()
    {
        if (!hasSuperMiniBeenShown)
        {
            superMiniPowerup.SetActive(true);
        }
        hasSuperMiniBeenShown = true;
    }

    public static void receiveSuperMiniPowerup()
    {
        // Draw power-up and handle toggling
    }
}
