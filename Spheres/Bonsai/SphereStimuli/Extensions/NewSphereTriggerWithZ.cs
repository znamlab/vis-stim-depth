using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class NewSphereTriggerWithZ
{
    public IObservable<bool> Process(IObservable<Tuple<IList<double>, float, float>> source)
    {
        return source.Select(value => 
        {
            float PreviousZ = Convert.ToSingle(value.Item1[0]);
            float NowZ = Convert.ToSingle(value.Item1[1]);
            float ZoSpacing = value.Item2;
            float thr = value.Item3;

            float CrossZ = Convert.ToSingle(Math.Floor(NowZ/ZoSpacing) * ZoSpacing + thr);

            
            return ((Math.Abs(NowZ)>=(ZoSpacing-thr)) & (NowZ>=CrossZ) & (PreviousZ<CrossZ));

        });
    }
}
