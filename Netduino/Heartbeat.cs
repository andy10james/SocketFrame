using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using Netduino.Output;


namespace Netduino {
    internal static class Heartbeat {

        private static Thread _heartbeatThread;
        private static Boolean _run;

        public static Boolean Beating {
            get { return _run; }
        }

        static Heartbeat() {
            _heartbeatThread = new Thread(Worker);
        }

        public static void Start() {
            _heartbeatThread.Start();
            _run = true;
        }

        public static void Stop() {
            _heartbeatThread.Abort();
            _run = false;
        }

        public static void Pause() {
            _run = false;
        }

        public static void Resume() {
            if (_heartbeatThread.ThreadState == ThreadState.Unstarted) {
                Start();
            } else {
                _run = true;
            }
        }

        private static void Worker() {
            while (true) {
                while (_run) {
                    OnboardLed.Blink(2);
                    Thread.Sleep(1500);
                }
            }
        }

    }
}
