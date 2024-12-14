using UnityEngine;

public class play : MonoBehaviour
{
    // Start is called once befo
    // re the first execution of Update after the MonoBehaviour is created
    public GameObject main;
    public GameObject spiritRacers;
    public GameObject plays;
    public GameObject scene1;
    public GameObject scene2;
    public GameObject scene3;
    public GameObject white;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject about;
    public void go()
    {
        main.SetActive(false);
        spiritRacers.SetActive(false);
        plays.SetActive(false);
        about.SetActive(true);
    }
    public void ok()
    {
        about.SetActive(false );
        scene1.SetActive(true);
        scene2.SetActive(true);
        scene3.SetActive(true);
        white.SetActive(true);
        text1.SetActive(true);
        text2.SetActive(true);
        text3.SetActive(true);
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
