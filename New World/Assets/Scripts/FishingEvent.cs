using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FishingEvent : MonoBehaviour
{
    Quest nowQuest;
    public GameObject questImagePrefab;
    private GameObject questImage;
    private Quest newQuest;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(QuestManager.Instance!= null)
        {
            if (QuestManager.Instance.IsActive && !isActive)
            {
                // ���� ����Ʈ ��������
                nowQuest = QuestManager.Instance.GetNowQuest();

                // ����Ʈ�� ������ "����"���� Ȯ��
                if (nowQuest.title == "����")
                {
                    isActive = true;
                    SetQuestImage();              
                }
            }
        }

        if(Fishing.isHit)
        {
            Debug.Log("���� ����");

            Fishing.isHit = false;
            QuestManager.CompleteQuest();
            Destroy(questImage);
        }
        
    }

    private void SetQuestImage()
    {
        // Fishing House ���� ����Ʈ �̹��� �߻�
        Debug.Log("���� ����Ʈ �߻�");

        Vector3 questPosition = transform.position;
        questPosition.y += 3;
        questPosition.x += 3;
        questImage = Instantiate(questImagePrefab, questPosition, Quaternion.identity);
        questImage.transform.SetParent(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (QuestManager.Instance.IsActive && isActive)
            {
                Debug.Log("���� ����Ʈ �̵�");
                // ���� ���� �̸��� ����
                string currentSceneName = SceneManager.GetActiveScene().name;

                // ���ο� ���� additive ���� �ε�
                SceneManager.LoadScene("Fishing", LoadSceneMode.Additive);

                // �ε��� ���� Ȱ��ȭ
                Scene loadedScene = SceneManager.GetSceneByName("Fishing");
                SceneManager.SetActiveScene(loadedScene);

                // ���� �� ������ ���� ���� ���� ���¸� ����
                SceneManager.MergeScenes(loadedScene, SceneManager.GetSceneByName(currentSceneName));


            }
        }
    }
}
