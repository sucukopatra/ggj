using UnityEngine;

[System.Serializable]
public class MaskDialogue
{
    public MaskType mask;
    public Dialogue[] dialogue;
}

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;

    [Header("Dialogue")]
    [SerializeField] private Dialogue[] defaultDialogue;
    [SerializeField] private MaskDialogue[] maskDialogues;

    [Header("Settings")]
    [SerializeField] private bool playOnlyOnce = false;

    private bool hasPlayed;
    private bool isPlayerNearby;
    private PlayerMask playerMask;

    [Header("Correct Answer Logic")]
    [SerializeField] private MaskType correctAnswer; 
    [SerializeField] private MonoBehaviour customLogic;
    private bool shouldRun;
    

    private void Update()
    {
        if (!isPlayerNearby) return;
        if (playOnlyOnce && hasPlayed) return;
        if (dialogueManager.IsDialogueActive) return;
        if (!InputManager.Instance.Interact()) return;

        Dialogue[] chosenDialogue = defaultDialogue;

        if (playerMask != null)
        {
            foreach (var entry in maskDialogues)
            {
                if (entry.mask == playerMask.CurrentMaskType)
                {
                    chosenDialogue = entry.dialogue;
                    break;
                }
            }
        }

        if(playerMask.CurrentMaskType == correctAnswer) shouldRun = true;

        playerMask.UnequipMask();

        dialogueManager.StartDialogue(chosenDialogue,customLogic, shouldRun);
        hasPlayed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNearby = true;
        playerMask = other.GetComponent<PlayerMask>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNearby = false;
        playerMask = null;
    }
}
