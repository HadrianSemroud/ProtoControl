using UnityEngine;
using System.Collections;

public class langageUnlockedTriggerZOne : MonoBehaviour {

    public bool unlockLangage1;
    public bool unlockLangage2;
    public bool unlockLangage3;
    public bool unlockLangage4;
 
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("UNLOCKED");
            if (unlockLangage1 == true)
            {
                (GameObject.FindObjectOfType(System.Type.GetType("langageGUI")) as langageGUI).langage1Unlocked = true;
            }

            if (unlockLangage2 == true)
            {
                (GameObject.FindObjectOfType(System.Type.GetType("langageGUI")) as langageGUI).langage2Unlocked = true;
            }

            if (unlockLangage3 == true)
            {
                (GameObject.FindObjectOfType(System.Type.GetType("langageGUI")) as langageGUI).langage3Unlocked = true;
            }

            if (unlockLangage4 == true)
            {
                (GameObject.FindObjectOfType(System.Type.GetType("langageGUI")) as langageGUI).langage4Unlocked = true;
            }
        }
    }
}
