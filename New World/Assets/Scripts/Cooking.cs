using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cooking: MonoBehaviour
{
    public int targetClicks = 50; // 목표 클릭 수
    public float gameDuration = 10f; // 게임 시간 (초)
    public TMP_Text timerText; // 시간을 표시할 TMP 텍스트
    public TMP_Text clickText; // 클릭 수를 표시할 TMP 텍스트
    public TMP_Text resultText; // 결과를 표시할 TMP 텍스트
    public Button clickButton; // 클릭 버튼
    private int currentClicks = 0; // 현재 클릭 수
    private float currentTime = 0f; // 현재 시간
    private bool gameEnded = false; // 게임 종료 여부

    public static bool isSuccess = false;

    void Start()
    {
        // 초기화
        currentClicks = 0;
        currentTime = gameDuration;
        gameEnded = false;

        // UI 초기화
        UpdateUI();
        
        // 클릭 버튼에 클릭 이벤트 리스너 추가
        clickButton.onClick.AddListener(IncrementClick);
    }

    void Update()
    {
        if (gameEnded)
            return;

        // 시간 갱신
        currentTime -= Time.deltaTime;
        UpdateUI();

        // 게임 종료 체크
        if (currentTime <= 0f || currentClicks >= targetClicks)
        {
            EndGame();
        }
    }

    void UpdateUI()
    {
        // TMP 텍스트 업데이트
        timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString() + "s";
        clickText.text = "Clicks: " + currentClicks.ToString() + "/" + targetClicks.ToString();
    }

    void IncrementClick()
    {
        if (gameEnded)
            return;

        // 클릭 수 증가
        currentClicks++;
        UpdateUI();
    }

    void EndGame()
    {
        gameEnded = true;

        if (currentClicks >= targetClicks)
        {
            // 게임 성공
            resultText.text = "Success!";
            isSuccess = true;
        }
        else
        {
            // 게임 실패
            resultText.text = "Failure!";
        }
    }
}