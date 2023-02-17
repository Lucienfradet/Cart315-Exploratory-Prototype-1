using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{   
    public int nicotine = 0;
    public int maxNicotine = 100;
    
    public float eyeHealth = 0f;
    private float eyeHealthSpeed = 0.3f;
    private float blinkPower = 100f;

    public float breathHealth = 0;
    private float breathHealthSpeed = 0.05f;
    private bool breathInhale = false;
    private bool breathSwitch = false;

    public CigBar cigBar;
    public GameObject topLid;
    public GameObject bottomLid;

    public AudioSource audio;
    public AudioClip woop;
    public AudioClip inhale;
    public AudioClip exhale;
    public AudioClip fumage;
    // Start is called before the first frame update
    void Start()
    {
        cigBar.setMaxNicotine(maxNicotine);
    }

    int timer = 0;
    void FixedUpdate()
    {
        if (timer % 25 == 0)
        {
            nicotineIncrease(1);
        }

        if (eyeHealth < 300)
        {
            eyeHealth += eyeHealthSpeed;
        }
        
        if (!breathSwitch)
        {
            if (breathHealth > -100)
            {
                breathHealth -= breathHealthSpeed;
            }
        }
        else
        {
            breathHealth += breathHealthSpeed * 5;
            if (breathHealth > 0)
            {
                breathHealth = 0;
                breathSwitch = false;
            }
        }
        

        timer++;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            topLid.GetComponent<Animator>().Play("blinkTop");
            bottomLid.GetComponent<Animator>().Play("blinkButtom");

            eyeHealth -= blinkPower;
            if (eyeHealth < 0)
            {
                eyeHealth = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            breathInhale = true;
            audio.PlayOneShot(inhale);
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            breathInhale = false;
            breathSwitch = true;
            audio.PlayOneShot(exhale);
        }
    }

    void nicotineIncrease(int amount)
    {
        this.nicotine += amount;
        cigBar.setNicotine(nicotine);
    }
}
