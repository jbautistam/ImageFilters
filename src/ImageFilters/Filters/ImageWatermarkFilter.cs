using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Bau.Libraries.ImageFilters.Filters
{
  /// <summary>
  /// Text Water Mark Filter .Can be used to add a watermark text to the image.
  /// The weater mark can be both horizontaly and verticaly aligned.
  /// Also provides simple captions functionality (Simple text on an image)
  /// </summary>
  public class ImageWatermarkFilter : BaseWaterMarkFilter
  { 
	// Variables privadas
	private float _alpha = 0.3f; //default

    /// <summary>
    /// Executes this filter on the input image and returns the image with the WaterMark
    /// </summary>
    /// <param name="source">input image</param>
    /// <returns>transformed image</returns>
    /// <example>
    /// <code>
    /// Image transformed;
    /// ImageWatermarkFilter imageWaterMark = new ImageWatermarkFilter();
    /// imageWaterMark.Valign = ImageWatermarkFilter.VAlign.Right;
    /// imageWaterMark.Halign = ImageWatermarkFilter.HAlign.Bottom;
    /// imageWaterMark.WaterMarkImage = Image.FromFile("Images/pacman.gif");
    /// transformed = imageWaterMark.ExecuteFilter(myImg);
    /// </code>
    /// </example>
    public override Image ExecuteFilter(Image source)
    {
      _height = source.Height;
      _width = source.Width;
      Bitmap bmWatermark = new Bitmap(source);
      bmWatermark.SetResolution(source.HorizontalResolution, source.VerticalResolution);
      //Load this Bitmap into a new Graphic Object
      Graphics grWatermark = Graphics.FromImage(bmWatermark);

      //To achieve a transulcent watermark we will apply (2) color 
      //manipulations by defineing a ImageAttributes object and 
      //seting (2) of its properties.
      ImageAttributes imageAttributes = new ImageAttributes();

      //The first step in manipulating the watermark image is to replace 
      //the background color with one that is trasparent (Alpha=0, R=0, G=0, B=0)
      //to do this we will use a Colormap and use this to define a RemapTable
      ColorMap colorMap = new ColorMap();

      //My watermark was defined with a background of 100% Green this will
      //be the color we search for and replace with transparency
      colorMap.OldColor = TransparentColor;
      colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

      ColorMap[] remapTable = { colorMap };

      imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

      //The second color manipulation is used to change the opacity of the 
      //watermark.  This is done by applying a 5x5 matrix that contains the 
      //coordinates for the RGBA space.  By setting the 3rd row and 3rd column 
      //to 0.3f we achive a level of opacity
      float[][] colorMatrixElements = { 
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},        
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},        
												new float[] {0.0f,  0.0f,  0.0f,  _alpha, 0.0f},        
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};
      ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

      imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
        ColorAdjustType.Bitmap);

      //For this example we will place the watermark in the upper right
      //hand corner of the photograph. offset down 10 pixels and to the 
      //left 10 pixles

      
      float xPosOfWm;// = ((rawImage.Width - _waterMarkImage.Width) - 2);
      float yPosOfWm;
      CalcDrawPosition(WaterMarkImage.Width, WaterMarkImage.Height, 0,out yPosOfWm,out xPosOfWm);

      grWatermark.DrawImage(WaterMarkImage,
        new Rectangle((int)xPosOfWm,(int) yPosOfWm, WaterMarkImage.Width, WaterMarkImage.Height),  //Set the detination Position
        0,                  // x-coordinate of the portion of the source image to draw. 
        0,                  // y-coordinate of the portion of the source image to draw. 
        WaterMarkImage.Width,            // Watermark Width
        WaterMarkImage.Height,		    // Watermark Height
        GraphicsUnit.Pixel, // Unit of measurment
        imageAttributes);   //ImageAttributes Object

      //Replace the original photgraphs bitmap with the new Bitmap
      //imgPhoto = bmWatermark;
      //grWatermark.Dispose();

      //save new image to file system.
      return bmWatermark; // bmPhoto;
    }

    /// <summary>
    /// A value between 0 to 1.0. Sets the opacity value
    /// </summary>
    public float Alpha
    {	get {	return _alpha; }
      set
      {
        if ((value > 1) || (value < 0))
          throw new Exception("Error setting the opacity value");
        else
          _alpha = value;
      }
    }

    public Image WaterMarkImage { get; set; }

    public Color TransparentColor { get; set; } = Color.FromArgb(255,255,255,255);
  }
}
