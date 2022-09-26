using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Quest Manager", menuName = "Managers/Quest Manager")]
public class QuestManager : ScriptableObject
{
    [System.NonSerialized] public UnityEvent<Quest> onQuestCreation;
    [System.NonSerialized] public UnityEvent onPanelTextUpdate;

    private void OnEnable()
    {
        if (onQuestCreation == null) onQuestCreation = new UnityEvent<Quest>();
        if (onPanelTextUpdate == null) onPanelTextUpdate = new UnityEvent();
    }

    public void QuestCreated(Quest quest)
    {
        onQuestCreation?.Invoke(quest);
    }

    public void TextUpdate()
    {
        onPanelTextUpdate?.Invoke();
    }
}