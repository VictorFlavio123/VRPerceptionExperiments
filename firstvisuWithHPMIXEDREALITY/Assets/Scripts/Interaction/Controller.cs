using UnityEngine;
using Valve.VR;

/* Controller
 * This script centralizes all controller interaction information.
 *
 * Class reviewed by Leonardo Pavanatto on 05/2019.
 */
public class Controller : Singleton<Navigation>
{
    [SerializeField]
    private Transform leftController;

    [SerializeField]
    private Transform rightController;

    [SerializeField]
    private Ray leftRay;

    [SerializeField]
    private Ray rightRay;

    //============================================================================

    /* Start is called before the first frame update */
    void Start()
    {
        // If not defined, define controller gameobjects
        if (!leftController)
            Debug.LogError("[Controller] Left Conteroller not set.");

        if (!rightController)
            Debug.LogError("[Controller] Left Conteroller not set.");
    }

    /* Update is called once per frame */
    void Update()
    {
        // Creates a ray from the point where camera is located
        leftRay = new Ray(leftController.position, leftController.forward);
        rightRay = new Ray(rightController.position, rightController.forward);

        // Register existing actions for each hand
        HandButtons(leftController, leftRay, SteamVR_Input_Sources.LeftHand);
        HandButtons(rightController,rightRay, SteamVR_Input_Sources.RightHand);
    }

    private void HandButtons(Transform controller, Ray ray, SteamVR_Input_Sources input)
    {
        // Teleport action while down
        if (SteamVR_Actions._default.Teleport.GetState(input))
        {
            Navigation.Instance.StartTeleport(ray);
        }

        // Teleport action when up
        if (SteamVR_Actions._default.Teleport.GetStateUp(input))
        {
            Navigation.Instance.FinishTeleport(ray);
        }
    }
}
