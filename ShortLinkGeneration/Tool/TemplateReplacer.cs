using System.Text.RegularExpressions;

namespace ShortLinkGeneration.Tool;

public static class TemplateReplacer
{
    public static string ReplacePlaceholders(Dictionary<string, string> placeholders)
    {
        string template = System.IO.File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "EmailTemplate.html"));
        foreach (var placeholder in placeholders)
        {
            string pattern = $"{{{{{placeholder.Key}}}}}";
            template = Regex.Replace(template, pattern, placeholder.Value);
        }

        return template;
    }
}