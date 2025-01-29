using UnityEngine;

public class Temprandommoveforportals : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            transform.position = new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
            transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        }
    }
}
