using AmalgamateLabs.Base;
using AmalgamateLabs.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmalgamateLabs.TagHelpers
{
    public class BlogSectionTagHelper : TagHelper
    {
        public List<Blog> Blogs { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            const string ID_ATTRIBUTE = "id";

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "section";
            output.Attributes.SetAttribute(ID_ATTRIBUTE, output.Attributes.ContainsName(ID_ATTRIBUTE) ? $"{output.Attributes[ID_ATTRIBUTE].Value} blog" : "blog");

            StringBuilder htmlContent = new StringBuilder();

            int numberOfSections = Convert.ToInt32(Math.Ceiling((double)Blogs.Count / 3));

            htmlContent.Append("<ol class='carousel-indicators'>");
            for (int i = 0; i < numberOfSections; i++)
            {
                if (i == 0)
                {
                    htmlContent.Append($"<li data-target='#blogCarousel' data-slide-to='{i}' class='active'></li>");
                }
                else
                {
                    htmlContent.Append($"<li data-target='#blogCarousel' data-slide-to='{i}'></li>");
                }
                
            }
            htmlContent.Append("</ol>");

            htmlContent.Append("<div class='carousel-inner'>");

            int counter = 0;
            foreach (Blog blog in Blogs.OrderByDescending(b => b.PostedDate))
            {
                if (counter == 0) // Start the first item and row.
                {
                    htmlContent.Append("<div class='carousel-item active'><div class='row'>");
                }
                else if (counter % 3 == 0) // Close the previous item and row, then start the next one.
                {
                    htmlContent.Append("</div></div><div class='carousel-item'><div class='row'>");
                }

                htmlContent.Append(
                    "<div class='col-md-4'>" +
                        "<div class='item-box-blog'>" +
                            "<div class='item-box-blog-image'>" +
                                $"<div class='item-box-blog-date bg-blue-ui white'> <span class='mon'>{blog.PostedDate.ToString("MMM d, yyyy")}</span> </div>" +
                                $"<figure> <img alt='' src='{blog.Thumbnail.ImagePath()}'> </figure>" +
                            "</div>" +
                            "<div class='item-box-blog-body'>" +
                                "<div class='item-box-blog-heading'>" +
                                    $"<a href='/Blog/{blog.BlogId}/{blog.URLSafeTitle}' tabindex='0'>" +
                                        $"<h5>{blog.Title}</h5>" +
                                    "</a>" +
                                "</div>" +
                                "<div class='item-box-blog-data' style='padding: 12px;'>" +
                                    $"<p><i class='fa fa-user-o'></i>{blog.Subtitle}</p>" +
                                "</div>" +
                                $"<div class='mt'> <a href='/Blog/{blog.BlogId}/{blog.URLSafeTitle}' tabindex='0' class='btn bg-blue-ui white read'>read more</a> </div>" +
                            "</div>" +
                        "</div>" +
                    "</div>");

                counter++;
            }

            // Close the last row, item, and carousel-inner.
            htmlContent.Append("</div></div></div>");

            output.Content.SetHtmlContent(htmlContent.ToString());
        }
    }
}
