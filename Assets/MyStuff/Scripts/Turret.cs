using UnityEngine;
using StarterAssets;
using System.Collections;

public class Turret : MonoBehaviour
   
{
    public Transform Spawn_Point_Red;

    private void Start()
    {
        Spawn_Point_Red = GameObject.Find("Spawn_Point_Red").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Red Player1"&& enabled)
        {
            Debug.Log("I SEEEEE YOUUUUUUU");
            other.GetComponent<ThirdPersonController>().enabled = false;
            other.transform.position=Spawn_Point_Red.position;
            StartCoroutine(Letmego(other.GetComponent<ThirdPersonController>()));
        }
    }
    IEnumerator Letmego(ThirdPersonController thirdPersonController)
    {
        yield return new WaitForSeconds(1);
        thirdPersonController.enabled = true;
    }
}
