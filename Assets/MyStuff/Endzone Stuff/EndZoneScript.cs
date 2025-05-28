using UnityEngine;

public class EndZoneScript : MonoBehaviour
{
    public bool blue;
    public bool red;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Red Player1" && red == true)
        {
            Debug.Log("enter");
            GameObject.Find("Level Manager").GetComponent<LevelManager>().neededForSkip += 1;
        }

        if (other.gameObject.name == "Blue Player2" && blue == true)
        {
            Debug.Log("enter");
            GameObject.Find("Level Manager").GetComponent<LevelManager>().neededForSkip += 1;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Red Player1" && red == true)
        {
            Debug.Log("exit");
            GameObject.Find("Level Manager").GetComponent<LevelManager>().neededForSkip -= 1;
        }

        if (other.gameObject.name == "Blue Player2" && blue == true)
        {
            Debug.Log("exit");
            GameObject.Find("Level Manager").GetComponent<LevelManager>().neededForSkip -= 1;
        }
    }
}
