using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Net.Mail;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class CreateFadingInOut
{
    public IObservable<float> Process(IObservable<Tuple<double, float, float, float, double, double, Tuple<double, float, float>>> source)


    {
        return source.Select(value => {
            float elapsedTime = Convert.ToSingle(value.Item1);
            float fadeinTime = value.Item2;
            float target_amb = value.Item3;
            float start_amb = value.Item4;
            float trialElapsedTime = Convert.ToSingle(value.Item5);
            float trialStopTime = Convert.ToSingle(value.Item6);
            float MouseZ = (float)value.Item7.Item1;
            float SphereZ = value.Item7.Item2;
            float Radius = value.Item7.Item3;

            double BinocularRadBlack;
            double BinocularRadGray;


            BinocularRadBlack = 0.4;
            BinocularRadGray = 0.27;


            float time_ambient=target_amb;
            float space_ambient=target_amb;

            float DistanceToSphere = SphereZ-MouseZ;

            double alpha = Math.Asin(Radius / Math.Sqrt((Radius * Radius) + (DistanceToSphere * DistanceToSphere)));

            float k1 = (target_amb-start_amb)/fadeinTime;

            if ((trialElapsedTime < trialStopTime) || trialStopTime < 0)
            {
                float time_fraction = trialElapsedTime / fadeinTime; //fade in the spheres in fadeinTime s. 
                time_ambient = start_amb*(1-time_fraction); // go to 0 linearly
                
                if (alpha <= BinocularRadGray)
                {
                    space_ambient=start_amb;
                }
                if (alpha <= BinocularRadBlack & alpha > BinocularRadGray)
                {

                    // Convert alpha to float for clamping
                    float alphaF = (float)alpha;

                    // Map alphaF in [0, BinocularRad] to ambient in [0.5, 0.0]
                    float fraction = (alphaF - (float)BinocularRadGray) / ((float)BinocularRadBlack-(float)BinocularRadGray);   // fraction goes [0..1] from BinocularRadGray to BinocularRadBlack
                    // Ambient at alpha=0 => 0.5
                    // Ambient at alpha=BinocularRadGray => 0.0
                    space_ambient = start_amb * (1f - fraction);
                }

                else if (alpha > BinocularRadBlack)
                {
                    space_ambient = target_amb;
                }
            }

            else
            {
                if (trialStopTime > 0)
                {
                    time_ambient = Math.Min(target_amb - (trialElapsedTime-trialStopTime)*k1, start_amb);// fadeout

                }
            }


            // Returns
            return (float) Math.Max(time_ambient, space_ambient);



        });
    }
}
