using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class CreateParamRange
{
    public IObservable<float[]> Process(IObservable<Tuple<float, float, int, float, float>> source)
    {
        return source.Select(value => 
        {
            float min = value.Item1;
            float max = value.Item2;
            int count = value.Item3;
            float minExc = value.Item4;
            float maxExc = value.Item5;

            float[] min_arr = linspace(min,minExc,Convert.ToInt32(count/2));
            float[] max_arr = linspace(maxExc,max,Convert.ToInt32(count/2));

            float[] result = new float[count];
            min_arr.CopyTo(result,0);
            max_arr.CopyTo(result,min_arr.Length);

            return result;

        });
    }


    //Generate a linspaced array
    private static float[] linspace(float x1, float x2, int n)
    {
        float step = (x2-x1)/(n-1);
        float[] linspaced_arr = new float[n];
        for (int i = 0; i < n; i++)
        {
            linspaced_arr[i] = x1 + step*(i);
        }

        return linspaced_arr;
    }
}
