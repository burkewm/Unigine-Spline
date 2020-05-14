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

#### QuadraticSplineSegment
Curved segment between 3 points

#### CubicSplineSegment
Curved segment between 4 points

### SplinePosition
The SplinePosition is mainly used for the start or end point for a SplineSegment.
It knows his index in the Spline.

Visulize the SplinePosition:
```
void Show();
```

### CurvePoint
Contains a vec3 for now its used for the AddQuadraticSegment and the CubicSplineSegment.

Visulize the SplinePosition:
```
void Show();
```

## Author

* **Paul Masan aka. Paulchen/Raining-Cloud**  [PurpleBooth](https://github.com/Raining-Cloud)

## License

Feel free to edit, but i would appriciate it if you mention me (via github url).


