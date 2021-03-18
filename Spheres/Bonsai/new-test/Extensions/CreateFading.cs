using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Bonsai.Reactive;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class CreateFading
{
    public IObservable<float> Process(IObservable<Tuple<int, double, float>> source)
    {
        return source.Select(value => 
        {
            float start_amb = 0.5f;
            int target_amb;
            int sphereID = value.Item1;
            float elapsedTime = Convert.ToSingle(value.Item2);
            float totalTime = value.Item3;
            float fadeinTime = value.Item3/8;
            float fadeoutTime = value.Item3/8;
            float stayTime = totalTime - fadeinTime - fadeoutTime;
            float ambient = start_amb;

            float k1 = (1-start_amb)/fadeinTime;
            float k2 = (1-start_amb)/fadeoutTime; 


            // For white spheres
            if (sphereID%2 ==1)
            {
                target_amb = 1;

                if (elapsedTime<= fadeinTime)
                {
                    ambient = start_amb + elapsedTime*k1;
                }

                else if (elapsedTime > fadeinTime && elapsedTime < fadeinTime+stayTime)
                {
                    ambient = target_amb;
                }

                else if (elapsedTime >= fadeinTime+stayTime && elapsedTime<=totalTime)
                {
                    ambient = target_amb - (elapsedTime-  stayTime - fadeinTime)*k2; 
                }

            }


            // For black spheres
            else if (sphereID%2 == 0)
            {
                target_amb = 0;

                if (elapsedTime<= fadeinTime)
                {
                    ambient = start_amb - elapsedTime*k1;
                }

                else if (elapsedTime > fadeinTime && elapsedTime < fadeinTime+stayTime)
                {
                    ambient = target_amb;
                }

                else if (elapsedTime >= fadeinTime+stayTime && elapsedTime<=totalTime)
                {
                    ambient = target_amb + (elapsedTime-  stayTime - fadeinTime)*k2; 
                }

            }


            // Returns
            return ambient;


        });
    }
}
