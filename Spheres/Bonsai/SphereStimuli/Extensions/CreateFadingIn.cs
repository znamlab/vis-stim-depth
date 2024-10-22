using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class CreateFadingIn
{
    public IObservable<float> Process(IObservable<Tuple<double, float, float, float>> source)
    {
        return source.Select(value => {
            float start_amb = value.Item4;
            float target_amb = value.Item3;
            float elapsedTime = Convert.ToSingle(value.Item1);
            float fadeinTime = value.Item2;
            float ambient = start_amb;

            float k1 = (target_amb-start_amb)/fadeinTime;


            if (elapsedTime <= fadeinTime)
            {
                ambient = start_amb + elapsedTime*k1;
            }

            else
            {
                ambient = target_amb;
            }


            // // if alternate colors

            // // For white spheres
            // if (sphereID%2 ==1)
            // {
            //     target_amb = 1;

            //     if (elapsedTime<= fadeinTime)
            //     {
            //         ambient = start_amb + elapsedTime*k1;
            //     }

            //     else if (elapsedTime > fadeinTime && elapsedTime < fadeinTime+stayTime)
            //     {
            //         ambient = target_amb;
            //     }

            //     else if (elapsedTime >= fadeinTime+stayTime && elapsedTime<=totalTime)
            //     {
            //         ambient = target_amb - (elapsedTime-  stayTime - fadeinTime)*k2; 
            //     }

            // }


            // // For black spheres
            // else if (sphereID%2 == 0)
            // {
            //     target_amb = 0;

            //     if (elapsedTime<= fadeinTime)
            //     {
            //         ambient = start_amb - elapsedTime*k1;
            //     }

            //     else if (elapsedTime > fadeinTime && elapsedTime < fadeinTime+stayTime)
            //     {
            //         ambient = target_amb;
            //     }

            //     else if (elapsedTime >= fadeinTime+stayTime && elapsedTime<=totalTime)
            //     {
            //         ambient = target_amb + (elapsedTime-  stayTime - fadeinTime)*k2; 
            //     }

            // }


            // Returns
            return ambient;


        });
    }
}
