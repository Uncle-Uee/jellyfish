// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using UnityEngine;

namespace SOFlow.Extensions
{
	public static class TextureExtensions
	{
		/// <summary>
		/// Resizes the given texture.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static Texture2D ResizeTexture(Texture source, int width, int height)
		{
			RenderTexture temporaryRenderTexture = RenderTexture.GetTemporary(width, height);

			temporaryRenderTexture.filterMode = FilterMode.Trilinear;
			RenderTexture.active              = temporaryRenderTexture;
			Graphics.Blit(source, temporaryRenderTexture);
			Texture2D resizedTexture = new Texture2D(width, height);

			resizedTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);

			resizedTexture.Apply();
			RenderTexture.active = null;
			RenderTexture.ReleaseTemporary(temporaryRenderTexture);

			return resizedTexture;
		}
	}
}