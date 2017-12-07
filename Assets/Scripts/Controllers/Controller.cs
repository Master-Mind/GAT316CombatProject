using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;


public class Controller
{
    private PlayerIndex _myIndex;
    private GamePadState _curState;
    private GamePadState _prevState;
    private float _curStrength;
    private float _vibTim;

    public enum Button
    {
        A,
        B,
        X,
        Y,
        Start,
        Back,
        LeftBumper,
        RightBumper,
        RightStick,
        LeftStick
    }

    public void Init(PlayerIndex MyIndex = PlayerIndex.One)
    {
        _myIndex = MyIndex;
        _curState = GamePad.GetState(_myIndex);
    }

    public void Update()
    {
        _prevState = _curState;
        _curState = GamePad.GetState(_myIndex);
        if (_vibTim > 0)
        {
            _vibTim -= Time.deltaTime;
        }
        else
        {
            _curStrength = 0;
        }
        GamePad.SetVibration(_myIndex, _curStrength, _curStrength);
    }

    public Vector2 GetAxis(bool isLeft)
    {
        if (isLeft)
        {
            return new Vector2(_curState.ThumbSticks.Left.X, _curState.ThumbSticks.Left.Y);
        }
        return new Vector2(_curState.ThumbSticks.Right.X, _curState.ThumbSticks.Right.Y);
    }

    //Has the user just pressed butt this frame?
    public bool GetButtonPressed(Button butt)
    {
        switch (butt)
        {
            case Button.A:
                return _curState.Buttons.A == ButtonState.Pressed && _prevState.Buttons.A == ButtonState.Released;
                break;
            case Button.B:
                return _curState.Buttons.B == ButtonState.Pressed && _prevState.Buttons.B == ButtonState.Released;
                break;
            case Button.X:
                return _curState.Buttons.X == ButtonState.Pressed && _prevState.Buttons.X == ButtonState.Released;
                break;
            case Button.Y:
                return _curState.Buttons.Y == ButtonState.Pressed && _prevState.Buttons.Y == ButtonState.Released;
                break;
            case Button.Start:
                return _curState.Buttons.Start == ButtonState.Pressed && _prevState.Buttons.Start == ButtonState.Released;
                break;
            case Button.Back:
                return _curState.Buttons.Back == ButtonState.Pressed && _prevState.Buttons.Back == ButtonState.Released;
                break;
            case Button.LeftBumper:
                return _curState.Buttons.LeftShoulder == ButtonState.Pressed && _prevState.Buttons.LeftShoulder == ButtonState.Released;
                break;
            case Button.RightBumper:
                return _curState.Buttons.RightShoulder == ButtonState.Pressed && _prevState.Buttons.RightShoulder == ButtonState.Released;
                break;
            case Button.RightStick:
                return _curState.Buttons.RightStick == ButtonState.Pressed && _prevState.Buttons.RightStick == ButtonState.Released;
                break;
            case Button.LeftStick:
                return _curState.Buttons.LeftStick == ButtonState.Pressed && _prevState.Buttons.LeftStick == ButtonState.Released;
                break;
            default:
                Debug.LogAssertion("Thats not a button dumbass");
                break;
        }
        return false;
    }

    //Has the user just Released butt this frame?
    public bool GetButtonReleased(Button butt)
    {
        switch (butt)
        {
            case Button.A:
                return _prevState.Buttons.A == ButtonState.Pressed && _curState.Buttons.A == ButtonState.Released;
                break;
            case Button.B:
                return _prevState.Buttons.B == ButtonState.Pressed && _curState.Buttons.B == ButtonState.Released;
                break;
            case Button.X:
                return _prevState.Buttons.X == ButtonState.Pressed && _curState.Buttons.X == ButtonState.Released;
                break;
            case Button.Y:
                return _prevState.Buttons.Y == ButtonState.Pressed && _curState.Buttons.Y == ButtonState.Released;
                break;
            case Button.Start:
                return _prevState.Buttons.Start == ButtonState.Pressed && _curState.Buttons.Start == ButtonState.Released;
                break;
            case Button.Back:
                return _prevState.Buttons.Back == ButtonState.Pressed && _curState.Buttons.Back == ButtonState.Released;
                break;
            case Button.LeftBumper:
                return _prevState.Buttons.LeftShoulder == ButtonState.Pressed && _curState.Buttons.LeftShoulder == ButtonState.Released;
                break;
            case Button.RightBumper:
                return _prevState.Buttons.RightShoulder == ButtonState.Pressed && _curState.Buttons.RightShoulder == ButtonState.Released;
                break;
            case Button.RightStick:
                return _prevState.Buttons.RightStick == ButtonState.Pressed && _curState.Buttons.RightStick == ButtonState.Released;
                break;
            case Button.LeftStick:
                return _prevState.Buttons.LeftStick == ButtonState.Pressed && _curState.Buttons.LeftStick == ButtonState.Released;
                break;
            default:
                Debug.LogAssertion("Thats not a button dumbass");
                break;
        }
        return false;
    }

    //Has the user been pressing butt?
    public bool GetButtonDown(Button butt)
    {
        switch (butt)
        {
            case Button.A:
                return _prevState.Buttons.A == ButtonState.Pressed && _curState.Buttons.A == ButtonState.Pressed;
                break;
            case Button.B:
                return _prevState.Buttons.B == ButtonState.Pressed && _curState.Buttons.B == ButtonState.Pressed;
                break;
            case Button.X:
                return _prevState.Buttons.X == ButtonState.Pressed && _curState.Buttons.X == ButtonState.Pressed;
                break;
            case Button.Y:
                return _prevState.Buttons.Y == ButtonState.Pressed && _curState.Buttons.Y == ButtonState.Pressed;
                break;
            case Button.Start:
                return _prevState.Buttons.Start == ButtonState.Pressed && _curState.Buttons.Start == ButtonState.Pressed;
                break;
            case Button.Back:
                return _prevState.Buttons.Back == ButtonState.Pressed && _curState.Buttons.Back == ButtonState.Pressed;
                break;
            case Button.LeftBumper:
                return _prevState.Buttons.LeftShoulder == ButtonState.Pressed && _curState.Buttons.LeftShoulder == ButtonState.Pressed;
                break;
            case Button.RightBumper:
                return _prevState.Buttons.RightShoulder == ButtonState.Pressed && _curState.Buttons.RightShoulder == ButtonState.Pressed;
                break;
            case Button.RightStick:
                return _prevState.Buttons.RightStick == ButtonState.Pressed && _curState.Buttons.RightStick == ButtonState.Pressed;
                break;
            case Button.LeftStick:
                return _prevState.Buttons.LeftStick == ButtonState.Pressed && _curState.Buttons.LeftStick == ButtonState.Pressed;
                break;
            default:
                Debug.LogAssertion("Thats not a button dumbass");
                break;
        }
        return false;
    }

    public void Stap()
    {
        GamePad.SetVibration(_myIndex, 0, 0);
    }
    public float GetTrigger(bool IsLeft)
    {
        if (IsLeft)
        {
            return _curState.Triggers.Left;
        }
        return _curState.Triggers.Right;
    }

    public void VibrateFor(float tim, float strength)
    {
        _vibTim = tim;
        _curStrength = strength;
    }
}
