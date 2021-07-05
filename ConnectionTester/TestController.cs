using System;
using System.Windows.Forms;

namespace ConnectionTester
{
	abstract class TestController
	{
		public readonly CheckBox State;
		public readonly GroupBox Box;

		public abstract string Sender { get; }

		public virtual bool Enabled
		{
			get => State.Checked;
			set => State.Checked = value;
		}

		public abstract bool Running { get; }

		protected TestController(CheckBox state, GroupBox box)
		{
			State = state ?? throw new ArgumentNullException(nameof(state));
			Box = box ?? throw new ArgumentNullException(nameof(box));
		}

		public virtual void SetControlsState(bool enabled)
		{
			if (enabled)
			{
				State.Enabled = true;
				Box.Enabled = State.Checked;
			}
			else
			{
				State.Enabled = false;
				Box.Enabled = false;
			}
		}

		public abstract void Setup();

		public abstract bool Run();

		public abstract void Stop();

		public abstract bool Aviable(out string message);
	}
}
