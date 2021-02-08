using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenTK;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Combinator)]

/*
public class AnimateSphere
{
    public IObservable<Vector4> Process(IObservable<Tuple<float, float, float>> source, IObservable<float> wheel)
    {
        return source.SelectMany(parameters =>
        {
            var radius = parameters.Item3;  //translation Z-radius
            
            return wheel.Select(value =>
            {
                //calculate spherical coordiantes for XYZ
                
                //this_x = this_r * cos(this_theta) * sin(this_phi);
                //this_y = this_r * sin(this_theta) * sin(this_phi);  
                //this_z = this_r * cos(this_phi); 
                
                var azi = parameters.Item1;  //deg
                var lat = parameters.Item2;  //deg
                var theta = azi * Math.PI/180;  //rad
                var phi = lat*Math.PI/180;  //rad
                var x = radius * Math.Cos(theta) * Math.Sin(phi);
                var y = radius * Math.Sin(theta) * Math.Sin(phi); 
                var addZ = radius * Math.Cos(phi);
                var z = addZ;  //translation Z final result
                
                // Change Z according to mouse position
                if (z - value < -1) z = value + addZ;
                else if (z - value > 10) z = value - addZ;
                


                Vector4 result;
                result.X = Convert.ToSingle(x);
                result.Y = Convert.ToSingle(y);
                result.Z = Convert.ToSingle(z); // wheel 
                result.W = 1; // ambient color
                return result;
            });
        });
    }
}

*/


public class AnimateSphere
{
    public IObservable<Vector4> Process(IObservable<Tuple<float, float, float>> source, IObservable<float> wheel)
    {
        return source.SelectMany(parameters =>
        {
            var startZ = parameters.Item3; 
            var z = startZ;
            return wheel.Select(value =>
            {
                if (z - value < -1) z = value + startZ;
                else if (z - value > 10) z = value - startZ;

                Vector4 result;
                result.X = parameters.Item1;
                result.Y = parameters.Item2;
                result.Z = z; // wheel 
                result.W = 1; // ambient color
                return result;
            });
        });
    }
}




/*

public class AnimateSphere
{
    public IObservable<Vector4> Process(IObservable<Tuple<float, float, float>> source, IObservable<float> wheel)
    {
        return source.SelectMany(parameters =>
        {
            var radius = parameters.Item3;  //translation Z-radius

            return wheel.Select(value =>
            {                
                //calculate spherical coordiantes for XYZ
                
                //this_x = this_r * cos(this_theta) * sin(this_phi);
                //this_y = this_r * sin(this_theta) * sin(this_phi);  --> this is z in bonsai 
                //this_z = this_r * cos(this_phi); --> this is y in bonsai
                
                azi = parameters.Item1;
                lat = parameters.Item2;
                theta = azi * Math.PI/180;  //rad
                phi = (90 - lat)*Math.PI/180;  //rad
                x = radius * Math.Cos(theta) * Math.Sin(phi);
                y = radius * Math.Cos(phi); 
                addZ = radius * Math.Sin(theta) * Math.Sin(phi);
                var z = addZ;  //translation Z final result
                
                // Change Z according to mouse position
                if (z - value < -1) z = value + addZ;
                else if (z - value > 10) z = value - addZ;


                Vector4 result;
                result.X = x;
                result.Y = y;
                result.Z = z; // wheel 
                result.W = 1; // ambient color
                return result;
            });
        });
    }
}
*/