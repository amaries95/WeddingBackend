using Application.Contracts.Views;
using Application.Data;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ViewsService
    {
        private readonly WeddingContext _context;

        public ViewsService(WeddingContext context)
        {
            _context = context;
        }

        public async Task<ViewsResponse> GetViews()
        {
            var result = await _context.Views.FirstAsync();

            return new ViewsResponse { NumberOfViews = result.NumberOfViews };
        }

        public async Task UpdateViews()
        {
            if(!_context.Views.Any())
            {
                var view = new View 
                {
                    NumberOfViews = 1
                };

                _context.Views.Add(view);
                await _context.SaveChangesAsync(); // maybe I can delete this

                return;
            }

            _context.Views.First().NumberOfViews++;
            await _context.SaveChangesAsync();
        }
    }
}
