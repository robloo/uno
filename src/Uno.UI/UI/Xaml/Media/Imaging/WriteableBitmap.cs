using System;
using Windows.Storage.Streams;
using UwpBuffer = Windows.Storage.Streams.Buffer;

namespace Windows.UI.Xaml.Media.Imaging
{
	public partial class WriteableBitmap : BitmapSource
	{
		private readonly UwpBuffer _buffer;

		public IBuffer PixelBuffer => _buffer;

		public WriteableBitmap(int pixelWidth, int pixelHeight) : base()
		{
			_buffer = new UwpBuffer((uint)(pixelWidth * pixelHeight * 4));

			PixelWidth = pixelWidth;
			PixelHeight = pixelHeight;
		}
	}
}
