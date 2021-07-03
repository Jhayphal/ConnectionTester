using System;
using System.IO;

namespace ConnectionTester
{
	internal sealed class SecurityStream : Stream
	{
		Stream stream;

		Random random;

		public override bool CanRead => stream.CanRead;

		public override bool CanSeek => stream.CanSeek;

		public override bool CanWrite => stream.CanWrite;

		public override long Length => stream.Length;

		public override long Position { get => stream.Position >> 1; set => stream.Position = value << 1; }

		public SecurityStream(Stream stream)
		{
			this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
			this.random = new Random(DateTime.Now.Millisecond);
		}

		public override void Flush()
		{
			stream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			count <<= 1;
			var protectedData = new byte[count];
			offset <<= 1;

			var readed = stream.Read(protectedData, offset, count);

			if (readed % 2 != 0)
				--readed;

			int index = 0;

			for (int i = 1; i < readed; i += 2)
				buffer[index++] = (byte)(protectedData[i] ^ protectedData[i - 1]);

			return readed >> 1;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			var result = stream.Seek(offset << 1, origin);

			if (result % 2 != 0)
				--result;

			return result >> 1;
		}

		public override void SetLength(long value)
		{
			stream.SetLength(value << 1);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			count <<= 1;
			var protectedData = new byte[count];
			offset <<= 1;

			int index = 0;

			for (int i = 0; i < count; ++i)
				if (i % 2 == 0)
					protectedData[i] = (byte)random.Next(0, 256);
				else
					protectedData[i] = (byte)(buffer[index++] ^ protectedData[i - 1]);

			stream.Write(protectedData, offset, count);
		}
	}
}
