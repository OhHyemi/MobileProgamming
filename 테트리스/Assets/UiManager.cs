using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public Text Score;
    public Text BonusScore;
    public Image GameOver;
    public Text Combo;
    private MapManager map;
    private float time;
    
    
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        map = GameObject.Find("Map").GetComponent<MapManager>();
        Combo.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Combo.gameObject.activeSelf)
            time += Time.deltaTime;

        GameOver.gameObject.SetActive(false);

        Score.text = "SCORE : " + map.score;

        if (map.gameOver)
            GameOver.gameObject.SetActive(true);
        
        if(map.combo > 0)
        {
            Combo.gameObject.SetActive(true);
            BonusScore.text = "BONUSE SCORE " + map.bonusScore;
            Combo.text = "COMBO" + map.combo;            
        }
        if (time > 2f)
        {
            Combo.gameObject.SetActive(false);
            time = 0;
        }

        
    }

    //재시작을 위한 것
    public void loadScene()
    {
        SceneManager.LoadScene("Main");
    }
}
