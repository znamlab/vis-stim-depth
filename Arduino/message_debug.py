import serial
import time

def send_speed_to_arduino(port, baudrate, speed):
    try:
        # Open serial connection
        with serial.Serial(port, baudrate, timeout=1) as ser:
            if 0 <= speed <= 10:
                ser.write(f"{speed}\n".encode())
                print(f"Sent speed: {speed}")
                time.sleep(0.1)  # Give some time for Arduino to process
                
                # Read response if available
                if ser.in_waiting > 0:
                    response = ser.readline().decode().strip()
                    print("Arduino response:", response)
            else:
                print("Speed must be between 0 and 10")
    except serial.SerialException as e:
        print("Error:", e)

if __name__ == "__main__":
    arduino_port = "/dev/tty.usbmodem1101"  # Change to match your Arduino port
    baud_rate = 9600
    speed_value = 5  
    
    send_speed_to_arduino(arduino_port, baud_rate, speed_value)