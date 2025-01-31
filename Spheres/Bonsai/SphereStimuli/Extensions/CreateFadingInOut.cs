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
    public IObservable<float> Process(IObservable<Tuple<double, float, float, float, double, double, Tuple<float, float, float>>> source)


    {
        return source.Select(value => {
            float elapsedTime = Convert.ToSingle(value.Item1);
            float fadeinTime = value.Item2;
            float target_amb = value.Item3;
            float start_amb = value.Item4;
            float trialElapsedTime = Convert.ToSingle(value.Item5);
            float trialStopTime = Convert.ToSingle(value.Item6);
            float MouseZ = value.Item7.Item1;
            float SphereZ = value.Item7.Item2;
            float Radius = value.Item7.Item3;

            double BinocularRad;

            BinocularRad = 0.5;


            float ambient = start_amb;
            float DistanceToSphere = SphereZ-MouseZ;

            double alpha = Math.Asin(Radius / Math.Sqrt((Radius * Radius) + (DistanceToSphere * DistanceToSphere)));

            float k1 = (target_amb-start_amb)/fadeinTime;

            if ((trialElapsedTime < trialStopTime) || trialStopTime < 0)
            {
                if (alpha <= BinocularRad)
                {
                    //ambient = (float)(alpha * (target_amb / BinocularRad));
                    //ambient = (float) 0.5 - (float) alpha * ((float) 0.5 / (float) BinocularRad);
                                // Convert alpha to float for clamping
                    float alphaF = (float)alpha;

                    // Map alphaF in [0, BinocularRad] to ambient in [0.5, 0.0]
                    float fraction = alphaF / (float)BinocularRad;   // fraction goes [0..1]
                    // Ambient at alpha=0 => 0.5
                    // Ambient at alpha=BinocularRad => 0.0
                    ambient = 0.5f * (1f - fraction);
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
            return (float) ambient;
            //return Tuple.Create(ambient, alpha);



        });
    }
}
