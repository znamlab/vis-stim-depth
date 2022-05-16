using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class Double2Single
{
    public IObservable<float> Process(IObservable<double> source)
    {
        return source.Select(value => 
        {
            return Convert.ToSingle(value);
        });
    }
}
