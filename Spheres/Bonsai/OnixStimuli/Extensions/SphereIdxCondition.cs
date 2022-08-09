using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class SphereIdxCondition
{
    public IObservable<bool> Process(IObservable<Tuple<Tuple<int, int>, float>> source)
    {
        return source.Select(value => 
        {
            int SphereID1 = value.Item1.Item1;
            int SphereID2 = value.Item1.Item2;
            float Depth = value.Item2;

            if (Depth==-9999)
            {
                return true;
            }
            else
            {
                return (SphereID1==SphereID2);
            }



        });
    }
}
