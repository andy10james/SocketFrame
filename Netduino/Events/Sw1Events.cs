using System;
using Microsoft.SPOT;
using System.Collections;
using System.Threading;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Netduino.Events {
    internal static class Sw1Events {

        public delegate void FnCallback();

        private readonly static Thread _signalThread;
        private readonly static ArrayList _pushCallbacks;
        private readonly static ArrayList _releaseCallbacks;

        static Sw1Events () {
            _signalThread = new Thread(Worker);
            _pushCallbacks = new ArrayList();
            _releaseCallbacks = new ArrayList();
            _signalThread.Start();
        }

        public static void OnPush(FnCallback fn) {
            _pushCallbacks.Add(fn);
        }

        public static void OnRelease(FnCallback fn) {
            _releaseCallbacks.Add(fn);
        }

        private static void Worker() {
            var sw1 = new InputPort(Pins.ONBOARD_SW1, false, Port.ResistorMode.Disabled);
            var previous = false;
            while (true) {
                var state = sw1.Read();
                if (state != previous) {
                    if (state) {
                        foreach (var callback in _pushCallbacks) {
                            var fn = callback as FnCallback;
                            if (fn != null) {
                                new Thread(() => fn()).Start();
                            }
                        }
                    } else {
                        foreach (var callback in _releaseCallbacks) {
                            var fn = callback as FnCallback;
                            if (fn != null) {
                                new Thread(() => fn()).Start();
                            }
                        }
                    }
                }
                previous = state;
            }
        }

    }
}
