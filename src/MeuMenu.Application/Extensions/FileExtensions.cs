using Microsoft.AspNetCore.Http;

namespace MeuMenu.Application.Extensions;

public static class FileExtensions
{
    public static async Task<byte[]> ConvertIFormFileToByteArray(this IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}