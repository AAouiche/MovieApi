using Domain.Interfaces;
using Domain.Models;
using Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class ImageRepository:IImageRepository
    {
        private readonly MovieContext _context;

        public ImageRepository(MovieContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Image image)
        {
            
            var existingImage = await _context.Images.FirstOrDefaultAsync(i => i.ApplicationUserId == image.ApplicationUserId);

            if (existingImage != null)
            {
            
                
                existingImage.Url = image.Url;
                existingImage.Size = image.Size;
                existingImage.FileName = image.FileName;
                
                
                 var check = _context.Images.Update(existingImage);
                var existingImage2 = await _context.Images.FirstOrDefaultAsync(i => i.ApplicationUserId == image.ApplicationUserId);
                
            }
            else
            {
                await _context.Images.AddAsync(image);
            }

            await _context.SaveChangesAsync();
        }
        
        public async Task<string> GetCurrentPublicId(string userId)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.ApplicationUserId == userId);

            if (image == null || string.IsNullOrEmpty(image.PublicId))
            {
                return string.Empty;
            }

            return image.PublicId;
        }
        public async Task CreateOrUpdateAsync(Image image)
        {
            var existingImage = await _context.Images
                .FirstOrDefaultAsync(i => i.ApplicationUserId == image.ApplicationUserId);

            if (existingImage != null)
            {
                existingImage.Url = image.Url;
                existingImage.Size = image.Size;
                existingImage.FileName = image.FileName;
                existingImage.PublicId = image.PublicId; 
                _context.Images.Update(existingImage);
            }
            else
            {
                await _context.Images.AddAsync(image);
            }

            await _context.SaveChangesAsync();
        }
    }
}
