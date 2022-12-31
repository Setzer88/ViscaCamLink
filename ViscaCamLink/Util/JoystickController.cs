using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using SharpDX.DirectInput;

namespace ViscaCamLink.Util {
    internal static class JoystickController {
        
        private static readonly SharpDX.DirectInput.Joystick _joystick;
        private static int _maxJoystickState = 65534; //double short
        private static int _centerJoystickState = 32767; //half _maxJoystickState
        private static int _joystickDeadZone = 1500;
        static JoystickController() {
            
            var joystickGuid = Guid.Empty;
            var di = new DirectInput();
            foreach (var device in di.GetDevices(SharpDX.DirectInput.DeviceType.Driving, DeviceEnumerationFlags.AllDevices)) {
                joystickGuid = device.InstanceGuid;
                break;
            }
            _joystick = new SharpDX.DirectInput.Joystick(di, joystickGuid);
            _joystick.Acquire();
        }

        private static int CalcDirectory(int joystickValue) {
            if (joystickValue < (_centerJoystickState - _joystickDeadZone))
                return -1;

            if (joystickValue > (_centerJoystickState + _joystickDeadZone))
                return 1;

            return 0;
        }

        private static int CalcSpeed(int joystickValue) {
            
            if (joystickValue < (_centerJoystickState - _joystickDeadZone)) {
                if (joystickValue >= 29703)
                    return 1;
                if (joystickValue >= 28140)
                    return 2;
                if (joystickValue >= 26576)
                    return 3;
                if (joystickValue >= 25013)
                    return 4;
                if (joystickValue >= 23450)
                    return 5;
                if (joystickValue >= 21886)
                    return 6;
                if (joystickValue >= 20323)
                    return 7;
                if (joystickValue >= 18760)
                    return 8;
                if (joystickValue >= 17196)
                    return 9;
                if (joystickValue >= 15633)
                    return 10;
                if (joystickValue >= 14070)
                    return 11;
                if (joystickValue >= 12506)
                    return 12;
                if (joystickValue >= 10943)
                    return 13;
                if (joystickValue >= 9380)
                    return 14;
                if (joystickValue >= 7816)
                    return 15;
                if (joystickValue >= 6253)
                    return 16;
                if (joystickValue >= 4690)
                    return 17;
                if (joystickValue >= 3126)
                    return 18;
                if (joystickValue >= 1563)
                    return 19;
                
                return 20;
            }      

            if (joystickValue > (_centerJoystickState + _joystickDeadZone)) {
                if (joystickValue <= 35830)
                    return 1;
                if (joystickValue <= 37393)
                    return 2;
                if (joystickValue <= 38957)
                    return 3;
                if (joystickValue <= 40520)
                    return 4;
                if (joystickValue <= 42083)
                    return 5;
                if (joystickValue <= 43647)
                    return 6;
                if (joystickValue <= 45210)
                    return 7;
                if (joystickValue <= 46773)
                    return 8;
                if (joystickValue <= 48337)
                    return 9;
                if (joystickValue <= 49900)
                    return 10;
                if (joystickValue <= 51463)
                    return 11;
                if (joystickValue <= 53027)
                    return 12;
                if (joystickValue <= 54590)
                    return 13;
                if (joystickValue <= 56153)
                    return 14;
                if (joystickValue <= 57717)
                    return 15;
                if (joystickValue <= 59280)
                    return 16;
                if (joystickValue <= 60844)
                    return 17;
                if (joystickValue <= 62407)
                    return 18;
                if (joystickValue <= 63971)
                    return 19;
               
                return 20;
            }
            return 0;
        }
        //Output
        public static class Joystick {
            /// <summary>
            /// Returns -1/1 for left/right directory
            /// </summary>
            public static int X {
                get {
                    if (_joystick == null)
                        return 0;

                    return CalcDirectory(_joystick.GetCurrentState().X);
                }
            }

            /// <summary>
            /// Returns -1/1 for up/down directory
            /// </summary>
            public static int Y {
                get {
                    if (_joystick == null)
                        return 0;

                    return CalcDirectory(_joystick.GetCurrentState().Y);
                }
            }

            /// <summary>
            /// Returns speed between 0 and 20
            /// </summary>
            public static int Speed {
                get {
                    if (_joystick == null)
                        return 0;

                    var xSpeed = CalcSpeed(_joystick.GetCurrentState().X);
                    var ySpeed = CalcSpeed(_joystick.GetCurrentState().Y);
                    return Math.Max(xSpeed, ySpeed);
                }
            }
        }
    }
}
