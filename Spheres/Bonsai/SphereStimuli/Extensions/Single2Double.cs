using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class Single2Double
{
    public IObservable<double> Process(IObservable<float> source)
    {
        return source.Select(value => 
        {
            return Convert.ToDouble(value);
        });
    }
}
