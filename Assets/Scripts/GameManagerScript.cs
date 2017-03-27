using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    public PlayerController player;
    public Rotator rotator;
    public int rotatorCount;
    public Transform pickUps;
    public Text winText;
    public Text countText;
    public Text timerText;
    public Button resetB;

    private float time;
    private bool gameStart;
    
    // Use this for initialization
    void Start () {        
        PlayerController.countA += SetCountText;
        initGame();        
    }

    private void initGame()
    {
        
        player.Reset();
        resetB.gameObject.SetActive(false);
        winText.text = "";
        time = 0;
        SetTimerText(time);
        gameStart = true;

        var centrePos = new Vector3(0, (float)0.5, 0);
        for (int pointNum = 0; pointNum < rotatorCount; pointNum++) {
            var i = (pointNum * 1.0) / rotatorCount;
            var angle = (float)(i * Mathf.PI * 2);
            var x = Mathf.Sin(angle) * 8;
            var y = Mathf.Cos(angle) * 8;
            var pos = new Vector3(x,  0, y) + centrePos;
            Rotator r = GameObject.Instantiate(rotator);
            r.transform.position = pos;
            r.transform.SetParent(pickUps, false);
        }
    }

	// Update is called once per frame
	void Update () {
        if (gameStart)
        {
            time += Time.deltaTime;
            SetTimerText(time);
        }        
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown("r"))
        {
            ResetFunction();
        }
    }

    private void OnDestroy()
    {
        PlayerController.countA -= SetCountText;
    }

    void SetCountText(int count)
    {
        countText.text = "Count: " + count.ToString() + "/" + rotatorCount.ToString();
        if (count >= rotatorCount)
        {            
            winText.text = "You Win!";
            resetB.gameObject.SetActive(true);
            gameStart = false;
        }
    }

    void SetTimerText(float time)
    {
        string tempTime = time.ToString("0.00");
        timerText.text = tempTime + " Sekunden";
    }


    public void ResetFunction()
    {
        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("Pick Up");
        foreach (GameObject ob in objs)
        {
            GameObject.Destroy(ob);
        }
        initGame();
    }
}
