using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Combinator)]
public class AnimateSphereTest
{
    public IObservable<Tuple<Tuple<float, float, float>, double>> Process(IObservable<Tuple<Tuple<float, float, float>, double>> source)
    {
        return source;
    }
}
