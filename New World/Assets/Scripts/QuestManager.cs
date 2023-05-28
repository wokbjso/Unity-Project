using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Quest nowQuest;

    // ���� �������� ����Ʈ�� �ִ���
    private static bool isActive = false;
    private static bool isCompleted = false;

    // �ִ� 1���� ����Ʈ�� ������ �� �ֵ��� �̱��� ����
    private static QuestManager instance;
    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
            {
                // QuestManager ������Ʈ�� ���� ���, ���ο� GameObject�� �����ϰ� QuestManager ��ũ��Ʈ�� �߰��մϴ�.
                GameObject questManagerObj = new GameObject("QuestManager");
                instance = questManagerObj.AddComponent<QuestManager>();
            }

            return instance;
        }
    }

  

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool IsActive
    {
        get
        {
            return isActive;
        }
    }

    public bool IsCompleted
    {
        get
        {
            return isCompleted;
        }
    }

    public void AssignQuest(Quest quest)
    {
        nowQuest = quest;
        isActive = true;
        isCompleted = false;
    }

    public Quest GetNowQuest()
    {
        return nowQuest;
    }

    public static void InactiveQuest()
    {
        isActive = false;
    }

    public static void CompleteQuest()
    {
        isCompleted = true;
    }

    // ���� �������� ����Ʈ ����
    public bool InactiveAndCompleted
    {
        get
        {
            return !isActive && isCompleted;
        }
       
    }
}
