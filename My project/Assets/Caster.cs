using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour
{
    public GameObject cigarette;
    public GameObject player;
    PlayerStats playerStats;

    public AudioSource audio;
    public AudioClip fumage;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("I Clicked");
            Ray theRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHitInfo;
            if (Physics.Raycast(theRay, out rayHitInfo))
            {
                if (rayHitInfo.collider.gameObject.CompareTag("cigarette"))
                {
                    Debug.Log("hit a touille");
                    GameObject.Destroy(rayHitInfo.collider.gameObject);
                    playerStats.nicotine = 0;
                    audio.PlayOneShot(fumage);
                }
            }
        }
    }
}
