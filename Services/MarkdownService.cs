using Markdig;
using System.IO;
namespace SeeFrontendTry002.Services;

public class MarkdownService
{
    private readonly MarkdownPipeline _pipeline;

    public MarkdownService()
    {
        _pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UsePipeTables()
            .UseGridTables()
            .Build();
    }

    public string ConvertToHtml(string markdown)
    {
        return Markdown.ToHtml(markdown, _pipeline);
    }

    public string GetMarkdownFromFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }  
}