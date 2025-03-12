import numpy as np
import pandas as pd

'''
We want to generate a sequence of 400 numbers in pulses per second to the motor. Those numbers must be one of five speeds, 
from 4 to 64 cm/s. Therefore, we need to know the size of the wheel and the number of pulses per turn of the motor. The motor 
specs say that it turns at 200 steps per rev, and we are running it microstepping in the MS2 pin, so running it at quarter steps, 
which means 800 steps per rev. The wheel is 10.5 cm radius.  
'''

MIN_SPEED = 4
MAX_SPEED = 64
N_SPEEDS = 5
N_CORRIDORS = 400
STEPS_PER_REV = 200
MICROSTEPPING = 1/4
WHEEL_RADIUS = 10.5

# Function to convert cm/s to steps/s
def cms_to_stepss(cms_list, wheel_radius, steps_per_rev):
    circunference = 2 * np.pi * wheel_radius
    print(circunference)
    stepss_list = np.array(cms_list) * (1 / circunference) * steps_per_rev
    stepss_list = [round(v) for v in stepss_list]
    return stepss_list

def random_k_values_from_list(values, k, output_file="speeds_stepss.csv"):
    # Choose k random values from the list
    random_values = np.random.choice(values, k, replace=True)

    # Save to CSV
    df = pd.DataFrame(random_values, columns=["Speeds (steps/s)"])
    df.to_csv(output_file, index=False)

    return output_file


true_steps_per_rev = STEPS_PER_REV * (1/MICROSTEPPING)

speeds_cms = np.linspace(MIN_SPEED, MAX_SPEED, N_SPEEDS).tolist()

speeds_stepss = cms_to_stepss(speeds_cms, WHEEL_RADIUS, true_steps_per_rev)

output_file = random_k_values_from_list(speeds_stepss, N_CORRIDORS)

