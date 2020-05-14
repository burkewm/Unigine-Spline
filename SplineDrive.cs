using System;
using System.Collections.Generic;
using System.Text;
using Unigine;

public abstract class SplineDrive
{
    private CustomSplineSegment _actSegment;
    public CustomSplineSegment actSegment 
    { 
        get { return _actSegment; }
        set { _actSegment = value;} 
    }
    private float movmentSpeed = 0f;
    private float t = 0f;

    private vec3 _lastPos;
    public vec3 lastPos => _lastPos;

    private vec3 _actPos;
    public vec3 actPos => _actPos;

    protected abstract void SetDrivePosition(vec3 pos); //use Node.WorldPosition = pos
    protected abstract void SetDriveRotation(vec3 target); //use Node.WorldLookAt(vec3 target)

    private void CalcMovementSpeed()
    {
        if (_actSegment.useMainSpeed)
        {
            movmentSpeed = _actSegment.parentSpline.mainspeed / _actSegment.length;
            return;
        }
        else
        {
            float startSpeed = _actSegment.startSpeed;
            float endSpeed = _actSegment.endSpeed;
            if (startSpeed == endSpeed) { movmentSpeed = startSpeed / _actSegment.length; return; }
            float dif = startSpeed - endSpeed;
            movmentSpeed = dif * t / _actSegment.length;
        }
    }
    private void JumpToNext()
    {
        _actSegment = _actSegment.nextSegment;
        t -= 1.0f;
        //CalcMovementSpeed();
    }

    public void Move(float ifps)
    {
        CalcMovementSpeed();
        t += ifps * movmentSpeed;

        if (t >= 1.0f)
        {
            JumpToNext();
        }

        vec3 newPos = _actSegment.CalcPoint(t);
        _lastPos = _actPos;
        _actPos = newPos;

        SetDrivePosition(_actPos);
        SetDriveRotation(_lastPos);
    }
}