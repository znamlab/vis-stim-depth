/*     Serial Controlled Stepper Motor
 *      
 *  Modified from example by Dejan Nedelkovski, www.HowToMechatronics.com
 */

#include <AccelStepper.h>
#include <elapsedMillis.h>

// Define pin numbers
const int stepPin = 3;
const int dirPin = 4;
//const int maxSpeed = 4000; // Maximum stepper speed
const int acceleration = 200; // Acceleration in steps/s^2

AccelStepper stepper(1, stepPin, dirPin);

elapsedMillis printTime;


float currentSpeed = 0;
float targetSpeed = 0;
float start_speed = 0;
unsigned long startTime = 0;
unsigned long transitionStartTime = 0;
bool transitioning = false;
float progress = 0;
unsigned long elapsedTime = 0;
float accelerationTime = 0;
int maxSpeed = 1213; //max possible speed is 100 cm/s w 1/4 microstepping for safety

void setup() {
    Serial.begin(9600); // Start serial communication
    stepper.setMaxSpeed(maxSpeed);
    stepper.setSpeed(0);
}

void loop() {

  if (printTime >= 1000) {
    //Serial.println("Checking slow operations");
    printTime = 0;
    if (Serial.available() > 0) {
        String input = Serial.readStringUntil('\n');  // Read full line
        input.trim();  // Remove spaces/newlines
        
        if (input.length() > 0) {  // Ensure valid input
            int speedInput = input.toInt();
            if (speedInput >= 0 && speedInput <= 1213) { //max possible speed is 100 cm/s w 1/4 microstepping for safety
                //targetSpeed = (speedInput / 10.0) * maxSpeed;
                targetSpeed = speedInput;
                transitionStartTime = millis();
                start_speed=stepper.speed();
                accelerationTime=abs(((targetSpeed-start_speed)/acceleration)*1000); //Acceleration time required in ms
                Serial.println(accelerationTime);
                transitioning = true;
                Serial.print("Target speed set to: ");
                Serial.println(targetSpeed);
            }
        }
      }
    }
  
  if (transitioning) {
      unsigned long elapsedTime = millis() - transitionStartTime;
      if (elapsedTime < accelerationTime) {
          float progress = (float)elapsedTime / accelerationTime;
          currentSpeed = start_speed + (progress * (targetSpeed - start_speed));
      } else {
          currentSpeed = targetSpeed;
          transitioning = false;
      }
      stepper.setSpeed(currentSpeed);
  }
  
  stepper.runSpeed();
}


