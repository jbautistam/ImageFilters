using System;

namespace Bau.Libraries.ImageFilters.Filters
{
  /// <summary>
  ///		Filtro abstracto para las marcas de agua
  /// </summary>
  public abstract class BaseWaterMarkFilter : BaseFilter
  {
		/// <summary>
		///		Posici�n vertical
		/// </summary>
    public enum VAlign
			{	Left,
				Center,
				Right
			}
		/// <summary>
		///		Posici�n horizontal
		/// </summary>
    public enum HAlign
			{ Top,
				Middle,
				Bottom
			}

		protected int _width; //Defualts
		protected int _height;

    /// <summary>
    /// Calculates the x and y positions to draw the watermark object
    /// </summary>
    /// <param name="width">width of the ghost/watermark image</param>
    /// <param name="height">height of the ghost/watermark image</param>
    /// <param name="yPixelsMargin">A y value margin</param>
    /// <param name="yPosFromBottom">the returned y value</param>
    /// <param name="xPositionFromLeft">the returned x value</param>
    protected void CalcDrawPosition(int width, int height, int yPixelsMargin, out float yPosFromBottom, out float xPositionFromLeft)
    {
      //Now that we have a point size use the caption string height 
      //to determine a y-coordinate to draw the string of the photograph

      if (this.Halign == HAlign.Bottom)
        yPosFromBottom = ((_height) - (height));
      else if (Halign == HAlign.Top)
        yPosFromBottom = (yPixelsMargin);
      else //center
        yPosFromBottom = (_height / 2 - (height / 2));

      //Determine X position

      if (Valign == VAlign.Right)
        xPositionFromLeft = (_width - (width));
      else if (Valign == VAlign.Left)
        xPositionFromLeft = 0;//(xPixelsMargin);
      else  //center
        xPositionFromLeft = (_width / 2 - (width / 2));
    }

		/// <summary>
		///		Alineaci�n horizontal
		/// </summary>
		public HAlign Halign { get; set; }
		
		/// <summary>
		///		Alineaci�n vertical
		/// </summary>
		public VAlign Valign { get; set; }
  }
}
