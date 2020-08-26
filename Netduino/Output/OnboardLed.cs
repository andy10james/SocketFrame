using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Netduino.Output {
    public static class OnboardLed {

        private readonly static OutputPort _led;

        static OnboardLed() {
            _led = new OutputPort(Pins.ONBOARD_LED, false);
        }

        public static void On() {
            _led.Write(true);
        }

        public static void Off() {
            _led.Write(false);
        }

        public static void Blink(int repeat = 1) {
            for (var i = 1; i <= repeat; i++) {
                _led.Write(true);
                Thread.Sleep(50);
                _led.Write(false);
                Thread.Sleep(50);
            }
        }

    }
}
