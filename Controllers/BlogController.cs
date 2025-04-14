using Microsoft.AspNetCore.Mvc;
using SeeFrontendTry002.Services;

namespace SeeFrontendTry002.Controllers;

public class BlogController : Controller
{
    private readonly MarkdownService _markdownService;
    private readonly string _contentPath;

    public BlogController(MarkdownService markdownService, IWebHostEnvironment env)
    {
        _markdownService = markdownService;
        _contentPath = Path.Combine(env.WebRootPath, "content");
    }

    public IActionResult DeepLearning()
    {
        var filePath = Path.Combine(_contentPath, "DeepLearningApproach.md");
        var markdown = _markdownService.GetMarkdownFromFile(filePath);
        var html = _markdownService.ConvertToHtml(markdown);
        
        ViewData["Content"] = html;
        ViewData["Title"] = "Deep Learning Approach for Software Effort Estimation";
        
        return View("BlogPost");
    }
    
    public IActionResult FinTechApproach()
    {
        var filePath = Path.Combine(_contentPath, "FinTechApproach.md");
        var markdown = _markdownService.GetMarkdownFromFile(filePath);
        var html = _markdownService.ConvertToHtml(markdown);
        
        ViewData["Content"] = html;
        ViewData["Title"] = "FinTech Software Effort Estimation";
        
        return View("BlogPost");
    }

    public IActionResult ClassicalML()
    {
        var filePath = Path.Combine(_contentPath, "ClassicalMLApproach.md");
        var markdown = _markdownService.GetMarkdownFromFile(filePath);
        var html = _markdownService.ConvertToHtml(markdown);
        
        ViewData["Content"] = html;
        ViewData["Title"] = "Classical Machine Learning Approach for Software Effort Estimation";
        
        return View("BlogPost");
    }
}