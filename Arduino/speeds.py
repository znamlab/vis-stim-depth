import numpy as np
import pandas as pd

'''
We want to generate a sequence of 400 numbers in pulses per second to the motor. 
Those numbers must be one of five speeds, from 4 to 64 cm/s. Therefore, we need 
to know the size of the wheel and the number of pulses per turn of the motor. The 
motor specs say that it turns at 200 steps per rev, and we are running it 
microstepping in the MS2 pin, so running it at quarter steps, 
which means 800 steps per rev. The wheel is 10.5 cm radius.  
'''

MIN_SPEED = 4
MAX_SPEED = 64
N_SPEEDS = 5
RUNNING_SPEEDS = [4, 8, 16, 32, 64]
OPTIC_FLOWS = [0.1, 1, 10, 100, 1000]
N_CORRIDORS = 400
STEPS_PER_REV = 200
MICROSTEPPING = 1/4
WHEEL_RADIUS = 10.5


true_steps_per_rev = STEPS_PER_REV * (1/MICROSTEPPING)

# Function to convert cm/s to steps/s
def cms_to_stepss(cms_list, wheel_radius, steps_per_rev):
    circunference = 2 * np.pi * wheel_radius
    stepss = np.array(cms_list) * (1 / circunference) * steps_per_rev
    stepss = np.round(stepss).astype(int)
    return stepss

def trial_values(optic_flows, running_speeds, steps_per_rev):
    output = []
    for of in optic_flows:
        for rs in running_speeds:
            steps = cms_to_stepss(rs, WHEEL_RADIUS, steps_per_rev)
            depth = (rs/100)/np.radians(of)
            output.append([steps, depth])
    return pd.DataFrame(output, columns=['sps', 'depth'])

def pseudo_random_sequence(optic_flows, running_speeds, ntrials, output_file="rpm_depth_combinations.csv", steps_per_rev=true_steps_per_rev):
    output = []
    single_trial = trial_values(optic_flows, running_speeds, steps_per_rev=steps_per_rev)
    nstimuli = len(single_trial)
    print(f'Single trial with {nstimuli} stimuli')
    randorder = np.arange(nstimuli)
    for trial in range(ntrials):
        np.random.shuffle(randorder)
        output.append(single_trial.loc[randorder].reset_index().copy())
    output = pd.concat(output, ignore_index=True)
    if output_file is not None:
        output.to_csv(output_file)
    return output


def random_k_values_from_list(values, k, output_file="speeds_stepss.csv"):
    # Choose k random values from the list
    random_values = np.random.choice(values, k, replace=True)

    # Save to CSV
    df = pd.DataFrame(random_values, columns=["Speeds (steps/s)"])
    df.to_csv(output_file, index=False)

    return output_file


single_trial = trial_values(OPTIC_FLOWS, RUNNING_SPEEDS, steps_per_rev=true_steps_per_rev)
stim_df = pseudo_random_sequence(OPTIC_FLOWS, RUNNING_SPEEDS, 2, output_file="steppersec_depth_combinations.csv", steps_per_rev=true_steps_per_rev)
print(stim_df.depth.describe())
print(stim_df.sps.describe())
print(f'Min depth: ')
