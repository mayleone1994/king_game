using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room Config", menuName = "Create New Room Config")]
public class RoomConfigSO : ScriptableObject
{
    [SerializeField] private RoomType _roomType;

    public RoomType RoomType => _roomType;
}
