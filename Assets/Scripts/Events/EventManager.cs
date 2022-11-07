using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public EventStatus Status;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEventStatus(EventStatus newStatus)
    {
        Status = newStatus;
        switch (newStatus)
        {
            case EventStatus.START_STATE:
                break;
            case EventStatus.TUTORIAL:
                break;
            case EventStatus.MAEVE:
                break;
            case EventStatus.FINALE:
                break;
            case EventStatus.END_STATE:
                break;
            case EventStatus.VOID:
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
        }
    }

    //This will handle the weather UI and starting dialog for the player
    public void HandleStartState()
    {

    }

    //This will handle when the player walks into the Tutorial Trigger

}

public enum EventStatus
{
    START_STATE,
    TUTORIAL,
    MAEVE,
    FINALE,
    END_STATE,
    VOID
}
