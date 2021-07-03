namespace ConnectionTester
{
	interface ITestController
	{
		string Sender { get; }

		bool Enabled { get; set; }

		void Setup();

		bool Run();

		void Stop();

		bool Aviable(out string message);
	}
}
