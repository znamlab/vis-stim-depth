using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class Double2Int
{
    public IObservable<int> Process(IObservable<double> source)
    {
        return source.Select(value => 
        {   
            int result = Convert.ToInt32(value);
            return result;
        });
    }
}
