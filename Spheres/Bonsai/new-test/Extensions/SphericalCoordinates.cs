using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Bonsai.Reactive;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class SphericalCoordinates
{
    public IObservable<Tuple<int,Tuple<float,float,float>,Tuple<float,float,float,float,float>, Tuple<float,float>>> Process(IObservable<Tuple<Tuple<ElementIndex<Tuple<double, double, float>>, float>, float>> source)
    {
        return source.Select(value => 
        {
            int SphereID = value.Item1.Item1.Index;
            float Azimuth = Convert.ToSingle(value.Item1.Item1.Value.Item1);
            float Elevation = Convert.ToSingle(value.Item1.Item1.Value.Item2);
            float Depth = value.Item1.Item1.Value.Item3;
            float OriginalSize = value.Item1.Item2;
            float MouseZ = value.Item2;


            // Rescale size of sphere
            float Size;
            Size = OriginalSize * Depth;


            // Spherical transformation from angles to XYZ coordinates
            float X;
            float Y;
            float Z0;
            X = Convert.ToSingle(Depth * Math.Cos(Elevation) * Math.Cos(Azimuth));
            Y = Convert.ToSingle(Depth * Math.Sin(Elevation));
            Z0 = Convert.ToSingle(Depth * Math.Cos(Elevation) * Math.Sin(Azimuth));

            // Add MouseZ
            float Z;
            Z = Z0 + MouseZ; 


            //Return result: <ID>, <Azi, Ele, Depth>, <X,Y,Z0,Z, MouseZ>, <Original Size, Size>
            Tuple<int,Tuple<float,float,float>,Tuple<float,float,float,float,float>, Tuple<float,float>> result = 
                        new Tuple<int,Tuple<float,float,float>,Tuple<float,float,float,float,float>, Tuple<float,float>>(SphereID,new Tuple<float,float,float>(Azimuth,Elevation,Depth),new Tuple<float,float,float,float,float>(X,Y,Z0,Z,MouseZ),new Tuple<float,float>(OriginalSize, Size));

            return result;

            

        });
    }
}
