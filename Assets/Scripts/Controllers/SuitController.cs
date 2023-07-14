using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitController : MonoBehaviour, IController
{
    private King_ServiceLocator _serviceLocator;

    public void Init(King_ServiceLocator serviceLocator)
    {
        _serviceLocator = serviceLocator;
    }
}
