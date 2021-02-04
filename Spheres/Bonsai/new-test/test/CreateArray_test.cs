using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class TransformScript01
{

    public IObservable<Tuple<float[],int>> Process(IObservable<float> source)
    {
        return source.Select(value => 
        {
            Random random = new Random();
            int num = random.Next(1,4);
            float[] result = linspace(value,value*3,num);
            Tuple<float[], int>My_Tuple2 = new Tuple<float[], int>(result,num);
            return My_Tuple2;

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
