# Unigine-Spline

This is a spline and a spline drive for the Unigine Engine.

*Its in development so not all is working! I try my best to fix some errors*!
## Docs

The Spline.cs contains the main spline with all classes needed for the spline. The SplineDrive.cs is for creating a object which moves along the spline.
The Spline is made to move a object/node along it. It is easy to use and have some nice extras.


### Spline

The Spline is a basic spline. It contains SplinePositions and SplineSegments.

* Create a Spline:
```
Spline spline = new Spline("Name");
```
#### SplinePositions

* Add a SplinePosition:
```
int CreateSplinePoint(vec3 pos);
```
returns the index of the SplinePosition.

* Set a SplinePosition:
```
void SetSplinePoint(vec3 newPos, int index)
```
The newPos is the new Position of the SplinePosition, the index the SplinePoint index in the Spline.

* Delete a SplinePosition
```
void DeleteSplinePoint(int index)
```
Delete the SplinePosition with the given index

#### SplineSegments
The Segments are the special thing of this Spline. 
You can choose between 3 different Segment types (Have a look at SplineSegment-System).
There are two ways to create a segment.
* Create a segment:
You can create a segment between two SplinePositions. Here use the method:
** CreateLinearSegment()
** CreateQuadraticSegment()
** CreateCubicSegment()

Each segment needs its paramters like the start/end point and curve points, but every type have three different functions to create.
This is because of the speed of the segment, its used of the SplineDrive.
The first its just the segments paramters, this segments uses the main speed of the Spline.
The second has a paramter called "speed" so the speed is custom and constant.
The third has two paramters "start_speed" "end_speed" this segment has non constant speed and exelarate/decrease the SplineDrive

* Add a segment:
You also can add a new Segment at the end of the Spline here you can use the methods:
    * AddQuadraticSegment(...)
    * AddQuadraticSegment(...)
    * AddCubicSegment(...)

It creates a new SplinePosition and then Creates a Segment between the last SplinePosition and the new one.
The speed paramters are the same as explained in Create.

The special thing about the Add methods for the curved segments (QuadraticSplineSegment, CubicSplineSegment) is that you don't have declare vec3 for the
curve point. You can just use the Add methods with the offset and curve_size. So the curve point gets calculated as follow:
Get the vec3 at offset (aka. t) at the segment. Substract the curve_size from the z of the calculated point

* Delete a SplineSegment
```
void DeleteSegment(int index);
```
Delete the segment with the given index.

Visulize the Spline:
```
void Show();
```

### SplineSegment-System

Its based on Beziers curves.
The SplineSegment-System is the special thing about this spline. You can choose between 3 different Segments:

#### LinearSplineSegment
Straight segment between two points.

![](https://github.com/Raining-Cloud/Unigine-Spline/blob/master/ReadmeImages/straight.png)

#### QuadraticSplineSegment
Curved segment between 3 points

![](https://github.com/Raining-Cloud/Unigine-Spline/blob/master/ReadmeImages/quadratic.png)

#### CubicSplineSegment
Curved segment between 4 points

![](https://github.com/Raining-Cloud/Unigine-Spline/blob/master/ReadmeImages/cubic.png)

### SplinePosition
The SplinePosition is mainly used for the start or end point for a SplineSegment, the position is .
It knows his index in the Spline.

Visulize the SplinePosition:
```
void Show();
```

### CurvePoint
Nearly the same as the SplinePosition, for now its used for the AddQuadraticSegment and the CubicSplineSegment.

Visulize the SplinePosition:
```
void Show();
```
##Using the SplineDrive
For the use of the SplineDrive I recommend to create a new C# Component.
Create a new class and inheritance from the SplineDrive. In the 2 methods that you need to override
you have to assign the position of the Object which you want to move.
For example:
```
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unigine;

[Component(PropertyGuid = "2325bbf869764ef771065feae1d30e41302d92ac")]
public class RWCarriers : Component
{
    [Parameter(Group ="Drive", Title ="SplineDrive Node", Tooltip ="Insert your node to follow the spline here")]
    public Node driveNode;
    
    Drive splineDrive;
    
    private void Init()
    {
         splineDrive = new SplineDrive();
         splineDrive.drive = driveNode;
    }

    private void Update()
    {
        splineDrive.Move(Game.iFps);
    }

    private class Drive : SplineDrive
    {
        
        public Node drive;
        
        protected override void SetDrivePosition(vec3 pos)
        {
            drive.WorldPosition = pos;
        }

        protected override void SetDriveRotation(vec3 target)
        {
            drive.WorldLookAt(target);
        }
    }
}
```

## Author

* **Paul Masan aka. Paulchen/Raining-Cloud**  [Raining-Cloud](https://github.com/Raining-Cloud)

## License

Feel free to use and edit, i would appriciate it if you mention me if you use it.
