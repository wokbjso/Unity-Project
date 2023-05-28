using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuest : MonoBehaviour
{
    public QuestManager questManager;
    public Quest[] quests;
    Quest newQuest;

    // 캐릭터 위에 퀘스트 이미지 생성에 쓰이는 변수
    public GameObject questImagePrefab;
    private GameObject questImage;
    public float minDelay = 3f;
    public float maxDelay = 5f;

    private bool isQuestActive = false;
    private float questTimer = 0f;

    private void Start()
    {
        questTimer = Random.Range(1f, 3f);  // 게임 시작 후 5~30초 사이에 퀘스트 생성
        QuestManager.CompleteQuest();
    }

    private void Update()
    {
        if (!isQuestActive && QuestManager.Instance.InactiveAndCompleted)
        {
            questTimer -= Time.deltaTime;
            if (questTimer <= 0f)
            {
                GenerateQuest();
            }
        }
              
    }

    // 퀘스트 생성되면 퀘스트 이미지 생성
    private void GenerateQuest()
    {
        Vector3 questPosition = transform.position;
        questPosition.y = questPosition.y + 3;
        questImage = Instantiate(questImagePrefab, questPosition, Quaternion.identity);
        questImage.transform.SetParent(transform);

        isQuestActive = true;
    }

    // 퀘스트를 생성한 NPC에게 가서 스페이스바 누르면 퀘스트 부여
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isQuestActive && !QuestManager.Instance.IsActive)
            {
                newQuest = GetRandomQuest();
                QuestManager.Instance.AssignQuest(newQuest);

                Debug.Log("새로운 퀘스트 부여: " + newQuest.title);
                Debug.Log("목표: " + newQuest.objectives);
                Debug.Log("보상: " + newQuest.rewards);                
            }

            // 퀘스트 완료된 후 NPC에게 다가가면 퀘스트 종료 및 포인트 획득
            if (QuestManager.Instance.IsCompleted && QuestManager.Instance.IsActive)
            {
                CompleteQuest();

            }
        }

        
    }

    private Quest GetRandomQuest()
    {
        int randomIndex = Random.Range(0, quests.Length);
        return quests[randomIndex];
    }

    public void CompleteQuest()
    {
        questTimer = Random.Range(minDelay, maxDelay);  // 퀘스트 완료되고 3~5초 사이에 새로운 퀘스트 생성
        QuestManager.InactiveQuest();
        Destroy(questImage);
        
        isQuestActive = false;
        if (PlayerPrefs.HasKey("Point"))
        {
            int point = PlayerPrefs.GetInt("Point");
            if(newQuest.title == "낚시")
            {
                point += 30;
            }
            else if (newQuest.title == "요리")
            {
                point += 50;
            }
            PlayerPrefs.SetInt("Point", point);
        }
        else
        {
            if (newQuest.title == "낚시")
            {
                PlayerPrefs.SetInt("Point", 30);
            }
            else if (newQuest.title == "요리")
            {
                PlayerPrefs.SetInt("Point", 50);
            }
            
        }
        Debug.Log("현재 포인트: " + PlayerPrefs.GetInt("Point"));

    }
}

