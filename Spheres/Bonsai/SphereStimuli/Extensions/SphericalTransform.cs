using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class SphericalTransform
{
    public IObservable<Tuple<Tuple<double,double,double>,Tuple<double,double,double>,Tuple<double,double>>> Process(IObservable<Tuple<Tuple<double, double, double, double>, double>> source)
    {
        return source.Select(value => 
        {
            double Azimuth;
            double Elevation;
            double Radius;
            double X;
            double Y; 
            double Z0;  //Z after spherical transform, without adding MouseZ
            double Size;
            double Z;   //Z after adding MouseZ


            //Spherical transform of XYZ
            Azimuth = value.Item1.Item1;
            Elevation = value.Item1.Item2;
            Radius = value.Item1.Item3;
            
            X = Radius * Math.Cos(Azimuth) * Math.Cos(Elevation);
            Y = Radius * Math.Sin(Elevation);
            Z0 = Math.Abs(Radius*Math.Sin(Azimuth)*Math.Cos(Elevation));


            //Scale size of sphere according to radius
            double SizeK = value.Item1.Item4;
            Size = SizeK *Radius;


            //Add Current MouseZ
            double MouseZSamp = value.Item2;
            Z = Z0 + MouseZSamp;


            //Return result
            Tuple<Tuple<double,double,double>,Tuple<double,double,double>,Tuple<double,double>> result = 
                        new Tuple<Tuple<double,double,double>,Tuple<double,double,double>,Tuple<double,double>>(new Tuple<double,double,double>(Azimuth,Elevation,Radius),new Tuple<double,double,double>(X,Y,Z),new Tuple<double,double>(Z0,Size));

            return result;
        });
    }
}
