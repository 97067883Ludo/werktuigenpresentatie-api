namespace api.Data.Helpers;

public static class ImageStore
{
    public async static Task<string> StoreImage(IFormFile file)
    {
        if (!Directory.DirectoryExists("images"))
        {
            Directory.CreateDirectory("images");
        }

        string fileName = GenerateRandomFilenName(file);
        
        using (Stream fileStream = new FileStream(Path.Combine(Directory.GetDirectory("images"), fileName), FileMode.Create)) {
            await file.CopyToAsync(fileStream);
        }
        
        return Path.Combine(Directory.GetDirectory("images"), fileName);
    }

    public static string GenerateRandomFilenName(IFormFile file)
    {
        Guid guid = Guid.NewGuid();

        string filetype = file.FileName.Split(".").Last();

        return guid + "." + filetype;
    }
}