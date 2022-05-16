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
public class TubeMapping
{
    public IObservable<Tuple<int,float,Tuple<float,float,float>,Tuple<float,float,float,float,float>, Tuple<float,float>>> Process(IObservable<Tuple<Tuple<ElementIndex<Tuple<Tuple<double, double, double>,double>>, float>, double>> source)
        {
            return source.Select(value => 
            {
                int SphereID = value.Item1.Item1.Index;
                float SphereStartTime = Convert.ToSingle(value.Item1.Item1.Value.Item2);
                float Theta = Convert.ToSingle(value.Item1.Item1.Value.Item1.Item1);
                float Z0 = Convert.ToSingle(value.Item1.Item1.Value.Item1.Item2);
                float Radius = Convert.ToSingle(value.Item1.Item1.Value.Item1.Item3);
                float OriginalSize = value.Item1.Item2;
                float MouseZ = Convert.ToSingle(value.Item2);


                // Rescale size of sphere
                float Size;
                Size = OriginalSize * Radius;


                // Spherical transformation from angles to XYZ coordinates
                float X;
                float Y;
                X = Convert.ToSingle(Radius * Math.Cos(Theta));
                Y = Convert.ToSingle(Radius * Math.Sin(Theta));

                // Add MouseZ
                float Z;
                Z = Z0 + MouseZ; 


                //Return result: <ID>, <Azi, Ele, Depth>, <X,Y,Z0,Z, MouseZ>, <Original Size, Size>
                Tuple<int, float, Tuple<float,float,float>,Tuple<float,float,float,float,float>, Tuple<float,float>> result = 
                            new Tuple<int, float, Tuple<float,float,float>,Tuple<float,float,float,float,float>, Tuple<float,float>>(SphereID, SphereStartTime, new Tuple<float,float,float>(Theta,Z0,Radius),new Tuple<float,float,float,float,float>(X,Y,Z0,Z,MouseZ),new Tuple<float,float>(OriginalSize, Size));

                return result;

                

            });
        }
}
