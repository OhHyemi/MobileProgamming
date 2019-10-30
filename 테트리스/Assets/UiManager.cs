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

        //평소에는 게임오버를 꺼놓음
        GameOver.gameObject.SetActive(false);

        Score.text = "SCORE : " + map.score;

        //게임 오버가 되었을 때 나타나기
        if (map.gameOver)
            GameOver.gameObject.SetActive(true);
        
        if(map.combo > 0)//콤보일때
        {
            Combo.gameObject.SetActive(true);
            BonusScore.text = "BONUSE SCORE " + map.bonusScore;
            Combo.text = "COMBO" + map.combo;            
        }
        if (time > 2f) //콤보 텍스트 2초 동안 활성화
        {
            Combo.gameObject.SetActive(false);
            time = 0;
        }

        
    }

    //재시작 -> scene을 다시 불러오기
    public void loadScene()
    {
        SceneManager.LoadScene("Main");
    }
}
