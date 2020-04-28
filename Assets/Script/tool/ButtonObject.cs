using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public abstract class ButtonObject : MonoBehaviour
{
    private EventTrigger eventTrigger;
    private EventTrigger.Entry entry;

    void Awake()
    {
        this.eventTrigger = this.gameObject.AddComponent<EventTrigger>();
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener(data => this.PointDown());

        eventTrigger.triggers.Add(entry);
    }

    public abstract void PointDown();
}
