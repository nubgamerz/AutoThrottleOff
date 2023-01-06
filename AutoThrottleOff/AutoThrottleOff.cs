using UnityEngine;

namespace AutoThrottleOff
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class AutoThrottleOff : MonoBehaviour
    {

        // Ensure that the feature is disabled at start (can't throttle to orbibt with it set to 0)
        bool isEnabled = false;

        // Create a null vessel reference which we can update when loading
        Vessel vessel = null;

        // Unity Start() method, will start when the module is loaded
        public void Start() {
            //Log the start of the mod, ensure that it actually works
            Debug.Log("AutoThrottleOff Loaded");
        }

        // Unity Update() method, called every frame. Using this to check for key inputs
        public void Update() {
            // Since I don't know how to create a toolbar icon (reference loaded, just not used)
            // Use a hotkey to switch the feature on/off (END key)
            if (Input.GetKeyDown(KeyCode.End)) {
                if (isEnabled) {
                    ToggleMod(false);
                    Debug.Log("AutoThrottleOff :: " + isEnabled);
                } else {
                    CheckAndSetVessel();
                }
            }
            // If the mod is enabled, correct vessel is selected, set throttle to 0 on shift key release
            if (isEnabled) {
                if (vessel != null) {
                    if (vessel == FlightGlobals.ActiveVessel) {
                        bool throttleUpKey = Input.GetKeyUp(KeyCode.LeftShift);
                        if (throttleUpKey == true) {
                            SetThrottleNone(vessel.ctrlState);
                        }
                    } else {
                        CheckAndSetVessel();
                    }
                }
                // Ensure that the vessel is matching flight globals, if not then disable the mod
                // User can re-enable with hotkey
                if (!IsActiveVessel()) {
                    ToggleMod(false);
                }
            }
        }

        // The main function to set the throttle to 0
        private void SetThrottleNone(FlightCtrlState fcs) {
            Debug.Log("Shift Released, Setting Throttle to 0");
            Debug.Log("Current Throttle :: " + fcs.mainThrottle.ToString());
            if (fcs.mainThrottle != 0) {
                fcs.mainThrottle = 0;
                SetFlightGlobals(0);
            }
        }

        // This is our failsafe. Make sure that the current vessel is the flight global
        // active vessel. If not, or if the vessel is null, then set it to the flight
        // global active vessel.
        private void CheckAndSetVessel() {
            // If no vessel selected, select the vessel and enable the mod
            if (vessel == null) {
                Debug.Log("No Vessel Active :: Switching to Active Vessel");
                try {
                    SetVessel();

                } catch {
                    Debug.Log("Cannot select vessel, likely due to no flight global vessel active");
                }
            // If vessel is not the current flight global vessel, change it and enable the mod
            } else if (vessel != FlightGlobals.ActiveVessel) {
                Debug.Log("Active Vessel Changed :: Switching to Active Vessel");
                SetVessel();
            // If the vessel is correct then just enable the mod
            } else {
                ToggleMod(true);
            }
        }

        // Set the active vessel
        private void SetVessel() {
            try {
                vessel = FlightGlobals.ActiveVessel;
                ToggleMod(true);

                Debug.Log("Vessel Active :: " + vessel.name.ToString());
                Debug.Log("AutoThrottleOff :: " + isEnabled);

            } catch {
                Debug.Log("Cannot select vessel, likely due to no flight global vessel active");
            }
        }

        // Thanks MechJeb for this function, this was the key to getting the throttle display to work
        // Reflects the throttle value to the on-screen throttle gauge
        // Ref: https://github.com/MuMech/MechJeb2
        private void SetFlightGlobals(double throttle) {
            if (FlightGlobals.ActiveVessel != null && vessel == FlightGlobals.ActiveVessel) {
                FlightInputHandler.state.mainThrottle = (float)throttle;
            }
        }

        // We need to check that the active vessel is the selected vessel during flight
        // If the vessel changes, then the mod should be disabled
        private bool IsActiveVessel() {
            bool result = false;
            if (vessel == FlightGlobals.ActiveVessel) {
                result = true;
            }
            return result;
        }

        // Enable/disable mod on function so we can send messages with it
        private void ToggleMod(bool status) {
            if (status == true) {
                isEnabled = true;
            } else {
                isEnabled = false;
            }
            SendNotif("AutoThrottleOff :: " + isEnabled.ToString());
        }

        // Simple function to send a message on the screen
        private void SendNotif(string message) {
            ScreenMessages.PostScreenMessage(
                message,
                5f
                );
        }
    }
}
