using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBAInput : MonoBehaviour
{
    [SerializeField] KeyCode _up;
    [SerializeField] KeyCode _down;
    [SerializeField] KeyCode _left;
    [SerializeField] KeyCode _right;
    [SerializeField] KeyCode _buttonA;
    [SerializeField] KeyCode _buttonB;

    public KeyCode Up { get => _up; set => _up = value; }
    public KeyCode Down { get => _down; set => _down = value; }
    public KeyCode Left { get => _left; set => _left = value; }
    public KeyCode Right { get => _right; set => _right = value; }
    public KeyCode ButtonA { get => _buttonA; set => _buttonA = value; }
    public KeyCode ButtonB { get => _buttonB; set => _buttonB = value; }

    private void Awake()
    {
        InitDefault();

    }

    void InitDefault()
    {
        Up = KeyCode.UpArrow;
        Down = KeyCode.DownArrow;
        Left = KeyCode.LeftArrow;
        Right = KeyCode.RightArrow;
        ButtonA = KeyCode.Z;
        ButtonB = KeyCode.X;
    }
}
