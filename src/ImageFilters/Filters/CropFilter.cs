using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Bau.Libraries.ImageFilters.Filters
{
  /// <summary>
  ///		Clase de filtro para cortar una imagen
  /// </summary>
  public class CropFilter : BaseFilter
  {	
    /// <summary>
    ///		Ejecuta el filtro
    /// </summary>
    public override Image ExecuteFilter(Image source)
    {	return (source as Bitmap).Clone(RectangleToCrop, source.PixelFormat);
    }
    
    /// <summary>
    ///		Rectángulo seleccionado para cortar la imagen
    /// </summary>
    public Rectangle RectangleToCrop { get; set; }
  }
}
