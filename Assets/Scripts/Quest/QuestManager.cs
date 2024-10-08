using System.Collections.Generic;
public class QuestManager
{
    public List<Quest> quests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
        quest.OnQuestCompleted += OnQuestCompleted;
    }

    private void OnQuestCompleted(Quest quest)
    {
        quests.Remove(quest);
    }

    public void CheckQuests()
    {
        foreach (var quest in quests)
        {
            quest.CheckCompletion();
        }
    }

    public List<Quest> getAllQuest() {
        return quests;
    }
}