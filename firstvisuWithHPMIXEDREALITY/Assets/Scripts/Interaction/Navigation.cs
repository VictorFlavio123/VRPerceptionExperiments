using UnityEngine;
using System.Collections;

/* Navigation
 * This script specifies how to teleport user to a new position.
 *
 * Class reviewed by Leonardo Pavanatto on 05/2019.
 */
public class Navigation : Singleton<Navigation>
{
    // Define the gameobject used to display future play area
    [SerializeField]
    private GameObject feedback;
    private bool moving = false;

    //============================================================================

    /* Start is called before the first frame update */
    private void Start()
    {
        // Configure feedback showing the place where user is pointing
        if (feedback)
        {
            feedback.GetComponent<Renderer>().material.color = Color.blue;
            feedback.transform.position = Vector3.zero;
            feedback.SetActive(false);
        }
        else
            Debug.LogError("[Navigation] Feedback gameobject not set.");
    }

    /* Shows feedback while user is holding button down */
    public void StartTeleport(Ray ray)
    {
        RaycastHit hit;

        // Performs a raycast to detect object
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            feedback.transform.position = hit.point;
            feedback.SetActive(true);
        }
    }

    /* Hides feedback and teleport when user releases button */
    public void FinishTeleport(Ray ray)
    {
        RaycastHit hit;

        // Performs a raycast to detect object
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (moving) return;

            StartCoroutine(QuantumTeleport(transform.position, hit.point + new Vector3(transform.position.x - transform.GetChild(2).position.x, 0, transform.position.z - transform.GetChild(2).position.z)));
            feedback.SetActive(false);
        }
        else
        {
            feedback.SetActive(false);
        }
    }

    /* Instead of teleport, we move the user very fast - this allows spatial understanding with reduced cybersickness */
    IEnumerator QuantumTeleport(Vector3 origin, Vector3 destination)
    {
        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            moving = true;
            transform.position = Vector3.Lerp(transform.position, destination, 20 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        moving = false;
    }

    /* Rotate the user left */
    public void RotateLeft()
    {
        transform.RotateAround(transform.GetChild(2).position,Vector3.up, 20);
    }

    /* Rotate the user right */
    public void RotateRight()
    {
        transform.RotateAround(transform.GetChild(2).position, Vector3.down, 20);
    }
}