# AutoThrottleOff
Kerbal Space Program Mod :: Automatically Set Throttle to 0 to allow feather taps

A small mod to allow 'feather taps' of the throttle.

By default, tapping the throttle up (SHIFT) will keep the throttle at the target amount.
One workaround was to hold X while tapping the throttle.

With this mod, there's no need.

## Install

Standard Install. Build the DLL and place in the GameData/AutoThrottleOff/Plugins folder.

## Usage

Mod activates in flight mode, but is disabled by default. Press END key to enable/ disable the mod.
A notification should appear saying `AutoThrottleOff :: True` or `AutoThrottleOff :: False`.

With this active, press the SHIFT key to throttle up. Once SHIFT has been released, the throttle
will automatically reset to 0. Allowing to feather tap the throttle for those tiny maneuvers.

When disabled, standard throttle mechanics work.

## Contribution

If you want to contribute, create a new branch and submit a Pull Request.

PR's will be approved by myself for now.

## Needed Enchancements

Right now, the mod activates/ deactivates on pressing the END key.
What I would like is to enable this with a toolbar icon, so there's
visibility. But I need to look into this myself, as I'm not sure how to
acoomplish it yet.

If you know, feel free to contribute and submit a PR. Toolbar references
already provided.
