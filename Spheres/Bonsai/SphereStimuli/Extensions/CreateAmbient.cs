using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class CreateAmbient
{
    public IObservable<float[]> Process(IObservable<Tuple<int, float>> source)
    {
        return source.Select(value => 
        {
            float start_amb = 0.5f;
            int target_amb;
            int sphereID = value.Item1;
            float framerate = value.Item2;
            float totalTime = 4f;
            float fadeinTime = 0.5f;
            float fadeoutTime = 0.5f;
            float stayTime = totalTime - fadeinTime - fadeoutTime;

            //Random random = new Random();
            //target_amb = random.Next(0,2); //create randomly a white or black sphere
            target_amb = sphereID % 2; 
            float target_ambf = Convert.ToSingle(target_amb);

            int fadein_frameNum = Convert.ToInt32(framerate * fadeinTime);
            int stay_frameNum = Convert.ToInt32(framerate * stayTime);
            int fadeout_frameNum = Convert.ToInt32(framerate * fadeoutTime);
            int total_frameNum = fadein_frameNum + stay_frameNum + fadeout_frameNum;

            float[] fadein_arr = linspace(start_amb,target_ambf,fadein_frameNum);
            float[] stay_arr = Enumerable.Repeat(target_ambf, stay_frameNum).ToArray();
            float[] fadeout_arr = linspace(target_ambf,start_amb,fadein_frameNum);

            float[] result = new float[total_frameNum];
            fadein_arr.CopyTo(result,0);
            stay_arr.CopyTo(result,fadein_frameNum);
            fadeout_arr.CopyTo(result,fadein_frameNum+stay_frameNum);

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
