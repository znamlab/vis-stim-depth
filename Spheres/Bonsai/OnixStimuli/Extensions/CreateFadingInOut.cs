using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class CreateFadingInOut
{
    public IObservable<float> Process(IObservable<Tuple<double, float, float, float, double, double>> source)
    {
        return source.Select(value => {
            float start_amb = value.Item4;
            float target_amb = value.Item3;
            float elapsedTime = Convert.ToSingle(value.Item1);
            float fadeinTime = value.Item2;
            float ambient = start_amb;
            float trialElapsedTime = Convert.ToSingle(value.Item5);
            float trialStopTime = Convert.ToSingle(value.Item6);

            float k1 = (target_amb-start_amb)/fadeinTime;

            if ((trialElapsedTime < trialStopTime) || trialStopTime < 0)
            {
                if (elapsedTime <= fadeinTime)
                {
                    ambient = start_amb + elapsedTime*k1;
                }

                else
                {
                    ambient = target_amb;
                }
            }

            else
            {
                if (trialStopTime > 0)
                {
                    ambient = Math.Min(target_amb - (trialElapsedTime-trialStopTime)*k1, start_amb);

                }
            }


            // Returns
            return ambient;


        });
    }
}
