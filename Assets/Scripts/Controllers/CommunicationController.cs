using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationController : MonoBehaviour
{
    [SerializeField] private EventsMediator _eventsMediator;

    public EventsMediator EventsMediator => _eventsMediator;
}
