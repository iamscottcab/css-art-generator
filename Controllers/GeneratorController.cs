using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace css_art_generator.Controllers
{
    public class GeneratorController : Controller
    {
        public GeneratorController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Image(IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                // If we don't have a file then just return something bad...
                return BadRequest();
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    using (var bitmap = new Bitmap(memoryStream))
                    {
                        int width = bitmap.Width;
                        int height = bitmap.Height;

                        Color[,] pixels = new Color[width, height];

                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                pixels[x, y] = bitmap.GetPixel(x, y);
                            }
                        }

                        ViewData["pixels"] = pixels;

                        return View();
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
