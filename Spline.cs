using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Unigine;

public class Spline
{
    public Spline() { _name = "spline"; }
    public Spline(string name) { _name = name; }

    private string _name;

    private List<SplinePosition> points = new List<SplinePosition>();
    private List<CustomSplineSegment> segments = new List<CustomSplineSegment>();

    public int PointCount { get { return points.Count; } }
    public int SegmentCount { get { return segments.Count; } }
    public float Lentgh
    {
        get
        {
            float ret = 0;
            foreach (CustomSplineSegment seg in segments)
            {
                ret += seg.length;
            }
            return ret;
        }
    }

    private int countPoints = -1;
    private int countSegments = -1;

    public int mainspeed = 1;

    public int CreateSplinePoint(vec3 pos) //this function adds a simple point to the Spline
    {
        countPoints++;
        SplinePosition point = new SplinePosition(countPoints, pos);

        points.Add(point);
        return countPoints;
    }

    public SplinePosition GetSplinePoint(int index) { return points[index]; } //returns the point with the given index
    public void SetSplinePoint(vec3 newPos, int index) 
    { 
        points[index].position = newPos; 
    }
    public void DeleteSplinePoint(int index) 
    { 
        points.Remove(GetSplinePoint(index)); 
    }


    public CustomSplineSegment GetSegment(int index) //returns the segment with the given index
    {
        if (segments.Count > index)
        {
            return segments[index];
        }
        else return null;
    }

    public vec3 CalcSegmentPoint(int index, float t) //calculate the position of the t on the spline with the given index
    {
        return GetSegment(index).CalcPoint(t);
    }

    //All CreateSegment methods for the LinearBezierCurveSegment
    public int CreateLinearSegment(int start_point_index, int end_point_index) //Create a straight segment between the start_point_index and end_point_index, the segment uses the main speed. Returns the segment index ehen sucsessful otherwise -1
    {
        if (start_point_index > countPoints || end_point_index > countPoints)
        {
            Log.Error(" CreateSegment(int start_point_index, int end_point_index, float speed), Index out of range!");
            return -1;
        }
        else
        {
            countSegments++;
            LinearBezierCurveSegment seg = new LinearBezierCurveSegment(countSegments, GetSplinePoint(start_point_index), GetSplinePoint(end_point_index));
            if (countSegments >= 1)
            {
                seg.behindSegment = segments[countSegments - 1];
                seg.nextSegment = segments[0];

                segments[countSegments - 1].nextSegment = seg;
            }
            else
            {
                seg.behindSegment = seg;
                seg.nextSegment = seg;
            }
            segments.Add(seg);
            return segments.Count;
        }
    }

    public int CreateLinearSegment(int start_point_index, int end_point_index, float speed) //Create a straight segment between the start_point_index and end_point_index, the segment uses speed. Returns the segment index ehen sucsessful otherwise -1
    {
        if (start_point_index > countPoints || end_point_index > countPoints)
        {
            Log.Error(" CreateSegment(int start_point_index, int end_point_index, float speed), Index out of range!");
            return -1;
        }
        else
        {
            countSegments++;
            LinearBezierCurveSegment seg = new LinearBezierCurveSegment(countSegments, GetSplinePoint(start_point_index), GetSplinePoint(end_point_index), speed);
            if (countSegments >= 1)
            {
                seg.behindSegment = segments[countSegments - 1];
                seg.nextSegment = segments[0];

                segments[countSegments - 1].nextSegment = seg;
            }
            else
            {
                seg.behindSegment = seg;
                seg.nextSegment = seg;
            }
            segments.Add(seg);
            return segments.Count;
        }
    }

    public int CreateLinearSegment(int start_point_index, int end_point_index, float start_speed, float end_speed)//Create a straight segment between the start_point_index and end_point_index, uses the start_speed and exelarate/decrease to the end_speed. Returns the segment index ehen sucsessful otherwise -1
    {
        if (start_point_index > countPoints || end_point_index > countPoints)
        {
            Log.Error(" CreateSegment(int start_point_index, int end_point_index, float speed), Index out of range!");
            return -1;
        }
        else
        {
            countSegments++;
            LinearBezierCurveSegment seg = new LinearBezierCurveSegment(countSegments, GetSplinePoint(start_point_index), GetSplinePoint(end_point_index), start_speed, end_speed);
            if (countSegments >= 1)
            {
                seg.behindSegment = segments[countSegments - 1];
                seg.nextSegment = segments[0];

                segments[countSegments - 1].nextSegment = seg;
            }
            else
            {
                seg.behindSegment = seg;
                seg.nextSegment = seg;
            }
            segments.Add(seg);
            return segments.Count;
        }
    }

    //All CreateSegment methods for the QuadraticBezierCurveSegment
    public int CreateQuadraticSegment(int start_point_index, int end_point_index, vec3 curve_point) //Create a straight segment between the start_point_index and end_point_index, the segment uses the main speed. Returns the segment index ehen sucsessful otherwise -1
    {
        if (start_point_index > countPoints || end_point_index > countPoints)
        {
            Log.Error(" CreateSegment(int start_point_index, int end_point_index, float speed), Index out of range!");
            return -1;
        }
        else
        {
            countSegments++;
            CurvePoint curvep = new CurvePoint(curve_point);
            QuadraticBezierCurveSegment seg = new QuadraticBezierCurveSegment(countSegments, GetSplinePoint(start_point_index), GetSplinePoint(end_point_index), curvep);
            if (countSegments >= 1)
            {
                seg.behindSegment = segments[countSegments - 1];
                seg.nextSegment = segments[0];

                segments[countSegments - 1].nextSegment = seg;
            }
            else
            {
                seg.behindSegment = seg;
                seg.nextSegment = seg;
            }
            segments.Add(seg);
            return segments.Count;
        }
    }

    public int CreateQuadraticSegment(int start_point_index, int end_point_index, vec3 curve_point, float speed) //Create a straight segment between the start_point_index and end_point_index, the segment uses speed. Returns the segment index ehen sucsessful otherwise -1
    {
        if (start_point_index > countPoints || end_point_index > countPoints)
        {
            Log.Error(" CreateSegment(int start_point_index, int end_point_index, float speed), Index out of range!");
            return -1;
        }
        else
        {
            countSegments++;
            CurvePoint curvep = new CurvePoint(curve_point);
            QuadraticBezierCurveSegment seg = new QuadraticBezierCurveSegment(countSegments, GetSplinePoint(start_point_index), GetSplinePoint(end_point_index), curvep, speed);
            if (countSegments >= 1)
            {
                seg.behindSegment = segments[countSegments - 1];
                seg.nextSegment = segments[0];

                segments[countSegments - 1].nextSegment = seg;
            }
            else
            {
                seg.behindSegment = seg;
                seg.nextSegment = seg;
            }
            segments.Add(seg);
            return segments.Count;
        }
    }

    public int CreateQuadraticSegment(int start_point_index, int end_point_index, vec3 curve_point, float start_speed, float end_speed)//Create a straight segment between the start_point_index and end_point_index, uses the start_speed and exelarate/decrease to the end_speed. Returns the segment index ehen sucsessful otherwise -1
    {
        if (start_point_index > countPoints || end_point_index > countPoints)
        {
            Log.Error(" CreateSegment(int start_point_index, int end_point_index, float speed), Index out of range!");
            return -1;
        }
        else
        {
            countSegments++;
            CurvePoint curvep = new CurvePoint(curve_point);
            QuadraticBezierCurveSegment seg = new QuadraticBezierCurveSegment(countSegments, GetSplinePoint(start_point_index), GetSplinePoint(end_point_index), curvep, start_speed, end_speed);
            if (countSegments >= 1)
            {
                seg.behindSegment = segments[countSegments - 1];
                seg.nextSegment = segments[0];

                segments[countSegments - 1].nextSegment = seg;
            }
            else
            {
                seg.behindSegment = seg;
                seg.nextSegment = seg;
            }
            segments.Add(seg);
            return segments.Count;
        }
    }

    //All CreateSegment methods for the CubicSplineSegment
    public int CreateCubicSegment(int start_point_index, int end_point_index, vec3 curve_point_1, vec3 curve_point_2) //Create a straight segment between the start_point_index and end_point_index, the segment uses the main speed. Returns the segment index ehen sucsessful otherwise -1
    {
        if (start_point_index > countPoints || end_point_index > countPoints)
        {
            Log.Error(" CreateSegment(int start_point_index, int end_point_index, float speed), Index out of range!");
            return -1;
        }
        else
        {
            countSegments++;
            CurvePoint curvep1 = new CurvePoint(curve_point_1);
            CurvePoint curvep2 = new CurvePoint(curve_point_2);
            CubicBezierCurveSegment seg = new CubicBezierCurveSegment(countSegments, GetSplinePoint(start_point_index), GetSplinePoint(end_point_index), curvep1, curvep2);
            if (countSegments >= 1)
            {
                seg.behindSegment = segments[countSegments - 1];
                seg.nextSegment = segments[0];

                segments[countSegments - 1].nextSegment = seg;
            }
            else
            {
                seg.behindSegment = seg;
                seg.nextSegment = seg;
            }
            segments.Add(seg);
            return segments.Count;
        }
    }

    public int CreateCubicSegment(int start_point_index, int end_point_index, vec3 curve_point_1, vec3 curve_point_2, float speed) //Create a straight segment between the start_point_index and end_point_index, the segment uses speed. Returns the segment index ehen sucsessful otherwise -1
    {
        if (start_point_index > countPoints || end_point_index > countPoints)
        {
            Log.Error(" CreateSegment(int start_point_index, int end_point_index, float speed), Index out of range!");
            return -1;
        }
        else
        {
            countSegments++;
            CurvePoint curvep1 = new CurvePoint(curve_point_1);
            CurvePoint curvep2 = new CurvePoint(curve_point_2);
            CubicBezierCurveSegment seg = new CubicBezierCurveSegment(countSegments, GetSplinePoint(start_point_index), GetSplinePoint(end_point_index), curvep1, curvep2, speed);
            if (countSegments >= 1)
            {
                seg.behindSegment = segments[countSegments - 1];
                seg.nextSegment = segments[0];

                segments[countSegments - 1].nextSegment = seg;
            }
            else
            {
                seg.behindSegment = seg;
                seg.nextSegment = seg;
            }
            segments.Add(seg);
            return segments.Count;
        }
    }

    public int CreateCubicSegment(int start_point_index, int end_point_index, vec3 curve_point_1, vec3 curve_point_2, float start_speed, float end_speed)//Create a straight segment between the start_point_index and end_point_index, uses the start_speed and exelarate/decrease to the end_speed. Returns the segment index ehen sucsessful otherwise -1
    {
        if (start_point_index > countPoints || end_point_index > countPoints)
        {
            Log.Error(" CreateSegment(int start_point_index, int end_point_index, float speed), Index out of range!");
            return -1;
        }
        else
        {
            countSegments++;
            CurvePoint curvep1 = new CurvePoint(curve_point_1);
            CurvePoint curvep2 = new CurvePoint(curve_point_2);
            CubicBezierCurveSegment seg = new CubicBezierCurveSegment(countSegments, GetSplinePoint(start_point_index), GetSplinePoint(end_point_index), curvep1,curvep2, start_speed, end_speed);
            if (countSegments >= 1)
            {
                seg.behindSegment = segments[countSegments - 1];
                seg.nextSegment = segments[0];

                segments[countSegments - 1].nextSegment = seg;
            }
            else
            {
                seg.behindSegment = seg;
                seg.nextSegment = seg;
            }
            segments.Add(seg);
            return segments.Count;
        }
    }

    //Add a linear Segment
    public void AddLinearSegment(vec3 newPoint)//Add a linear segment at the end of the spline, it uses the main speed, CustomSplineSegment nextSegment)
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            CreateLinearSegment(actPoint - 1, actPoint);
        }
    } 

    public void AddLinearSegment(vec3 newPoint, float speed)//Add a linear segment at the end of the spline, it uses the speed as movment speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            CreateLinearSegment(actPoint - 1, actPoint, speed);
        }
    } 

    public void AddLinearSegment(vec3 newPoint, float start_speed, float end_speed)//Add a linear segment at the end of the spline, it uses start_speed and exelerate/decrease to the end_speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            CreateLinearSegment(actPoint - 1, actPoint, start_speed, end_speed);
        }
    } 

    //Add a quadratic Segment
    public void AddQuadraticSegment(vec3 newPoint, vec3 curve_point)//Add a quadratic segment at the end of the spline, it uses the main speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            CreateQuadraticSegment(actPoint - 1, actPoint, curve_point);
        }
    }
    public void AddQuadraticSegment(vec3 newPoint, float curve_size, float offset = 0.5f)//Add a quadratic segment at the end of the spline, it uses the main speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            vec3 curve_point = new vec3((GetSplinePoint(actPoint - 1).position * (1f - offset) + newPoint * offset));
            curve_point.z -= curve_size;

            CreateQuadraticSegment(actPoint - 1, actPoint, curve_point);
        }
    }

    public void AddQuadraticSegment(vec3 newPoint, vec3 curve_point, float speed)//Add a quadratic segment at the end of the spline, it uses the speed as movment speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            CreateQuadraticSegment(actPoint - 1, actPoint, curve_point, speed);
        }
    }
    public void AddQuadraticSegment(vec3 newPoint, float speed ,float curve_size ,float offset = 0.5f)//Add a quadratic segment at the end of the spline, it uses the speed as movment speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            vec3 curve_point = new vec3((GetSplinePoint(actPoint - 1).position * (1f - offset) + newPoint * offset));
            curve_point.z -= curve_size;

            CreateQuadraticSegment(actPoint - 1, actPoint, curve_point, speed);
        }
    }

    public void AddQuadraticSegment(vec3 newPoint, vec3 curve_point, float start_speed, float end_speed)//Add a quadratic segment at the end of the spline, it uses start_speed and exelerate/decrease to the end_speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            CreateQuadraticSegment(actPoint - 1, actPoint, curve_point, start_speed, end_speed);
        }
    }
    public void AddQuadraticSegment(vec3 newPoint, float start_speed, float end_speed, float curve_size, float offset = 0.5f)//Add a quadratic segment at the end of the spline, it uses start_speed and exelerate/decrease to the end_speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            vec3 curve_point = new vec3((GetSplinePoint(actPoint - 1).position * (1f - offset) + newPoint * offset));
            curve_point.z -= curve_size;

            CreateQuadraticSegment(actPoint - 1, actPoint, curve_point, start_speed, end_speed);
        }
    }

    //Add a cubic Segment
    public void AddCubicSegment(vec3 newPoint, vec3 curve_point_1, vec3 curve_point_2)//Add a cubic segment at the end of the spline, it uses the main speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            CreateCubicSegment(actPoint - 1, actPoint, curve_point_1, curve_point_2);
        }
    }
    public void AddCubicSegment(vec3 newPoint, float curve_size_1, float curve_size_2, float offset_1 = 0.3f, float offset_2 = 0.9f)//Add a cubic segment at the end of the spline, it uses the main speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            vec3 curve_point_1 = new vec3((GetSplinePoint(actPoint - 1).position * (1f - offset_1) + newPoint * offset_1));
            curve_point_1.z -= curve_size_1;
            vec3 curve_point_2 = new vec3((GetSplinePoint(actPoint - 1).position * (1f - offset_2) + newPoint * offset_2));
            curve_point_2.z -= curve_size_2;

            CreateCubicSegment(actPoint - 1, actPoint, curve_point_1, curve_point_2);
        }
    }

    public void AddCubicSegment(vec3 newPoint, vec3 curve_point_1, vec3 curve_point_2, float speed)//Add a cubic segment at the end of the spline, it uses the speed as movment speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            CreateCubicSegment(actPoint - 1, actPoint, curve_point_1, curve_point_2, speed);
        }
    }
    public void AddCubicSegment(vec3 newPoint, float speed, float curve_size_1, float curve_size_2, float offset_1 = 0.3f, float offset_2 = 0.9f)//Add a cubic segment at the end of the spline, it uses the speed as movment speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            vec3 curve_point_1 = new vec3((GetSplinePoint(actPoint - 1).position * (1f - offset_1) + newPoint * offset_1));
            curve_point_1.z -= curve_size_1;
            vec3 curve_point_2 = new vec3((GetSplinePoint(actPoint - 1).position * (1f - offset_2) + newPoint * offset_2));
            curve_point_2.z -= curve_size_2;

            CreateCubicSegment(actPoint - 1, actPoint, curve_point_1, curve_point_2, speed);
        }
    }

    public void AddCubicSegment(vec3 newPoint, vec3 curve_point_1, vec3 curve_point_2, float start_speed, float end_speed)//Add a cubic segment at the end of the spline, it uses start_speed and exelerate/decrease to the end_speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            CreateCubicSegment(actPoint - 1, actPoint, curve_point_1, curve_point_2, start_speed, end_speed);
        }
    }
    public void AddCubicSegment(vec3 newPoint, float start_speed, float end_speed, float curve_size_1, float curve_size_2, float offset_1 = 0.3f, float offset_2 = 0.9f)//Add a cubic segment at the end of the spline, it uses start_speed and exelerate/decrease to the end_speed
    {
        int actPoint = CreateSplinePoint(newPoint);
        if (actPoint > 0)
        {
            vec3 curve_point_1 = new vec3((GetSplinePoint(actPoint - 1).position * (1f - offset_1) + newPoint * offset_1));
            curve_point_1.z -= curve_size_1;
            vec3 curve_point_2 = new vec3((GetSplinePoint(actPoint - 1).position * (1f - offset_2) + newPoint * offset_2));
            curve_point_2.z -= curve_size_2;

            CreateCubicSegment(actPoint - 1, actPoint, curve_point_1, curve_point_2, start_speed, end_speed);
        }
    }

    public void RemoveSegment(int index)
    {
        
    }

    public void Save()
    {

    }
    public void Load()
    {

    }

    public void Clear() //Clears the spline
    {
        points.Clear();
        segments.Clear();

        countPoints = -1;
        countSegments = -1;
    }

    public void Show() //renders the spline
    {
        if (!Visualizer.Enabled) Visualizer.Enabled = true;

        Visualizer.RenderVector(vec3.BACK, vec3.BACK * 2, new vec4(1.0f, 0.0f, 0.0f, 1.0f));

        for (int i = 0; i < PointCount; i++)
        {
            Visualizer.RenderPoint3D(GetSplinePoint(i).position, 0.1f, new vec4(1.0f, 1.0f, 1.0f, 1.0f));
        }

        vec4 color = new vec4(1.0f, 0.0f, 0.0f, 1.0f);
        vec4 color1 = new vec4(0.0f, 0.0f, 1.0f, 1.0f);

        float renderSegmentLength = 0.05f;
        foreach (CustomSplineSegment seg in segments)
        {
            for (float j = 0; j < 1; j += renderSegmentLength)
            {
                vec3 p0 = seg.CalcPoint(j);
                vec3 p1 = seg.CalcPoint(j + renderSegmentLength);
                Visualizer.RenderLine3D(p0, p1, color);
            }
        }
    }
}

public abstract class CustomSplineSegment
{
    protected int _index;
    public int index => _index;
    protected float _length;
    public float length => _length;

    protected SplinePosition start_point;
    protected SplinePosition end_point;

    public SplinePosition StartPoint
    {
        get { return start_point; }
        set { start_point = value; CalcLength(); }
    }
    public SplinePosition EndPoint
    {
        get { return end_point; }
        set { end_point = value; CalcLength(); }
    }

    protected Spline _parentSpline;
    public Spline parentSpline => _parentSpline;

    public CustomSplineSegment nextSegment;
    public CustomSplineSegment behindSegment;

    protected bool _useMainSpeed = true;
    public bool useMainSpeed => _useMainSpeed;
    public float startSpeed = 1;
    public float endSpeed = 1;

    public abstract vec3 CalcPoint(float t); //Returns the point t as a vector the spline starts at 0 and goes to 1

    protected abstract float CalcLength();

    public quat CalcRot(float t)
    {
        float interval = 0.01f;
        vec3 p1 = new vec3(CalcPoint(t));
        if (t - interval < 0)
        {
            vec3 p0 = new vec3(CalcPoint(t + interval));
            return MathLib.LookAt(p1, p0, vec3.BACK).GetRotate();
        }
        else
        {
            vec3 p0 = new vec3(CalcPoint(t - interval));
            return MathLib.LookAt(p1, p0, vec3.BACK).GetRotate();
        }

    }
}
public class LinearBezierCurveSegment : CustomSplineSegment
{
    public LinearBezierCurveSegment() 
    {
        _index = -1;
        start_point = new SplinePosition(-1, vec3.ZERO);
        end_point = new SplinePosition(-1, vec3.ZERO);
        _useMainSpeed = true;
        startSpeed = 1;
        endSpeed = 1;

        CalcLength();
    }

    public LinearBezierCurveSegment(int index, SplinePosition startPoint, SplinePosition endPoint)
    {
        _index = index;
        start_point = startPoint;
        end_point = endPoint;
        _useMainSpeed = true;
        startSpeed = 1;
        endSpeed = 1;

        CalcLength();
    }

    public LinearBezierCurveSegment(int index, SplinePosition startPoint, SplinePosition endPoint, float speed)
    {
        _index = index;
        start_point = startPoint;
        end_point = endPoint;
        _useMainSpeed = false;
        startSpeed = speed;
        endSpeed = speed;

        CalcLength();
    }

    public LinearBezierCurveSegment(int index, SplinePosition startPoint, SplinePosition endPoint, float start_speed, float end_speed)
    {
        _index = index;
        start_point = startPoint;
        end_point = endPoint;
        _useMainSpeed = false;
        startSpeed = start_speed;
        endSpeed = end_speed;

        CalcLength();
    }

    public override vec3 CalcPoint(float t)
    {
       return new vec3(start_point.position * (1f - t) + end_point.position * t);
    }

    protected override float CalcLength()
    {
        float length = (float)Math.Sqrt(Math.Pow(end_point.position.x - start_point.position.x, 2) + Math.Pow(end_point.position.y - start_point.position.y, 2) + Math.Pow(end_point.position.z - start_point.position.z, 2));
        _length = length;
        return length;
    }
}

public class QuadraticBezierCurveSegment : CustomSplineSegment
{
    public QuadraticBezierCurveSegment() 
    {
        _index = index;
        start_point = new SplinePosition(-1,vec3.ZERO);
        end_point = new SplinePosition(-1,vec3.ZERO);
        curve_point = new CurvePoint(vec3.ZERO);
        _useMainSpeed = true;
        startSpeed = 1;
        endSpeed = 1;

        CalcLength();
    }
    public QuadraticBezierCurveSegment(int index, SplinePosition startPoint, SplinePosition endPoint, CurvePoint curvePoint)
    { 
        _index = index;
        start_point = startPoint;
        end_point = endPoint;
        curve_point = curvePoint;
        _useMainSpeed = true;
        startSpeed = 1;
        endSpeed = 1;

        CalcLength();
    }

    public QuadraticBezierCurveSegment(int index, SplinePosition startPoint, SplinePosition endPoint, CurvePoint curvePoint, float speed)
    {
        _index = index;
        start_point = startPoint;
        end_point = endPoint;
        curve_point = curvePoint;
        _useMainSpeed = false;
        startSpeed = speed;
        endSpeed = speed;

        CalcLength();
    }

    public QuadraticBezierCurveSegment(int index, SplinePosition startPoint, SplinePosition endPoint, CurvePoint curvePoint, float start_speed, float end_speed)
    {
        _index = index;
        start_point = startPoint;
        end_point = endPoint;
        curve_point = curvePoint;
        _useMainSpeed = false;
        startSpeed = start_speed;
        endSpeed = end_speed;

        CalcLength();
    }

    CurvePoint curve_point;

    public override vec3 CalcPoint(float t)
    {
        float b = (1f - t);
        return new vec3(start_point.position * MathLib.Pow(b,2) + 
                        curve_point.position * t * b * 2 + 
                        end_point.position * (t * t));
    }

    protected override float CalcLength()
    {
        float precision = 0.05f;
        float dt = precision / (end_point.position - start_point.position).Length, length = 0.0f;
        for (float t = dt; t < 1.0; t += dt)
            length += (CalcPoint(t - dt) - CalcPoint(t)).Length;
        _length = length;
        return length;
    }
}

public class CubicBezierCurveSegment : CustomSplineSegment
{
    public CubicBezierCurveSegment() 
    {
        _index = index;
        start_point = new SplinePosition(-1, vec3.ZERO);
        end_point = new SplinePosition(-1, vec3.ZERO);
        curve_point_1 = new CurvePoint(vec3.ZERO);
        curve_point_2 = new CurvePoint(vec3.ZERO);
        _useMainSpeed = true;
        startSpeed = 1;
        endSpeed = 1;

        CalcLength();
    }
    public CubicBezierCurveSegment(int index, SplinePosition startPoint, SplinePosition endPoint, CurvePoint curvePoint_1, CurvePoint curvePoint_2)
    {
        _index = index;
        start_point = startPoint;
        end_point = endPoint;
        curve_point_1 = curvePoint_1;
        curve_point_2 = curvePoint_2;
        _useMainSpeed = true;
        startSpeed = 1;
        endSpeed = 1;

        CalcLength();
    }

    public CubicBezierCurveSegment(int index, SplinePosition startPoint, SplinePosition endPoint, CurvePoint curvePoint_1, CurvePoint curvePoint_2, float speed)
    {
        _index = index;
        start_point = startPoint;
        end_point = endPoint;
        curve_point_1 = curvePoint_1;
        curve_point_2 = curvePoint_2;
        _useMainSpeed = false;
        startSpeed = speed;
        endSpeed = speed;

        CalcLength();
    }

    public CubicBezierCurveSegment(int index, SplinePosition startPoint, SplinePosition endPoint, CurvePoint curvePoint_1, CurvePoint curvePoint_2, float start_speed, float end_speed)
    {
        _index = index;
        start_point = startPoint;
        end_point = endPoint;
        curve_point_1 = curvePoint_1;
        curve_point_2 = curvePoint_2;
        _useMainSpeed = false;
        startSpeed = start_speed;
        endSpeed = end_speed;

        CalcLength();
    }

    CurvePoint curve_point_1;
    CurvePoint curve_point_2;

    public override vec3 CalcPoint(float t)
    {
        float b = (1f - t);
        return new vec3(start_point.position * MathLib.Pow(b, 3) +
                        curve_point_1.position * t * 3 * (MathLib.Pow(b, 2)) +
                        curve_point_2.position * t * 3 * (MathLib.Pow(b, 2)) +
                        end_point.position * MathLib.Pow(t, 3));
    }

    protected override float CalcLength()
    {
        float precision = 0.05f;
        float dt = precision / (end_point.position - start_point.position).Length, length = 0.0f;
        for (float t = dt; t < 1.0; t += dt)
            length += (CalcPoint(t - dt) - CalcPoint(t)).Length;
        _length = length;
        return length;
    }
}



public class SplinePosition
{
    private int _index;
    public int index => _index;

    private List<CustomSplineSegment> usedSegments = new List<CustomSplineSegment>();

    public CustomSplineSegment[] GetAttachedSegments() { return usedSegments.ToArray(); }

    public SplinePosition() 
    { 
        _position = new vec3(vec3.ZERO);
        _index = -1;
    }
    public SplinePosition(int index, vec3 pos) 
    { 
        _position = new vec3(pos);
        _index = index;
    }

    private vec3 _position;
    public vec3 position
    {
        get { return _position; }
        set 
        { 
            _position = value; 
        }
    }
    public void Show()
    {
        if (!Visualizer.Enabled) Visualizer.Enabled = true;

        Visualizer.RenderPoint3D(_position, 0.1f, new vec4(1.0f, 1.0f, 1.0f, 1.0f));
    }
}

public class CurvePoint
{
    public CurvePoint() { position = new vec3(vec3.ZERO); }
    public CurvePoint(vec3 pos) { position = pos; }

    public vec3 position;

    public void Show()
    {
        if (!Visualizer.Enabled) Visualizer.Enabled = true;

        Visualizer.RenderPoint3D(position, 0.1f, new vec4(1.0f, 1.0f, 1.0f, 1.0f));
    }
}

public class SplineTrigger
{

}