using UnityEngine;

namespace ShapeShifting
{
    public static class TextureExtensions
    {
        public static Texture2D ReadRenderTexture(this RenderTexture i_RenderTexture)
        {
            RenderTexture.active = i_RenderTexture;
            Texture2D texture2D = new Texture2D(i_RenderTexture.width, i_RenderTexture.height, TextureFormat.RGB24, false);
            texture2D.ReadPixels(new Rect(0, 0, i_RenderTexture.width, i_RenderTexture.height), 0, 0);
            texture2D.Apply();
            RenderTexture.active = null;
            return texture2D;
        }

        public static void GaussianBlur(this Texture2D source, int radius, float sigma)
        {
            int kernelSize = 2 * radius + 1;
            float[,] kernel = createGaussianKernel(kernelSize, sigma);

            Color[] sourceColors = source.GetPixels();
            Color[] resultColors = new Color[sourceColors.Length];

            int width = source.width;
            int height = source.height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color blurredPixel = new Color(0, 0, 0, 0);

                    for (int ky = -radius; ky <= radius; ky++)
                    {
                        for (int kx = -radius; kx <= radius; kx++)
                        {
                            int pixelPosX = Mathf.Clamp(x + kx, 0, width - 1);
                            int pixelPosY = Mathf.Clamp(y + ky, 0, height - 1);
                            Color pixel = sourceColors[pixelPosY * width + pixelPosX];
                            blurredPixel += pixel * kernel[ky + radius, kx + radius];
                        }
                    }

                    resultColors[y * width + x] = blurredPixel;
                }
            }

            source.SetPixels(resultColors);
            source.Apply();
        }
        private static float[,] createGaussianKernel(int length, float sigma)
        {
            float[,] kernel = new float[length, length];
            float sumTotal = 0;
            int radius = length / 2;
            float distance = 0;
            float constant = 1f / (2f * Mathf.PI * sigma * sigma);

            for (int filterY = -radius; filterY <= radius; filterY++)
            {
                for (int filterX = -radius; filterX <= radius; filterX++)
                {
                    distance = (filterX * filterX + filterY * filterY) / (2 * sigma * sigma);
                    kernel[filterY + radius, filterX + radius] = constant * Mathf.Exp(-distance);
                    sumTotal += kernel[filterY + radius, filterX + radius];
                }
            }

            // Normalize the kernel
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    kernel[y, x] /= sumTotal;
                }
            }

            return kernel;
        }
    }
}