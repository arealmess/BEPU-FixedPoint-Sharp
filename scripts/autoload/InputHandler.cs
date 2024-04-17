using Godot;
using System;
using System.Collections.Generic; // Contains list structure

using FixMath.NET;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.IO.Compression;
using Deterministic.FixedPoint;
using BEPUutilities;


public partial class InputHandler : Node
{
	public enum InputState {
		READ,
		IGNORE,
		RECORD,
		REPLAY,
	}

	public InputState inputState = InputState.READ;

	int DeviceID = 0;
	// Joystick values
	BEPUutilities.Vector2 JoyAxisLeft = new BEPUutilities.Vector2();
	BEPUutilities.Vector2 JoyAxisRight = new BEPUutilities.Vector2();
	fp deadzoneInnerLeft = (fp)0.2m;
	fp deadzoneInnerRight = (fp)0.2m;

	// Action input values
	const int ActionBufferLength = 20;
	List<Dictionary<string, bool>> ActionBuffer = new List<Dictionary<string, bool>>(ActionBufferLength);
	Dictionary<string, bool> ActionValuesTemplate = new Dictionary<string, bool>();
	Dictionary<string, bool> ActionValues = new Dictionary<string, bool>();

	public bool run = false;


	public override void _Ready()
	{
		InitializeInputDict();
    Input.MouseMode = Input.MouseModeEnum.Captured;
  }

	public override void _PhysicsProcess(double delta)
	{
		ResetInputValues();

		if (!run) {
			return;
		}

		switch (inputState) {
			case InputState.READ:
				PollInputs();
				break;
			case InputState.IGNORE:
				PollInputs(true);
				break;
			case InputState.RECORD:
				break;
			case InputState.REPLAY:
				break;
		}
	}

	public BEPUutilities.Vector2 GetJoyAxisLeft()
	{
		return JoyAxisLeft;
	}

	public BEPUutilities.Vector2 GetJoyAxisRight()
	{
		return JoyAxisRight;
	}

	public bool IsActionPressed(string action)
	{
		if (ActionBuffer.Count == 0) {
			GD.Print("attempted to check if " + '"' + action + '"' + " was pressed in IsActionPressed, but inputBuffer was empty");
			return false;
		}

		if (ActionBuffer[0][action] == true) {
			return true;
		}
		return false;
	}

	private void PollInputs(bool ignore = false)
	{
		PollJoyAxis(ignore);
		PollInputActions(ignore);
	}

  //public override void _Input(InputEvent @event)
  //{
  //  // Mouse in viewport coordinates.
  //  if (@event is InputEventMouseButton eventMouseButton)
  //    GD.Print("Mouse Click/Unclick at: ", eventMouseButton.Position);
  //  else if (@event is InputEventMouseMotion eventMouseMotion)
  //    GD.Print("Mouse Motion at: ", eventMouseMotion.Position);

  //  // Print the size of the viewport.
  //  GD.Print("Viewport Resolution is: ", GetViewport().GetVisibleRect().Size);
  //}

  // Could set action values for up, down, left, right here
  private void PollJoyAxis(bool ignore = false)
	{
		if (!ignore) {
			JoyAxisLeft.X = (fp)Godot.Input.GetJoyAxis(DeviceID, Godot.JoyAxis.LeftX);
			JoyAxisLeft.Y = (fp)Godot.Input.GetJoyAxis(DeviceID, Godot.JoyAxis.LeftY);
			JoyAxisRight.X = (fp)Godot.Input.GetJoyAxis(DeviceID, Godot.JoyAxis.RightX);
			JoyAxisRight.Y = (fp)Godot.Input.GetJoyAxis(DeviceID, Godot.JoyAxis.RightY);

      if (Input.IsKeyPressed(Key.W)) JoyAxisLeft.Y -= 1;
      if (Input.IsKeyPressed(Key.A)) JoyAxisLeft.X -= 1; 
      if (Input.IsKeyPressed(Key.S)) JoyAxisLeft.Y += 1;
      if (Input.IsKeyPressed(Key.D)) JoyAxisLeft.X += 1; 

      JoyAxisRight.X = (fp)Input.GetLastMouseVelocity().X * F64.C0p001;
      JoyAxisRight.Y = (fp)Input.GetLastMouseVelocity().Y * F64.C0p001; 

      // Apply deadzones
      if (JoyAxisLeft.Length() < deadzoneInnerLeft) {
				JoyAxisLeft = new BEPUutilities.Vector2();
			}
			if (JoyAxisRight.Length() < deadzoneInnerRight) {
				JoyAxisRight = new BEPUutilities.Vector2();
			}

			// Make sure non deadzone area covers full input range from 0-1
			JoyAxisLeft = ((JoyAxisLeft.Length() - deadzoneInnerLeft) / ((fp)1 - deadzoneInnerLeft)) * JoyAxisLeft;
			JoyAxisRight = ((JoyAxisRight.Length() - deadzoneInnerRight) / ((fp)1 - deadzoneInnerRight)) * JoyAxisRight;
		}
	}

	// Polls what the player's current inputs on the frame this is called
	private void PollInputActions(bool ignore = false) {
		// Check what inputs are pressed
		if (!ignore) {  // do not check input values if set to ignore
			foreach (string action in ActionValues.Keys) {
				if (Godot.Input.IsActionPressed(action)) {
					ActionValues[action] = true;
				}
			}
		}

		UpdateInputBuffer();
	}

	private void UpdateInputBuffer() 
	{
		// Update input buffer
		if (ActionBuffer.Count == ActionBufferLength) {  // remove oldest input dict before inserting if buffer limit reached
			ActionBuffer.RemoveAt(ActionBufferLength - 1);
		}

		ActionBuffer.Insert(0, ActionValues);  // insert new input values at beginning of input buffer
	}

	private void ResetInputValues() {
		JoyAxisLeft = new BEPUutilities.Vector2();
		JoyAxisRight = new BEPUutilities.Vector2();
		ActionValues = new Dictionary<string, bool>(ActionValuesTemplate);
	}

	private void InitializeInputDict()
	{
		// Loop through actions and add them to template input dictionary
		foreach (string action in Godot.InputMap.GetActions()) {
			if (!action.Contains("ui_")) {  // filter out built in Godot UI actions
				ActionValuesTemplate.Add(action, false);
			}
		}
	}

	// INPUTHANDLER STATE FUNCTIONS

	public void Start()
	{
		run = true;
	}

	public void Halt()
	{
		run = false;
	}

	// Handle anything that needs to be done when changing state here
	public void ChangeState(InputHandler.InputState state)
	{
		switch (state) {
			case InputHandler.InputState.READ:
				inputState = InputState.READ;
				break;
			case InputHandler.InputState.IGNORE:
				inputState = InputState.IGNORE;
				break;
			case InputHandler.InputState.RECORD:
				inputState = InputState.RECORD;
				break;
			case InputHandler.InputState.REPLAY:
				inputState = InputState.REPLAY;
				break;
		}
	}
}

