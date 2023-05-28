using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CookingEvent : MonoBehaviour
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
                if (nowQuest.title == "�丮")
                {
                    isActive = true;
                    SetQuestImage();              
                }
            }
        }

        if(Cooking.isSuccess)
        {
            Debug.Log("�丮 ����");

            Cooking.isSuccess = false;
            QuestManager.CompleteQuest();
            Destroy(questImage);
        }
        
    }

    private void SetQuestImage()
    {
        // Fishing House ���� ����Ʈ �̹��� �߻�
        Debug.Log("�丮 ����Ʈ �߻�");

        Vector3 questPosition = transform.position;
        questPosition.y += 3;
        questPosition.z += 4;
        questImage = Instantiate(questImagePrefab, questPosition, Quaternion.identity);
        questImage.transform.SetParent(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (QuestManager.Instance.IsActive && isActive)
            {
                Debug.Log("�丮 ����Ʈ �̵�");
                // ���� ���� �̸��� ����
                string currentSceneName = SceneManager.GetActiveScene().name;

                // ���ο� ���� additive ���� �ε�
                SceneManager.LoadScene("Cooking", LoadSceneMode.Additive);

                // �ε��� ���� Ȱ��ȭ
                Scene loadedScene = SceneManager.GetSceneByName("Cooking");
                SceneManager.SetActiveScene(loadedScene);

                // ���� �� ������ ���� ���� ���� ���¸� ����
                SceneManager.MergeScenes(loadedScene, SceneManager.GetSceneByName(currentSceneName));


            }
        }
    }
}
