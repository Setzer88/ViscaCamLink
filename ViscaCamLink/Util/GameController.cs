using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SharpDX.XInput;

namespace ViscaCamLink.Util {
    internal static class GameController {
        private static readonly Controller _controller1 = new Controller(UserIndex.One);
        
        private static int deadband = 2500;

        public static void Update() {
            if (!_controller1.IsConnected)
                return;

            var gamepad = _controller1.GetState().Gamepad;

            //leftThumb.X = (Math.Abs((float)gamepad.LeftThumbX) < deadband) ? 0 : (float)gamepad.LeftThumbX / short.MinValue * -100;
            //leftThumb.Y = (Math.Abs((float)gamepad.LeftThumbY) < deadband) ? 0 : (float)gamepad.LeftThumbY / short.MaxValue * 100;
            //rightThumb.Y = (Math.Abs((float)gamepad.RightThumbX) < deadband) ? 0 : (float)gamepad.RightThumbX / short.MaxValue * 100;
            //rightThumb.X = (Math.Abs((float)gamepad.RightThumbY) < deadband) ? 0 : (float)gamepad.RightThumbY / short.MaxValue * 100;

            //leftTrigger = gamepad.LeftTrigger;
            //rightTrigger = gamepad.RightTrigger;
        }

        //Output
        public static class One {
            public static class Left {
                public static double X {
                    get {
                        if (!_controller1.IsConnected)
                            return 0;

                        if (Math.Abs((float)_controller1.GetState().Gamepad.LeftThumbX) < deadband)
                            return 0;
                        else
                            return (float)_controller1.GetState().Gamepad.LeftThumbX / short.MinValue * -100;
                    }
                }
                public static double Y {
                    get {
                        if (!_controller1.IsConnected)
                            return 0;

                        if (Math.Abs((float)_controller1.GetState().Gamepad.LeftThumbY) < deadband)
                            return 0;
                        else
                            return (float)_controller1.GetState().Gamepad.LeftThumbY / short.MinValue * 100;
                    }
                }
            }
        }
    }
}
