using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO.Compression;

namespace ZipArchive_Practice.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
      byte[] data = CreateZip();
      string fileName = "test.zip";
      return File(data, "application/octet-stream", fileName);
    }

    /// <summary>
    /// Zipファイル作成
    /// </summary>
    /// <returns>バイト配列</returns>
    private byte[] CreateZip()
    {
        using(MemoryStream memoryStream = new MemoryStream()){
            using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
                using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                {
                    writer.WriteLine("Information about this package.");
                    writer.WriteLine("========================");
                }
            }
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream.ToArray();
        }
    }
}
