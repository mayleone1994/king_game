using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReferences : MonoBehaviour
{

    // EDITOR REFERENCES:
    [SerializeField] private PlayerContainerController _playerContainers;
    [SerializeField] private DeckController _deckController;
    [SerializeField] private Initializer _initializer;
    [SerializeField] private PrefabsController _prefabsController;

    public PlayerContainerController PlayerContainers => _playerContainers;
    public DeckController DeckController => _deckController;
    public Initializer Initializer => _initializer;
    public PrefabsController PrefabsController => _prefabsController;
}
