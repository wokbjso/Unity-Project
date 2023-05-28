using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuest : MonoBehaviour
{
    public QuestManager questManager;
    public Quest[] quests;
    Quest newQuest;

    // ĳ���� ���� ����Ʈ �̹��� ������ ���̴� ����
    public GameObject questImagePrefab;
    private GameObject questImage;
    public float minDelay = 3f;
    public float maxDelay = 5f;

    private bool isQuestActive = false;
    private float questTimer = 0f;

    private void Start()
    {
        questTimer = Random.Range(1f, 3f);  // ���� ���� �� 5~30�� ���̿� ����Ʈ ����
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

    // ����Ʈ �����Ǹ� ����Ʈ �̹��� ����
    private void GenerateQuest()
    {
        Vector3 questPosition = transform.position;
        questPosition.y = questPosition.y + 3;
        questImage = Instantiate(questImagePrefab, questPosition, Quaternion.identity);
        questImage.transform.SetParent(transform);

        isQuestActive = true;
    }

    // ����Ʈ�� ������ NPC���� ���� �����̽��� ������ ����Ʈ �ο�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isQuestActive && !QuestManager.Instance.IsActive)
            {
                newQuest = GetRandomQuest();
                QuestManager.Instance.AssignQuest(newQuest);

                Debug.Log("���ο� ����Ʈ �ο�: " + newQuest.title);
                Debug.Log("��ǥ: " + newQuest.objectives);
                Debug.Log("����: " + newQuest.rewards);                
            }

            // ����Ʈ �Ϸ�� �� NPC���� �ٰ����� ����Ʈ ���� �� ����Ʈ ȹ��
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
        questTimer = Random.Range(minDelay, maxDelay);  // ����Ʈ �Ϸ�ǰ� 3~5�� ���̿� ���ο� ����Ʈ ����
        QuestManager.InactiveQuest();
        Destroy(questImage);
        
        isQuestActive = false;
        if (PlayerPrefs.HasKey("Point"))
        {
            int point = PlayerPrefs.GetInt("Point");
            if(newQuest.title == "����")
            {
                point += 30;
            }
            else if (newQuest.title == "�丮")
            {
                point += 50;
            }
            PlayerPrefs.SetInt("Point", point);
        }
        else
        {
            if (newQuest.title == "����")
            {
                PlayerPrefs.SetInt("Point", 30);
            }
            else if (newQuest.title == "�丮")
            {
                PlayerPrefs.SetInt("Point", 50);
            }
            
        }
        Debug.Log("���� ����Ʈ: " + PlayerPrefs.GetInt("Point"));

    }
}

