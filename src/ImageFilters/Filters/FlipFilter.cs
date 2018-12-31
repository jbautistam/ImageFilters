using System;
using System.Drawing;

namespace Bau.Libraries.ImageFilters.Filters
{
	/// <summary>
	///		Filtro para cambiar la orientaci�n de una imagen
	/// </summary>
	public class FlipFilter : BaseFilter
	{ 
		public FlipFilter()
		{ RotateFlip = RotateFlipType.Rotate180FlipNone;
		}
		
		/// <summary>
		///		Ejecuta el filtro sobre la imagen
		/// </summary>
		public override Image ExecuteFilter(Image source)
		{ Bitmap bmpTarget = new Bitmap(source);
				
				// Rota la imagen
					bmpTarget.RotateFlip(RotateFlip);
				// Devuelve la imagen rotada
					return bmpTarget;
		}
		
		/// <summary>
		///		Tipo de rotaci�n
		/// </summary>
		public RotateFlipType RotateFlip { get; set; }
	}
}
