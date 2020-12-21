using AmalgamateLabs.Base;
using AmalgamateLabs.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmalgamateLabs.TagHelpers
{
    public class GameSectionTagHelper : TagHelper
    {
        public List<WebGLGame> WebGLGames { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            const string ID_ATTRIBUTE = "id";

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "section";
            output.Attributes.SetAttribute(ID_ATTRIBUTE, output.Attributes.ContainsName(ID_ATTRIBUTE) ? $"{output.Attributes[ID_ATTRIBUTE].Value} games" : "games");

            StringBuilder htmlContent = new StringBuilder();


            foreach (WebGLGame webGLGame in WebGLGames.OrderByDescending(wglg => wglg.PostedDate).ThenBy(wglg => wglg.GameType))
            {
                htmlContent.Append("<div class='col-lg-4 col-md-6 portfolio-thumbnail all minigames'>" +
                        $"<a href='/WebGLGame/Details/{webGLGame.WebGLGameId}/'><img src='{webGLGame.SmallImage.ImagePath()}' alt='{webGLGame.Title} image'></a>" +
                    "</div>");
            }

            output.Content.SetHtmlContent(htmlContent.ToString());
        }
    }
}
