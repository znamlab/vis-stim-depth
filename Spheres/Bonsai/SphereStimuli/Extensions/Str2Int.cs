using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class Str2Int
{
    public IObservable<int> Process(IObservable<string> source)
    {
        return source.Select(value => 
        {   
            int result = Int32.Parse(value);
            return result;
        });
    }
}
