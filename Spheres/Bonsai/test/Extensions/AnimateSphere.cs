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
