using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Bau.Libraries.ImageFilters.Filters
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class SkewFilter : BaseFilter
	{
		public const string RIGHT_SHIFT_TOKEN_NAME = "RightShift";
		public const string UP_SHIFT_TOKEN_NAME = "UpShift";

		private int _rightShift = -20; //Defualts
		private int _upShift = 0;


		/// <summary>
		/// Executes this filter on the input image and returns the result
		/// </summary>
		/// <param name="inputImage">input image</param>
		/// <returns>transformed image</returns>
		/// <example>
		/// <code>
		/// SkewFilter skewFilter = new SkewFilter();
		/// skewFilter.UpShift = 0;
		/// skewFilter.RightShift = 5;
		/// transformed = skewFilter.ExecuteFilter(myImg);
		/// </code>
		/// </example>
		public override Image ExecuteFilter(Image source)
		{

			Bitmap result = new Bitmap(source.Width + Math.Abs(_rightShift), source.Height + Math.Abs(_upShift));
			Graphics g = Graphics.FromImage(result);
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			Point[] points = new Point[3];
			int horShiftCorrections = 0;
			int verShiftCorrections = 0;
			if (_rightShift < 0)
			{
				horShiftCorrections = _rightShift * (-1);
			}
			if (_upShift < 0)
			{
				verShiftCorrections = _upShift * (-1);
			}
			points[0] = new Point(horShiftCorrections + _rightShift, verShiftCorrections);
			points[1] = new Point(horShiftCorrections + _rightShift + source.Width, verShiftCorrections + _upShift);
			points[2] = new Point(horShiftCorrections, verShiftCorrections + source.Height);

			try
			{
				g.DrawImage(source, points);
			}
			catch { }
			return result;
		}

		public int RightShift
		{
			get { return _rightShift; }
			set { _rightShift = value; }
		}

		public int UpShift
		{
			get { return _upShift; }
			set { _upShift = value; }
		}
	}
}
