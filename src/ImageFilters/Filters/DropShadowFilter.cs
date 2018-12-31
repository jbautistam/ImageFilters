using System;
using System.Drawing;
using System.Reflection;
using System.Resources;


namespace Bau.Libraries.ImageFilters.Filters
{

  /// <summary>
  /// DropShadowFilter. adds the picture a drop shadow as if the image is hovering 
  /// above the floor or infront of the wall.
  /// </summary>
  public class DropShadowFilter : BaseFilter
  {
    /// <summary>
    /// Executes this drop shadow 
    /// filter on the input image and returns the result
    /// </summary>
    /// <param name="source">input image</param>
    /// <returns>Shadow Dropped Image</returns>
    /// <example>
    /// <code>
    /// Image transformed;
    /// DropShadowFilter dropShadow = new DropShadowFilter();
    /// transformed = dropShadow.ExecuteFilter(myImg);
    /// </code>
    /// </example>
    public override Image ExecuteFilter(Image source)
    {
      int rightMargin = 4;
      int bottomMargin = 4;
      
      //Get the shadow image
      Assembly asm = Assembly.GetExecutingAssembly();
      ResourceManager rm = new ResourceManager("Bau.Libraries.ImageFilters.Filters.Images", asm);
      Bitmap shadow = (Bitmap)rm.GetObject("img");


      
      Bitmap fullImage = new Bitmap(source.Width + 6, source.Height + 6);
      Graphics g = Graphics.FromImage(fullImage);
      g.DrawImage(source, 0, 0, source.Width, source.Height);
      GraphicsUnit units = GraphicsUnit.Pixel;


      //Draw the shadow's right lower corner
      Point ulCorner = new Point(fullImage.Width - 6, fullImage.Height - 6);
      Point urCorner = new Point(fullImage.Width, fullImage.Height - 6);
      Point llCorner = new Point(fullImage.Width - 6, fullImage.Height);
      Point[] destPara = { ulCorner, urCorner, llCorner };
      Rectangle srcRect = new Rectangle(shadow.Width - 6, shadow.Height - 6, 6, 6);
      g.DrawImage(shadow, destPara, srcRect, units);

      //Draw the shadow's right hand side
      ulCorner = new Point(fullImage.Width - 6, bottomMargin);
      urCorner = new Point(fullImage.Width, bottomMargin);
      llCorner = new Point(fullImage.Width - 6, fullImage.Height - 6);
      destPara = new Point[] { ulCorner, urCorner, llCorner };
      srcRect = new Rectangle(shadow.Width - 6, 0, 6, shadow.Height - 6);
      g.DrawImage(shadow, destPara, srcRect, units);

      //Draw the shadow's bottom hand side
      ulCorner = new Point(rightMargin, fullImage.Height - 6);
      urCorner = new Point(fullImage.Width - 6, fullImage.Height - 6);
      llCorner = new Point(rightMargin, fullImage.Height);
      destPara = new Point[] { ulCorner, urCorner, llCorner };
      srcRect = new Rectangle(0, shadow.Height - 6, shadow.Width - 6, 6);
      g.DrawImage(shadow, destPara, srcRect, units);

      return fullImage;
    }
    
  }
}
