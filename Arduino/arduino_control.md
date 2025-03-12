# Which Arduino control and why

- stepper_control will try to take an int from 1 to 10 and convert it to a stepper speed between 0 to 1000 steps per second. It will make every acceleration smoothly over 5s
- stepper_control_accel is similar, but holds acceleration constant at 200 steps/s^2
- stepper_control_steps is our version of choice, holds acceleration constant at the same value, but takes inputs in steps/s, as provided by speeds.py. The reason is that this way we can control everything smoothly without having to update the arduino. 