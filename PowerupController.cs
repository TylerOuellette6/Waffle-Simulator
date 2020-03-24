using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public static GameObject waffle;
    private static PlayerController waffleController;

    private static bool hasSuperSpeedBeenShown = false;
    private static bool hasSuperJumpBeenShown = false;
    private static bool hasSuperMiniBeenShown = false;

    public static GameObject superSpeedPowerup;
    public static GameObject superJumpPowerup;
    public static GameObject superMiniPowerup;

    void Start()
    {
        waffle = GameObject.Find("Waffle");
        waffleController = waffle.GetComponent<PlayerController>();

        GameObject mouse = GameObject.Find("Mouse");
        superSpeedPowerup = mouse.transform.Find("Super Speed Powerup").gameObject;

        GameObject frenchToast = GameObject.Find("French Toast");
        superJumpPowerup = frenchToast.transform.Find("Super Jump Powerup").gameObject;

        GameObject elffaw = GameObject.Find("Elffaw");
        superMiniPowerup = elffaw.transform.Find("Super Mini Powerup").gameObject;
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

    public static void toggleSuperJumpPowerup(bool superJumpOn)
    {
        if (superJumpOn)
        {
            waffleController.setJumpHeight(200);
        }
        else
        {
            // TODO: Come back to this 
            waffleController.setJumpHeight(20);
        }
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

    public static void toggleSuperMiniPowerup(bool superMiniOn)
    {
        if (superMiniOn)
        {
            waffle.transform.localScale = new Vector3(3, 3, 0.2f);
        }
        else
        {
            Vector3 curPos = waffle.transform.position;
            waffle.transform.position = new Vector3(curPos.x, curPos.y + 4, curPos.z);
            waffle.transform.localScale = new Vector3(6, 6, 0.4f);
        }
    }
}
