using AmalgamateLabs.Base;
using System.Collections.Generic;
using System.Linq;

namespace AmalgamateLabs.Models
{
    // Model used to hold multiple models in case we want to use more than one in a view.
    // www.dotnet-stuff.com/tutorials/aspnet-mvc/way-to-use-multiple-models-in-a-view-in-asp-net-mvc
    public class ViewModel
    {
        public ContactForm ContactForm { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Movie> Movies { get; set; }
        public List<SystemConfig> SystemConfigs { get; set; }

        public ViewModel(SQLiteDBContext context, params string[] modelNames)
        {
            foreach (string modelName in modelNames)
            {
                switch (modelName)
                {
                    case nameof(Blog):
                        Blogs = context.Blogs.ToList();
                        break;
                    case nameof(ContactForm):
                        ContactForm = new ContactForm();
                        break;
                    case nameof(Movie):
                        Movies = context.Movies.ToList();
                        break;
                    case nameof(SystemConfig):
                        SystemConfigs = context.SystemConfigs.ToList();
                        break;
                }
            }
        }
    }
}
