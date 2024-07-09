public abstract class Quest
{
    public string QuestName { get; protected set; }
    public bool IsCompleted { get; protected set; }
    public event System.Action<Quest> OnQuestCompleted;

    public abstract void CheckCompletion();

    protected void CompleteQuest()
    {
        IsCompleted = true;
        OnQuestCompleted?.Invoke(this);
    }
}